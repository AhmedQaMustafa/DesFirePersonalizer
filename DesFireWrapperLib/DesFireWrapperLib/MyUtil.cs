using PCSC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DesFireWrapperLib
{
    public class MyUtil
    {
        public static byte[] SubArray(byte[] data, int from, int to)
        {
            if (to < from)
                throw new Exception("To index must be higher than from index in SubArray"); ;

            int length = to - from;

            byte[] result = new byte[length];
            Array.Copy(data, from, result, 0, length);

            return result;
        }

        // convert from string to array
        public static byte[] StringToByteArray(string hex)
        {
            byte[] result;
            try {
                result = Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();

                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message); ;
            }
        }

        //get the description from an enum
        public static string GetEnumIns2Description(DesfireInstructionCode value)
        {
            // Get the Description attribute value for the enum value
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                    typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }

        //get the description from an enum
        public static string GetEnumSw1Sw2Description(DesFireSw1Sw2 value)
        {
            // Get the Description attribute value for the enum value
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                    typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }

        //get the description from an enum
        public static string GetEnumPcscErrorDescription(SCardError value)
        {
            // Get the Description attribute value for the enum value
            FieldInfo fi = value.GetType().GetField(value.ToString());

            if (fi == null)
                return "Unknown Error";

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                    typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }

        public static bool ArraysEqual<T>(T[] a1, T[] a2)
        {
            if (ReferenceEquals(a1, a2))
                return true;

            if (a1 == null || a2 == null)
                return false;

            if (a1.Length != a2.Length)
                return false;

            EqualityComparer<T> comparer = EqualityComparer<T>.Default;
            for (int i = 0; i < a1.Length; i++)
            {
                if (!comparer.Equals(a1[i], a2[i])) return false;
            }
            return true;
        }

        public static string ConvertHextoAscii(string HexString)
        {
            string asciiString = "";
            for (int i = 0; i < HexString.Length; i += 2)
            {
                if (HexString.Length >= i + 2)
                {
                    String hs = HexString.Substring(i, 2);
                    asciiString = asciiString + System.Convert.ToChar(System.Convert.ToUInt32(HexString.Substring(i, 2), 16)).ToString();
                }
            }
            return asciiString;
        }

        public static String asciiToHexString(String asciiStr)
        {
            return String.Join(String.Empty, asciiStr.Select(c => ((int)c).ToString("X")).ToArray());
        }

    }
}
