using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesFirePersonalizer.Apps_Cood
{
    public class ComboboxItem
    {
        public string Text { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
