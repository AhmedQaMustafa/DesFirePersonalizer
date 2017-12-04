using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using PCSC;
using PCSC.Iso7816;
using nfcjlib.core.util;
using System.IO;

namespace DesFireWrapperLib
{
    public class DesFireWrapper
    {
        SCardReader mainCardReader = null;
        static readonly byte FAKE_NO = (byte)0xff;

        private KeyType m_ktype;    // type of key used for authentication
        private byte m_kno;         // keyNo used for successful authentication
        private byte[] m_aid;       // currently selected 3-byte AID
        private byte[] m_iv;        // the IV, kept updated between operations (for 3K3DES/AES)
        private byte[] m_skey;      // session key: set on successful authentication
        private byte m_fileNo;
        private byte[] m_fileSett;  // file settings of the last used file (caching)

        private int code;

        //Constructor, pass the card reader object to the class
        public DesFireWrapper(SCardReader card)
        {
            mainCardReader = card;

            if (m_aid == null)
                m_aid = new byte[3];

            this.m_fileNo = 0xff;//force to get file setting first time
        }

        //Get the current selected active application aid
        public string getAid()
        {
            return BitConverter.ToString(m_aid == null ? new byte[3] : m_aid).Replace("-", string.Empty);
        }

        //Set the aid to the last selected application aid
        public void set_selected_aid(string aid)
        {
            m_aid = MyUtil.StringToByteArray(aid);
        }

        /** Ciphers supported by DESFire EV1. */
        public enum KeyType
        {
            DES,
            TDES,
            TKTDES,
            AES,
            FAKE
        }

        /**
         * The communication settings between PCD and PICC.
         * The communication mode can be plain text, plain text with MAC/CMAC, or
         * plain text with CRC and full encryption.
         */
        public enum CommunicationSetting
        {
            PLAIN,
            MACED,
            ENCIPHERED,
            NONE
        }

        //Set the aid to the latest selected application aid
        public void set_selected_aid(byte[] aid)
        {
            Array.Copy(aid, m_aid, aid.Length);
        }

        //This function will transmit the apdu command byte array and in return with response APDU, and return in reference the 
        //processed desfire response
        private ResponseApdu transmitAndReceiveResp(byte[] apdu, ref DesFireWrappeResponse dsf_str)
        {

            byte[] resp = new byte[300];
            var err = mainCardReader.Transmit(apdu, ref resp);

            if (err != SCardError.Success)
            {
                uint err_uint = (uint)err;
                err_uint |= (uint)0x80100000;
                dsf_str.sw1sw2 = "6FFF";
                dsf_str.err_msg_pcsc += "- " + MyUtil.GetEnumPcscErrorDescription((SCardError)err_uint) + "\n";
            }

            ResponseApdu respApdu = new ResponseApdu(resp, IsoCase.Case4Short, SCardProtocol.Any);
            feedback(apdu, respApdu, ref dsf_str);

            return respApdu;
        }

        //This function will transmit the apdu command and in return with response APDU, and return in reference the 
        //processed desfire response
        private ResponseApdu transmitAndReceiveResp(CommandApdu apdu, ref DesFireWrappeResponse dsf_str)
        {
            return transmitAndReceiveResp(apdu.ToArray(), ref dsf_str);
        }

        //Select application based on the Application ID (AID)
        public DesFireWrappeResponse getCardInformation(ref string uid, ref string size)
        {
            uid = null;
            size = null;

            DesFireWrappeResponse dsf_str = new DesFireWrappeResponse();

            // 1st message exchange
            var apdu = new CommandApdu(IsoCase.Case2Short, SCardProtocol.Any);
            apdu.CLA = (byte)0x90;
            apdu.INS = (byte)DesfireInstructionCode.EnveGetVersion;
            apdu.P1 = 0x00;
            apdu.P2 = 0x00;

            ResponseApdu respApdu = transmitAndReceiveResp(apdu, ref dsf_str);

            if (respApdu.SW2 != 0xAF)
                return dsf_str;

            int hexsize = Convert.ToInt32(dsf_str.data.Substring(10, 2), 16);
            double storage_size = Math.Pow(2, hexsize >> 1);

            size = Convert.ToString((int)storage_size);

            // message exchange
            apdu = new CommandApdu(IsoCase.Case2Short, SCardProtocol.Any);
            apdu.CLA = (byte)0x90;
            apdu.INS = (byte)DesfireInstructionCode.AdditionalFrame;
            apdu.P1 = 0x00;
            apdu.P2 = 0x00;

            do
            {
                respApdu = transmitAndReceiveResp(apdu, ref dsf_str);
            }
            while (respApdu.SW2 == 0xAF);

            if (respApdu.HasData)
                uid = dsf_str.data.Substring(0, 14);

            return dsf_str;
        }

        //Select application based on the Application ID (AID)
        public DesFireWrappeResponse SelectApplication(String AppAID)
        {
            DesFireWrappeResponse dsf_str = new DesFireWrappeResponse();
            byte[] appAidBytes = MyUtil.StringToByteArray(AppAID);

            var apdu = new CommandApdu(IsoCase.Case4Short, SCardProtocol.Any);
            apdu.CLA = (byte)0x90;
            apdu.INS = (byte)DesfireInstructionCode.SelectApplication;
            apdu.P1 = 0x00;
            apdu.P2 = 0x00;
            apdu.Data = appAidBytes;

            ResponseApdu respApdu = transmitAndReceiveResp(apdu, ref dsf_str);

            if (respApdu.SW2 == 0x00)
                Array.Copy(appAidBytes, m_aid, appAidBytes.Length);

            return dsf_str;
        }

        //List applications under the main
        public DesFireWrappeResponse ListApplications(ref List<String> application_list)
        {
            DesFireWrappeResponse dsf_str = new DesFireWrappeResponse();

            var apdu = new CommandApdu(IsoCase.Case2Short, SCardProtocol.Any);
            apdu.CLA = (byte)0x90;
            apdu.INS = (byte)DesfireInstructionCode.GetApplicationsIDs;
            apdu.P1 = 0x00;
            apdu.P2 = 0x00;

            byte[] preprocessed_apdu = preprocess(apdu.ToArray(), CommunicationSetting.PLAIN, ref dsf_str);

            ResponseApdu respApdu = transmitAndReceiveResp(preprocessed_apdu, ref dsf_str);
            //if (code == 0x00)
            //{
            //    System.arraycopy(response.getBytes(), 0, fullResp, index, response.getBytes().length);
            //    index += response.getBytes().length;
            //}
            //else if (code == 0xAF)
            //{
            //    System.arraycopy(response.getData(), 0, fullResp, index, response.getData().length);
            //    index += response.getData().length;
            //}
            //else
            //{
            //    return null;
            //}

            //while (response.getSW2() == 0xAF)
            //{
            //    apdu[1] = (byte)Command.MORE.getCode();
            //    command = new CommandAPDU(apdu);
            //    response = transmit(command);
            //    code = response.getSW2();
            //    feedback(command, response);
            //    if (code == 0x00)
            //    {
            //        System.arraycopy(response.getBytes(), 0, fullResp, index, response.getBytes().length);
            //        index += response.getBytes().length;
            //    }
            //    else if (code == 0xAF)
            //    {
            //        System.arraycopy(response.getData(), 0, fullResp, index, response.getData().length);
            //        index += response.getData().length;
            //    }
            //    else
            //    {
            //        return null;
            //    }
            //}
            //byte[] ret = new byte[index];
            //System.arraycopy(fullResp, 0, ret, 0, index);

            postprocess(respApdu.ToArray(), CommunicationSetting.PLAIN, ref dsf_str);

            if (respApdu.HasData)
            {
                string raw_str = BitConverter.ToString(respApdu.GetData()).Replace("-", string.Empty);
                int index = 0;

                while (index < raw_str.Length)
                {
                    string app_id = raw_str.Substring(index, 6);
                    application_list.Add(app_id);
                    index += 6;
                }
            }

            return dsf_str;
        }

        //List files under an application
        public DesFireWrappeResponse ListFiles(ref List<string> file_list)
        {
            DesFireWrappeResponse dsf_str = new DesFireWrappeResponse();

            var apdu = new CommandApdu(IsoCase.Case2Short, SCardProtocol.Any);
            apdu.CLA = (byte)0x90;
            apdu.INS = (byte)DesfireInstructionCode.GetFileIDs;
            apdu.P1 = 0x00;
            apdu.P2 = 0x00;

            ResponseApdu respApdu = transmitAndReceiveResp(apdu, ref dsf_str);

            if (respApdu.HasData)
            {
                string raw_str = BitConverter.ToString(respApdu.GetData()).Replace("-", string.Empty);
                int index = 0;

                while (index < raw_str.Length)
                {
                    string file_id = raw_str.Substring(index, 2);
                    file_list.Add(file_id);
                    index += 2;
                }
            }

            return dsf_str;
        }

        //Select a file
        public DesFireWrappeResponse SelectFile(String fileNo)
        {
            DesFireWrappeResponse dsf_str = new DesFireWrappeResponse();

            byte[] appAidBytes = MyUtil.StringToByteArray(fileNo);

            var apdu = new CommandApdu(IsoCase.Case4Short, SCardProtocol.Any);
            apdu.CLA = (byte)0x90;
            apdu.INS = (byte)DesfireInstructionCode.GetFileIDs;
            apdu.P1 = 0x00;
            apdu.P2 = 0x00;
            apdu.Data = appAidBytes;

            ResponseApdu respApdu = transmitAndReceiveResp(apdu, ref dsf_str);

            return dsf_str;
        }

        // Send APDU
        public DesFireWrappeResponse sendAPDU(String apduCmdAndData)
        {
            DesFireWrappeResponse dsf_str = new DesFireWrappeResponse();

            String apduCmd = apduCmdAndData.Replace(" ", "").Replace("\t", "").Replace("\n", "").Replace("\r", "");

            if (apduCmd == String.Empty)
                return dsf_str;

            byte[] apduCmdBytes = MyUtil.StringToByteArray(apduCmd);

            ResponseApdu respApdu = transmitAndReceiveResp(apduCmdBytes, ref dsf_str);

            return dsf_str;
        }

        public DesFireWrappeResponse createFile(BasicFile bf)
        {
            DesFireWrappeResponse dsf_str = new DesFireWrappeResponse();

            if (bf.isStandardFile())
            {
                StandardFile sf = (StandardFile)bf;
                dsf_str = createStandardFile(sf.fileid, sf.communication_settings, sf.access_rights, sf.file_size_hex_content_length);
            }
            else if (bf.isValueFile())
            {
                ValueFile vf = (ValueFile)bf;
                dsf_str = createValueFile(vf.fileid, vf.communication_settings, vf.access_rights, vf.lower_limit_hex, vf.upper_limit_hex, vf.value_hex, vf.limited_credit_enabled_hex);
            }
            else if (bf.isRecordFile())
            {
                RecordFile rf = (RecordFile)bf;
                dsf_str = createRecordFile(rf.isLinearRecord, rf.fileid, rf.communication_settings, rf.access_rights, rf.record_size, rf.maxNoOfRecords);
            }

            return dsf_str;
        }


        // Create a standard file
        public DesFireWrappeResponse createStandardFile(string newFileId, String commSettings, string AccessRights, String newFileSize)
        {
            DesFireWrappeResponse dsf_str = new DesFireWrappeResponse();

            byte[] newFileAidBytes = MyUtil.StringToByteArray(newFileId);
            byte[] commSettingsBytes = MyUtil.StringToByteArray(commSettings);

            byte[] accessRightsBytes = MyUtil.StringToByteArray(AccessRights);
            Array.Reverse(accessRightsBytes);

            byte[] newFileFileSizeBytes = MyUtil.StringToByteArray(newFileSize);
            Array.Reverse(newFileFileSizeBytes, 0, newFileFileSizeBytes.Length);

            byte[] data = new byte[newFileAidBytes.Length + commSettingsBytes.Length + accessRightsBytes.Length + newFileFileSizeBytes.Length];
            Array.Copy(newFileAidBytes, 0, data, 0, newFileAidBytes.Length);
            Array.Copy(commSettingsBytes, 0, data, newFileAidBytes.Length, commSettingsBytes.Length);
            Array.Copy(accessRightsBytes, 0, data, newFileAidBytes.Length + commSettingsBytes.Length, accessRightsBytes.Length);
            Array.Copy(newFileFileSizeBytes, 0, data, newFileAidBytes.Length + commSettingsBytes.Length + accessRightsBytes.Length, newFileFileSizeBytes.Length);

            var apdu = new CommandApdu(IsoCase.Case4Short, SCardProtocol.Any);
            apdu.CLA = (byte)0x90;
            apdu.INS = (byte)DesfireInstructionCode.CreateStdDataFile;
            apdu.P1 = 0x00;
            apdu.P2 = 0x00;
            apdu.Data = data;

            ResponseApdu respApdu = transmitAndReceiveResp(apdu, ref dsf_str);

            return dsf_str;
        }

        // Create a value file
        public DesFireWrappeResponse createValueFile(string newFileId, String commSettings, string AccessRights,
            String LowerLimit, String UpperLimit, String Value, String limitedCreditEnabled)
        {
            DesFireWrappeResponse dsf_str = new DesFireWrappeResponse();

            byte[] newFileAidBytes = MyUtil.StringToByteArray(newFileId);
            byte[] commSettingsBytes = MyUtil.StringToByteArray(commSettings);
            byte[] accessRightsBytes = MyUtil.StringToByteArray(AccessRights);
            Array.Reverse(accessRightsBytes);

            byte[] LowerLimitBytes = MyUtil.StringToByteArray(LowerLimit);
            byte[] UpperLimitBytes = MyUtil.StringToByteArray(UpperLimit);
            byte[] ValueBytes = MyUtil.StringToByteArray(Value);
            byte[] limitedCreditEnabledBytes = MyUtil.StringToByteArray(limitedCreditEnabled);

            Array.Reverse(LowerLimitBytes, 0, LowerLimitBytes.Length);
            Array.Reverse(UpperLimitBytes, 0, UpperLimitBytes.Length);
            Array.Reverse(ValueBytes, 0, ValueBytes.Length);

            byte[] data = new byte[newFileAidBytes.Length + commSettingsBytes.Length + accessRightsBytes.Length + LowerLimitBytes.Length
                + UpperLimitBytes.Length + ValueBytes.Length + limitedCreditEnabledBytes.Length];
            Array.Copy(newFileAidBytes, 0, data, 0, newFileAidBytes.Length);
            Array.Copy(commSettingsBytes, 0, data, newFileAidBytes.Length, commSettingsBytes.Length);
            Array.Copy(accessRightsBytes, 0, data, newFileAidBytes.Length + commSettingsBytes.Length, accessRightsBytes.Length);
            Array.Copy(LowerLimitBytes, 0, data, newFileAidBytes.Length + commSettingsBytes.Length + accessRightsBytes.Length, LowerLimitBytes.Length);
            Array.Copy(UpperLimitBytes, 0, data, newFileAidBytes.Length + commSettingsBytes.Length + accessRightsBytes.Length + LowerLimitBytes.Length, UpperLimitBytes.Length);
            Array.Copy(ValueBytes, 0, data, newFileAidBytes.Length + commSettingsBytes.Length + accessRightsBytes.Length + LowerLimitBytes.Length + UpperLimitBytes.Length, ValueBytes.Length);
            Array.Copy(limitedCreditEnabledBytes, 0, data, newFileAidBytes.Length + commSettingsBytes.Length + accessRightsBytes.Length + LowerLimitBytes.Length + UpperLimitBytes.Length + ValueBytes.Length, limitedCreditEnabledBytes.Length);

            var apdu = new CommandApdu(IsoCase.Case4Short, SCardProtocol.Any);
            apdu.CLA = (byte)0x90;
            apdu.INS = (byte)DesfireInstructionCode.CreateValueFile;
            apdu.P1 = 0x00;
            apdu.P2 = 0x00;
            apdu.Data = data;

            ResponseApdu respApdu = transmitAndReceiveResp(apdu, ref dsf_str);

            return dsf_str;
        }

