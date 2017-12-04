using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesFirePersonalizer.Model
{
    class BookStatus
    {
        public virtual string BookID { get; set; }
        public virtual string Title { get; set; }
        public virtual string Author { get; set; }
        public virtual string Year { get; set; }
        public virtual bool availability { get; set; }
    }
}
