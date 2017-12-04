using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesFireWrapperLib
{
    public class RecordFile : BasicFile
    {
        public bool isLinearRecord { get; set; }
        public string record_size { get; set; }
        public string maxNoOfRecords { get; set; }
        public string currentNoOfRecords { get; set; }
        public string content { get; set; }

        public RecordFile(BasicFile bf) : base(bf) { }

        //public RecordFile(BasicFile bf)
        //{
        //    this.filetype = bf.filetype;
        //    this.communication_settings = bf.communication_settings;
        //    this.access_rights = bf.access_rights;
        //}

        public string getRecordSize()
        {
            int hex = Convert.ToInt32(record_size, 16);
            string size_dec = hex.ToString("D");

            return "Record Size (dec): " + size_dec;
        }

        public string getMaxNoOfRecords()
        {
            int hex = Convert.ToInt32(maxNoOfRecords, 16);
            string size_dec = hex.ToString("D");

            return "Max No of Records (dec): " + size_dec;
        }

        public Int32 getCurrentNoOfRecordsInInt()
        {
            return Convert.ToInt32(currentNoOfRecords, 16);
        }
    }
}