        // Change file settings
        public DesFireWrappeResponse changeFileSettings(string newFileId, String commSettings, string AccessRights)
        {
            DesFireWrappeResponse dsf_str = new DesFireWrappeResponse();

            byte[] fileAidBytes = MyUtil.StringToByteArray(newFileId);
            byte[] commSettingsBytes = MyUtil.StringToByteArray(commSettings);

            byte[] accessRightsBytes = MyUtil.StringToByteArray(AccessRights);
            Array.Reverse(accessRightsBytes);

            byte[] data = new byte[fileAidBytes.Length + commSettingsBytes.Length + accessRightsBytes.Length];
            Array.Copy(fileAidBytes, 0, data, 0, fileAidBytes.Length);
            Array.Copy(commSettingsBytes, 0, data, fileAidBytes.Length, commSettingsBytes.Length);
            Array.Copy(accessRightsBytes, 0, data, fileAidBytes.Length + commSettingsBytes.Length, accessRightsBytes.Length);

            CommunicationSetting cs = getFileCommSett(fileAidBytes[0], false, true, false, false, ref dsf_str);
            if (cs == CommunicationSetting.NONE)
            {
                dsf_str.sw1sw2 = "6FFF";
                dsf_str.err_msg_pcsc += "- Check if card authenticated with correct key\n";

                return dsf_str;
            }

            var apdu = new CommandApdu(IsoCase.Case4Short, SCardProtocol.Any);
            apdu.CLA = (byte)0x90;
            apdu.INS = (byte)DesfireInstructionCode.ChangeFileSettings;
            apdu.P1 = 0x00;
            apdu.P2 = 0x00;
            apdu.Data = data;

            byte[] apduBytes = apdu.ToArray();

            byte[] apdu_processed = preprocess(apduBytes.ToArray(), 1, cs, ref dsf_str);

            ResponseApdu respApdu = transmitAndReceiveResp(apdu_processed, ref dsf_str);

            code = respApdu.SW2;

            // get rid of cache
            if (respApdu.SW2 == 0x00 && fileAidBytes[0] == this.m_fileNo)
            {
                this.m_fileNo = FAKE_NO;
                this.m_fileSett = null;
            }

            bool success = postprocess(respApdu.ToArray(), CommunicationSetting.PLAIN, ref dsf_str) != null;

            return dsf_str;
        }


        //Do credit in a value file
        public DesFireWrappeResponse doCreditDirect(string fileId, CommunicationSetting cs, String amount)
        {
            DesFireWrappeResponse dsf_str = new DesFireWrappeResponse();

            byte[] fileAidBytes = MyUtil.StringToByteArray(fileId);
            byte[] amountBytes = MyUtil.StringToByteArray(amount);

            Array.Reverse(amountBytes, 0, amountBytes.Length);

            byte[] data = new byte[fileAidBytes.Length + amountBytes.Length];
            Array.Copy(fileAidBytes, 0, data, 0, fileAidBytes.Length);
            Array.Copy(amountBytes, 0, data, fileAidBytes.Length, amountBytes.Length);

            //CommunicationSetting cs = getFileCommSett(fileAidBytes[0], true, false, true, true, ref dsf_str);
            //if (cs == CommunicationSetting.NONE)
            //{
            //    dsf_str.sw1sw2 = "6FFF";
            //    dsf_str.err_msg_pcsc += "- Check if card authenticated with correct key\n";

            //    return dsf_str;
            //}

            var apdu = new CommandApdu(IsoCase.Case4Short, SCardProtocol.Any);
            apdu.CLA = (byte)0x90;
            apdu.INS = (byte)DesfireInstructionCode.Credit;
            apdu.P1 = 0x00;
            apdu.P2 = 0x00;
            apdu.Data = data;

            byte[] apduBytes = apdu.ToArray();

            byte[] apdu_processed = preprocess(apduBytes.ToArray(), 1, cs, ref dsf_str);

            ResponseApdu respApdu = transmitAndReceiveResp(apdu_processed, ref dsf_str);

            code = respApdu.SW2;

            bool success = postprocess(respApdu.ToArray(), CommunicationSetting.PLAIN, ref dsf_str) != null;

            return dsf_str;
        }


        //Do credit in a value file
        public DesFireWrappeResponse doCredit(string fileId, String amount)
        {
            DesFireWrappeResponse dsf_str = new DesFireWrappeResponse();

            byte[] fileAidBytes = MyUtil.StringToByteArray(fileId);
            byte[] amountBytes = MyUtil.StringToByteArray(amount);

            Array.Reverse(amountBytes, 0, amountBytes.Length);

            byte[] data = new byte[fileAidBytes.Length + amountBytes.Length];
            Array.Copy(fileAidBytes, 0, data, 0, fileAidBytes.Length);
            Array.Copy(amountBytes, 0, data, fileAidBytes.Length, amountBytes.Length);

            CommunicationSetting cs = getFileCommSett(fileAidBytes[0], true, false, true, true, ref dsf_str);
            if (cs == CommunicationSetting.NONE)
            {
                dsf_str.sw1sw2 = "6FFF";
                dsf_str.err_msg_pcsc += "- Check if card authenticated with correct key\n";

                return dsf_str;
            }

            var apdu = new CommandApdu(IsoCase.Case4Short, SCardProtocol.Any);
            apdu.CLA = (byte)0x90;
            apdu.INS = (byte)DesfireInstructionCode.Credit;
            apdu.P1 = 0x00;
            apdu.P2 = 0x00;
            apdu.Data = data;

            byte[] apduBytes = apdu.ToArray();

            byte[] apdu_processed = preprocess(apduBytes.ToArray(), 1, cs, ref dsf_str);

            ResponseApdu respApdu = transmitAndReceiveResp(apdu_processed, ref dsf_str);

            code = respApdu.SW2;

            bool success = postprocess(respApdu.ToArray(), CommunicationSetting.PLAIN, ref dsf_str) != null;

            return dsf_str;
        }

        //Do debit on a value file
        public DesFireWrappeResponse doDebitDirect(string fileId, CommunicationSetting cs, String amount)
        {
            DesFireWrappeResponse dsf_str = new DesFireWrappeResponse();

            byte[] fileAidBytes = MyUtil.StringToByteArray(fileId);
            byte[] amountBytes = MyUtil.StringToByteArray(amount);

            Array.Reverse(amountBytes, 0, amountBytes.Length);

            byte[] data = new byte[fileAidBytes.Length + amountBytes.Length];
            Array.Copy(fileAidBytes, 0, data, 0, fileAidBytes.Length);
            Array.Copy(amountBytes, 0, data, fileAidBytes.Length, amountBytes.Length);

            var apdu = new CommandApdu(IsoCase.Case4Short, SCardProtocol.Any);
            apdu.CLA = (byte)0x90;
            apdu.INS = (byte)DesfireInstructionCode.Debit;
            apdu.P1 = 0x00;
            apdu.P2 = 0x00;
            apdu.Data = data;

            byte[] apduBytes = apdu.ToArray();

            byte[] apdu_processed = preprocess(apduBytes.ToArray(), 1, cs, ref dsf_str);

            ResponseApdu respApdu = transmitAndReceiveResp(apdu_processed, ref dsf_str);

            code = respApdu.SW2;

            bool success = postprocess(respApdu.ToArray(), CommunicationSetting.PLAIN, ref dsf_str) != null;

            return dsf_str;
        }


        //Do debit on a value file
        public DesFireWrappeResponse doDebit(string fileId, String amount)
        {
            DesFireWrappeResponse dsf_str = new DesFireWrappeResponse();

            byte[] fileAidBytes = MyUtil.StringToByteArray(fileId);
            byte[] amountBytes = MyUtil.StringToByteArray(amount);

            Array.Reverse(amountBytes, 0, amountBytes.Length);

            byte[] data = new byte[fileAidBytes.Length + amountBytes.Length];
            Array.Copy(fileAidBytes, 0, data, 0, fileAidBytes.Length);
            Array.Copy(amountBytes, 0, data, fileAidBytes.Length, amountBytes.Length);

            CommunicationSetting cs = getFileCommSett(fileAidBytes[0], true, false, true, true, ref dsf_str);
            if (cs == CommunicationSetting.NONE)
            {
                dsf_str.sw1sw2 = "6FFF";
                dsf_str.err_msg_pcsc += "- Check if card authenticated with correct key\n";

                return dsf_str;
            }

            var apdu = new CommandApdu(IsoCase.Case4Short, SCardProtocol.Any);
            apdu.CLA = (byte)0x90;
            apdu.INS = (byte)DesfireInstructionCode.Debit;
            apdu.P1 = 0x00;
            apdu.P2 = 0x00;
            apdu.Data = data;

            byte[] apduBytes = apdu.ToArray();

            byte[] apdu_processed = preprocess(apduBytes.ToArray(), 1, cs, ref dsf_str);

            ResponseApdu respApdu = transmitAndReceiveResp(apdu_processed, ref dsf_str);

            code = respApdu.SW2;

            bool success = postprocess(respApdu.ToArray(), CommunicationSetting.PLAIN, ref dsf_str) != null;

            return dsf_str;
        }

        //Do limited credit on a value file
        public DesFireWrappeResponse doLimitedCredit(string fileId, String amount)
        {
            DesFireWrappeResponse dsf_str = new DesFireWrappeResponse();

            byte[] fileAidBytes = MyUtil.StringToByteArray(fileId);
            byte[] amountBytes = MyUtil.StringToByteArray(amount);

            Array.Reverse(amountBytes, 0, amountBytes.Length);

            byte[] data = new byte[fileAidBytes.Length + amountBytes.Length];
            Array.Copy(fileAidBytes, 0, data, 0, fileAidBytes.Length);
            Array.Copy(amountBytes, 0, data, fileAidBytes.Length, amountBytes.Length);

            CommunicationSetting cs = getFileCommSett(fileAidBytes[0], true, false, true, true, ref dsf_str);
            if (cs == CommunicationSetting.NONE)
            {
                dsf_str.sw1sw2 = "6FFF";
                dsf_str.err_msg_pcsc += "- Check if card authenticated with correct key\n";

                return dsf_str;
            }

            var apdu = new CommandApdu(IsoCase.Case4Short, SCardProtocol.Any);
            apdu.CLA = (byte)0x90;
            apdu.INS = (byte)DesfireInstructionCode.LimitedCredit;
            apdu.P1 = 0x00;
            apdu.P2 = 0x00;
            apdu.Data = data;

            byte[] apduBytes = apdu.ToArray();

            byte[] apdu_processed = preprocess(apduBytes.ToArray(), 1, cs, ref dsf_str);

            ResponseApdu respApdu = transmitAndReceiveResp(apdu_processed, ref dsf_str);

            code = respApdu.SW2;

            bool success = postprocess(respApdu.ToArray(), CommunicationSetting.PLAIN, ref dsf_str) != null;

            return dsf_str;
        }

        //Do commit transaction
        public DesFireWrappeResponse doCommitTransaction()
        {
            DesFireWrappeResponse dsf_str = new DesFireWrappeResponse();

            var apdu = new CommandApdu(IsoCase.Case2Short, SCardProtocol.Any);
            apdu.CLA = (byte)0x90;
            apdu.INS = (byte)DesfireInstructionCode.CommitTransaction;
            apdu.P1 = 0x00;
            apdu.P2 = 0x00;

            ResponseApdu respApdu = transmitAndReceiveResp(apdu, ref dsf_str);

            return dsf_str;
        }

        //Do abort transaction
        public DesFireWrappeResponse doAbortTransaction()
        {
            DesFireWrappeResponse dsf_str = new DesFireWrappeResponse();

            var apdu = new CommandApdu(IsoCase.Case2Short, SCardProtocol.Any);
            apdu.CLA = (byte)0x90;
            apdu.INS = (byte)DesfireInstructionCode.AbortTransaction;
            apdu.P1 = 0x00;
            apdu.P2 = 0x00;

            ResponseApdu respApdu = transmitAndReceiveResp(apdu, ref dsf_str);

            return dsf_str;
        }

        public DesFireWrappeResponse getValueDirect(string fileId, CommunicationSetting cs, ref string value_ret)
        {
            DesFireWrappeResponse dsf_str = new DesFireWrappeResponse();

            byte[] fileAidBytes = MyUtil.StringToByteArray(fileId);

            //CommunicationSetting cs = getFileCommSett(fileAidBytes[0], true, false, true, true, ref dsf_str);
            //if (cs == CommunicationSetting.NONE)
            //{
            //    dsf_str.sw1sw2 = "6FFF";
            //    dsf_str.err_msg_pcsc += "- Check if card authenticated with correct key\n";

            //    return dsf_str;
            //}

            var apdu = new CommandApdu(IsoCase.Case4Short, SCardProtocol.Any);
            apdu.CLA = (byte)0x90;
            apdu.INS = (byte)DesfireInstructionCode.GetValue;
            apdu.P1 = 0x00;
            apdu.P2 = 0x00;
            apdu.Data = fileAidBytes;

            byte[] apduBytes = apdu.ToArray();
            byte[] apdu_processed = preprocess(apduBytes, CommunicationSetting.PLAIN, ref dsf_str);

            ResponseApdu respApdu = transmitAndReceiveResp(apdu_processed, ref dsf_str);
            code = respApdu.SW2;

            if (code != 0x00)
                return dsf_str;

            byte[] ret = postprocess(respApdu.ToArray(), 4, cs, ref dsf_str);
            if (ret == null)
            {
                value_ret = null;
            }
            else
            {
                Array.Reverse(ret, 0, ret.Length);
                value_ret = BitConverter.ToString(ret.ToArray()).Replace("-", string.Empty);
            }

            return dsf_str;
        }

        //Get value from a value file
        public DesFireWrappeResponse getValue(string fileId, ref string value_ret)
        {
            DesFireWrappeResponse dsf_str = new DesFireWrappeResponse();

            byte[] fileAidBytes = MyUtil.StringToByteArray(fileId);

            CommunicationSetting cs = getFileCommSett(fileAidBytes[0], true, false, true, true, ref dsf_str);
            if (cs == CommunicationSetting.NONE)
            {
                dsf_str.sw1sw2 = "6FFF";
                dsf_str.err_msg_pcsc += "- Check if card authenticated with correct key\n";

                return dsf_str;
            }

            var apdu = new CommandApdu(IsoCase.Case4Short, SCardProtocol.Any);
            apdu.CLA = (byte)0x90;
            apdu.INS = (byte)DesfireInstructionCode.GetValue;
            apdu.P1 = 0x00;
            apdu.P2 = 0x00;
            apdu.Data = fileAidBytes;

            byte[] apduBytes = apdu.ToArray();
            byte[] apdu_processed = preprocess(apduBytes, CommunicationSetting.PLAIN, ref dsf_str);

            ResponseApdu respApdu = transmitAndReceiveResp(apdu_processed, ref dsf_str);
            code = respApdu.SW2;

            if (code != 0x00)
                return dsf_str;

            byte[] ret = postprocess(respApdu.ToArray(), 4, cs, ref dsf_str);
            if (ret == null)
            {
                value_ret = null;
            }
            else
            {
                Array.Reverse(ret, 0, ret.Length);
                value_ret = BitConverter.ToString(ret.ToArray()).Replace("-", string.Empty);
            }

            return dsf_str;
        }

        //create an application (under PICC master application)
        public DesFireWrappeResponse createApplication(string newAppAid, string keySettings, string NoOfKey)
        {
            DesFireWrappeResponse dsf_str = new DesFireWrappeResponse();

            byte[] newAppAidBytes = MyUtil.StringToByteArray(newAppAid);
            byte[] keySettingsBytes = MyUtil.StringToByteArray(keySettings);
            byte[] NoOfKeyBytes = MyUtil.StringToByteArray(NoOfKey);

            byte[] data = new byte[newAppAidBytes.Length + keySettingsBytes.Length + NoOfKeyBytes.Length];
            Array.Copy(newAppAidBytes, 0, data, 0, newAppAidBytes.Length);
            Array.Copy(keySettingsBytes, 0, data, newAppAidBytes.Length, keySettingsBytes.Length);
            Array.Copy(NoOfKeyBytes, 0, data, newAppAidBytes.Length + keySettingsBytes.Length, NoOfKeyBytes.Length);

            var apdu = new CommandApdu(IsoCase.Case4Short, SCardProtocol.Any);
            apdu.CLA = (byte)0x90;
            apdu.INS = (byte)DesfireInstructionCode.CreateApplication;
            apdu.P1 = 0x00;
            apdu.P2 = 0x00;
            apdu.Data = data;

            byte[] preprocessed_apdu = preprocess(apdu.ToArray(), CommunicationSetting.PLAIN, ref dsf_str);

            ResponseApdu respApdu = transmitAndReceiveResp(preprocessed_apdu, ref dsf_str);

            postprocess(respApdu.ToArray(), CommunicationSetting.PLAIN, ref dsf_str);

            return dsf_str;
        }

