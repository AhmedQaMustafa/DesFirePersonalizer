using DesFireWrapperLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DesFirePersonalizer
{
    class Student
    {
        DesFireWrapperLib.DesFireWrapper dsf = null;

        public static string PURCHASE_TRANSACTION = "DE";
        public static string TOPUP_TRANSACTION = "CC";

        public static string BORROW_TRANSACTION = "BB";
        public static string RETURN_TRANSACTION = "EE";

        public static string PERSONAL_DATA = "PersonalData";
        public static string UNIVERSITY_DATA = "UniversityData";
        public static string PHOTO_DATA = "PhotoData";
        public static string FINGERPRINT_DATA = "FingerPrintData";
        public static string CREDITS_DATA = "CreditsValue";
        public static string HISTORY_DATA = "PurchaseHistory";
        public static string LIBRARY_DATA = "LibraryCreditValue";
        public static string COUNTER_DATA = "CounterValue";

        public static int OPERATION_INCREMENT = 0x99;
        public static int OPERATION_DECREMENT = 0xAA;

        public static String BOOKED = "BOOKED";
        public static String COMPLETED = "COMPLETED";

        public static int LIB_LOG_LENGTH = 45 * 2;

        //picc information
        public static String PICC_AID = "000000";
        public static String PICC_NO_OFF_KEYS = "1";
        public static String PICC_KEY_0 = "00";

        String picc_key_settings { get; set; }
        String master_key_picc { get; set; }
        DesFireWrapperLib.DesFireWrapper.KeyType picc_key_type;
        String picc_session_key;

        /* Key use in al virgin state device */
        String virgin_key { get; set; }

        FileInfo xmlTemplate;

        //A00001 Application
        public const String GENERIC_APP_AID = "A00001";
        private DesfireApplication genericApp { get; set; }

        //B00001 Application
        public const String CREDIT_APP_AID = "B00001";
        private DesfireApplication creditApp { get; set; }

        public static String PURSE_VALUE_FILE_ID = "00";
        public static String DEBIT_PURSE_LOG_FILE_ID = "01";
        public static String CREDIT_PURSE_LOG_FILE_ID = "02";

        //C00001 Application
        public const String LIBRARY_APP_AID = "C00001";
        private DesfireApplication libraryApp { get; set; }

        public static String LIBRARY_VALUE_FILE_ID = "00";
        public static String BOOK1_LOG_FILE_ID = "01";
        public static String BOOK2_LOG_FILE_ID = "02";
        public static String Std_LOG_FILE_ID = "03";

        //D00001 Application
        public const String COUNTER_APP_AID = "D00001";
        private DesfireApplication counterApp { get; set; }
        public static String COUNTER_FILE_ID = "00";

        String newFile;
        String PersonalData;
        String UniversityData;
        String ImageData;
        String FingerPrintData;
        String InitialCreditValue;
        String InitialBookValue;
        String InitialCounterValue;
        String xmlFileTemplate;
        bool isFormatFirst;

        public Student(DesFireWrapperLib.DesFireWrapper _dsf, String xmlFileTemplate, String newFile)
        {
            this.dsf = _dsf;

            xmlTemplate = new FileInfo(xmlFileTemplate);

            if (xmlTemplate.Exists == false)
                throw new FileNotFoundException();

            //save the data locally
            this.xmlFileTemplate = xmlFileTemplate;
            this.newFile = newFile;
        }

        /// <summary>
        //////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////
        // i changed this method to allow us to use without picture and finger print 
        public void fillStudentData1(String PersonalData, String UniversityData,  String initial_credit, String initial_book_value, String initial_counter_value, bool isFormatFirst)
        {
            this.PersonalData = PersonalData;
            this.UniversityData = UniversityData;
           // this.ImageData = ImageData;
           // this.FingerPrintData = FingerPrintData;
            this.InitialCreditValue = initial_credit;
            this.isFormatFirst = isFormatFirst;
            this.InitialBookValue = initial_book_value;
            this.InitialCounterValue = initial_counter_value;
        }
        //////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////
        public void fillStudentData(String PersonalData, String UniversityData, String ImageData,
                   String FingerPrintData, String initial_credit, String initial_book_value, String initial_counter_value, bool isFormatFirst)//
        {
            this.PersonalData = PersonalData;
            this.UniversityData = UniversityData;
            this.ImageData = ImageData;
            this.FingerPrintData = FingerPrintData;
            this.InitialCreditValue = initial_credit;
            this.isFormatFirst = isFormatFirst;
            this.InitialBookValue = initial_book_value;
            this.InitialCounterValue = initial_counter_value;
        }

        public void LoadXml(bool fillData)
        {
            loadXmlFile(this.xmlFileTemplate, fillData);
        }


        //load the xml File
        private void loadXmlFile(String xmlFilefile, bool doFilldata)
        {
            XmlDocument myXmlDocument = new XmlDocument();
            myXmlDocument.Load(xmlFilefile);

            // get the PICC element
            validateAndParsePiccMasterNode(myXmlDocument, "/picc");

            // get the application list             
            XmlNodeList nodeList = myXmlDocument.SelectNodes("/picc/applications/application");

            foreach (XmlNode a_node in nodeList)
            {
                //App ID A00001
                if (a_node.Attributes["id"].Value == GENERIC_APP_AID)
                {
                    //instantiate the application
                    genericApp = new DesfireApplication(dsf, GENERIC_APP_AID);

                    //check if all settings are available
                    if (a_node.Attributes["id"].Value == null || a_node.Attributes["no_of_keys"].Value == null ||
                        a_node.Attributes["settings"].Value == null || a_node.Attributes["key_type"].Value == null
                        || a_node.Attributes["key_xml_file"].Value == null)
                    {
                        throw new AppSettingsNotFoundException("One of Application Attribute not found");
                    }

                    //set the key type
                    DesFireWrapperLib.DesFireWrapper.KeyType _key_type;
                    Enum.TryParse(a_node.Attributes["key_type"].Value, out _key_type);
                    genericApp.key_type = _key_type;

                    //set the key settings
                    genericApp.key_settings = a_node.Attributes["settings"].Value;

                    //set the key no
                    genericApp.max_no_of_key = a_node.Attributes["no_of_keys"].Value;

                    //parse and save the key to object
                    genericApp.populateKeys(a_node.Attributes["key_xml_file"].Value);

                    XmlNodeList x = a_node.FirstChild.ChildNodes;

                    foreach (XmlNode fNode in x)
                    {
                        if (String.Equals(fNode.Name, "file", StringComparison.OrdinalIgnoreCase))
                        {
                            //check if all settings are available
                            if (fNode.Attributes["id"].Value == null || fNode.Attributes["type"].Value == null ||
                                fNode.Attributes["access_rights"].Value == null || fNode.Attributes["comm_settings"].Value == null ||
                                fNode.Attributes["max_size"].Value == null || fNode.Attributes["name"].Value == null)
                            {
                                throw new FileAttributesNotFoundException("One of File Attribute not found");
                            }

                            BasicFile bf = createFile(fNode, fNode.Attributes["name"].Value);
                            bf.fileid = fNode.Attributes["id"].Value;
                            bf.filetype = convertXmlFileTypeToStr(fNode.Attributes["type"].Value);
                            bf.access_rights = fNode.Attributes["access_rights"].Value;
                            bf.communication_settings = convertXmlCommSettingsTypeToStr(fNode.Attributes["comm_settings"].Value);

                            if (doFilldata)
                            {
                                compareFileNameAndInsertContent(fNode.Attributes["name"].Value, PERSONAL_DATA, PersonalData, bf);
                                compareFileNameAndInsertContent(fNode.Attributes["name"].Value, PHOTO_DATA, ImageData, bf);
                                compareFileNameAndInsertContent(fNode.Attributes["name"].Value, FINGERPRINT_DATA, FingerPrintData, bf);
                                compareFileNameAndInsertContent(fNode.Attributes["name"].Value, UNIVERSITY_DATA, UniversityData, bf);
                            }

                            genericApp.addFile(bf);
                        }
                    }
                }
                //App ID B00001
                else if (a_node.Attributes["id"].Value == CREDIT_APP_AID)
                {
                    creditApp = new DesfireApplication(dsf, CREDIT_APP_AID);

                    //check if all settings are available
                    if (a_node.Attributes["id"].Value == null || a_node.Attributes["no_of_keys"].Value == null ||
                        a_node.Attributes["settings"].Value == null || a_node.Attributes["key_type"].Value == null
                        || a_node.Attributes["key_xml_file"].Value == null)
                    {
                        throw new AppSettingsNotFoundException("One of Application Attribute not found");
                    }

                    //set the key type
                    DesFireWrapperLib.DesFireWrapper.KeyType _key_type;
                    Enum.TryParse(a_node.Attributes["key_type"].Value, out _key_type);
                    creditApp.key_type = _key_type;

                    //set the key settings
                    creditApp.key_settings = a_node.Attributes["settings"].Value;

                    //set the key no
                    creditApp.max_no_of_key = a_node.Attributes["no_of_keys"].Value;

                    //parse and save the key to object
                    creditApp.populateKeys(a_node.Attributes["key_xml_file"].Value);

                    XmlNodeList x = a_node.FirstChild.ChildNodes;

                    foreach (XmlNode fNode in x)
                    {
                        if (String.Equals(fNode.Name, "file", StringComparison.OrdinalIgnoreCase))
                        {
                            //check if all settings are available
                            if (fNode.Attributes["id"].Value == null || fNode.Attributes["type"].Value == null ||
                                fNode.Attributes["access_rights"].Value == null || fNode.Attributes["comm_settings"].Value == null ||
                                fNode.Attributes["name"].Value == null)
                            {
                                throw new FileAttributesNotFoundException("One of File Attribute not found");
                            }

                            BasicFile bf = createFile(fNode, fNode.Attributes["name"].Value);

                            bf.fileid = fNode.Attributes["id"].Value;
                            bf.filetype = convertXmlFileTypeToStr(fNode.Attributes["type"].Value);
                            bf.access_rights = fNode.Attributes["access_rights"].Value;
                            bf.communication_settings = convertXmlCommSettingsTypeToStr(fNode.Attributes["comm_settings"].Value);

                            if (doFilldata)
                            {
                                compareFileNameAndInsertContent(fNode.Attributes["name"].Value, CREDITS_DATA, InitialCreditValue, bf);
                                compareFileNameAndInsertContent(fNode.Attributes["name"].Value, HISTORY_DATA, ImageData, bf);
                            }

                            creditApp.addFile(bf);
                        }
                    }
                }
                //App ID C00001
                else if (a_node.Attributes["id"].Value == LIBRARY_APP_AID)
                {
                    libraryApp = new DesfireApplication(dsf, LIBRARY_APP_AID);

                    //check if all settings are available
                    if (a_node.Attributes["id"].Value == null || a_node.Attributes["no_of_keys"].Value == null ||
                        a_node.Attributes["settings"].Value == null || a_node.Attributes["key_type"].Value == null
                        || a_node.Attributes["key_xml_file"].Value == null)
                    {
                        throw new AppSettingsNotFoundException("One of Application Attribute not found");
                    }

                    //set the key type
                    DesFireWrapperLib.DesFireWrapper.KeyType _key_type;
                    Enum.TryParse(a_node.Attributes["key_type"].Value, out _key_type);
                    libraryApp.key_type = _key_type;

                    //set the key settings
                    libraryApp.key_settings = a_node.Attributes["settings"].Value;

                    //set the key no
                    libraryApp.max_no_of_key = a_node.Attributes["no_of_keys"].Value;

                    //parse and save the key to object
                    libraryApp.populateKeys(a_node.Attributes["key_xml_file"].Value);

                    XmlNodeList x = a_node.FirstChild.ChildNodes;

                    foreach (XmlNode fNode in x)
                    {
                        if (String.Equals(fNode.Name, "file", StringComparison.OrdinalIgnoreCase))
                        {
                            //check if all settings are available
                            if (fNode.Attributes["id"].Value == null || fNode.Attributes["type"].Value == null ||
                                fNode.Attributes["access_rights"].Value == null || fNode.Attributes["comm_settings"].Value == null ||
                                fNode.Attributes["name"].Value == null)
                            {
                                throw new FileAttributesNotFoundException("One of File Attribute not found");
                            }

                            BasicFile bf = createFile(fNode, fNode.Attributes["name"].Value);

                            bf.fileid = fNode.Attributes["id"].Value;
                            bf.filetype = convertXmlFileTypeToStr(fNode.Attributes["type"].Value);
                            bf.access_rights = fNode.Attributes["access_rights"].Value;
                            bf.communication_settings = convertXmlCommSettingsTypeToStr(fNode.Attributes["comm_settings"].Value);

                            if (doFilldata)
                            {
                                compareFileNameAndInsertContent(fNode.Attributes["name"].Value, LIBRARY_DATA, InitialBookValue, bf);
                            }

                            libraryApp.addFile(bf);
                        }
                    }
                }
                //App ID D00001
                else if (a_node.Attributes["id"].Value == COUNTER_APP_AID)
                {
                    counterApp = new DesfireApplication(dsf, COUNTER_APP_AID);

                    //check if all settings are available
                    if (a_node.Attributes["id"].Value == null || a_node.Attributes["no_of_keys"].Value == null ||
                        a_node.Attributes["settings"].Value == null || a_node.Attributes["key_type"].Value == null
                        || a_node.Attributes["key_xml_file"].Value == null)
                    {
                        throw new AppSettingsNotFoundException("One of Application Attribute not found");
                    }

                    //set the key type
                    DesFireWrapperLib.DesFireWrapper.KeyType _key_type;
                    Enum.TryParse(a_node.Attributes["key_type"].Value, out _key_type);
                    counterApp.key_type = _key_type;

                    //set the key settings
                    counterApp.key_settings = a_node.Attributes["settings"].Value;

                    //set the key no
                    counterApp.max_no_of_key = a_node.Attributes["no_of_keys"].Value;

                    //parse and save the key to object
                    counterApp.populateKeys(a_node.Attributes["key_xml_file"].Value);

                    XmlNodeList x = a_node.FirstChild.ChildNodes;

                    foreach (XmlNode fNode in x)
                    {
                        if (String.Equals(fNode.Name, "file", StringComparison.OrdinalIgnoreCase))
                        {
                            //check if all settings are available
                            if (fNode.Attributes["id"].Value == null || fNode.Attributes["type"].Value == null ||
                                fNode.Attributes["access_rights"].Value == null || fNode.Attributes["comm_settings"].Value == null ||
                                fNode.Attributes["name"].Value == null)
                            {
                                throw new FileAttributesNotFoundException("One of File Attribute not found");
                            }

                            BasicFile bf = createFile(fNode, fNode.Attributes["name"].Value);

                            bf.fileid = fNode.Attributes["id"].Value;
                            bf.filetype = convertXmlFileTypeToStr(fNode.Attributes["type"].Value);
                            bf.access_rights = fNode.Attributes["access_rights"].Value;
                            bf.communication_settings = convertXmlCommSettingsTypeToStr(fNode.Attributes["comm_settings"].Value);

                            if (doFilldata)
                            {
                                compareFileNameAndInsertContent(fNode.Attributes["name"].Value, COUNTER_DATA, InitialCounterValue, bf);
                            }

                            counterApp.addFile(bf);
                        }
                    }
                }
            }
        }

        private void compareFileNameAndInsertContent(String filename_ref, String file, String Content, BasicFile bf)
        {
            if (String.Equals(filename_ref, file, StringComparison.OrdinalIgnoreCase))
            {
                if (bf.isStandardFile())
                {
                    ((StandardFile)bf).content = Content;

                    if (Content == null || Content == string.Empty)
                        return;

                    int dec = Convert.ToInt32(((StandardFile)bf).file_size_dec_max_string, 10);

                    int tmp = Content.Length / 2;
                    if (Content.Length / 2 > dec)
                        throw new FileSizeException("File Content Exceed the maximum allowed size");

                    int content_size = Content.Length / 2;
                    ((StandardFile)bf).file_size_hex_content_length = content_size.ToString("X6");

                }
                else if (bf.isRecordFile())
                {
                    ((RecordFile)bf).content = Content;
                }
                else if (bf.isValueFile())
                {
                    ((ValueFile)bf).value_hex = Content;
                }
                else
                    throw new RuntimeErrorException("File not detected!");
            }

        }

        //validate and parse the element <picc>
        private void validateAndParsePiccMasterNode(XmlDocument myXmlDocument, string elementPicc)
        {
            XmlNode node = myXmlDocument.SelectSingleNode(elementPicc);

            if (node.Attributes["id"].Value == null || node.Attributes["no_of_keys"].Value == null ||
                node.Attributes["settings"].Value == null || node.Attributes["key_type"].Value == null
                || node.Attributes["key_xml_file"].Value == null)
            {
                throw new PiccSettingsNotFoundException("One of PICC Attribute not found");
            }

            if (node.Attributes["id"].Value != PICC_AID)
            {
                throw new WrongPiccSettingException("AID is not " + PICC_AID);
            }

            if (node.Attributes["no_of_keys"].Value != PICC_NO_OFF_KEYS)
            {
                throw new WrongPiccSettingException("PICC Key must be only " + PICC_NO_OFF_KEYS);
            }

            picc_key_settings = node.Attributes["settings"].Value;
            Enum.TryParse(node.Attributes["key_type"].Value, out picc_key_type);

            //populate key
            String key_file = node.Attributes["key_xml_file"].Value;
            String keyLiteral;
            FileInfo keyFileInfo = new FileInfo(key_file);

            if (keyFileInfo.Exists == false)
                throw new FileNotFoundException("Cannot find file: " + key_file);

            XmlDocument myKeyXmlDoc = new XmlDocument();
            myKeyXmlDoc.Load(key_file);

            keyLiteral = "/Keys/Key0";

            node = myKeyXmlDoc.SelectSingleNode(keyLiteral);
            if (String.IsNullOrWhiteSpace(node.InnerText))
                throw new StringNullOrEmptyException("Key " + keyLiteral + " is empty or null");

            master_key_picc = node.InnerText;


            keyLiteral = "/Keys/VirginKey";

            node = myKeyXmlDoc.SelectSingleNode(keyLiteral);
            if (String.IsNullOrWhiteSpace(node.InnerText))
                throw new StringNullOrEmptyException("Key " + keyLiteral + " is empty or null");

            virgin_key = node.InnerText;

        }

        ////internal void fillStudentData(string v11, string v22, string v33, string v44, string v55, bool doFormat)
        ////{
        ////    throw new NotImplementedException();
        ////}

        //internal void fillStudentData(string v1, string v2, string v3, string v4, string v5, bool doFormat)
        //{
        //    throw new NotImplementedException();
        //}
        ////internal void fillStudentData(string v1, string v2, string v3, string v4, string v5, bool doFormat)
        ////{
        ////    throw new NotImplementedException();
        ////}
        //Create file based on the filetype on the xml file
        private BasicFile createFile(XmlNode fNode, String filename)
        {
            BasicFile bf = new BasicFile();

            bf.filename = filename;
            bf.fileid = fNode.Attributes["id"].Value;
            bf.access_rights = (fNode.Attributes["access_rights"].Value);
            bf.communication_settings = convertXmlCommSettingsTypeToStr((fNode.Attributes["comm_settings"].Value));

            String filetype = fNode.Attributes["type"].Value;

            if (convertXmlFileTypeToStr(filetype) == "00") //if filetype = "standard"
            {
                StandardFile sf = new StandardFile(bf);

                if (String.IsNullOrWhiteSpace(fNode.Attributes["max_size"].Value))
                    throw new RuntimeErrorException(fNode.Attributes["max_size"].Value + " is Null or empty");

                sf.file_size_dec_max_string = fNode.Attributes["max_size"].Value;

                return sf;
            }
            else if (convertXmlFileTypeToStr(filetype) == "01") //if filetype = "standard1"
            {
                StandardFile sf = new StandardFile(bf);
                sf.file_size_dec_max_string = fNode.Attributes["max_size"].Value;

                return sf;
            }
            else if (convertXmlFileTypeToStr(filetype) == "02") //if filetype = "value"
            {
                ValueFile vf = new ValueFile(bf);

                if (String.IsNullOrWhiteSpace(fNode.Attributes["lower_limit"].Value))
                    throw new RuntimeErrorException(fNode.Attributes["lower_limit"].Value + " is Null or empty");
                if (String.IsNullOrWhiteSpace(fNode.Attributes["upper_limit"].Value))
                    throw new RuntimeErrorException(fNode.Attributes["upper_limit"].Value + " is Null or empty");
                if (String.IsNullOrWhiteSpace(fNode.Attributes["limited_credit_enabled"].Value))
                    throw new RuntimeErrorException(fNode.Attributes["limited_credit_enabled"].Value + " is Null or empty");

                vf.lower_limit_hex = fNode.Attributes["lower_limit"].Value;
                vf.upper_limit_hex = fNode.Attributes["upper_limit"].Value;
                vf.limited_credit_enabled_hex = fNode.Attributes["limited_credit_enabled"].Value;

                return vf;
            }
            else if (convertXmlFileTypeToStr(filetype) == "03") //if filetype = "record_linear"
            {
                RecordFile rf = new RecordFile(bf);

                rf.isLinearRecord = true;

                if (String.IsNullOrWhiteSpace(fNode.Attributes["rec_size"].Value))
                    throw new RuntimeErrorException(fNode.Attributes["rec_size"].Value + " is Null or empty");
                if (String.IsNullOrWhiteSpace(fNode.Attributes["no_records"].Value))
                    throw new RuntimeErrorException(fNode.Attributes["no_records"].Value + " is Null or empty");

                rf.record_size = fNode.Attributes["rec_size"].Value;
                rf.maxNoOfRecords = fNode.Attributes["no_records"].Value;

                return rf;
            }
            else if (convertXmlFileTypeToStr(filetype) == "04") //if filetype = "record_cyclic"
            {
                RecordFile rf = new RecordFile(bf);

                rf.isLinearRecord = false;

                if (String.IsNullOrWhiteSpace(fNode.Attributes["rec_size"].Value))
                    throw new RuntimeErrorException(fNode.Attributes["rec_size"].Value + " is Null or empty");
                if (String.IsNullOrWhiteSpace(fNode.Attributes["no_records"].Value))
                    throw new RuntimeErrorException(fNode.Attributes["no_records"].Value + " is Null or empty");

                rf.record_size = fNode.Attributes["rec_size"].Value;
                rf.maxNoOfRecords = fNode.Attributes["no_records"].Value;

                return rf;
            }

            return null;
        }

        //Convert from text in xml to the verbatim library file type
        private String convertXmlFileTypeToStr(String xml_file_type)
        {
            if (String.Equals(xml_file_type, "standard", StringComparison.OrdinalIgnoreCase))
            {
                return "00";
            }
            else if (String.Equals(xml_file_type, "standard1", StringComparison.OrdinalIgnoreCase))
            {
                return "01";
            }
            else if (String.Equals(xml_file_type, "value", StringComparison.OrdinalIgnoreCase))
            {
                return "02";
            }
            else if (String.Equals(xml_file_type, "record_linear", StringComparison.OrdinalIgnoreCase))
            {
                return "03";
            }
            else if (String.Equals(xml_file_type, "record_cyclic", StringComparison.OrdinalIgnoreCase))
            {
                return "04";
            }
            else
                throw new WrongFileAttributesException("File " + xml_file_type + " type not recognized");
        }

        //Convert from text "comm_settings: PLAIN, MACED, ENCHIPERED" in xml to the verbatim library file type ("00", "01", 03")
        private String convertXmlCommSettingsTypeToStr(String xml_comm_settings)
        {
            if (String.Equals(xml_comm_settings, "PLAIN", StringComparison.OrdinalIgnoreCase))
            {
                return "00";
            }
            else if (String.Equals(xml_comm_settings, "MACED", StringComparison.OrdinalIgnoreCase))
            {
                return "01";
            }
            else if (String.Equals(xml_comm_settings, "ENCIPHERED", StringComparison.OrdinalIgnoreCase))
            {
                return "03";
            }
            else
                throw new WrongFileAttributesException("comm_settings: " + xml_comm_settings + " type not recognized");
        }


        public DesFireWrappeResponse readFile(ref BasicFile bf)
        {
            DesFireWrappeResponse dfw_resp = new DesFireWrappeResponse();

            int fixed_len = 250;
            string total = "";

            String header = "";
            String lenStr = "";

            dsf.getFileCommSettPublic(bf.fileid, true, false, true, false, ref dfw_resp);
            LogWrapper.Debug(dfw_resp);

            if (!String.Equals(bf.getReadKeyNo(), "0E", StringComparison.OrdinalIgnoreCase) && !String.Equals(bf.getReadKeyNo(), "0F", StringComparison.OrdinalIgnoreCase))
            {
                //authenticate with reading key
                dfw_resp = dsf.authenticate(getKeyBasedOnLength(genericApp.app_key[bf.getReadKeyNoInt()], genericApp.key_type),
                    bf.getReadKeyNo(), genericApp.key_type, out picc_session_key);
                LogWrapper.Debug(dfw_resp);
            }

            if (bf.isStandardFile())
            {
                StandardFile sf = (StandardFile)bf;

                //if an encrypted file
                if (bf.getCommunicationSetting() == DesFireWrapper.CommunicationSetting.ENCIPHERED || bf.getCommunicationSetting() == DesFireWrapper.CommunicationSetting.MACED)
                {
                    dfw_resp = dsf.readStandardFileDirect(bf.fileid, bf.getCommunicationSetting(), "000000", "000000", ref total); //read header
                    LogWrapper.Debug(dfw_resp);

                    header = total.Substring(0, 4);
                    lenStr = total.Substring(4, 4);
                }
                else
                {
                    dfw_resp = dsf.readStandardFileDirect(sf.fileid, bf.getCommunicationSetting(), "000000", "000004", ref total); //read header
                    LogWrapper.Debug(dfw_resp);

                    header = total.Substring(0, 4);
                    lenStr = total.Substring(4, 4);

                    int len = Convert.ToInt32(lenStr, 16);
                    if (len < 246)
                    {
                        dfw_resp = dsf.readStandardFileDirect(sf.fileid, bf.getCommunicationSetting(), "000000", "000000", ref total); //read header
                        LogWrapper.Debug(dfw_resp);
                    }
                    else //repeat reading
                    {
                        total = "";
                        int loop = len / fixed_len;
                        int remainder = len % fixed_len;

                        string tmp = "";
                        int start = 0;
                        for (int i = 0; i < loop; i++)
                        {
                            start = i * fixed_len;
                            dfw_resp = dsf.readStandardFileDirect(sf.fileid, bf.getCommunicationSetting(), start.ToString("X6"),
                                fixed_len.ToString("X6"), ref tmp); //read header
                            LogWrapper.Debug(dfw_resp);
                            total += tmp;
                        }

                        start = loop * fixed_len;
                        if (remainder > 0)
                        {
                            dfw_resp = dsf.readStandardFileDirect(sf.fileid, bf.getCommunicationSetting(), start.ToString("X6"), "000000", ref tmp); //read header
                            LogWrapper.Debug(dfw_resp);
                        }

                        total += tmp;
                    }
                }

                sf.content = total.Substring(0, (Convert.ToInt32(lenStr, 16) * 2) + 8);

            }
            else if (bf.isValueFile())
            {
                ValueFile vf = (ValueFile)bf;

                String val = "";
                dfw_resp = dsf.getValueDirect(vf.fileid, vf.getCommunicationSetting(), ref val);
                LogWrapper.Debug(dfw_resp);

                vf.value_hex = val;

            }
            else if (bf.isRecordFile())
            {
                RecordFile rf = (RecordFile)bf;

                String content = "";
                dfw_resp = dsf.readRecordFile(bf.fileid, "000000", "000000", ref content);
                LogWrapper.Debug(dfw_resp);

                rf.content = content;
            }

            return dfw_resp;
        }

        public String getContentFromAFile(string filename)
        {
            if (genericApp != null)
                foreach (BasicFile bf in genericApp.fileList)
                {
                    if (String.Equals(bf.filename, filename, StringComparison.OrdinalIgnoreCase))
                    {
                        if (bf.isStandardFile())
                        {
                            return ((StandardFile)bf).content;
                        }
                        else if (bf.isRecordFile())
                        {
                            return ((RecordFile)bf).content;
                        }
                    }
                }

            if (creditApp != null)
                foreach (BasicFile bf in creditApp.fileList)
                {
                    if (String.Equals(bf.filename, filename, StringComparison.OrdinalIgnoreCase))
                    {
                        if (bf.isStandardFile())
                        {
                            return ((StandardFile)bf).content;
                        }
                        else if (bf.isRecordFile())
                        {
                            return ((RecordFile)bf).content;
                        }
                    }
                }

            if (libraryApp != null)
                foreach (BasicFile bf in libraryApp.fileList)
                {
                    if (String.Equals(bf.filename, filename, StringComparison.OrdinalIgnoreCase))
                    {
                        if (bf.isStandardFile())
                        {
                            return ((StandardFile)bf).content;
                        }
                        else if (bf.isRecordFile())
                        {
                            return ((RecordFile)bf).content;
                        }
                    }
                }

            if (counterApp != null)
                foreach (BasicFile bf in counterApp.fileList)
                {
                    if (String.Equals(bf.filename, filename, StringComparison.OrdinalIgnoreCase))
                    {
                        if (bf.isStandardFile())
                        {
                            return ((StandardFile)bf).content;
                        }
                        else if (bf.isRecordFile())
                        {
                            return ((RecordFile)bf).content;
                        }
                    }
                }

            return null;
        }

        public void readAStudentCard()
        {
            DesFireWrappeResponse dfw_resp;

            //select picc
            dfw_resp = dsf.SelectApplication(PICC_AID);
            LogWrapper.Debug(dfw_resp);

            //select generic app
            dfw_resp = dsf.SelectApplication(genericApp.application_aid);
            LogWrapper.Debug(dfw_resp);

            //read files
            foreach (BasicFile bf in genericApp.fileList)
            {
                BasicFile basFile = bf;

                dfw_resp = readFile(ref basFile);
                LogWrapper.Debug(dfw_resp);
            }
        }



        public void WriteToCard()
        {
            DesFireWrappeResponse dfw_resp;

            if (isFormatFirst)
            {
                //select picc
                dfw_resp = dsf.SelectApplication(PICC_AID);
                LogWrapper.Debug(dfw_resp);

                //authenticate first with picc
                dfw_resp = dsf.authenticate(getKeyBasedOnLength(virgin_key, picc_key_type), PICC_KEY_0, picc_key_type, out picc_session_key);
                LogWrapper.Debug(dfw_resp);

                //select picc
                dfw_resp = dsf.formatCard();
                LogWrapper.Debug(dfw_resp);
            }

            /*
                Create Applications and files
            */

            if (genericApp != null)
            {
                //select picc
                dfw_resp = dsf.SelectApplication(PICC_AID);
                LogWrapper.Debug(dfw_resp);

                //authenticate first with picc
                dfw_resp = dsf.authenticate(getKeyBasedOnLength(virgin_key, picc_key_type), PICC_KEY_0, picc_key_type, out picc_session_key);
                LogWrapper.Debug(dfw_resp);

                ////create data application
                dfw_resp = dsf.createApplication(genericApp.application_aid, genericApp.key_settings, genericApp.max_no_of_key);
                LogWrapper.Debug(dfw_resp);

                //select generic app
                dfw_resp = dsf.SelectApplication(genericApp.application_aid);
                LogWrapper.Debug(dfw_resp);

                //create files
                foreach (BasicFile bf in genericApp.fileList)
                {
                    dfw_resp = dsf.createFile(bf);
                    LogWrapper.Debug(dfw_resp);
                }

                /*
                 Change Key
                */
                ChangeApplicationKey(genericApp);
            }


            if (creditApp != null)
            {
                //select picc
                dfw_resp = dsf.SelectApplication(PICC_AID);
                LogWrapper.Debug(dfw_resp);

                //authenticate first with picc
                dfw_resp = dsf.authenticate(getKeyBasedOnLength(virgin_key, picc_key_type), PICC_KEY_0, picc_key_type, out picc_session_key);
                LogWrapper.Debug(dfw_resp);

                //create data application
                dfw_resp = dsf.createApplication(creditApp.application_aid, creditApp.key_settings, creditApp.max_no_of_key);
                LogWrapper.Debug(dfw_resp);

                //select generic app
                dfw_resp = dsf.SelectApplication(creditApp.application_aid);
                LogWrapper.Debug(dfw_resp);

                //create files
                foreach (BasicFile bf in creditApp.fileList)
                {
                    dfw_resp = dsf.createFile(bf);
                    LogWrapper.Debug(dfw_resp);
                }

                /*
                  Change Key
                */
                ChangeApplicationKey(creditApp);
            }

            if (libraryApp != null)
            {

                //select picc
                dfw_resp = dsf.SelectApplication(PICC_AID);
                LogWrapper.Debug(dfw_resp);

                //authenticate first with picc
                dfw_resp = dsf.authenticate(getKeyBasedOnLength(virgin_key, picc_key_type), PICC_KEY_0, picc_key_type, out picc_session_key);
                LogWrapper.Debug(dfw_resp);

                //create data application
                dfw_resp = dsf.createApplication(libraryApp.application_aid, libraryApp.key_settings, libraryApp.max_no_of_key);
                LogWrapper.Debug(dfw_resp);

                //select generic app
                dfw_resp = dsf.SelectApplication(libraryApp.application_aid);
                LogWrapper.Debug(dfw_resp);

                //create files
                foreach (BasicFile bf in libraryApp.fileList)
                {
                    dfw_resp = dsf.createFile(bf);
                    LogWrapper.Debug(dfw_resp);
                }

                /*
                  Change Key
                */
                ChangeApplicationKey(libraryApp);
            }

            if (counterApp != null)
            {
                //select picc
                dfw_resp = dsf.SelectApplication(PICC_AID);
                LogWrapper.Debug(dfw_resp);

                //authenticate first with picc
                dfw_resp = dsf.authenticate(getKeyBasedOnLength(virgin_key, picc_key_type), PICC_KEY_0, picc_key_type, out picc_session_key);
                LogWrapper.Debug(dfw_resp);

                //create data application
                dfw_resp = dsf.createApplication(counterApp.application_aid, counterApp.key_settings, counterApp.max_no_of_key);
                LogWrapper.Debug(dfw_resp);

                //select generic app
                dfw_resp = dsf.SelectApplication(counterApp.application_aid);
                LogWrapper.Debug(dfw_resp);

                //create files
                foreach (BasicFile bf in counterApp.fileList)
                {
                    dfw_resp = dsf.createFile(bf);
                    LogWrapper.Debug(dfw_resp);
                }

                /*
                  Change Key
                */
                ChangeApplicationKey(counterApp);
            }


            /*
                Write data to application
            */
            foreach (BasicFile bf in genericApp.fileList)
            {
                writeDataAfterAuthenticate(genericApp, bf);
            }

        }


        private String getKeyBasedOnLength(String keyValue, DesFireWrapper.KeyType kt)
        {
            if (kt == DesFireWrapper.KeyType.TKTDES)
            {
                return keyValue.Substring(0, 48);
            }
            else if (kt == DesFireWrapper.KeyType.DES)
            {
                return keyValue.Substring(0, 16);
            }
            else
                return keyValue.Substring(0, 32);
        }

        private void ChangeApplicationKey(DesfireApplication app)
        {
            DesFireWrappeResponse dfw_resp = dsf.SelectApplication(app.application_aid);
            LogWrapper.Debug(dfw_resp);

            String keyNoDefault = "00";

            for (int i = 1; i < app.app_key.Length - 1; i++)
            {
                //authenticate with key 0
                dfw_resp = dsf.authenticate(getKeyBasedOnLength(virgin_key, app.key_type), keyNoDefault, app.key_type, out picc_session_key);
                LogWrapper.Debug(dfw_resp);

                string keyNo = i.ToString("X2"); ;
                string oldKeyVal = getKeyBasedOnLength(virgin_key, app.key_type);
                string newKeyVal = getKeyBasedOnLength(app.app_key[i], app.key_type);
                string newKeyVersion = "00";

                DesFireWrapper.KeyType old_key_type = (DesFireWrapper.KeyType)app.key_type;
                DesFireWrapper.KeyType new_key_type = (DesFireWrapper.KeyType)app.key_type;

                dfw_resp = dsf.changeKeyDesfire(keyNo, old_key_type, oldKeyVal, new_key_type, newKeyVersion, newKeyVal);
                LogWrapper.Debug(dfw_resp);
            }

            {
                //change master app key
                //authenticate with key 0
                dfw_resp = dsf.authenticate(virgin_key, keyNoDefault, app.key_type, out picc_session_key);
                LogWrapper.Debug(dfw_resp);

                string keyNo = "00"; ;
                string oldKeyVal = getKeyBasedOnLength(virgin_key, app.key_type);
                string newKeyVal = getKeyBasedOnLength(app.app_key[0], app.key_type);
                string newKeyVersion = "00";

                DesFireWrapper.KeyType old_key_type = (DesFireWrapper.KeyType)app.key_type;
                DesFireWrapper.KeyType new_key_type = (DesFireWrapper.KeyType)app.key_type;

                dfw_resp = dsf.changeKeyDesfire(keyNo, old_key_type, oldKeyVal, new_key_type, newKeyVersion, newKeyVal);
                LogWrapper.Debug(dfw_resp);
            }
        }

        private bool writeDataAfterAuthenticate(DesfireApplication app, BasicFile file)
        {
            bool result = false;

            //select application
            DesFireWrappeResponse dfw_resp = dsf.SelectApplication(app.application_aid);
            LogWrapper.Debug(dfw_resp);

            dsf.getFileCommSettPublic(file.fileid, true, false, false, true, ref dfw_resp);
            LogWrapper.Debug(dfw_resp);

            bool bool1 = String.Equals(file.getWriteKeyNo(), "0E", StringComparison.OrdinalIgnoreCase);
            bool bool2 = String.Equals(file.getWriteKeyNo(), "0F", StringComparison.OrdinalIgnoreCase);

            if (!bool1 && !bool2)
            {
                //authenticate with writing key
                dfw_resp = dsf.authenticate(getKeyBasedOnLength(app.app_key[file.getWriteKeyNoInt()], app.key_type),
                    file.getWriteKeyNo(), app.key_type, out picc_session_key);

                LogWrapper.Debug(dfw_resp);
            }

            if (file.isStandardFile())
            {
                StandardFile sf = (StandardFile)file;

                dfw_resp = dsf.writeStdFileDirect(file.fileid, file.getCommunicationSetting(), "000000", (sf.content.Length / 2).ToString("X6"), sf.content);
                LogWrapper.Debug(dfw_resp);
            }

            return result;
        }


        public ValueFile getCreditPurseAppValueFile()
        {
            DesFireWrappeResponse dfw_resp;

            BasicFile FileObj = null;

            //select picc
            dfw_resp = dsf.SelectApplication(PICC_AID);
            LogWrapper.Debug(dfw_resp);

            //select generic app
            dfw_resp = dsf.SelectApplication(creditApp.application_aid);
            LogWrapper.Debug(dfw_resp);

            //get value file settings
            BasicFile bf = creditApp.searchNodesByFileAid(PURSE_VALUE_FILE_ID);
            ValueFile vf_obj = (ValueFile)bf;

            dfw_resp = dsf.getFileSettings(bf.fileid, ref FileObj);
            LogWrapper.Debug(dfw_resp);

            ValueFile vf = (ValueFile)FileObj;
            vf_obj.lower_limit_hex = vf.lower_limit_hex;
            vf_obj.upper_limit_hex = vf.upper_limit_hex;
            vf_obj.limited_credit_value_hex = vf.limited_credit_value_hex;
            vf_obj.limited_credit_enabled_hex = vf.limited_credit_enabled_hex;

            BasicFile basfile = (BasicFile)vf;

            readFile(ref basfile);

            return (ValueFile)basfile;
        }



        public ValueFile getCounterValueFromFile()
        {
            DesFireWrappeResponse dfw_resp;

            BasicFile FileObj = null;

            //select picc
            dfw_resp = dsf.SelectApplication(PICC_AID);
            LogWrapper.Debug(dfw_resp);

            //select generic app
            dfw_resp = dsf.SelectApplication(counterApp.application_aid);
            LogWrapper.Debug(dfw_resp);

            //get value file settings
            BasicFile bf = counterApp.searchNodesByFileAid(COUNTER_FILE_ID);
            ValueFile vf_obj = (ValueFile)bf;

            dfw_resp = dsf.getFileSettings(bf.fileid, ref FileObj);
            LogWrapper.Debug(dfw_resp);

            ValueFile vf = (ValueFile)FileObj;
            vf_obj.lower_limit_hex = vf.lower_limit_hex;
            vf_obj.upper_limit_hex = vf.upper_limit_hex;
            vf_obj.limited_credit_value_hex = vf.limited_credit_value_hex;
            vf_obj.limited_credit_enabled_hex = vf.limited_credit_enabled_hex;

            BasicFile basfile = (BasicFile)vf;

            readFile(ref basfile);

            return (ValueFile)basfile;
        }


        public bool doIncreaseCounter()
        {
            bool success = false;
            bool isDone = false;

            DesFireWrappeResponse dfw_resp;
            BasicFile FileObj = null;

            //select picc
            dfw_resp = dsf.SelectApplication(PICC_AID);
            LogWrapper.Debug(dfw_resp);

            //select generic app
            dfw_resp = dsf.SelectApplication(counterApp.application_aid);
            LogWrapper.Debug(dfw_resp);

            BasicFile bf = counterApp.searchNodesByFileAid(COUNTER_FILE_ID);
            ValueFile vf_obj = (ValueFile)bf;

            dfw_resp = dsf.getFileSettings(bf.fileid, ref FileObj);
            LogWrapper.Debug(dfw_resp);

            ValueFile vf = (ValueFile)FileObj;
            vf_obj.lower_limit_hex = vf.lower_limit_hex;
            vf_obj.upper_limit_hex = vf.upper_limit_hex;
            vf_obj.limited_credit_value_hex = vf.limited_credit_value_hex;
            vf_obj.limited_credit_enabled_hex = vf.limited_credit_enabled_hex;

            //authenticate with read w key
            bool bool1 = String.Equals(bf.getReadWriteKeyNo(), "0E", StringComparison.OrdinalIgnoreCase);
            bool bool2 = String.Equals(bf.getReadWriteKeyNo(), "0F", StringComparison.OrdinalIgnoreCase);

            if (!bool1 && !bool2)
            {
                //authenticate with writing key
                dfw_resp = dsf.authenticate(getKeyBasedOnLength(counterApp.app_key[bf.getReadWriteKeyNoInt()], counterApp.key_type),
                    bf.getReadWriteKeyNo(), counterApp.key_type, out picc_session_key);

                LogWrapper.Debug(dfw_resp);
            }

            try
            {
                dfw_resp = dsf.doCreditDirect(COUNTER_FILE_ID, bf.getCommunicationSetting(), Convert.ToInt32("1", 10).ToString("X8"));
                LogWrapper.Debug(dfw_resp);

                isDone = true;

                dfw_resp = dsf.doCommitTransaction();
                LogWrapper.Debug(dfw_resp);

                success = true;
            }
            catch (Exception e)
            {
                success = false;

                if (isDone)
                {
                    dfw_resp = dsf.doAbortTransaction();
                    LogWrapper.Debug(dfw_resp);
                }
            }

            return success;
        }

        public bool doDecreaseCounter()
        {
            bool success = false;
            bool isDone = false;

            DesFireWrappeResponse dfw_resp;
            BasicFile FileObj = null;
            //select picc
            dfw_resp = dsf.SelectApplication(PICC_AID);
            LogWrapper.Debug(dfw_resp);

            //select generic app
            dfw_resp = dsf.SelectApplication(counterApp.application_aid);
            LogWrapper.Debug(dfw_resp);

            BasicFile bf = counterApp.searchNodesByFileAid(COUNTER_FILE_ID);
            ValueFile vf_obj = (ValueFile)bf;

            dfw_resp = dsf.getFileSettings(bf.fileid, ref FileObj);
            LogWrapper.Debug(dfw_resp);

            ValueFile vf = (ValueFile)FileObj;
            vf_obj.lower_limit_hex = vf.lower_limit_hex;
            vf_obj.upper_limit_hex = vf.upper_limit_hex;
            vf_obj.limited_credit_value_hex = vf.limited_credit_value_hex;
            vf_obj.limited_credit_enabled_hex = vf.limited_credit_enabled_hex;

            //read the file
            BasicFile basfile = (BasicFile)vf;

            readFile(ref basfile);

            String balance = Convert.ToInt32(vf.value_hex, 16).ToString("D");

            //authenticate with read w key
            bool bool1 = String.Equals(bf.getReadWriteKeyNo(), "0E", StringComparison.OrdinalIgnoreCase);
            bool bool2 = String.Equals(bf.getReadWriteKeyNo(), "0F", StringComparison.OrdinalIgnoreCase);

            if (!bool1 && !bool2)
            {
                //authenticate with writing key
                dfw_resp = dsf.authenticate(getKeyBasedOnLength(counterApp.app_key[bf.getWriteKeyNoInt()], counterApp.key_type),
                    bf.getWriteKeyNo(), counterApp.key_type, out picc_session_key);

                LogWrapper.Debug(dfw_resp);
            }

            try
            {
                //deduct the balance
                dfw_resp = dsf.doDebitDirect(COUNTER_FILE_ID, bf.getCommunicationSetting(), Convert.ToInt32("1", 10).ToString("X8"));
                LogWrapper.Debug(dfw_resp);

                isDone = true;

                dfw_resp = dsf.doCommitTransaction();
                LogWrapper.Debug(dfw_resp);

                success = true;
            }
            catch (Exception ce)
            {
                success = false;

                if (isDone)
                {
                    dfw_resp = dsf.doAbortTransaction();
                    LogWrapper.Debug(dfw_resp);
                }
            }

            return success;
        }

        public bool doDebitPurseValueFile00(String debitAmountInDec, String logData)
        {
            bool success = false;
            bool isDone = false;

            DesFireWrappeResponse dfw_resp;
            BasicFile FileObj = null;
            //select picc
            dfw_resp = dsf.SelectApplication(PICC_AID);
            LogWrapper.Debug(dfw_resp);

            //select generic app
            dfw_resp = dsf.SelectApplication(creditApp.application_aid);
            LogWrapper.Debug(dfw_resp);

            BasicFile bf = creditApp.searchNodesByFileAid(PURSE_VALUE_FILE_ID);
            ValueFile vf_obj = (ValueFile)bf;

            dfw_resp = dsf.getFileSettings(bf.fileid, ref FileObj);
            LogWrapper.Debug(dfw_resp);

            ValueFile vf = (ValueFile)FileObj;
            vf_obj.lower_limit_hex = vf.lower_limit_hex;
            vf_obj.upper_limit_hex = vf.upper_limit_hex;
            vf_obj.limited_credit_value_hex = vf.limited_credit_value_hex;
            vf_obj.limited_credit_enabled_hex = vf.limited_credit_enabled_hex;

            //read the file
            BasicFile basfile = (BasicFile)vf;

            readFile(ref basfile);

            String balance = Convert.ToInt32(vf.value_hex, 16).ToString("D");

            //authenticate with read w key
            bool bool1 = String.Equals(bf.getReadWriteKeyNo(), "0E", StringComparison.OrdinalIgnoreCase);
            bool bool2 = String.Equals(bf.getReadWriteKeyNo(), "0F", StringComparison.OrdinalIgnoreCase);

            if (!bool1 && !bool2)
            {
                //authenticate with writing key
                dfw_resp = dsf.authenticate(getKeyBasedOnLength(creditApp.app_key[bf.getWriteKeyNoInt()], creditApp.key_type),
                    bf.getWriteKeyNo(), creditApp.key_type, out picc_session_key);

                LogWrapper.Debug(dfw_resp);
            }

            try
            {
                //deduct the balance
                dfw_resp = dsf.doDebitDirect(PURSE_VALUE_FILE_ID, bf.getCommunicationSetting(), Convert.ToInt32(debitAmountInDec, 10).ToString("X8"));
                LogWrapper.Debug(dfw_resp);

                isDone = true;

                //write all the log
                doWritePurseLog(logData, ref dfw_resp, DEBIT_PURSE_LOG_FILE_ID);

                dfw_resp = dsf.doCommitTransaction();
                LogWrapper.Debug(dfw_resp);

                success = true;
            }
            catch (Exception ce)
            {
                success = false;

                if (isDone)
                {
                    dfw_resp = dsf.doAbortTransaction();
                    LogWrapper.Debug(dfw_resp);
                }
            }
            //Write Log

            return success;
        }

        public bool doCreditPurseValueFile00(String creditAmountInDec)//, String logData
        {
            bool success = false;
            bool isDone = false;

            DesFireWrappeResponse dfw_resp;
            BasicFile FileObj = null;

            //select picc
            dfw_resp = dsf.SelectApplication(PICC_AID);
            LogWrapper.Debug(dfw_resp);

            //select generic app
            dfw_resp = dsf.SelectApplication(creditApp.application_aid);
            LogWrapper.Debug(dfw_resp);

            BasicFile bf = creditApp.searchNodesByFileAid(PURSE_VALUE_FILE_ID);
            ValueFile vf_obj = (ValueFile)bf;

            dfw_resp = dsf.getFileSettings(bf.fileid, ref FileObj);
            LogWrapper.Debug(dfw_resp);

            ValueFile vf = (ValueFile)FileObj;
            vf_obj.lower_limit_hex = vf.lower_limit_hex;
            vf_obj.upper_limit_hex = vf.upper_limit_hex;
            vf_obj.limited_credit_value_hex = vf.limited_credit_value_hex;
            vf_obj.limited_credit_enabled_hex = vf.limited_credit_enabled_hex;

            //authenticate with read w key
            bool bool1 = String.Equals(bf.getReadWriteKeyNo(), "0E", StringComparison.OrdinalIgnoreCase);
            bool bool2 = String.Equals(bf.getReadWriteKeyNo(), "0F", StringComparison.OrdinalIgnoreCase);

            if (!bool1 && !bool2)
            {
                //authenticate with writing key
                dfw_resp = dsf.authenticate(getKeyBasedOnLength(creditApp.app_key[bf.getReadWriteKeyNoInt()], creditApp.key_type),
                    bf.getReadWriteKeyNo(), creditApp.key_type, out picc_session_key);

                LogWrapper.Debug(dfw_resp);
            }

            try
            {
                dfw_resp = dsf.doCreditDirect(PURSE_VALUE_FILE_ID, bf.getCommunicationSetting(), Convert.ToInt32(creditAmountInDec, 10).ToString("X8"));
                LogWrapper.Debug(dfw_resp);

                isDone = true;

                //doWritePurseLog(logData, ref dfw_resp,CREDIT_PURSE_LOG_FILE_ID);

                dfw_resp = dsf.doCommitTransaction();
                LogWrapper.Debug(dfw_resp);

                success = true;
            }
            catch (Exception e)
            {
                success = false;

                if (isDone)
                {
                    dfw_resp = dsf.doAbortTransaction();
                    LogWrapper.Debug(dfw_resp);
                }
            }

            //Write Log

            return success;
        }

        public void doWritePurseLog(String logData, ref DesFireWrappeResponse dfw_resp, String fileid)
        {
            BasicFile bf = creditApp.searchNodesByFileAid(fileid);
            RecordFile rf_obj = (RecordFile)bf;

            dsf.getFileCommSettPublic(rf_obj.fileid, true, false, false, true, ref dfw_resp);
            LogWrapper.Debug(dfw_resp);

            bool bool1 = String.Equals(rf_obj.getWriteKeyNo(), "0E", StringComparison.OrdinalIgnoreCase);
            bool bool2 = String.Equals(rf_obj.getWriteKeyNo(), "0F", StringComparison.OrdinalIgnoreCase);

            if (!bool1 && !bool2)
            {
                //authenticate with writing key
                dfw_resp = dsf.authenticate(getKeyBasedOnLength(creditApp.app_key[rf_obj.getWriteKeyNoInt()], creditApp.key_type),
                    rf_obj.getWriteKeyNo(), creditApp.key_type, out picc_session_key);

                LogWrapper.Debug(dfw_resp);
            }

            dfw_resp = dsf.writeRecordFile(rf_obj.fileid, "000000", (logData.Length / 2).ToString("X6"), logData);
            LogWrapper.Debug(dfw_resp);
        }



        public RecordFile readPurseLog(string logFileID)
        {
            String logTotal = "";

            DesFireWrappeResponse dfw_resp;
            BasicFile FileObj = null;
            
            //select picc
            dfw_resp = dsf.SelectApplication(PICC_AID);
            LogWrapper.Debug(dfw_resp);

            //select generic app
            dfw_resp = dsf.SelectApplication(creditApp.application_aid);
            LogWrapper.Debug(dfw_resp);

            BasicFile bf = creditApp.searchNodesByFileAid(logFileID);
            RecordFile rf_obj = (RecordFile)bf;

            dfw_resp = dsf.getFileSettings(bf.fileid, ref FileObj);
            LogWrapper.Debug(dfw_resp);

            RecordFile rf = (RecordFile)FileObj;
            rf_obj.record_size = rf.record_size;
            rf_obj.maxNoOfRecords = rf.maxNoOfRecords;
            rf_obj.isLinearRecord= rf.isLinearRecord;

            BasicFile basfile = (BasicFile)rf;

            if (Convert.ToInt32(rf.currentNoOfRecords, 16) > 0)
            {
                readFile(ref basfile);
                return (RecordFile)basfile;
            }
            else return (RecordFile)rf;
        }

        public ValueFile getCreditLibraryAppValueFile()
        {
            DesFireWrappeResponse dfw_resp;

            BasicFile FileObj = null;
            
            //select picc
            dfw_resp = dsf.SelectApplication(PICC_AID);
            LogWrapper.Debug(dfw_resp);

            //select generic app
            dfw_resp = dsf.SelectApplication(libraryApp.application_aid);
            LogWrapper.Debug(dfw_resp);

            //get value file settings
            BasicFile bf = libraryApp.searchNodesByFileAid(LIBRARY_VALUE_FILE_ID);
            ValueFile vf_obj = (ValueFile)bf;

            dfw_resp = dsf.getFileSettings(bf.fileid, ref FileObj);
            LogWrapper.Debug(dfw_resp);

            ValueFile vf = (ValueFile)FileObj;
            vf_obj.lower_limit_hex = vf.lower_limit_hex;
            vf_obj.upper_limit_hex = vf.upper_limit_hex;
            vf_obj.limited_credit_value_hex = vf.limited_credit_value_hex;
            vf_obj.limited_credit_enabled_hex = vf.limited_credit_enabled_hex;

            BasicFile basfile = (BasicFile)vf;

            readFile(ref basfile);

            return (ValueFile)basfile;
        }

        public RecordFile readLibraryLog(string logFileID)
        {
            String logTotal = "";

            DesFireWrappeResponse dfw_resp;
            BasicFile FileObj = null;

            //select picc
            dfw_resp = dsf.SelectApplication(PICC_AID);
            LogWrapper.Debug(dfw_resp);

            //select generic app
            dfw_resp = dsf.SelectApplication(libraryApp.application_aid);
            LogWrapper.Debug(dfw_resp);

            BasicFile bf = libraryApp.searchNodesByFileAid(logFileID);
            RecordFile rf_obj = (RecordFile)bf;

            dfw_resp = dsf.getFileSettings(bf.fileid, ref FileObj);
            LogWrapper.Debug(dfw_resp);

            RecordFile rf = (RecordFile)FileObj;
            rf_obj.record_size = rf.record_size;
            rf_obj.maxNoOfRecords = rf.maxNoOfRecords;
            rf_obj.isLinearRecord = rf.isLinearRecord;

            BasicFile basfile = (BasicFile)rf;

            if (Convert.ToInt32(rf.currentNoOfRecords, 16) > 0)
            {
                readFile(ref basfile);
                return (RecordFile)basfile;
            }
            else return (RecordFile)rf;
        }

        public BasicFile getFileSettings(String fileid)
        {
            DesFireWrappeResponse dfw_resp;
            BasicFile FileObj = null;

            dfw_resp = dsf.getFileSettings(fileid, ref FileObj);

            return FileObj;
        }


        public void doWriteLibLog(String logData, String fileid)
        {
            DesFireWrappeResponse dfw_resp = new DesFireWrappeResponse();

            BasicFile bf = libraryApp.searchNodesByFileAid(fileid);
            RecordFile rf_obj = (RecordFile)bf;

            dsf.getFileCommSettPublic(rf_obj.fileid, true, false, false, true, ref dfw_resp);
            LogWrapper.Debug(dfw_resp);

            bool bool1 = String.Equals(rf_obj.getWriteKeyNo(), "0E", StringComparison.OrdinalIgnoreCase);
            bool bool2 = String.Equals(rf_obj.getWriteKeyNo(), "0F", StringComparison.OrdinalIgnoreCase);

            if (!bool1 && !bool2)
            {
                //authenticate with writing key
                dfw_resp = dsf.authenticate(getKeyBasedOnLength(libraryApp.app_key[rf_obj.getWriteKeyNoInt()], libraryApp.key_type),
                    rf_obj.getWriteKeyNo(), libraryApp.key_type, out picc_session_key);

                LogWrapper.Debug(dfw_resp);
            }

            dfw_resp = dsf.writeRecordFile(rf_obj.fileid, "000000", (logData.Length / 2).ToString("X6"), logData);
            LogWrapper.Debug(dfw_resp);
        }


        public bool processLibraryCreditFileAndCommit(BasicFile bf, int operation_type)
        {
            DesFireWrappeResponse dfw_resp = new DesFireWrappeResponse();

            bool isDone = false;
            bool success = false;

            //authenticate with read w key
            bool bool1 = String.Equals(bf.getReadWriteKeyNo(), "0E", StringComparison.OrdinalIgnoreCase);
            bool bool2 = String.Equals(bf.getReadWriteKeyNo(), "0F", StringComparison.OrdinalIgnoreCase);

            if (!bool1 && !bool2)
            {
                //authenticate with writing key
                dfw_resp = dsf.authenticate(getKeyBasedOnLength(libraryApp.app_key[bf.getReadWriteKeyNoInt()], libraryApp.key_type),
                    bf.getReadWriteKeyNo(), libraryApp.key_type, out picc_session_key);

                LogWrapper.Debug(dfw_resp);
            }

            try
            {
                if (operation_type == OPERATION_DECREMENT)
                {
                    //deduct the balance
                    dfw_resp = dsf.doDebitDirect(bf.fileid, bf.getCommunicationSetting(), Convert.ToInt32("1", 10).ToString("X8"));
                }
                else if (operation_type == OPERATION_INCREMENT)
                {
                    //increse the counter
                    dfw_resp = dsf.doCreditDirect(bf.fileid, bf.getCommunicationSetting(), Convert.ToInt32("1", 10).ToString("X8"));
                }

                LogWrapper.Debug(dfw_resp);

                isDone = true;

                dfw_resp = dsf.doCommitTransaction();
                LogWrapper.Debug(dfw_resp);

                success = true;
            }
            catch (Exception ce)
            {
                success = false;

                if (isDone)
                {
                    dfw_resp = dsf.doAbortTransaction();
                    LogWrapper.Debug(dfw_resp);
                }
            }
            //Write Log

            return success;
        }


    }

}
