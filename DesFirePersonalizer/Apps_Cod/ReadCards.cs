using DesFireWrapperLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesFirePersonalizer.Apps_Cod
{
    class ReadCards
    {


        public Model.Book book
        {
            get; set;
        }


        public ReadCards(String log)
        {
            int index = 0;

            String STDIDNO = log.Substring(index, 8); index += 4 * 2;
            book = new Model.Book();
            book.STDID = STDIDNO;

           // Type = log.Substring(index, 2); index += 1 * 2;

        }

    }
}