        //create an application (under PICC master application)
        public DesFireWrappeResponse deleteApplication(string AppAid)
        {
            DesFireWrappeResponse dsf_str = new DesFireWrappeResponse();

            byte[] AppAidBytes = MyUtil.StringToByteArray(AppAid);

            byte[] apdu = new byte[] {
                (byte) 0x90,
                (byte) DesfireInstructionCode.DeleteApplication,
                0x00,
                0x00,
                0x03,
                AppAidBytes[0],
                AppAidBytes[1],
                AppAidBytes[2],
                0x00
            };

            if (this.m_fileNo == AppAidBytes[0])
            {
                m_fileNo = FAKE_NO;
                m_fileSett = null;
            }

            byte[] apdu_preprocessed = preprocess(apdu, CommunicationSetting.PLAIN, ref dsf_str);

            byte[] resp = new byte[300];
            var err = mainCardReader.Transmit(apdu_preprocessed, ref resp);

            ResponseApdu respApdu = new ResponseApdu(resp, IsoCase.Case4Short, SCardProtocol.Any);
            feedback(apdu_preprocessed, respApdu, ref dsf_str);
            code = respApdu.SW2;

            postprocess(respApdu.ToArray(), CommunicationSetting.PLAIN, ref dsf_str);

            return dsf_str;
        }

        public DesFireWrappeResponse deleteFile(string fileNo)
        {
            DesFireWrappeResponse dsf_str = new DesFireWrappeResponse();

            byte[] fileIdByte = MyUtil.StringToByteArray(fileNo);

            byte[] apdu = new byte[] {
                (byte) 0x90,
                (byte) DesfireInstructionCode.DeleteFile,
                0x00,
                0x00,
                0x01,
                fileIdByte[0],
                0x00
            };

            if (this.m_fileNo == fileIdByte[0])
            {
                m_fileNo = FAKE_NO;
                m_fileSett = null;
            }

            byte[] preprocessed_apdu = preprocess(apdu, CommunicationSetting.PLAIN, ref dsf_str);

            byte[] resp = new byte[300];
            var err = mainCardReader.Transmit(preprocessed_apdu, ref resp);

            ResponseApdu respApdu = new ResponseApdu(resp, IsoCase.Case4Short, SCardProtocol.Any);
            feedback(preprocessed_apdu, respApdu, ref dsf_str);
            code = respApdu.SW2;

            postprocess(respApdu.ToArray(), CommunicationSetting.PLAIN, ref dsf_str);

            return dsf_str;
        }

        public DesFireWrappeResponse clearRecordFile(string fileNo)
        {
            DesFireWrappeResponse dsf_str = new DesFireWrappeResponse();

            byte[] fileIdByte = MyUtil.StringToByteArray(fileNo);

            byte[] apdu = new byte[] {
                (byte) 0x90,
                (byte) DesfireInstructionCode.ClearRecordFile,
                0x00,
                0x00,
                0x01,
                fileIdByte[0],
                0x00
            };

            byte[] preprocessed_apdu = preprocess(apdu, CommunicationSetting.PLAIN, ref dsf_str);

            byte[] resp = new byte[300];
            var err = mainCardReader.Transmit(preprocessed_apdu, ref resp);

            ResponseApdu respApdu = new ResponseApdu(resp, IsoCase.Case4Short, SCardProtocol.Any);
            feedback(preprocessed_apdu, respApdu, ref dsf_str);
            code = respApdu.SW2;

            postprocess(respApdu.ToArray(), CommunicationSetting.PLAIN, ref dsf_str);

            return dsf_str;
        }

        //Read a standard file
        public DesFireWrappeResponse readStandardFileDirect(string fileAid, CommunicationSetting cs, string startOffset, string stdFileLength, ref string deciphered_value)
        {
            DesFireWrappeResponse dsf_str = new DesFireWrappeResponse();

            byte[] fileAidBytes = MyUtil.StringToByteArray(fileAid);
            byte[] startOffsetBytes = MyUtil.StringToByteArray(startOffset);
            byte[] stdFileLengthBytes = MyUtil.StringToByteArray(stdFileLength);

            Array.Reverse(startOffsetBytes, 0, startOffsetBytes.Length);
            Array.Reverse(stdFileLengthBytes, 0, stdFileLengthBytes.Length);

            byte[] data = new byte[fileAidBytes.Length + startOffsetBytes.Length + stdFileLengthBytes.Length];
            Array.Copy(fileAidBytes, 0, data, 0, fileAidBytes.Length);
            Array.Copy(startOffsetBytes, 0, data, fileAidBytes.Length, startOffsetBytes.Length);
            Array.Copy(stdFileLengthBytes, 0, data, fileAidBytes.Length + startOffsetBytes.Length, stdFileLengthBytes.Length);

            byte[] data_read = readDirect(data, (int)DesfireInstructionCode.ReadData, cs, ref dsf_str);
            if (data_read != null)
                deciphered_value = BitConverter.ToString(data_read).Replace("-", String.Empty);

            return dsf_str;
        }

        //Read a standard file
        public DesFireWrappeResponse readStandardFile(string fileAid, string startOffset, string stdFileLength, ref string deciphered_value)
        {
            DesFireWrappeResponse dsf_str = new DesFireWrappeResponse();

            byte[] fileAidBytes = MyUtil.StringToByteArray(fileAid);
            byte[] startOffsetBytes = MyUtil.StringToByteArray(startOffset);
            byte[] stdFileLengthBytes = MyUtil.StringToByteArray(stdFileLength);

            Array.Reverse(startOffsetBytes, 0, startOffsetBytes.Length);
            Array.Reverse(stdFileLengthBytes, 0, stdFileLengthBytes.Length);

            byte[] data = new byte[fileAidBytes.Length + startOffsetBytes.Length + stdFileLengthBytes.Length];
            Array.Copy(fileAidBytes, 0, data, 0, fileAidBytes.Length);
            Array.Copy(startOffsetBytes, 0, data, fileAidBytes.Length, startOffsetBytes.Length);
            Array.Copy(stdFileLengthBytes, 0, data, fileAidBytes.Length + startOffsetBytes.Length, stdFileLengthBytes.Length);

            byte[] data_read = read(data, (int)DesfireInstructionCode.ReadData, ref dsf_str);
            if (data_read != null)
                deciphered_value = BitConverter.ToString(data_read).Replace("-", String.Empty);

            return dsf_str;
        }


        //Read a standard file
        public DesFireWrappeResponse readRecordFileDirect(string fileAid, CommunicationSetting cs, string offset, string RecLength, ref string deciphered_value)
        {
            DesFireWrappeResponse dsf_str = new DesFireWrappeResponse();

            byte[] fileAidBytes = MyUtil.StringToByteArray(fileAid);
            byte[] offsetBytes = MyUtil.StringToByteArray(offset);
            byte[] RecordLengthBytes = MyUtil.StringToByteArray(RecLength);

            Array.Reverse(offsetBytes, 0, offsetBytes.Length);
            Array.Reverse(RecordLengthBytes, 0, RecordLengthBytes.Length);

            byte[] data = new byte[fileAidBytes.Length + offsetBytes.Length + RecordLengthBytes.Length];
            Array.Copy(fileAidBytes, 0, data, 0, fileAidBytes.Length);
            Array.Copy(offsetBytes, 0, data, fileAidBytes.Length, offsetBytes.Length);
            Array.Copy(RecordLengthBytes, 0, data, fileAidBytes.Length + offsetBytes.Length, RecordLengthBytes.Length);

            byte[] data_read = readDirect(data, (int)DesfireInstructionCode.ReadRecords, cs, ref dsf_str);
            if (data_read != null)
                deciphered_value = BitConverter.ToString(data_read).Replace("-", String.Empty);

            return dsf_str;
        }

        //Read a standard file
        public DesFireWrappeResponse readRecordFile(string fileAid, string offset, string RecLength, ref string deciphered_value)
        {
            DesFireWrappeResponse dsf_str = new DesFireWrappeResponse();

            byte[] fileAidBytes = MyUtil.StringToByteArray(fileAid);
            byte[] offsetBytes = MyUtil.StringToByteArray(offset);
            byte[] RecordLengthBytes = MyUtil.StringToByteArray(RecLength);

            Array.Reverse(offsetBytes, 0, offsetBytes.Length);
            Array.Reverse(RecordLengthBytes, 0, RecordLengthBytes.Length);

            byte[] data = new byte[fileAidBytes.Length + offsetBytes.Length + RecordLengthBytes.Length];
            Array.Copy(fileAidBytes, 0, data, 0, fileAidBytes.Length);
            Array.Copy(offsetBytes, 0, data, fileAidBytes.Length, offsetBytes.Length);
            Array.Copy(RecordLengthBytes, 0, data, fileAidBytes.Length + offsetBytes.Length, RecordLengthBytes.Length);

            byte[] data_read = read(data, (int)DesfireInstructionCode.ReadRecords, ref dsf_str);
            if (data_read != null)
                deciphered_value = BitConverter.ToString(data_read).Replace("-", String.Empty);

            return dsf_str;
        }

        /**
	     * Read data from standard data files or backup data files.
	     * 
	     * @param payload	a byte array with the following contents:
	     * 					<br>file number (1 byte),
	     * 					<br>offset within the file being read (3 bytes LSB),
	     * 					<br>length of the data to read (3 byte LSB)
	     * 					When the length of the data being read is 0x000000,
	     * 					the entire file is read, starting from offset.
	     * @return			{@code true} on success, {@code false otherwise}
	     */
        public byte[] readData(byte[] payload, ref DesFireWrappeResponse dsf_str)
        {
            return read(payload, (int)DesfireInstructionCode.ReadData, ref dsf_str);
        }

        /**
	     * Get information on the properties of a specific file.
	     * 
	     * @param fileNo	the file number
	     * @return			the properties of the file on success, {@code null} otherwise
	     */
        public byte[] getFileSettings(int fileNo, ref DesFireWrappeResponse dsf_str)
        {
            //TODO: create some file object that allows to query properties?
            byte[] apdu = new byte[7];
            apdu[0] = (byte)0x90;
            apdu[1] = (byte)0xF5;
            apdu[2] = 0x00;
            apdu[3] = 0x00;
            apdu[4] = 0x01;
            apdu[5] = (byte)fileNo;
            apdu[6] = 0x00;

            byte[] preprocessed_apdu = preprocess(apdu, CommunicationSetting.PLAIN, ref dsf_str);

            byte[] resp = new byte[300];
            var err = mainCardReader.Transmit(preprocessed_apdu, ref resp);

            ResponseApdu respApdu = new ResponseApdu(resp, IsoCase.Case4Short, SCardProtocol.Any);
            feedback(preprocessed_apdu, respApdu, ref dsf_str);
            code = respApdu.SW2;

            return postprocess(respApdu.ToArray(), CommunicationSetting.PLAIN, ref dsf_str);
        }


        /**
	     * Called internally by some methods to make sure the file settings
	     * are up-to-date. Avoids multiple calls to the PICC to fetch the
	     * settings of the file being manipulated.
	     * 
	     * @param fileNo		the file number
	     * @param forceUpdate	force the update?
	     * @return				{@code true} on success, {@code false} otherwise
	     */
        private bool updateFileSett(byte fileNo, bool forceUpdate, ref DesFireWrappeResponse dsf_str)
        {
            if (this.m_fileNo != fileNo || forceUpdate)
            {

                byte[] tmp = getFileSettings(fileNo, ref dsf_str);

                if (tmp != null)
                {
                    this.m_fileNo = fileNo;
                    this.m_fileSett = tmp;
                }
                else
                {
                    this.m_fileNo = FAKE_NO;
                    this.m_fileSett = null;
                    return false;
                }
            }

            return true;
        }

        /**
        * Find which communication mode to use when operating on a file.
        * The arguments depend on the operation being performed.
        * 
        * @param fileNo	the file number
        * @param rw		read-write access
        * @param car		change access rights
        * @param r			read access
        * @param w			write access
        * @return			the communication mode on success, {@code null} on error
        */
        private CommunicationSetting getFileCommSett(byte fileNo, bool rw, bool car, bool r, bool w, ref DesFireWrappeResponse dsf_str)
        {
            if (updateFileSett(fileNo, false, ref dsf_str) == false)
                return CommunicationSetting.NONE;

            bool isAuthKey = false;
            bool isFreeAccess = false;

            if (rw)
            {
                if (((uint)(m_fileSett[2] & 0xF0)) >> 4 == m_kno)
                {
                    isAuthKey = true;
                }
                else if ((m_fileSett[2] & 0xF0) == 0xE0)
                {
                    isFreeAccess = true;
                }
            }

            if (car)
            {
                if ((m_fileSett[2] & 0x0F) == m_kno)
                {
                    return CommunicationSetting.ENCIPHERED;
                }
                else if ((m_fileSett[2] & 0x0F) == 0x0E)
                {
                    //isFreeAccess = true;
                    return CommunicationSetting.PLAIN;
                }
            }

            if (r)
            {
                if (((uint)(m_fileSett[3] & 0xF0)) >> 4 == m_kno)
                {
                    isAuthKey = true;
                }
                else if ((m_fileSett[3] & 0xF0) == 0xE0)
                {
                    isFreeAccess = true;
                }
            }

            if (w)
            {
                if ((m_fileSett[3] & 0x0F) == m_kno)
                {
                    isAuthKey = true;
                }
                else if ((m_fileSett[3] & 0x0F) == 0x0E)
                {
                    isFreeAccess = true;
                }
            }

            if (isAuthKey)
            {
                // at least one of the ARs matches the authentication keyNo
                return getFileCommSett(m_fileSett[1]);
            }
            else if (isFreeAccess)
            {
                // at least one of the ARs allows free access
                return CommunicationSetting.PLAIN;
            }
            else
            {
                // access is denied
                return CommunicationSetting.NONE;
            }
        }

        /* Support method for getFileCommSett(rw, car, r, w). */
        private CommunicationSetting getFileCommSett(byte cs)
        {
            switch (cs)
            {
                case 0x00:
                    return CommunicationSetting.PLAIN;
                case 0x01:
                    return CommunicationSetting.MACED;
                case 0x03:
                    return CommunicationSetting.ENCIPHERED;
                default:
                    return CommunicationSetting.NONE;
            }
        }

        /* Support method for read(). Find length of just the data. Retrieved
	     * APDU is likely to be longer due to encryption (e.g. CRC/padding).
	     */
        private int findResponseLength(byte[] payload, int cmd)
        {
            int responseLength = 0;

            switch (cmd)
            {
                case 0xBD:  // data files
                    int offsetDF = 0;
                    byte[] sourceRespLen;

                    if (payload[4] != 0 || payload[5] != 0 || payload[6] != 0)
                    {
                        sourceRespLen = payload;
                    }
                    else
                    {
                        sourceRespLen = m_fileSett;

                        offsetDF |= (payload[1] & 0xFF) << 0;
                        offsetDF |= (payload[2] & 0xFF) << 8;
                        offsetDF |= (payload[3] & 0xFF) << 16;
                    }

                    responseLength |= (sourceRespLen[6] & 0xff) << 16;
                    responseLength |= (sourceRespLen[5] & 0xff) << 8;
                    responseLength |= (sourceRespLen[4] & 0xff) << 0;
                    responseLength -= offsetDF;
                    break;
                case 0xBB:  // record files
                    int singleRecordSize = 0;
                    int offsetRF = 0;
                    int recordsToRead = 0;

                    singleRecordSize |= (m_fileSett[4] & 0xFF) << 0;
                    singleRecordSize |= (m_fileSett[5] & 0xFF) << 8;
                    singleRecordSize |= (m_fileSett[6] & 0xFF) << 16;

                    if (payload[4] != 0 || payload[5] != 0 || payload[6] != 0)
                    {
                        recordsToRead |= (payload[6] & 0xff) << 16;
                        recordsToRead |= (payload[5] & 0xff) << 8;
                        recordsToRead |= (payload[4] & 0xff) << 0;
                    }
                    else
                    {
                        offsetRF |= (payload[1] & 0xFF) << 0;
                        offsetRF |= (payload[2] & 0xFF) << 8;
                        offsetRF |= (payload[3] & 0xFF) << 16;

                        recordsToRead |= (m_fileSett[10] & 0xFF) << 0;
                        recordsToRead |= (m_fileSett[11] & 0xFF) << 8;
                        recordsToRead |= (m_fileSett[12] & 0xFF) << 16;
                        recordsToRead -= offsetRF;
                    }

                    responseLength = singleRecordSize * recordsToRead;
                    break;
                default:
                    return -1;  // never reached
            }

            return responseLength;
        }


