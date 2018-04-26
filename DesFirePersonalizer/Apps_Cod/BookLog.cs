using DesFireWrapperLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesFirePersonalizer
{
    class BookLog
    {
        public String TerminalID { get; set; }
        public String Type { get; set; }

        public Model.Book book
        {
            get; set;
        }

        public String DateBorrowed { get; set; }
        public String DateReturned { get; set; }

        public BookLog(String log)
        {
            int index = 0;

            TerminalID = log.Substring(index, 8); index += 4 * 2;

            String Number = log.Substring(index, 8); index += 4 * 2;
            String Title = log.Substring(index, 32); index += 16 * 2;
            String STDIDNO = log.Substring(index, 8); index += 4 * 2;
            book = new Model.Book();
            book.BookID = Number;
            book.Title = Title;
            book.STDID = STDIDNO;

            Type = log.Substring(index, 2); index += 1 * 2;

            DateBorrowed = log.Substring(index, 20); index += 10 * 2;
            DateReturned = log.Substring(index, 20); index += 10 * 2;
        }

        public DateTime getDateBorrowed()
        {
            String date = MyUtil.ConvertHextoAscii(DateBorrowed);
            DateTime theDate = DateTime.ParseExact(date, "dd/MM/yyyy",
                               System.Globalization.CultureInfo.InvariantCulture);

            return theDate;
        }

        public DateTime getDateReturned()
        {
            String date = MyUtil.ConvertHextoAscii(DateReturned);
            DateTime theDate = DateTime.ParseExact(date, "dd/MM/yyyy",
                               System.Globalization.CultureInfo.InvariantCulture);

            return theDate;
        }



    }
}
