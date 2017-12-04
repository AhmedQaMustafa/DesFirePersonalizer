using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesFireWrapperLib
{
    public class StandardFile: BasicFile
    {
        public string file_size_dec_max_string { get; set; }
        public string file_size_hex_content_length { get; set; }
        public string content { get; set; }

        public StandardFile(BasicFile bf) : base(bf) { }

        //public StandardFile(BasicFile bf)
        //{
        //    this.filename = bf.filename;
        //    this.filetype = bf.filetype;
        //    this.communication_settings = bf.communication_settings;
        //    this.access_rights = bf.access_rights;
        //}

        public string getFileSize()
        {
            int hex = Convert.ToInt32(file_size_dec_max_string, 16);
            string size_dec = hex.ToString("D");

            return "Size (dec bytes): " + size_dec;
        }
    }
}