        public CommunicationSetting getFileCommSettPublic(String fileid, bool rw, bool car, bool r, bool w, ref DesFireWrappeResponse dsf_str)
        {

            return getFileCommSett(MyUtil.StringToByteArray(fileid)[0], rw, car, r, w, ref dsf_str);
        }

        /* Support method for readData/readRecords. */
        private byte[] readDirect(byte[] payload, int cmd, CommunicationSetting cs, ref DesFireWrappeResponse dsf_str)
        {
            // record files: file settings could be cached,
            // returning an erroneous number of current records
            if (cmd == 0xBB && !updateFileSett(payload[0], true, ref dsf_str))
            {
                dsf_str.sw1sw2 = "6FFF";
                dsf_str.err_msg_pcsc += "- record files: file settings could be cached\n";

                return null;
            }

            //CommunicationSetting cs = getFileCommSett(payload[0], true, false, true, false, ref dsf_str);
            //if (cs == CommunicationSetting.NONE)
            //{
            //    dsf_str.sw1sw2 = "6FFF";
            //    dsf_str.err_msg_pcsc += "- Check if card authenticated with correct key\n";
            //    return null;
            //}

            MemoryStream baos = new MemoryStream();
            int responseLength = findResponseLength(payload, cmd);

            byte[] apdu = new byte[13];
            apdu[0] = (byte)0x90;
            apdu[1] = (byte)cmd;
            apdu[2] = 0x00;
            apdu[3] = 0x00;
            apdu[4] = 0x07;
            Array.Copy(payload, 0, apdu, 5, 7);
            apdu[12] = 0x00;

            byte[] preprocessed_apdu = preprocess(apdu, CommunicationSetting.PLAIN, ref dsf_str);

            //CommandAPDU command = new CommandAPDU(apdu);
            //ResponseAPDU response = transmit(command);

            byte[] resp = new byte[300];
            var err = mainCardReader.Transmit(preprocessed_apdu, ref resp);

            if (err != SCardError.Success)
            {
                uint err_uint = (uint)err;
                err_uint |= (uint)0x80100000;
                dsf_str.sw1sw2 = "6FFF";
                dsf_str.err_msg_pcsc += "- " + MyUtil.GetEnumPcscErrorDescription((SCardError)err_uint) + "\n";

                return null;
            }

            ResponseApdu respApdu = new ResponseApdu(resp, IsoCase.Case4Short, SCardProtocol.Any);

            feedback(preprocessed_apdu, respApdu, ref dsf_str);
            try
            {
                baos.Write(respApdu.GetData(), 0, respApdu.GetData().Length);
            }
            catch (Exception e1)
            {
                dsf_str.err_msg_pcsc += "- " + e1.Message + "\n";
                dsf_str.sw1sw2 = "6FFF";
                //disconnect();
                return null;
            }

            while (respApdu.SW2 == 0xAF)
            {
                apdu = new byte[] { (byte)0x90, (byte)DesfireInstructionCode.AdditionalFrame, 0x00, 0x00, 0x00 };

                resp = new byte[300];
                err = mainCardReader.Transmit(apdu.ToArray(), ref resp);

                if (err != SCardError.Success)
                {
                    uint err_uint = (uint)err;
                    err_uint |= (uint)0x80100000;
                    dsf_str.sw1sw2 = "6FFF";
                    dsf_str.err_msg_pcsc += "- " + MyUtil.GetEnumPcscErrorDescription((SCardError)err_uint) + "\n";

                    return null;
                }

                respApdu = new ResponseApdu(resp, IsoCase.Case4Short, SCardProtocol.Any);

                feedback(apdu, respApdu, ref dsf_str);
                try
                {
                    baos.Write(respApdu.GetData(), 0, respApdu.GetData().Length);
                }
                catch (Exception e)
                {
                    dsf_str.err_msg_pcsc += "- " + e.Message + "\n";
                    dsf_str.sw1sw2 = "6FFF";
                    //disconnect();
                    return null;
                }
            }

            baos.Write(respApdu.ToArray(), respApdu.ToArray().Length - 2, 2);

            return postprocess(baos.ToArray(), responseLength, cs, ref dsf_str);
        }

        /* Support method for readData/readRecords. */
        private byte[] read(byte[] payload, int cmd, ref DesFireWrappeResponse dsf_str)
        {
            // record files: file settings could be cached,
            // returning an erroneous number of current records
            if (cmd == 0xBB && !updateFileSett(payload[0], true, ref dsf_str))
            {
                dsf_str.sw1sw2 = "6FFF";
                dsf_str.err_msg_pcsc += "- record files: file settings could be cached\n";

                return null;
            }

            CommunicationSetting cs = getFileCommSett(payload[0], true, false, true, false, ref dsf_str);
            if (cs == CommunicationSetting.NONE)
            {
                dsf_str.sw1sw2 = "6FFF";
                dsf_str.err_msg_pcsc += "- Check if card authenticated with correct key\n";
                return null;
            }

            MemoryStream baos = new MemoryStream();
            int responseLength = findResponseLength(payload, cmd);

            byte[] apdu = new byte[13];
            apdu[0] = (byte)0x90;
            apdu[1] = (byte)cmd;
            apdu[2] = 0x00;
            apdu[3] = 0x00;
            apdu[4] = 0x07;
            Array.Copy(payload, 0, apdu, 5, 7);
            apdu[12] = 0x00;

            byte[] preprocessed_apdu = preprocess(apdu, CommunicationSetting.PLAIN, ref dsf_str);

            //CommandAPDU command = new CommandAPDU(apdu);
            //ResponseAPDU response = transmit(command);

            byte[] resp = new byte[300];
            var err = mainCardReader.Transmit(preprocessed_apdu, ref resp);

            if (err != SCardError.Success)
            {
                uint err_uint = (uint)err;
                err_uint |= (uint)0x80100000;
                dsf_str.sw1sw2 = "6FFF";
                dsf_str.err_msg_pcsc += "- " + MyUtil.GetEnumPcscErrorDescription((SCardError)err_uint) + "\n";

                return null;
            }

            ResponseApdu respApdu = new ResponseApdu(resp, IsoCase.Case4Short, SCardProtocol.Any);

            feedback(preprocessed_apdu, respApdu, ref dsf_str);
            try
            {
                baos.Write(respApdu.GetData(), 0, respApdu.GetData().Length);
            }
            catch (Exception e1)
            {
                dsf_str.err_msg_pcsc += "- " + e1.Message + "\n";
                dsf_str.sw1sw2 = "6FFF";
                //disconnect();
                return null;
            }

            while (respApdu.SW2 == 0xAF)
            {
                apdu = new byte[] { (byte)0x90, (byte)DesfireInstructionCode.AdditionalFrame, 0x00, 0x00, 0x00 };

                resp = new byte[300];
                err = mainCardReader.Transmit(apdu.ToArray(), ref resp);

                if (err != SCardError.Success)
                {
                    uint err_uint = (uint)err;
                    err_uint |= (uint)0x80100000;
                    dsf_str.sw1sw2 = "6FFF";
                    dsf_str.err_msg_pcsc += "- " + MyUtil.GetEnumPcscErrorDescription((SCardError)err_uint) + "\n";

                    return null;
                }

                respApdu = new ResponseApdu(resp, IsoCase.Case4Short, SCardProtocol.Any);

                feedback(apdu, respApdu, ref dsf_str);
                try
                {
                    baos.Write(respApdu.GetData(), 0, respApdu.GetData().Length);
                }
                catch (Exception e)
                {
                    dsf_str.err_msg_pcsc += "- " + e.Message + "\n";
                    dsf_str.sw1sw2 = "6FFF";
                    //disconnect();
                    return null;
                }
            }

            baos.Write(respApdu.ToArray(), respApdu.ToArray().Length - 2, 2);

            return postprocess(baos.ToArray(), responseLength, cs, ref dsf_str);
        }



        // Get file settings
        public DesFireWrappeResponse getFileSettings(string fileId, ref BasicFile fileObj)
        {
            fileObj = null;

            DesFireWrappeResponse dsf_str = new DesFireWrappeResponse();

            byte[] fileAidBytes = MyUtil.StringToByteArray(fileId);

            var apdu = new CommandApdu(IsoCase.Case4Short, SCardProtocol.Any);
            apdu.CLA = (byte)0x90;
            apdu.INS = (byte)DesfireInstructionCode.GetFileSettings;
            apdu.P1 = 0x00;
            apdu.P2 = 0x00;
            apdu.Data = fileAidBytes;

            ResponseApdu respApdu = transmitAndReceiveResp(apdu, ref dsf_str);

            string val = dsf_str.data;
            byte[] resp = respApdu.GetData();

            StandardFile sf = null;
            ValueFile vf = null;
            RecordFile rf = null;

            fileObj = null;
            if (resp == null)
            {
                return dsf_str;
            }

            if (val != string.Empty)
            {
                BasicFile bf = new BasicFile();
                bf.fileid = fileId;
                bf.filetype = val.Substring(0, 2);
                bf.communication_settings = val.Substring(2, 2);

                //read by reverse             
                bf.access_rights = val.Substring(6, 2);
                bf.access_rights += val.Substring(4, 2);

                if (resp[0] == 0x00) // standard file
                {
                    sf = new StandardFile(bf);
                    sf.file_size_dec_max_string = reverseEndian(val.Substring(8, 6));
                    fileObj = sf;

                }
                else if (resp[0] == 0x01) // backup file
                {
                    sf = new StandardFile(bf);
                    sf.file_size_dec_max_string = reverseEndian(val.Substring(8, 6));
                    fileObj = sf;
                }
                else if (resp[0] == 0x02) // Value files with backup
                {
                    vf = new ValueFile(bf);

                    vf.lower_limit_hex = reverseEndian(val.Substring(8, 8));
                    vf.upper_limit_hex = reverseEndian(val.Substring(16, 8));
                    vf.limited_credit_value_hex = reverseEndian(val.Substring(24, 8));
                    vf.limited_credit_enabled_hex = reverseEndian(val.Substring(32, 2));
                    fileObj = vf;
                }
                else if (resp[0] == 0x03) // Linear Record Files with Backup
                {
                    rf = new RecordFile(bf);

                    rf.record_size = reverseEndian(val.Substring(8, 6));
                    rf.maxNoOfRecords = reverseEndian(val.Substring(14, 6));
                    rf.currentNoOfRecords = reverseEndian(val.Substring(20, 6));
                    rf.isLinearRecord = true;

                    fileObj = rf;
                }
                else if (resp[0] == 0x04) // Cyclic Record Files with Backup
                {
                    rf = new RecordFile(bf);
                    rf.isLinearRecord = false;

                    rf.record_size = reverseEndian(val.Substring(8, 6));
                    rf.maxNoOfRecords = reverseEndian(val.Substring(14, 6));
                    rf.currentNoOfRecords = reverseEndian(val.Substring(20, 6));

                    fileObj = rf;
                }
            }

            return dsf_str;
        }

        // reverse the array        
        public string reverseEndian(string val)
        {
            byte[] tmp = MyUtil.StringToByteArray(val);
            Array.Reverse(tmp, 0, tmp.Length);

            return BitConverter.ToString(tmp).Replace("-", string.Empty);
        }


        // write to a standard file
        public DesFireWrappeResponse writeStdFileDirect(string fileAid, CommunicationSetting cs, string startOffset, string stdFileLength, string fileContent)
        {
            DesFireWrappeResponse dsf_str = new DesFireWrappeResponse();

            byte[] fileAidBytes = MyUtil.StringToByteArray(fileAid);
            byte[] startOffsetBytes = MyUtil.StringToByteArray(startOffset);
            byte[] stdFileLengthBytes = MyUtil.StringToByteArray(stdFileLength);

            Array.Reverse(startOffsetBytes, 0, startOffsetBytes.Length);
            Array.Reverse(stdFileLengthBytes, 0, stdFileLengthBytes.Length);

            //file content
            byte[] fileContentBytes = MyUtil.StringToByteArray(fileContent);

            byte[] data = new byte[fileAidBytes.Length + startOffsetBytes.Length + stdFileLengthBytes.Length + fileContentBytes.Length];
            Array.Copy(fileAidBytes, 0, data, 0, fileAidBytes.Length);
            Array.Copy(startOffsetBytes, 0, data, fileAidBytes.Length, startOffsetBytes.Length);
            Array.Copy(stdFileLengthBytes, 0, data, fileAidBytes.Length + startOffsetBytes.Length, stdFileLengthBytes.Length);
            Array.Copy(fileContentBytes, 0, data, fileAidBytes.Length + startOffsetBytes.Length + stdFileLengthBytes.Length, fileContentBytes.Length);

            //ResponseApdu respApdu = transmitAndReceiveResp(apdu, ref dsf_str);
            bool success = writeDirect(data, (byte)DesfireInstructionCode.WriteData, cs, ref dsf_str);

            return dsf_str;
        }

        // write to a standard file
        public DesFireWrappeResponse writeStdFile(string fileAid, string startOffset, string stdFileLength, string fileContent)
        {
            DesFireWrappeResponse dsf_str = new DesFireWrappeResponse();

            byte[] fileAidBytes = MyUtil.StringToByteArray(fileAid);
            byte[] startOffsetBytes = MyUtil.StringToByteArray(startOffset);
            byte[] stdFileLengthBytes = MyUtil.StringToByteArray(stdFileLength);

            Array.Reverse(startOffsetBytes, 0, startOffsetBytes.Length);
            Array.Reverse(stdFileLengthBytes, 0, stdFileLengthBytes.Length);

            //file content
            byte[] fileContentBytes = MyUtil.StringToByteArray(fileContent);

            byte[] data = new byte[fileAidBytes.Length + startOffsetBytes.Length + stdFileLengthBytes.Length + fileContentBytes.Length];
            Array.Copy(fileAidBytes, 0, data, 0, fileAidBytes.Length);
            Array.Copy(startOffsetBytes, 0, data, fileAidBytes.Length, startOffsetBytes.Length);
            Array.Copy(stdFileLengthBytes, 0, data, fileAidBytes.Length + startOffsetBytes.Length, stdFileLengthBytes.Length);
            Array.Copy(fileContentBytes, 0, data, fileAidBytes.Length + startOffsetBytes.Length + stdFileLengthBytes.Length, fileContentBytes.Length);

            //ResponseApdu respApdu = transmitAndReceiveResp(apdu, ref dsf_str);
            bool success = write(data, (byte)DesfireInstructionCode.WriteData, ref dsf_str);

            return dsf_str;
        }

        // write to a record file
        public DesFireWrappeResponse writeRecordFileDirect(string fileAid, CommunicationSetting cs, string offset, string stdFileLength, string fileContent)
        {
            DesFireWrappeResponse dsf_str = new DesFireWrappeResponse();

            byte[] fileAidBytes = MyUtil.StringToByteArray(fileAid);
            byte[] offsetBytes = MyUtil.StringToByteArray(offset);
            byte[] RecLengthBytes = MyUtil.StringToByteArray(stdFileLength);

            Array.Reverse(offsetBytes, 0, offsetBytes.Length);
            Array.Reverse(RecLengthBytes, 0, RecLengthBytes.Length);

            //file content
            byte[] fileContentBytes = MyUtil.StringToByteArray(fileContent);

            byte[] data = new byte[fileAidBytes.Length + offsetBytes.Length + RecLengthBytes.Length + fileContentBytes.Length];
            Array.Copy(fileAidBytes, 0, data, 0, fileAidBytes.Length);
            Array.Copy(offsetBytes, 0, data, fileAidBytes.Length, offsetBytes.Length);
            Array.Copy(RecLengthBytes, 0, data, fileAidBytes.Length + offsetBytes.Length, RecLengthBytes.Length);
            Array.Copy(fileContentBytes, 0, data, fileAidBytes.Length + offsetBytes.Length + RecLengthBytes.Length, fileContentBytes.Length);

            //ResponseApdu respApdu = transmitAndReceiveResp(apdu, ref dsf_str);
            bool success = writeDirect(data, (byte)DesfireInstructionCode.WriteRecord, cs, ref dsf_str);

            return dsf_str;
        }

