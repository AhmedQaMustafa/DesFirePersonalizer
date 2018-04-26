using DesFireWrapperLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesFirePersonalizer
{
    class LogWrapper
    {
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void Debug(DesFireWrappeResponse dsf_str)
        {
            log.Debug(dsf_str.apdu_message);
            writeRespMsg(dsf_str);
        }

        public static void Info(DesFireWrappeResponse dsf_str)
        {
            log.Info(dsf_str.apdu_message);
            writeRespMsg(dsf_str);
        }

        public static void Fatal(DesFireWrappeResponse dsf_str)
        {
            log.Fatal(dsf_str.apdu_message);
            writeRespMsg(dsf_str);
        }

        public static void Error(DesFireWrappeResponse dsf_str)
        {
            log.Error(dsf_str.apdu_message);
            writeRespMsg(dsf_str);
        }

        public static void Warn(DesFireWrappeResponse dsf_str)
        {
            log.Warn(dsf_str.apdu_message);
            writeRespMsg(dsf_str);
        }

        private static bool writeRespMsg(DesFireWrappeResponse dsf_str)
        {
            string sw1sw2 = dsf_str.sw1sw2;

            if (String.IsNullOrWhiteSpace(sw1sw2))
                return true;

            if (sw1sw2 != "9100" && sw1sw2 != "91AF" && sw1sw2 != null)
            {
                if (sw1sw2 == "6FFF")
                {
                    byte[] sw1sw2Bytes = MyUtil.StringToByteArray(sw1sw2);

                    string msg = ("SW1SW2 = " + sw1sw2 + "\n\n" + MyUtil.GetEnumSw1Sw2Description((DesFireSw1Sw2)sw1sw2Bytes[1]) + "\n\n" +
                        "Message:\n" + dsf_str.err_msg_pcsc);

                    log.Error(msg);

                    throw new RuntimeErrorException("Error Occured, Check Log File!");

                    return false;
                }
                else
                {
                    byte[] sw1sw2Bytes = MyUtil.StringToByteArray(sw1sw2);

                    log.Error("SW1SW2 = " + sw1sw2 + "\n\n" + MyUtil.GetEnumSw1Sw2Description((DesFireSw1Sw2)sw1sw2Bytes[1]));

                    throw new RuntimeErrorException("Error Occured, Check Log!");

                    return false;
                }
                
            }
         
            return true;
        }
               

    }
}
