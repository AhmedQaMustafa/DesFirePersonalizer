using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DesFireWrapperLib
{
    class DESCryptoUtil
    {
        ///USING MICROSOFT LIBRARY CRYPTO
        public static string EncrypTDestStr(string plain, string key, string iv)
        {
            byte[] encrypted = EncryptTDes(plain, key, iv);

            return BitConverter.ToString(encrypted).Replace("-", string.Empty);
        }

        public static string DecrypTDestStr(string encipher, string key, string iv)
        {
            byte[] decrypted = DecryptTDes(encipher, key, iv);

            return BitConverter.ToString(decrypted).Replace("-", string.Empty);
        }

        public static byte[] EncryptTDes(byte[] plain, int startOffset, int Length, string key, string iv)
        {
            string rawkey = key;
            if (key.Length <= 16)
            {
                rawkey = key + key; //make it 3 key length but single des
            }
            byte[] keyArray = MyUtil.StringToByteArray(rawkey);

            TripleDES tripleDESalg = TripleDES.Create();
            TripleDESCryptoServiceProvider sm = tripleDESalg as TripleDESCryptoServiceProvider;
            sm.Mode = CipherMode.CBC;
            sm.Padding = PaddingMode.None;

            byte[] IV = null;
            if (iv == null || iv == string.Empty)
                IV = new byte[8];
            else
                IV = MyUtil.StringToByteArray(iv);

            ICryptoTransform transEncrypt = DESCryptoExtensions.CreateWeakEncryptor(sm, keyArray, IV);

            byte[] result = transEncrypt.TransformFinalBlock(plain, startOffset, Length);

            return result;
        }

        public static byte[] EncryptTDes(string plain, string key, string iv)
        {
            string rawkey = key;
            if (key.Length <= 16)
            {
                rawkey = key + key; //make it 3 key length but single des
            }
            byte[] keyArray = MyUtil.StringToByteArray(rawkey);
            byte[] plainByte = MyUtil.StringToByteArray(plain);

            TripleDES tripleDESalg = TripleDES.Create();
            TripleDESCryptoServiceProvider sm = tripleDESalg as TripleDESCryptoServiceProvider;
            sm.Mode = CipherMode.CBC;
            sm.Padding = PaddingMode.None;

            byte[] IV = null;
            if (iv == null || iv == string.Empty)
                IV = new byte[8];
            else
                IV = MyUtil.StringToByteArray(iv);
            
            ICryptoTransform transEncrypt = DESCryptoExtensions.CreateWeakEncryptor(sm, keyArray, IV);

            byte[] data = MyUtil.StringToByteArray(plain);
            byte[] result = transEncrypt.TransformFinalBlock(data, 0, data.Length);

            return result;
        }


        //decryptor
        public static byte[] DecryptTDes(byte[] encipher, int startOffset, int Length, string key, string iv)
        {
            string rawkey = key;
            if (key.Length <= 16)
            {
                rawkey = key + key; //make it 3 key length but single des
            }
            byte[] keyArray = MyUtil.StringToByteArray(rawkey);

            TripleDES tripleDESalg = TripleDES.Create();
            TripleDESCryptoServiceProvider sm = tripleDESalg as TripleDESCryptoServiceProvider;
            sm.Mode = CipherMode.CBC;
            sm.Padding = PaddingMode.None;

            byte[] IV = null;
            if (iv == null || iv == string.Empty)
                IV = new byte[8];
            else
                IV = MyUtil.StringToByteArray(iv);

            ICryptoTransform transEncrypt = DESCryptoExtensions.CreateWeakDecryptor(sm, keyArray, IV);

            byte[] result = transEncrypt.TransformFinalBlock(encipher, startOffset, Length);

            return result;
        }

        public static byte[] DecryptTDes(string enchipered, String key, string iv)
        {
            string rawkey = key;
            if (key.Length <= 16)
            {
                rawkey = key + key; //make it 3 key length but single des
            }
            byte[] keyArray = MyUtil.StringToByteArray(rawkey);
            byte[] enchiperedBytes = MyUtil.StringToByteArray(enchipered);

            TripleDES tripleDESalg = TripleDES.Create();
            TripleDESCryptoServiceProvider sm = tripleDESalg as TripleDESCryptoServiceProvider;
            sm.Mode = CipherMode.CBC;
            sm.Padding = PaddingMode.None;

            byte[] IV = null;
            if (iv == null || iv == string.Empty)
                IV = new byte[8];
            else
                IV = MyUtil.StringToByteArray(iv);

            ICryptoTransform transEncrypt = DESCryptoExtensions.CreateWeakDecryptor(sm, keyArray, IV);

            byte[] result = transEncrypt.TransformFinalBlock(enchiperedBytes, 0, enchiperedBytes.Length);

            return result;
        }

        public static byte[] RotateLeft(byte[] source)
        {
            return source.Skip(1).Concat(source.Take(1)).ToArray();
        }
        
        public static void test3Des()
        {
            string message = "";
            string rawData = "1122334455667788";
            string rawKey = "000000000000000000000000000000000000000000000000";

            //encrypt
            byte[] Key = MyUtil.StringToByteArray(rawKey);
            byte[] IV = new byte[8];

            TripleDES tripleDESalg = TripleDES.Create();
            TripleDESCryptoServiceProvider sm = tripleDESalg as TripleDESCryptoServiceProvider;
            sm.Mode = CipherMode.CBC;
            sm.Padding = PaddingMode.None;

            ICryptoTransform transEncrypt = DESCryptoExtensions.CreateWeakEncryptor(sm, Key, IV);

            byte[] data = MyUtil.StringToByteArray(rawData);
            //byte[] result = new byte[8];
            byte[] result = null;
            result = transEncrypt.TransformFinalBlock(data, 0, 8);
            message += ("Raw Data = " + rawData + "\n");
            message += ("Raw Key = " + rawKey + "\n");
            message += ("Encrypted " + BitConverter.ToString(result).Replace("-", string.Empty) + "\n");

            //decrypt
            ICryptoTransform transDecrypt = DESCryptoExtensions.CreateWeakDecryptor(sm, Key, IV);

            result = transDecrypt.TransformFinalBlock(result, 0, 8);
            message += ("Raw Data = " + BitConverter.ToString(result).Replace("-", string.Empty) + "\n");
            message += ("Raw Key = " + rawKey + "\n");
            message += ("Decrypted " + BitConverter.ToString(result).Replace("-", string.Empty) + "\n");
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public static byte[] decrypt_basic(byte[] myIV, byte[] myKey, byte[] myMsg, int offset, int length)
        {            
            TripleDES tripleDESalg = TripleDES.Create();
            TripleDESCryptoServiceProvider sm = tripleDESalg as TripleDESCryptoServiceProvider;
            sm.Mode = CipherMode.CBC;
            sm.Padding = PaddingMode.None;

            ICryptoTransform transDecrypt = DESCryptoExtensions.CreateWeakDecryptor(sm, myKey, myIV);

            byte[] result = transDecrypt.TransformFinalBlock(myMsg, offset, length);

            return result;
        }

        public static byte[] encrypt_basic(byte[] myIV, byte[] myKey, byte[] myMsg)
        {
            TripleDES tripleDESalg = TripleDES.Create();
            TripleDESCryptoServiceProvider sm = tripleDESalg as TripleDESCryptoServiceProvider;
            sm.Mode = CipherMode.CBC;
            sm.Padding = PaddingMode.None;

            ICryptoTransform transEncrypt = DESCryptoExtensions.CreateWeakEncryptor(sm, myKey, myIV);

            byte[] result = transEncrypt.TransformFinalBlock(myMsg, 0, myMsg.Length);

            return result;
        }


        // ciphertext inside msg at offset and with length length
        public static byte[] decrypt(byte[] myKey, byte[] myMsg, int offset, int length)
        {
            return decrypt_basic(new byte[8], myKey, myMsg, offset, length);
        }

        /**
         * Decrypt using 3DES: DESede/CBC/NoPadding.
         * 
         * @param myIV	The initialization vector
         * @param myKey	Secret key (24 Bytes)
         * @param myMsg	Message to decrypt
         * @return
         */
        public static byte[] decrypt(byte[] myIV, byte[] myKey, byte[] myMsg)
        {
            return decrypt_basic(myIV, myKey, myMsg, 0, myMsg.Length);
        }


    }
}
