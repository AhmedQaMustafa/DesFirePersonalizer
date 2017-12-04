using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesFirePersonalizer.Model
{
    class Menu
    {
        public virtual long Id { get; set; }
        public virtual string MenuId { get; set; }
        public virtual string MenuName { get; set; }
        public virtual string Price { get; set; }
    }
}