        // write to a record file
        public DesFireWrappeResponse writeRecordFile(string fileAid, string offset, string stdFileLength, string fileContent)
        {
            DesFireWrappeResponse dsf_str = new DesFireWrappeResponse();

            byte[] fileAidBytes = MyUtil.StringToByteArray(fileAid);
            byte[] offsetBytes = MyUtil.StringToByteArray(offset);
            byte[] RecLengthBytes = MyUtil.StringToByteArray(stdFileLength);

            Array.Reverse(offsetBytes, 0, offsetBytes.Length);
            Array.Reverse(RecLengthBytes, 0, RecLengthBytes.Length);

            //file content
            byte[] fileContentBytes = MyUtil.StringToByteArray(fileContent);

            byte[] data = new byte[fileAidBytes.Length + offsetBytes.Length + RecLengthBytes.Length + fileContentBytes.Length];
            Array.Copy(fileAidBytes, 0, data, 0, fileAidBytes.Length);
            Array.Copy(offsetBytes, 0, data, fileAidBytes.Length, offsetBytes.Length);
            Array.Copy(RecLengthBytes, 0, data, fileAidBytes.Length + offsetBytes.Length, RecLengthBytes.Length);
            Array.Copy(fileContentBytes, 0, data, fileAidBytes.Length + offsetBytes.Length + RecLengthBytes.Length, fileContentBytes.Length);

            //ResponseApdu respApdu = transmitAndReceiveResp(apdu, ref dsf_str);
            bool success = write(data, (byte)DesfireInstructionCode.WriteRecord, ref dsf_str);

            return dsf_str;
        }

        // format card
        public DesFireWrappeResponse formatCard()
        {
            DesFireWrappeResponse dsf_str = new DesFireWrappeResponse();

            var apdu = new CommandApdu(IsoCase.Case2Short, SCardProtocol.Any);
            apdu.CLA = (byte)0x90;
            apdu.INS = (byte)DesfireInstructionCode.FormatPICC;
            apdu.P1 = 0x00;
            apdu.P2 = 0x00;

            ResponseApdu respApdu = transmitAndReceiveResp(apdu, ref dsf_str);

            return dsf_str;
        }


        public DesFireWrappeResponse authenticate(string keyVal, string keyNo, KeyType key_type, out string session_key)
        {
            byte[] skey_loc = null;
            session_key = null;
            DesFireWrappeResponse dsf_str = new DesFireWrappeResponse();

            byte[] keyNoBytes = MyUtil.StringToByteArray(keyNo);
            byte[] keyValBytes = MyUtil.StringToByteArray(keyVal);

            dsf_str = authenticate_internal(keyValBytes, keyNoBytes[0], key_type, ref skey_loc);

            if (skey_loc != null)
            {
                session_key = BitConverter.ToString(m_skey).Replace("-", string.Empty);
                this.m_skey = skey_loc;
            }

            return dsf_str;
        }


        /**
	     * Validates a key according with its type.
	     * 
	     * @param key	the key
	     * @param type	the type
	     * @return		{@code true} if the key matches the type,
	     * 				{@code false} otherwise
	     */
        public static bool validateKey(byte[] key, KeyType type, ref DesFireWrappeResponse dsf_str)
        {
            //    if (type == KeyType.DES && (key.Length != 8)
            //            || type == KeyType.TDES && (key.Length != 16 || !isKey3DES(key))
            //            || type == KeyType.TKTDES && (key.Length != 24 || !isKey3DES(key))
            //            || type == KeyType.AES && key.Length != 16)
            //    {
            //        dsf_str.err_msg_pcsc += "- Wrong key length, please check key size & properties! (DES = 8, 2KTDES = 16, 3K3DES = 24, AES = 16)" +
            //            "or the key is not valid 3 DES key";
            //        dsf_str.sw1sw2 = "6FFF";

            //        return false;
            //    }

            if (type == KeyType.DES && (key.Length != 8)
                    || type == KeyType.TDES && (key.Length != 16) //|| !isKey3DES(key))
                    || type == KeyType.TKTDES && (key.Length != 24) //|| !isKey3DES(key))
                    || type == KeyType.AES && key.Length != 16)
            {
                dsf_str.err_msg_pcsc += "- Wrong key length, please check key size & properties! (DES = 8, 2KTDES = 16, 3K3DES = 24, AES = 16)" +
                    "or the key is not valid 3 DES key";
                dsf_str.sw1sw2 = "6FFF";

                return false;
            }

            return true;
        }

        /**
         * Checks whether a 16-byte key is a 3DES key.
         * <p>
         * Some 3DES keys may actually be DES keys because the LSBit of
         * each byte is used for key versioning by MDF. A 16-byte key is
         * also a DES key if the first half of the key is equal to the second.
         * 
         * @param key	the 16-byte 3DES key to check
         * @return		<code>true</code> if the key is a 3DES key
         */
        public static bool isKey3DES(byte[] key)
        {
            if (key.Length < 16)
                return false;

            //byte[] tmpKey = Arrays.copyOfRange(key, 0, key.length);
            //byte[] tmpKey = key.Take(key.Length).ToArray();
            byte[] tmpKey = MyUtil.SubArray(key, 0, key.Length);

            setKeyVersion(tmpKey, 0, tmpKey.Length, (byte)0x00);
            for (int i = 0; i < 8; i++)
                if (tmpKey[i] != tmpKey[i + 8])
                    return true;

            if (key.Length > 16)
            {
                for (int i = 0; i < 8; i++)
                    if (tmpKey[i] != tmpKey[i + 16])
                        return true;
            }

            return false;
        }

        /**
         * Set the version on a DES key. Each least significant bit of each byte of
         * the DES key, takes one bit of the version. Since the version is only
         * one byte, the information is repeated if dealing with 16/24-byte keys.
         * 
         * @param a			1K/2K/3K 3DES
         * @param offset	start position of the key within a
         * @param length	key length
         * @param version	the 1-byte version
         */
        private static void setKeyVersion(byte[] a, int offset, int length, byte version)
        {
            if (length == 8 || length == 16 || length == 24)
            {
                for (int i = offset + length - 1, j = 0; i >= offset; i--, j = (j + 1) % 8)
                {
                    a[i] &= 0xFE;
                    a[i] |= (byte)(((uint)version >> j) & 0x01);
                }
            }
        }

        // DES/3DES decryption: CBC send mode and CBC receive mode
        private static byte[] decrypt(byte[] key, byte[] data, DESMode mode)
        {
            byte[] modifiedKey = new byte[24];
            Array.Copy(key, 0, modifiedKey, 16, 8);
            Array.Copy(key, 0, modifiedKey, 8, 8);
            Array.Copy(key, 0, modifiedKey, 0, key.Length);

            /* MF3ICD40, which only supports DES/3DES, has two cryptographic
             * modes of operation (CBC): send mode and receive mode. In send mode,
             * data is first XORed with the IV and then decrypted. In receive
             * mode, data is first decrypted and then XORed with the IV. The PCD
             * always decrypts. The initial IV, reset in all operations, is all zeros
             * and the subsequent IVs are the last decrypted/plain block according with mode.
             * 
             * MDF EV1 supports 3K3DES/AES and remains compatible with MF3ICD40.
             */
            byte[] ciphertext = new byte[data.Length];
            byte[] cipheredBlock = new byte[8];

            switch (mode)
            {
                case DESMode.SEND_MODE:
                    // XOR w/ previous ciphered block --> decrypt
                    for (int i = 0; i < data.Length; i += 8)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            data[i + j] ^= cipheredBlock[j];
                        }
                        cipheredBlock = DESCryptoUtil.decrypt(modifiedKey, data, i, 8);
                        Array.Copy(cipheredBlock, 0, ciphertext, i, 8);
                    }
                    break;
                case DESMode.RECEIVE_MODE:
                    // decrypt --> XOR w/ previous plaintext block
                    cipheredBlock = DESCryptoUtil.decrypt(modifiedKey, data, 0, 8);
                    // implicitly XORed w/ IV all zeros
                    Array.Copy(cipheredBlock, 0, ciphertext, 0, 8);
                    for (int i = 8; i < data.Length; i += 8)
                    {
                        cipheredBlock = DESCryptoUtil.decrypt(modifiedKey, data, i, 8);
                        for (int j = 0; j < 8; j++)
                        {
                            cipheredBlock[j] ^= data[i + j - 8];
                        }
                        Array.Copy(cipheredBlock, 0, ciphertext, i, 8);
                    }
                    break;
                default:
                    throw new Exception("Wrong way (decrypt)");
                    return null;
            }

