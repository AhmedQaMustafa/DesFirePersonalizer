using DesFireWrapperLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace DesFirePersonalizer
{
    class DesfireApplication
    {
        public string application_aid { get; set; }
        public List<BasicFile> fileList { get; set; } = new List<BasicFile>();

        private DesFireWrapper desfire { get; set; }
        public string key_settings { get; set; }

        private Int32 key_settings_int { get; set; }
        public DesFireWrapper.KeyType key_type { get; set; } = DesFireWrapper.KeyType.FAKE;
        public string max_no_of_key { get; set; } = null;

        public string[] app_key = new String[0x0E];

        public DesfireApplication(DesFireWrapper dsf, string app_aid)
        {
            this.desfire = dsf;
            this.application_aid = app_aid;
        }

        public void addFile(BasicFile file)
        {
            fileList.Add(file);
        }     

        public BasicFile searchNodesByFileAid(string fileId)
        {
            BasicFile res = null;

            foreach (var file in fileList)
            {
                BasicFile bf = (BasicFile)file;

                if (fileId == bf.fileid)
                {
                    res = (BasicFile)file;
                    break;
                }
            }

            return res;
        }

        public void populateKeys(String key_file)
        {
            String keyLiteral;
            FileInfo keyFileInfo = new FileInfo(key_file);

            if (keyFileInfo.Exists == false)
                throw new FileNotFoundException("Cannot find file: " + key_file);

            XmlDocument myXmlDocument = new XmlDocument();
            myXmlDocument.Load(key_file);

            keyLiteral = "/Keys/Key";

            app_key = new String[Convert.ToInt16(max_no_of_key, 16) & 0x0F];

            for (int index = 0; index < app_key.Length; index++)
            {
                XmlNode node = myXmlDocument.SelectSingleNode(keyLiteral + Convert.ToString(index, 10));
                if (String.IsNullOrWhiteSpace(node.InnerText))
                    throw new StringNullOrEmptyException("Key " + keyLiteral + " is empty or null");

                app_key[index] = node.InnerText;
            }
        }

        public string getKey(String keyNo)
        {
            int index = Convert.ToInt16(keyNo, 16);

            if (index == 0x0E || index == 0x0F)
                return null;

            if (key_type == DesFireWrapper.KeyType.TKTDES)
            {
                return app_key[index].Substring(0, 48);
            }
            else if (key_type == DesFireWrapper.KeyType.DES)
            {
                return app_key[index].Substring(0, 16);
            }
            else
                return app_key[index].Substring(0, 32);

        }

        public string getKey(int index)
        { 
            if (index == 0x0E || index == 0x0F)
                return null;

            if (key_type == DesFireWrapper.KeyType.TKTDES)
            {
                return app_key[index].Substring(0, 48);
            }
            else if (key_type == DesFireWrapper.KeyType.DES)
            {
                return app_key[index].Substring(0, 16);
            }
            else
                return app_key[index].Substring(0, 32);

        }
    }
}
