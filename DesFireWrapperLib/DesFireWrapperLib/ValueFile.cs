using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesFireWrapperLib
{
    public class ValueFile : BasicFile
    {
        public string lower_limit_hex;
        public string upper_limit_hex;
        public string value_hex;
        public string limited_credit_value_hex;
        public string limited_credit_enabled_hex;

        public ValueFile(BasicFile bf) : base(bf) { }
        //{
        //    this.filetype = bf.filetype;
        //    this.communication_settings = bf.communication_settings;
        //    this.access_rights = bf.access_rights;
        //}

        public string getLowerLimit()
        {
            int hex = Convert.ToInt32(lower_limit_hex, 16);
            string size_dec = hex.ToString("D");

            return "Lower Limit (dec): " + size_dec;
        }

        public string getUpperLimit()
        {
            int hex = Convert.ToInt32(upper_limit_hex, 16);
            string size_dec = hex.ToString("D");

            return "Upper Limit (dec): " + size_dec;
        }

        public Int32 getValueInt()
        {
            return Convert.ToInt32(value_hex, 16);
        }
    }
}