            return ciphertext;
        }

        /**
         * DES/3DES mode of operation.
         * @see nfcjlib.core.DESFireEV1#decryptDES(byte[], byte[], DESMode)
         */
        private enum DESMode
        {
            SEND_MODE,
            RECEIVE_MODE
        }

        // Receiving data that needs decryption.
        private static byte[] recv(byte[] key, byte[] data, KeyType type, byte[] iv)
        {
            switch (type)
            {
                case KeyType.DES:
                case KeyType.TDES:
                    return decrypt(key, data, DESMode.RECEIVE_MODE);
                case KeyType.TKTDES:
                    return DESCryptoUtil.decrypt(iv == null ? new byte[8] : iv, key, data);
                case KeyType.AES:
                    return AES.decrypt(iv == null ? new byte[16] : iv, key, data);
                default:
                    return null;
            }
        }

        // IV sent is the global one but it is better to be explicit about it: can be null for DES/3DES
        // if IV is null, then it is set to zeros
        // Sending data that needs encryption.
        private static byte[] send(byte[] key, byte[] data, KeyType type, byte[] iv)
        {
            switch (type)
            {
                case KeyType.DES:
                case KeyType.TDES:
                    return decrypt(key, data, DESMode.SEND_MODE);
                case KeyType.TKTDES:
                    return DESCryptoUtil.encrypt_basic(iv == null ? new byte[8] : iv, key, data);
                case KeyType.AES:
                    return AES.encrypt(iv == null ? new byte[16] : iv, key, data);
                default:
                    return null;
            }
        }


        public void feedback(CommandApdu cmd, ResponseApdu resp, ref DesFireWrappeResponse dsf_str)
        {
            dsf_str.apdu_message += MyUtil.GetEnumIns2Description((DesfireInstructionCode)cmd.INS) + ": " + BitConverter.ToString(cmd.ToArray()).Replace("-", string.Empty) + "\n";
            dsf_str.apdu_message += ("Response: " + BitConverter.ToString(resp.ToArray()).Replace("-", string.Empty) + " (" + ((DesFireSw1Sw2)resp.SW2) + ")\n");

            dsf_str.data = resp.HasData ? BitConverter.ToString(resp.GetData()).Replace("-", string.Empty) : "";
            byte[] sw1sw2 = { resp.SW1, resp.SW2 };
            dsf_str.sw1sw2 = BitConverter.ToString(sw1sw2).Replace("-", string.Empty);
        }

        public void feedback(byte[] cmd, ResponseApdu resp, ref DesFireWrappeResponse dsf_str)
        {
            string test = MyUtil.GetEnumIns2Description((DesfireInstructionCode)cmd[1]);
            string test2 = BitConverter.ToString(cmd.ToArray()).Replace("-", string.Empty);

            dsf_str.apdu_message += MyUtil.GetEnumIns2Description((DesfireInstructionCode)cmd[1]) + ": " + BitConverter.ToString(cmd.ToArray()).Replace("-", string.Empty) + "\n";
            dsf_str.apdu_message += ("Response: " + BitConverter.ToString(resp.ToArray()).Replace("-", string.Empty) + " (" + ((DesFireSw1Sw2)resp.SW2) + ")\n");

            dsf_str.data = resp.HasData ? BitConverter.ToString(resp.GetData()).Replace("-", string.Empty) : "";
            byte[] sw1sw2 = { resp.SW1, resp.SW2 };
            dsf_str.sw1sw2 = BitConverter.ToString(sw1sw2).Replace("-", string.Empty);
        }

        private DesFireWrappeResponse authenticate_internal(byte[] key, byte keyNo, KeyType type, ref byte[] skey)
        {
            DesFireWrappeResponse dsf_str = new DesFireWrappeResponse();

            //dsf_str.sw1sw2 = "6FFF";
            dsf_str.data = null;

            if (!validateKey(key, type, ref dsf_str))
                return dsf_str;

            if (type != KeyType.AES)
            {
                // remove version bits from Triple DES keys
                setKeyVersion(key, 0, key.Length, (byte)0x00);
            }

            byte[] iv0 = type == KeyType.AES ? new byte[16] : new byte[8];

            // 1st message exchange
            var apdu = new CommandApdu(IsoCase.Case4Short, SCardProtocol.Any);
            apdu.CLA = (byte)0x90;
            switch (type)
            {
                case KeyType.DES:
                case KeyType.TDES:
                    apdu.INS = (byte)DesfireInstructionCode.Authenticate2K3DES;
                    break;
                case KeyType.TKTDES:
                    apdu.INS = (byte)DesfireInstructionCode.Authenticate3K3DES;
                    break;
                case KeyType.AES:
                    apdu.INS = (byte)DesfireInstructionCode.AuthenticateAES;
                    break;
                default:
                    dsf_str.err_msg_pcsc += "- " + "Authentication instruction not found\n";
                    dsf_str.sw1sw2 = "6FFF";
                    return dsf_str;
            }
            apdu.P1 = 0x00;
            apdu.P2 = 0x00;
            byte[] data = { keyNo };
            apdu.Data = data;

            byte[] resp = new byte[300];
            var err = mainCardReader.Transmit(apdu.ToArray(), ref resp);

            if (err != SCardError.Success)
            {
                uint err_uint = (uint)err;
                err_uint |= (uint)0x80100000;
                dsf_str.sw1sw2 = "6FFF";
                dsf_str.err_msg_pcsc += "- " + MyUtil.GetEnumPcscErrorDescription((SCardError)err_uint) + "\n";

                return dsf_str;
            }

            ResponseApdu respApdu = new ResponseApdu(resp, IsoCase.Case4Short, SCardProtocol.Any);

            feedback(apdu, respApdu, ref dsf_str);

            if (respApdu.SW2 != 0xAF)
                return dsf_str;

            // step 3
            byte[] randB = recv(key, respApdu.GetData(), type, iv0);
            if (randB == null)
                return dsf_str;

            byte[] randBr = DESCryptoUtil.RotateLeft(randB);
            byte[] randA = new byte[randB.Length];
            var r = new Random();
            r.NextBytes(randA);

            // step 3: encryption
            byte[] plaintext = new byte[randA.Length + randBr.Length];
            Array.Copy(randA, 0, plaintext, 0, randA.Length);
            Array.Copy(randBr, 0, plaintext, randA.Length, randBr.Length);
            byte[] iv1 = MyUtil.SubArray(respApdu.GetData(),
                    respApdu.GetData().Length - iv0.Length, respApdu.GetData().Length);
            byte[] ciphertext = send(key, plaintext, type, iv1);
            if (ciphertext == null)
                return dsf_str;

            // 2nd message exchange
            apdu.CLA = (byte)0x90;
            apdu.INS = (byte)0xAF;
            apdu.Data = ciphertext;

            err = mainCardReader.Transmit(apdu.ToArray(), ref resp);

            if (err != SCardError.Success)
            {
                uint err_uint = (uint)err;
                err_uint |= (uint)0x80100000;
                dsf_str.sw1sw2 = "6FFF";
                dsf_str.err_msg_pcsc += "- " + MyUtil.GetEnumPcscErrorDescription((SCardError)err_uint) + "\n";

                return dsf_str;
            }

            respApdu = new ResponseApdu(resp, IsoCase.Case4Short, SCardProtocol.Any);

            feedback(apdu, respApdu, ref dsf_str);

            if (respApdu.SW2 != 0x00)
                return dsf_str;

            //// step 5
            byte[] iv2 = MyUtil.SubArray(ciphertext, ciphertext.Length - iv0.Length, ciphertext.Length);
            byte[] randAr = recv(key, respApdu.GetData(), type, iv2);
            if (randAr == null)
                return dsf_str;
            byte[] randAr2 = DESCryptoUtil.RotateLeft(randA);
            for (int i = 0; i < randAr2.Length; i++)
                if (randAr[i] != randAr2[i])
                    return dsf_str;

            //// step 6
            skey = generateSessionKey(randA, randB, type);
            dsf_str.apdu_message += ("The random A is " + BitConverter.ToString(randA).Replace("-", string.Empty) + "\n");
            dsf_str.apdu_message += ("The random B is " + BitConverter.ToString(randB).Replace("-", string.Empty) + "\n");

            if (skey == null)
            {
                dsf_str.sw1sw2 = "6FFF";
                dsf_str.err_msg_pcsc += "Session Key is null, probably Random Length is less than key type" + "\n";                
            }
            else
            dsf_str.apdu_message += ("Session Key =  " + BitConverter.ToString(skey).Replace("-", string.Empty) + "\n");

            this.m_ktype = type;
            this.m_kno = keyNo;
            this.m_iv = iv0;
            this.m_skey = skey;

            return dsf_str;
        }

        /**
	     * Generate the session key using the random A generated by the PICC and
	     * the random B generated by the PCD.
	     * 
	     * @param randA	the random number A
	     * @param randB	the random number B
	     * @param type	the type of key
	     * @return		the session key
	 */
        private static byte[] generateSessionKey(byte[] randA, byte[] randB, KeyType type)
        {
            byte[] skey = null;

            if (randA.Length != randB.Length)
                return null;

            switch (type)
            {
                case KeyType.DES:
                    if (randA.Length < 4)
                        return null;
                    skey = new byte[8];
                    Array.Copy(randA, 0, skey, 0, 4);
                    Array.Copy(randB, 0, skey, 4, 4);
                    break;
                case KeyType.TDES:
                    if (randA.Length < 8)
                        return null;
                    skey = new byte[16];
                    Array.Copy(randA, 0, skey, 0, 4);
                    Array.Copy(randB, 0, skey, 4, 4);
                    Array.Copy(randA, 4, skey, 8, 4);
                    Array.Copy(randB, 4, skey, 12, 4);
                    break;
                case KeyType.TKTDES:
                    if (randA.Length < 16)
                        return null;
                    skey = new byte[24];
                    Array.Copy(randA, 0, skey, 0, 4);
                    Array.Copy(randB, 0, skey, 4, 4);
                    Array.Copy(randA, 6, skey, 8, 4);
                    Array.Copy(randB, 6, skey, 12, 4);
                    Array.Copy(randA, 12, skey, 16, 4);
                    Array.Copy(randB, 12, skey, 20, 4);
                    break;
                case KeyType.AES:
                    if (randA.Length < 16)
                        return null;
                    skey = new byte[16];
                    Array.Copy(randA, 0, skey, 0, 4);
                    Array.Copy(randB, 0, skey, 4, 4);
                    Array.Copy(randA, 12, skey, 8, 4);
                    Array.Copy(randB, 12, skey, 12, 4);
                    break;
                default:
                    throw new Exception("Wrong key type");
                    break;
            }

            return skey;
        }

        public DesFireWrappeResponse getKeySettings(ref string keySettings, ref string keyType, ref string maxNoKey)
        {
            DesFireWrappeResponse dsf_str = new DesFireWrappeResponse();

            var apdu = new CommandApdu(IsoCase.Case2Short, SCardProtocol.Any);
            apdu.CLA = (byte)0x90;
            apdu.INS = (byte)DesfireInstructionCode.GetKeySettings;
            apdu.P1 = 0x00;
            apdu.P2 = 0x00;

            ResponseApdu respApdu = transmitAndReceiveResp(apdu, ref dsf_str);

            if (dsf_str.data != string.Empty && dsf_str.data.Length > 0)
            {
                keySettings = dsf_str.data.Substring(0, 2);
                keyType = dsf_str.data.Substring(2, 1);
                maxNoKey = dsf_str.data.Substring(3, 1);
            }

            return dsf_str;
        }

        private static byte[] calculateApduCMAC(byte[] apdu, byte[] sessionKey, byte[] iv, KeyType type)
        {
            byte[] block;

            if (apdu.Length == 5)
            {
                block = new byte[apdu.Length - 4];
            }
            else
            {
                // trailing 00h exists
                block = new byte[apdu.Length - 5];
                Array.Copy(apdu, 5, block, 1, apdu.Length - 6);
            }
            block[0] = apdu[1];

            switch (type)
            {
                case KeyType.TKTDES:
                    return CMAC.get(CMAC.Type.TKTDES, sessionKey, block, iv);
                case KeyType.AES:
                    return CMAC.get(CMAC.Type.AES, sessionKey, block, iv);
                default:
                    return null;
            }
        }

        // update global IV
        private byte[] preprocessPlain(byte[] apdu)
        {
            if (m_ktype == KeyType.TKTDES || m_ktype == KeyType.AES)
            {
                m_iv = calculateApduCMAC(apdu, m_skey, m_iv, m_ktype);
            }

            return apdu;
        }

        /**
	     * Calculate the MAC of {@code data}.
	     * <p>
	     * The MAC is calculated using Triple DES encryption. The MAC is
	     * the first half of the last block of ciphertext.
	     * 
	     * @param data	the data (length is multiple of 8)
	     * @param key	the 8/16-byte key
	     * @return		the 4-byte MAC
	     */
        /* Support method for calculateApduMAC(C|R). */
        private static byte[] calculateMAC(byte[] data, byte[] key)
        {
            byte[] key24 = new byte[24];
            Array.Copy(key, 0, key24, 16, 8);
            Array.Copy(key, 0, key24, 8, 8);
            Array.Copy(key, 0, key24, 0, key.Length);

            //byte[] ciphertext = nfcjlib.core.util.TripleDES.encrypt(new byte[8], key24, data);
            byte[] ciphertext = DESCryptoUtil.encrypt_basic(new byte[8], key24, data);

            return MyUtil.SubArray(ciphertext, ciphertext.Length - 8, ciphertext.Length - 4);
        }

        // calculated only over data (header also left out: e.g. could be keyNo)
        private static byte[] calculateApduMACC(byte[] apdu, byte[] skey, int offset)
        {
            int datalen = apdu.Length == 5 ? 0 : apdu.Length - 6 - offset;
            byte[] block = new byte[datalen % 8 == 0 ? datalen : (datalen / 8 + 1) * 8];
            Array.Copy(apdu, 5 + offset, block, 0, apdu.Length - 6 - offset);

            return calculateMAC(block, skey);
        }

        // update global IV and append
        //(2K3)DES?
        private byte[] preprocessMaced(byte[] apdu, int offset)
        {
            switch (m_ktype)
            {
                case KeyType.DES:
                case KeyType.TDES:
                    byte[] mac = calculateApduMACC(apdu, m_skey, offset);

                    byte[] ret1 = new byte[apdu.Length + 4];
                    Array.Copy(apdu, 0, ret1, 0, apdu.Length);
                    Array.Copy(mac, 0, ret1, apdu.Length - 1, 4);
                    ret1[4] += 4;

                    return ret1;
                case KeyType.TKTDES:
                case KeyType.AES:
                    m_iv = calculateApduCMAC(apdu, m_skey, m_iv, m_ktype);

                    byte[] ret2 = new byte[apdu.Length + 8];
                    Array.Copy(apdu, 0, ret2, 0, apdu.Length);
                    Array.Copy(m_iv, 0, ret2, apdu.Length - 1, 8);  // trailing 00
                    ret2[4] += 8;

                    return ret2;
                default:
                    return null;
            }
        }

        // CRC16 calculated only over data
        private static byte[] calculateApduCRC16C(byte[] apdu, int offset)
        {
            if (apdu.Length == 5)
            {
                return CRC16.get(new byte[0]);
            }
            else
            {
                return CRC16.get(apdu, 5 + offset, apdu.Length - 5 - offset - 1);
            }
        }

        // CRC32 calculated over INS+header+data
        private static byte[] calculateApduCRC32C(byte[] apdu)
        {
            byte[] data;

            if (apdu.Length == 5)
            {
                data = new byte[apdu.Length - 4];
            }
            else
            {
                data = new byte[apdu.Length - 5];
                Array.Copy(apdu, 5, data, 1, apdu.Length - 6);
            }
            data[0] = apdu[1];

            return CRC32.get(data);
        }

        /* Only data is encrypted. Headers are left out (e.g. keyNo for credit). */
        private static byte[] encryptApdu(byte[] apdu, int offset, byte[] sessionKey, byte[] iv, KeyType type)
        {
            int blockSize = type == KeyType.AES ? 16 : 8;
            int payloadLen = apdu.Length - 6;
            byte[] crc = null;

            switch (type)
            {
                case KeyType.DES:
                case KeyType.TDES:
                    crc = calculateApduCRC16C(apdu, offset);
                    break;
                case KeyType.TKTDES:
                case KeyType.AES:
                    crc = calculateApduCRC32C(apdu);
                    break;
            }

            int padding = 0;  // padding=0 if block length is adequate
            if ((payloadLen - offset + crc.Length) % blockSize != 0)
                padding = blockSize - (payloadLen - offset + crc.Length) % blockSize;
            int ciphertextLen = payloadLen - offset + crc.Length + padding;
            byte[] plaintext = new byte[ciphertextLen];
            Array.Copy(apdu, 5 + offset, plaintext, 0, payloadLen - offset);
            Array.Copy(crc, 0, plaintext, payloadLen - offset, crc.Length);

            return send(sessionKey, plaintext, type, iv);
        }


        // calculate CRC and append, encrypt, and update global IV
        private byte[] preprocessEnciphered(byte[] apdu, int offset)
        {
            byte[] ciphertext = encryptApdu(apdu, offset, m_skey, m_iv, m_ktype);

            byte[] ret = new byte[5 + offset + ciphertext.Length + 1];
            Array.Copy(apdu, 0, ret, 0, 5 + offset);
            Array.Copy(ciphertext, 0, ret, 5 + offset, ciphertext.Length);
            ret[4] = (byte)(offset + ciphertext.Length);

            if (m_ktype == KeyType.TKTDES || m_ktype == KeyType.AES)
            {
                m_iv = new byte[m_iv.Length];
                Array.Copy(ciphertext, ciphertext.Length - m_iv.Length, m_iv, 0, m_iv.Length);
            }

            return ret;
        }

        /**
	     * Pre-process command APDU before sending it to PICC.
	     * The global IV is updated.
	     * 
	     * <p>If not authenticated, the APDU is immediately returned.
	     * 
	     * @param apdu		the APDU
	     * @param offset	the offset of data within the command (for enciphered).
	     * 					For example, credit does not encrypt the 1-byte
	     * 					key number so the offset would be 1.
	     * @param commSett	the communication mode
	     * @return			For PLAIN, returns the APDU. For MACed, returns the
	     * 					APDU with the CMAC appended. For ENCIPHERED,
	     * 					returns the ciphered version of the APDU.
	     * 					If an error occurs, returns <code>null</code>.
	     */
        private byte[] preprocess(byte[] apdu, int offset, CommunicationSetting commSett, ref DesFireWrappeResponse dsf_str)
        {
            if (commSett == CommunicationSetting.NONE)
            {
                dsf_str.err_msg_pcsc += ("- preprocess: commSett is null\n");
                dsf_str.sw1sw2 = "6FFF";
                return null;
            }

            switch (commSett)
            {
                case CommunicationSetting.PLAIN:
                    return preprocessPlain(apdu);
                case CommunicationSetting.MACED:
                    if (m_skey == null)
                    {
                        dsf_str.err_msg_pcsc += ("- preprocess: skey is null\n");
                        dsf_str.sw1sw2 = "6FFF";

                        return apdu;
                    }

                    return preprocessMaced(apdu, offset);
                case CommunicationSetting.ENCIPHERED:

                    if (m_skey == null)
                    {
                        dsf_str.err_msg_pcsc += ("- preprocess: skey is null\n");
                        dsf_str.sw1sw2 = "6FFF";

                        return apdu;
                    }
                    return preprocessEnciphered(apdu, offset);
                default:
                    return null;  // never reached
            }
        }

        private byte[] preprocess(byte[] apdu, CommunicationSetting commSett, ref DesFireWrappeResponse dsf_str)
        {
            return preprocess(apdu, 0, commSett, ref dsf_str);
        }


        /**
	     * Change the master key settings or the application master key settings,
	     * depending on the selected AID.
	     * <p>
	     * Requires a preceding authentication.
	     * 
	     * @param keySett	the new key settings
	     * @return			{@code true} on success, {@code false} otherwise
	     */
        public DesFireWrappeResponse changeKeySettings(string keySetStr, ref bool success)
        {
            byte keySett = Convert.ToByte(keySetStr, 16);

            DesFireWrappeResponse dsf_str = new DesFireWrappeResponse();

            byte[] apdu = new byte[7];
            apdu[0] = (byte)0x90;
            apdu[1] = (byte)DesfireInstructionCode.ChangeKeySettings;
            apdu[4] = 0x01;
            apdu[5] = keySett;

            byte[] preprocessed_apdu = preprocess(apdu, CommunicationSetting.ENCIPHERED, ref dsf_str);

            byte[] resp = new byte[300];
            var err = mainCardReader.Transmit(preprocessed_apdu, ref resp);

            if (err != SCardError.Success)
            {
                uint err_uint = (uint)err;
                err_uint |= (uint)0x80100000;
                dsf_str.sw1sw2 = "6FFF";
                dsf_str.err_msg_pcsc += "- " + MyUtil.GetEnumPcscErrorDescription((SCardError)err_uint) + "\n";

                return dsf_str;
            }

            ResponseApdu respApdu = new ResponseApdu(resp, IsoCase.Case4Short, SCardProtocol.Any);
            feedback(preprocessed_apdu, respApdu, ref dsf_str);

            success = postprocess(respApdu.ToArray(), CommunicationSetting.PLAIN, ref dsf_str) != null;

            return dsf_str;
        }

        private byte[] postprocess(byte[] resp_apdu, CommunicationSetting commSett, ref DesFireWrappeResponse dsf_str)
        {
            return postprocess(resp_apdu, -1, commSett, ref dsf_str);
        }

        /**
	 * Some commands require post-processing. It can be used to check if
	 * the received CMAC is correct, or to decipher a response APDU and
	 * verify if the CRC is correct. The global IV is updated.
	 * 
	 * <p>If not authenticated, the APDU is immediately returned.
	 * This also happens if the APDU length is 2 and the status code is
	 * different from success (0x00).
	 * 
	 * FIXME: return only relevant data from apdu
	 * FIXME: handle limitedCredit boundary error, denied permisson, .......
	 * 
	 * @param apdu		the APDU
	 * @param length	the length of data (0 to beginning of CRC32)
	 * @param commSett	the communication mode
	 * @return			For PLAIN and MACed, it returns the APDU (without MAC/CMAC and status code 91 xx).
	 * 					For ENCIPHERED, returns the deciphered APDU.
	 * 					If an error occurs, returns <code>null</code>.
	 */
        private byte[] postprocess(byte[] resp_apdu, int length, CommunicationSetting commSett, ref DesFireWrappeResponse dsf_str)
        {
            if (commSett == CommunicationSetting.NONE)
            {
                dsf_str.err_msg_pcsc += "- postprocess: commSett is null\n";
                dsf_str.sw1sw2 = "6FFF";

                return null;
            }

            if (resp_apdu[resp_apdu.Length - 1] != 0x00)
            {
                dsf_str.err_msg_pcsc += "- postprocess: status <> 00 (" + MyUtil.GetEnumSw1Sw2Description((DesFireSw1Sw2)resp_apdu[resp_apdu.Length - 1]) + ")\n";
                dsf_str.sw1sw2 = "6FFF";
                reset();
                return null;
            }

            if (resp_apdu.Length - 2 <= length)
            {
                return MyUtil.SubArray(resp_apdu, 0, resp_apdu.Length - 2);
            }


            switch (commSett)
            {
                case CommunicationSetting.PLAIN:
                    //if (m_ktype == KeyType.DES || m_ktype == KeyType.TDES)
                    return MyUtil.SubArray(resp_apdu, 0, resp_apdu.Length - 2);
                    break;
                case CommunicationSetting.MACED:
                    if (m_skey == null)
                    {
                        //System.err.println("postprocess: skey is null");
                        dsf_str.err_msg_pcsc += "- postprocess: skey is null\n";
                        dsf_str.sw1sw2 = "6FFF";
                        return MyUtil.SubArray(resp_apdu, 0, resp_apdu.Length - 2);
                    }
                    return postprocessMaced(resp_apdu, ref dsf_str);
                    break;
                case CommunicationSetting.ENCIPHERED:
                    if (m_skey == null)
                    {
                        //System.err.println("postprocess: skey is null");
                        dsf_str.err_msg_pcsc += "- postprocess: skey is null\n";
                        dsf_str.sw1sw2 = "6FFF";
                        return MyUtil.SubArray(resp_apdu, 0, resp_apdu.Length - 2);
                    }
                    return postprocessEnciphered(resp_apdu, length, ref dsf_str);
                    break;
                default:
                    return null;  // never reached
            }

            return null;
        }

        // calculated only over data
        private static byte[] calculateApduMACR(byte[] apdu, byte[] skey)
        {
            int datalen = apdu.Length - 6;
            int blockSize = datalen % 8 == 0 ? datalen : (datalen / 8 + 1) * 8;
            byte[] block = new byte[blockSize];
            Array.Copy(apdu, 0, block, 0, datalen);

            return calculateMAC(block, skey);
        }

        private static byte[] calculateApduCRC16R(byte[] apdu, int length)
        {
            byte[] data = new byte[length];

            Array.Copy(apdu, 0, data, 0, length);

            return CRC16.get(data);
        }

        private static byte[] calculateApduCRC32R(byte[] apdu, int length)
        {
            byte[] data = new byte[length + 1];

            Array.Copy(apdu, 0, data, 0, length);// response code is at the end

            return CRC32.get(data);
        }

        private byte[] postprocessEnciphered(byte[] resp_apdu, int length, ref DesFireWrappeResponse dsf_str)
        {
            if ((resp_apdu.Length >= 2) == false)
            {
                dsf_str.err_msg_pcsc += "- Apdu Length less than 2\n";
                dsf_str.sw1sw2 = "6FFF";
                return null;
            }

            byte[] ciphertext = MyUtil.SubArray(resp_apdu, 0, resp_apdu.Length - 2);
            byte[] plaintext = recv(m_skey, ciphertext, m_ktype, m_iv);

            byte[] crc;
            switch (m_ktype)
            {
                case KeyType.DES:
                case KeyType.TDES:
                    crc = calculateApduCRC16R(plaintext, length);
                    break;
                case KeyType.TKTDES:
                case KeyType.AES:
                    m_iv = MyUtil.SubArray(resp_apdu, resp_apdu.Length - 2 - m_iv.Length, resp_apdu.Length - 2);
                    crc = calculateApduCRC32R(plaintext, length);
                    break;
                default:
                    return null;
            }
            for (int i = 0; i < crc.Length; i++)
            {
                if (crc[i] != plaintext[i + length])
                {
                    //System.err.println("Received CMAC does not match calculated CMAC.");
                    dsf_str.err_msg_pcsc += "- Received CMAC does not match calculated CMAC.\n";
                    dsf_str.sw1sw2 = "6FFF";
                    return null;
                }
            }

            return MyUtil.SubArray(plaintext, 0, length);
        }

        /**
	     * Write data to standard data files or backup data files.
	     * <p>
	     * When writing to backup data files, a {@linkplain #commitTransaction()}
	     * is required to validate the changes.
	     * 
	     * @param payload	a byte array with the following contents:
	     * 					<br>file number (1 byte),
	     * 					<br>offset within the file being written (3 bytes LSB),
	     * 					<br>length of the data (3 byte LSB),
	     * 					<br>the data (1+ bytes)
	     * @return			{@code true} on success, {@code false otherwise}
	     */
        public bool writeData(byte[] payload, ref DesFireWrappeResponse dsf_str)
        {
            return write(payload, (byte)DesfireInstructionCode.WriteData, ref dsf_str);
        }


        /* Support method for writeData/writeRecord. */
        private bool writeDirect(byte[] payload, byte cmd, CommunicationSetting cs, ref DesFireWrappeResponse dsf_str)
        {
            //CommunicationSetting cs = getFileCommSett(payload[0], true, false, false, true, ref dsf_str);
            //if (cs == CommunicationSetting.NONE)
            //{
            //    dsf_str.sw1sw2 = "6FFF";
            //    dsf_str.err_msg_pcsc += "- Check if card authenticated with correct key\n";

            //    return false;
            //}

            byte[] apdu;
            byte[] fullApdu = new byte[6 + payload.Length];
            fullApdu[0] = (byte)0x90;
            fullApdu[1] = cmd;
            fullApdu[4] = 0xff;
            Array.Copy(payload, 0, fullApdu, 5, payload.Length);

            fullApdu = preprocess(fullApdu, 7, cs, ref dsf_str);  // 7 = 1+3+3 (keyNo+off+len)

            int totalPayload = fullApdu.Length - 6;
            int payloadSent = 0;

            ResponseApdu respApdu = null;

            do
            {
                int sendThisFrame = totalPayload - payloadSent > 52 ? 52
                        : totalPayload - payloadSent;

                //System.out.println(String.format("totalPaylod=%d, payloadSent=%d, sendThisFrame=%d",
                //		totalPayload, payloadSent, sendThisFrame));

                apdu = new byte[6 + sendThisFrame];
                apdu[0] = fullApdu[0];
                apdu[1] = payloadSent == 0 ? fullApdu[1] : (byte)DesfireInstructionCode.AdditionalFrame;
                apdu[4] = (byte)sendThisFrame;
                Array.Copy(fullApdu, 5 + payloadSent, apdu, 5, sendThisFrame);

                //command = new CommandAPDU(apdu);
                //response = transmit(command);

                byte[] resp = new byte[300];
                var err = mainCardReader.Transmit(apdu, ref resp);

                if (err != SCardError.Success)
                {
                    uint err_uint = (uint)err;
                    err_uint |= (uint)0x80100000;
                    dsf_str.sw1sw2 = "6FFF";
                    dsf_str.err_msg_pcsc += "- " + MyUtil.GetEnumPcscErrorDescription((SCardError)err_uint) + "\n";
                }

                respApdu = new ResponseApdu(resp, IsoCase.Case4Short, SCardProtocol.Any);
                //feedback(apdu, respApdu, ref dsf_str);

                code = respApdu.SW2;
                feedback(apdu, respApdu, ref dsf_str);

                payloadSent += sendThisFrame;
            } while (totalPayload - payloadSent > 0 && code == 0xAF);

            return postprocess(respApdu.ToArray(), CommunicationSetting.PLAIN, ref dsf_str) != null;
        }

        /* Support method for writeData/writeRecord. */
        private bool write(byte[] payload, byte cmd, ref DesFireWrappeResponse dsf_str)
        {
            CommunicationSetting cs = getFileCommSett(payload[0], true, false, false, true, ref dsf_str);
            if (cs == CommunicationSetting.NONE)
            {
                dsf_str.sw1sw2 = "6FFF";
                dsf_str.err_msg_pcsc += "- Check if card authenticated with correct key\n";

                return false;
            }

            byte[] apdu;
            byte[] fullApdu = new byte[6 + payload.Length];
            fullApdu[0] = (byte)0x90;
            fullApdu[1] = cmd;
            fullApdu[4] = 0xff;
            Array.Copy(payload, 0, fullApdu, 5, payload.Length);

            fullApdu = preprocess(fullApdu, 7, cs, ref dsf_str);  // 7 = 1+3+3 (keyNo+off+len)

            int totalPayload = fullApdu.Length - 6;
            int payloadSent = 0;

            ResponseApdu respApdu = null;

            do
            {
                int sendThisFrame = totalPayload - payloadSent > 52 ? 52
                        : totalPayload - payloadSent;

                //System.out.println(String.format("totalPaylod=%d, payloadSent=%d, sendThisFrame=%d",
                //		totalPayload, payloadSent, sendThisFrame));

                apdu = new byte[6 + sendThisFrame];
                apdu[0] = fullApdu[0];
                apdu[1] = payloadSent == 0 ? fullApdu[1] : (byte)DesfireInstructionCode.AdditionalFrame;
                apdu[4] = (byte)sendThisFrame;
                Array.Copy(fullApdu, 5 + payloadSent, apdu, 5, sendThisFrame);

                //command = new CommandAPDU(apdu);
                //response = transmit(command);

                byte[] resp = new byte[300];
                var err = mainCardReader.Transmit(apdu, ref resp);

                if (err != SCardError.Success)
                {
                    uint err_uint = (uint)err;
                    err_uint |= (uint)0x80100000;
                    dsf_str.sw1sw2 = "6FFF";
                    dsf_str.err_msg_pcsc += "- " + MyUtil.GetEnumPcscErrorDescription((SCardError)err_uint) + "\n";
                }

                respApdu = new ResponseApdu(resp, IsoCase.Case4Short, SCardProtocol.Any);
                //feedback(apdu, respApdu, ref dsf_str);

                code = respApdu.SW2;
                feedback(apdu, respApdu, ref dsf_str);

                payloadSent += sendThisFrame;
            } while (totalPayload - payloadSent > 0 && code == 0xAF);

            return postprocess(respApdu.ToArray(), CommunicationSetting.PLAIN, ref dsf_str) != null;
        }

        private byte[] postprocessMaced(byte[] resp_byte, ref DesFireWrappeResponse dsf_str)
        {
            byte[] resp_apdu = resp_byte;

            switch (m_ktype)
            {
                case KeyType.DES:
                case KeyType.TDES:
                    //assert apdu.length >= 4 + 2;
                    //Assert. ();
                    if ((resp_apdu.Length >= 4 + 2) == false)
                    {
                        dsf_str.err_msg_pcsc += "- Apdu Length less than 6\n";
                        dsf_str.sw1sw2 = "6FFF";
                        return null;
                    }

                    byte[] mac = calculateApduMACR(resp_apdu, m_skey);
                    for (int i = 0, j = resp_apdu.Length - 6; i < 4 && j < resp_apdu.Length - 2; i++, j++)
                    {
                        if (mac[i] != resp_apdu[j])
                        {
                            return null;
                        }
                    }

                    return MyUtil.SubArray(resp_apdu, 0, resp_apdu.Length - 4 - 2);
                case KeyType.TKTDES:
                case KeyType.AES:
                    //assert resp_apdu.length >= 8 + 2;
                    if ((resp_apdu.Length >= 8 + 2) == false)
                    {
                        dsf_str.err_msg_pcsc += "Apdu Length less than 10\n";
                        dsf_str.sw1sw2 = "6FFF";
                        return null;
                    }

                    byte[] block2 = new byte[resp_apdu.Length - 9];
                    Array.Copy(resp_apdu, 0, block2, 0, resp_apdu.Length - 10);
                    block2[block2.Length - 1] = resp_apdu[resp_apdu.Length - 1];

                    CMAC.Type cmacType = m_ktype == KeyType.AES ? CMAC.Type.AES : CMAC.Type.TKTDES;
                    byte[] cmac = CMAC.get(cmacType, m_skey, block2, m_iv);
                    for (int i = 0, j = resp_apdu.Length - 10; i < 8 && j < resp_apdu.Length - 2; i++, j++)
                    {
                        if (cmac[i] != resp_apdu[j])
                        {
                            dsf_str.err_msg_pcsc += "- Received CMAC does not match calculated CMAC.\n";
                            dsf_str.sw1sw2 = "6FFF";
                            return null;
                        }
                    }
                    m_iv = cmac;

                    return MyUtil.SubArray(resp_apdu, 0, resp_apdu.Length - 8 - 2);
                default:
                    return null;  // never reached
            }
        }

        /**
	     * Reset the attributes of this instance to their default values.
	     * Called when the authentication status is changed, such as after a
	     * change key or AID selection operation.
	     */
        private void reset()
        {
            m_ktype = KeyType.FAKE;
            m_kno = FAKE_NO;
            //aid = new byte[3];  // authentication resets but AID does not change.
            m_iv = null;
            m_skey = null;

            m_fileNo = FAKE_NO;
            m_fileSett = null;
        }

        public bool isSessionKeyValid()
        {
            if (m_skey != null)
                return true;

            return false;
        }

        public DesFireWrappeResponse changeKeyDesfire(string keyNo, DesFireWrapper.KeyType old_key_type, string oldKey, DesFireWrapper.KeyType new_key_type, string newKeyVersion, string newKeyValue)
        {
            DesFireWrappeResponse dsf_str = new DesFireWrappeResponse();

            byte[] keyNoByte = MyUtil.StringToByteArray(keyNo);
            byte[] oldKeyByte = MyUtil.StringToByteArray(oldKey);
            byte[] newKeyVersionByte = MyUtil.StringToByteArray(newKeyVersion);
            byte[] newKeyByte = MyUtil.StringToByteArray(newKeyValue);

            bool success = changeKey(keyNoByte[0], newKeyVersionByte[0], new_key_type, newKeyByte, old_key_type, oldKeyByte, ref dsf_str);

            return dsf_str;
        }


        /**
	     * Change any key stored on the PICC. The version will be set to zero.
	     * 
	     * @param keyNo		the number of the key to be changed
	     * @param newType	the type of the new key
	     * @param newKey	the new key (8-bytes for DES,
	     * 					16-bytes for 2K3DES/AES, 24-bytes 3K3DES)
	     * @param oldKey	the old key (only required if the the key being
	     * 					changed is different from the authenticated key; can be
	     * 					set to <code>null</code> if both keys are the same)
	     * @return			the APDU received from the PICC
	     */
        public bool changeKey(byte keyNo, KeyType newType, byte[] newKey, KeyType old_key_type, byte[] oldKey, ref DesFireWrappeResponse dsf_str)
        {
            return changeKey(keyNo, (byte)0x00, newType, newKey, old_key_type, oldKey, m_skey, ref dsf_str);
        }

        public bool changeKey(byte keyNo, byte keyVersion, KeyType newType, byte[] newKey, KeyType old_key_type, byte[] oldKey, ref DesFireWrappeResponse dsf_str)
        {
            return changeKey(keyNo, keyVersion, newType, newKey, old_key_type, oldKey, m_skey, ref dsf_str);
        }

        /**
	     * Validates a key according with its type.
	     * 
	     * @param key	the key
	     * @param type	the type
	     * @return		{@code true} if the key matches the type,
	     * 				{@code false} otherwise
	     */
        public static bool validateKey(byte[] key, KeyType type)
        {
            if (type == KeyType.DES && (key.Length != 8)
                    || type == KeyType.TDES && (key.Length != 16 || !isKey3DES(key))
                    || type == KeyType.TKTDES && key.Length != 24
                    || type == KeyType.AES && key.Length != 16)
            {
                //System.err.println(String.format("Key validation failed: length is %d and type is %s", key.length, type));
                return false;
            }
            return true;
        }

        // version is 1 separate byte for AES, and the LSBit of each byte for DES keys
        private bool changeKey(byte keyNo, byte keyVersion, KeyType new_key_type, byte[] newKey,
            KeyType old_key_type, byte[] oldKey, byte[] sessionKey, ref DesFireWrappeResponse dsf_str)
        {
            //if (!validateKey(newKey, new_key_type)
            //        || Array.Equals(m_aid, new byte[3]) && keyNo != 0x00
            //        || m_kno != (keyNo & 0x0F)
            //        && (oldKey == null
            //        || m_ktype == KeyType.DES && oldKey.Length != 8
            //        || m_ktype == KeyType.TDES && oldKey.Length != 16
            //        || m_ktype == KeyType.TKTDES && oldKey.Length != 24
            //        || m_ktype == KeyType.AES && oldKey.Length != 16))
            //if (!validateKey(newKey, new_key_type)
            //        || Array.Equals(m_aid, new byte[3]) && keyNo != 0x00
            //        //|| m_kno != (keyNo & 0x0F)
            //        && (oldKey == null
            //        || old_key_type == KeyType.DES && oldKey.Length != 8
            //        || old_key_type == KeyType.TDES && oldKey.Length != 16
            //        || old_key_type == KeyType.TKTDES && oldKey.Length != 24
            //        || old_key_type == KeyType.AES && oldKey.Length != 16))
            //{
            //    // basic checks to mitigate the possibility of messing up the keys
            //    //System.err.println("You're doing it wrong, chief! (changeKey: check your args)");
            //    //this.code = Response.WRONG_ARGUMENT.getCode();
            //    dsf_str.err_msg_pcsc += "- " + "Wrong key argument, check key length and type!" + "\n";
            //    dsf_str.sw1sw2 = "6FFF";
            //    return false;
            //}

            byte[] backup_new_key = new byte[newKey.Length];
            Array.Copy(newKey, backup_new_key, newKey.Length);

            if (!validateKey(newKey, new_key_type) || oldKey == null)
            {
                // basic checks to mitigate the possibility of messing up the keys
                //System.err.println("You're doing it wrong, chief! (changeKey: check your args)");
                //this.code = Response.WRONG_ARGUMENT.getCode();
                dsf_str.err_msg_pcsc += "- " + "Wrong key argument, check key length and type!" + "\n";
                dsf_str.sw1sw2 = "6FFF";

                return false;
            }

            if (Enumerable.SequenceEqual(m_aid, new byte[3]) && m_kno == 0x00)
            {
                //MessageBoxButtons buttons = MessageBoxButtons.OKCancel;

                //DialogResult result = MessageBox.Show("You are about to change PICC Master Key, CONFIRM?", "Warning", buttons, MessageBoxIcon.Warning);

                //if (result == DialogResult.Cancel)
                //    return false;
            }

            byte[] plaintext = null;
            byte[] ciphertext = null;

            int nklen = new_key_type == KeyType.TKTDES ? 24 : 16;  // length of new key

            switch (m_ktype)
            {
                case KeyType.DES:
                case KeyType.TDES:
                    plaintext = new_key_type == KeyType.TKTDES ? new byte[32] : new byte[24];
                    break;
                case KeyType.TKTDES:
                case KeyType.AES:
                    plaintext = new byte[32];
                    break;
                default:
                    //assert false : ktype; // this point should never be reached
                    return false;
            }

            //dsf_str.apdu_message += "plaintext= " + BitConverter.ToString(plaintext).Replace("-", string.Empty) + "\n";

            if (new_key_type == KeyType.AES)
            {
                plaintext[16] = keyVersion;
            }
            else
            {
                setKeyVersion(newKey, 0, newKey.Length, keyVersion);
            }

            Array.Copy(newKey, 0, plaintext, 0, newKey.Length);

            //dsf_str.apdu_message += "plaintext with keyversion= " + BitConverter.ToString(plaintext).Replace("-", string.Empty) + "\n";

            if (new_key_type == KeyType.DES)
            {
                // 8-byte DES keys accepted: internally have to be handled w/ 16 bytes
                Array.Copy(newKey, 0, plaintext, 8, newKey.Length);
                newKey = MyUtil.SubArray(plaintext, 0, 16);
            }

            // tweak for when changing PICC master key
            //bool eq = Array.Equals(m_aid, new byte[3]);
            bool eq = MyUtil.ArraysEqual(m_aid, new byte[3]);
            if (eq)
            {
                switch (new_key_type)
                {
                    case KeyType.TKTDES:
                        keyNo = 0x40;
                        break;
                    case KeyType.AES:
                        keyNo = (byte)0x80;
                        break;
                    default:
                        break;
                }
            }

            if ((keyNo & 0x0F) != m_kno)
            {
                for (int i = 0; i < newKey.Length; i++)
                {
                    plaintext[i] ^= oldKey[i % oldKey.Length];
                }
            }

            byte[] tmpForCRC;
            byte[] crc;
            int addAesKeyVersionByte = new_key_type == KeyType.AES ? 1 : 0;

            switch (m_ktype)
            {
                case KeyType.DES:
                case KeyType.TDES:
                    crc = CRC16.get(plaintext, 0, nklen + addAesKeyVersionByte);
                    Array.Copy(crc, 0, plaintext, nklen + addAesKeyVersionByte, 2);

                    if ((keyNo & 0x0F) != m_kno)
                    {
                        crc = CRC16.get(newKey);
                        //crc = CRC16.get(backup_new_key);
                        Array.Copy(crc, 0, plaintext, nklen + addAesKeyVersionByte + 2, 2);
                    }

                    ciphertext = send(sessionKey, plaintext, m_ktype, null);
                    break;
                case KeyType.TKTDES:
                case KeyType.AES:
                    tmpForCRC = new byte[1 + 1 + nklen + addAesKeyVersionByte];
                    //tmpForCRC[0] = (byte)Command.CHANGE_KEY.getCode();
                    tmpForCRC[0] = (byte)DesfireInstructionCode.ChangeKey;
                    tmpForCRC[1] = keyNo;
                    Array.Copy(plaintext, 0, tmpForCRC, 2, nklen + addAesKeyVersionByte);
                    crc = CRC32.get(tmpForCRC);
                    Array.Copy(crc, 0, plaintext, nklen + addAesKeyVersionByte, crc.Length);

                    if ((keyNo & 0x0F) != m_kno)
                    {
                        crc = CRC32.get(newKey);
                        Array.Copy(crc, 0, plaintext, nklen + addAesKeyVersionByte + 4, crc.Length);
                    }

                    ciphertext = send(sessionKey, plaintext, m_ktype, m_iv);
                    m_iv = MyUtil.SubArray(ciphertext, ciphertext.Length - m_iv.Length, ciphertext.Length);
                    break;
                default:
                    //assert false : ktype; // should never be reached
                    return false;
            }

            byte[] data = new byte[1 + ciphertext.Length];
            data[0] = keyNo;
            Array.Copy(ciphertext, 0, data, 1, ciphertext.Length);

            // 1st message exchange
            var apdu = new CommandApdu(IsoCase.Case4Short, SCardProtocol.Any);
            apdu.CLA = (byte)0x90;
            apdu.INS = (byte)DesfireInstructionCode.ChangeKey;
            apdu.P1 = 0x00;
            apdu.P2 = 0x00;
            apdu.Data = data;

            ResponseApdu respApdu = transmitAndReceiveResp(apdu, ref dsf_str);

            this.code = (int)(respApdu.SW2 & 0xff);

            if (this.code != 0x00)
                return false;

            if ((keyNo & 0x0F) == m_kno)
            {
                //reset();
            }
            else
            {
                if (postprocess(respApdu.ToArray(), CommunicationSetting.PLAIN, ref dsf_str) == null)
                    return false;
            }

            return true;
        }


        // Create a standard file
        public DesFireWrappeResponse createRecordFile(bool isLinear, string newFileId,
            String commSettings, string AccessRights, String RecordSize, String MaxOfRecords)
        {
            DesFireWrappeResponse dsf_str = new DesFireWrappeResponse();

            byte[] newFileAidBytes = MyUtil.StringToByteArray(newFileId);
            byte[] commSettingsBytes = MyUtil.StringToByteArray(commSettings);

            byte[] accessRightsBytes = MyUtil.StringToByteArray(AccessRights);
            Array.Reverse(accessRightsBytes);

            byte[] RecordSizeBytes = MyUtil.StringToByteArray(RecordSize);
            byte[] MaxOfRecordsBytes = MyUtil.StringToByteArray(MaxOfRecords);
            Array.Reverse(RecordSizeBytes, 0, RecordSizeBytes.Length);
            Array.Reverse(MaxOfRecordsBytes, 0, MaxOfRecordsBytes.Length);

            byte[] data = new byte[newFileAidBytes.Length + commSettingsBytes.Length + accessRightsBytes.Length
                + RecordSizeBytes.Length + MaxOfRecordsBytes.Length];
            Array.Copy(newFileAidBytes, 0, data, 0, newFileAidBytes.Length);
            Array.Copy(commSettingsBytes, 0, data, newFileAidBytes.Length, commSettingsBytes.Length);
            Array.Copy(accessRightsBytes, 0, data, newFileAidBytes.Length + commSettingsBytes.Length, accessRightsBytes.Length);
            Array.Copy(RecordSizeBytes, 0, data, newFileAidBytes.Length + commSettingsBytes.Length + accessRightsBytes.Length, RecordSizeBytes.Length);
            Array.Copy(MaxOfRecordsBytes, 0, data, newFileAidBytes.Length + commSettingsBytes.Length + accessRightsBytes.Length + RecordSizeBytes.Length, MaxOfRecordsBytes.Length);

            var apdu = new CommandApdu(IsoCase.Case4Short, SCardProtocol.Any);
            apdu.CLA = (byte)0x90;
            apdu.INS = (isLinear == true) ? (byte)DesfireInstructionCode.CreateLinearRecordFile : (byte)DesfireInstructionCode.CreateCyclicRecordFile;
            apdu.P1 = 0x00;
            apdu.P2 = 0x00;
            apdu.Data = data;

            ResponseApdu respApdu = transmitAndReceiveResp(apdu, ref dsf_str);

            return dsf_str;
        }

    }

    //enum of desfire commands
    public enum DesfireInstructionCode : byte
    {
        [DescriptionAttribute("Authenticate DES 2K3DES")]
        Authenticate2K3DES = (byte)0x0A,
        [DescriptionAttribute("Authenticate DES 3K3DES")]
        Authenticate3K3DES = (byte)0x1A,
        [DescriptionAttribute("Authenticate AES")]
        AuthenticateAES = (byte)0xAA,
        [DescriptionAttribute("Additional Frame")]
        AdditionalFrame = (byte)0xAF,
        [DescriptionAttribute("Change Key Settings")]
        ChangeKeySettings = (byte)0x54,
        [DescriptionAttribute("Get Key Settings")]
        GetKeySettings = (byte)0x45,
        [DescriptionAttribute("Change Key")]
        ChangeKey = (byte)0xC4,
        [DescriptionAttribute("Get Key Version")]
        GetKeyVersion = (byte)0x64,
        [DescriptionAttribute("Create Application")]
        CreateApplication = (byte)0xCA,
        [DescriptionAttribute("Delete Application")]
        DeleteApplication = (byte)0xDA,
        [DescriptionAttribute("Get Applications IDs")]
        GetApplicationsIDs = (byte)0x6A,
        [DescriptionAttribute("Select Application")]
        SelectApplication = (byte)0x5A,
        [DescriptionAttribute("Format PICC")]
        FormatPICC = (byte)0xFC,
        [DescriptionAttribute("Get Version")]
        EnveGetVersion = (byte)0x60,

        [DescriptionAttribute("Get File IDs")]
        GetFileIDs = (byte)0x6F,
        [DescriptionAttribute("Get File Settings")]
        GetFileSettings = (byte)0xF5,
        [DescriptionAttribute("Change FileSettings")]
        ChangeFileSettings = (byte)0x5F,
        [DescriptionAttribute("Create Standard Data File")]
        CreateStdDataFile = (byte)0xCD,
        [DescriptionAttribute("Create Backup Data File")]
        CreateBackupDataFile = (byte)0xCB,
        [DescriptionAttribute("Create Value File")]
        CreateValueFile = (byte)0xCC,
        [DescriptionAttribute("Create Linear Record File")]
        CreateLinearRecordFile = (byte)0xC1,
        [DescriptionAttribute("CreateCyclicRecordFile")]
        CreateCyclicRecordFile = (byte)0xC0,
        [DescriptionAttribute("DeleteFile")]
        DeleteFile = (byte)0xDF,

        [DescriptionAttribute("Read Data")]
        ReadData = (byte)0xBD,
        [DescriptionAttribute("Write Data")]
        WriteData = (byte)0x3D,
        [DescriptionAttribute("Get Value")]
        GetValue = (byte)0x6C,
        [DescriptionAttribute("Credit")]
        Credit = (byte)0x0C,
        [DescriptionAttribute("Debit")]
        Debit = (byte)0xDC,
        [DescriptionAttribute("Limited Credit")]
        LimitedCredit = (byte)0x1C,
        [DescriptionAttribute("Write Record")]
        WriteRecord = (byte)0x3B,
        [DescriptionAttribute("Read Records")]
        ReadRecords = (byte)0xBB,
        [DescriptionAttribute("Clear RecordFile")]
        ClearRecordFile = (byte)0xEB,
        [DescriptionAttribute("Commit Transaction")]
        CommitTransaction = (byte)0xC7,
        [DescriptionAttribute("Abort Transaction")]
        AbortTransaction = (byte)0xA7
    }

    //endum of command response
    public enum DesFireSw1Sw2 : byte
    {
        [DescriptionAttribute("OPERATION_OK: Successful operation")]
        OPERATION_OK = 0x00,
        [DescriptionAttribute("NO_CHANGES: No changes done to backup files, CommitTransaction / AbortTransaction not necessary")]
        NO_CHANGES = 0x0C,
        [DescriptionAttribute("OUT_OF_EEPROM_ERROR: Insufficient NV-Memory to complete command")]
        OUT_OF_EEPROM_ERROR = 0x0E,
        [DescriptionAttribute("ILLEGAL_COMMAND_CODE: Command code not supported")]
        ILLEGAL_COMMAND_CODE = 0x1C,
        [DescriptionAttribute("INTEGRITY_ERROR: CRC or MAC does not match data or Padding bytes not valid")]
        INTEGRITY_ERROR = 0x1E,
        [DescriptionAttribute("NO_SUCH_KEY: Invalid key number specified")]
        NO_SUCH_KEY = 0x40,
        [DescriptionAttribute("LENGTH_ERROR: Length of command string invalid")]
        LENGTH_ERROR = 0x7E,
        [DescriptionAttribute("PERMISSION_DENIED: Current configuration / status does not allow the requested command")]
        PERMISSION_DENIED = 0x9D,
        [DescriptionAttribute("PARAMETER_ERROR: Value of the parameter(s) invalid")]
        PARAMETER_ERROR = 0x9E,
        [DescriptionAttribute("APPLICATION_NOT_FOUND: Requested AID not present on PICC")]
        APPLICATION_NOT_FOUND = 0xA0,
        [DescriptionAttribute("APPL_INTEGRITY_ERROR: Unrecoverable error within application, application will be disabled ")]
        APPL_INTEGRITY_ERROR = 0xA1,
        [DescriptionAttribute("AUTHENTICATION_ERROR:  Current authentication status does not allow the requested command")]
        AUTHENTICATION_ERROR = 0xAE,
        [DescriptionAttribute("ADDITIONAL_FRAME: Additional data frame is expected to be sent")]
        ADDITIONAL_FRAME = 0xAF,
        [DescriptionAttribute("BOUNDARY_ERROR: Attempt to read/write data from/to beyond the file's/record's limits.Attempt to exceed the limits of a value file")]
        BOUNDARY_ERROR = 0xBE,
        [DescriptionAttribute("PICC_INTEGRITY_ERROR: Unrecoverable error within PICC, PICC will be disabled *")]
        PICC_INTEGRITY_ERROR = 0xC1,
        [DescriptionAttribute("COMMAND_ABORTED: Previous Command was not fully completed. Not all Frames were requested or provided by the PCD")]
        COMMAND_ABORTED = 0xCA,
        [DescriptionAttribute("PICC_DISABLED_ERROR: PICC was disabled by an unrecoverable error *")]
        PICC_DISABLED_ERROR = 0xCD,
        [DescriptionAttribute("COUNT_ERROR:  Number of Applications limited to 28, no additional CreateApplication possible")]
        COUNT_ERROR = 0xCE,
        [DescriptionAttribute("DUPLICATE_ERROR: Creation of file/application failed because file/application with same number already exists")]
        DUPLICATE_ERROR = 0xDE,
        [DescriptionAttribute("EEPROM_ERROR: Could not complete NV-write operation due to loss of power, internal backup/rollback mechanism activated*")]
        EEPROM_ERROR = 0xEE,
        [DescriptionAttribute("FILE_NOT_FOUND: Specified file number does not exist")]
        FILE_NOT_FOUND = 0xF0,
        [DescriptionAttribute("FILE_INTEGRITY_ERROR: Unrecoverable error within file, file will be disabled *")]
        FILE_INTEGRITY_ERROR = 0xF1,
        [DescriptionAttribute("SOFTWARE ERROR: Check error message")]
        INTERNAL_SOFTWARE_ERROR = 0xFF

    }

    public struct DesFireWrappeResponse
    {
        public string apdu_message;
        public string data;
        public string sw1sw2;
        public string err_msg_pcsc;
    }

}
