using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesFireWrapperLib
{
    public class BasicFile
    {
        public virtual string filename { get; set; }
        public virtual string fileid { get; set; }
        public virtual string filetype { get; set; }
        public virtual string communication_settings { get; set; }
        public virtual string access_rights { get; set; }

        public BasicFile() { }

        public BasicFile(BasicFile bf)
        {
            this.filename = bf.filename;
            this.fileid = bf.fileid;
            this.filetype = bf.filetype;
            this.communication_settings = bf.communication_settings;
            this.access_rights = bf.access_rights;
        }

        public virtual bool isStandardFile()
        {
            if (filetype == "00" || filetype == "01")
                return true;

            else return false;
        }

        public virtual bool isValueFile()
        {
            if (filetype == "02")
                return true;

            else return false;
        }

        public virtual bool isRecordFile()
        {
            if (filetype == "03" || filetype == "04")
                return true;

            else return false;
        }

        public virtual string getReadAccessType()
        {
            string read;
            string read_access = access_rights.Substring(0, 1);
            
            if (read_access == "E")
            {
                read = "Read:Free";
            }
            else if (read_access == "F")
            {
                read = "Read:Never";
            }
            else
            {
                read = "Read:Auth Key " + read_access;
            }
            
            return read;
        }

        public virtual string getReadKeyNo()
        {
            return "0"+access_rights.Substring(0, 1);
        }

        public virtual int getReadKeyNoInt()
        {
            return Convert.ToInt16(access_rights.Substring(0, 1), 16);
        }

        public virtual string getWriteAccessType()
        {
            string write;
            string write_access = access_rights.Substring(1, 1);

            if (write_access == "E")
            {
                write = "Write:Free";
            }
            else if (write_access == "F")
            {
                write = "Write:Never";
            }
            else
            {
                write = "Write:Auth Key " + write_access;
            }

            return write;
        }

        public virtual string getWriteKeyNo()
        {
            return "0" + access_rights.Substring(1, 1);
        }


        public virtual int getWriteKeyNoInt()
        {
            return Convert.ToInt16(access_rights.Substring(1, 1), 16);
        }

        public virtual string getReadWriteAccessType()
        {
            string read_write;
            string read_write_access = access_rights.Substring(2, 1);

            if (read_write_access == "E")
            {
                read_write = "Read-Write:Free";
            }
            else if (read_write_access == "F")
            {
                read_write = "Read-Write:Never";
            }
            else
            {
                read_write = "Read-Write:Auth Key " + read_write_access;
            }

            return read_write;
        }

        public virtual string getReadWriteKeyNo()
        {
            return "0" + access_rights.Substring(2, 1);
        }

        public virtual int getReadWriteKeyNoInt()
        {
            return Convert.ToInt16(access_rights.Substring(2, 1), 16);
        }


        public virtual string getChangeAccessType()
        {
            string change;
            string change_access = access_rights.Substring(3, 1);                      

            if (change_access == "E")
            {
                change = "Change:Free";
            }
            else if (change_access == "F")
            {
                change = "Change:Never";
            }
            else
            {
                change = "Change:Auth Key " + change_access;
            }

            return change;
        }

        public virtual string getChangeKeyNo()
        {
            return "0" + access_rights.Substring(3, 1);
        }


        public virtual string getCommType()
        {
            if (communication_settings == "00")
            {
                return "Comm: Plain";
            }
            else if (communication_settings == "01")
            {
                return "Comm: MAC";
            }
            else if (communication_settings == "03")
            {
                return "Comm: Encrypted";
            }

            return "None";
        }

        public virtual DesFireWrapper.CommunicationSetting getCommunicationSetting()
        {
            if (communication_settings == "00")
            {
                return DesFireWrapper.CommunicationSetting.PLAIN;
            }
            else if (communication_settings == "01")
            {
                return DesFireWrapper.CommunicationSetting.MACED;
            }
            else if (communication_settings == "03")
            {
                return DesFireWrapper.CommunicationSetting.ENCIPHERED;
            }

            return DesFireWrapper.CommunicationSetting.NONE;
        }
    }
}
