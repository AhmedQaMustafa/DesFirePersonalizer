using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesFirePersonalizer.Mapping
{
    class MenuMapping : ClassMap<Model.Menu>
    {
        public MenuMapping()
        {
            Id(x => x.Id).GeneratedBy.Increment().Not.Nullable(); ;
            Map(x => x.MenuId).Unique().Not.Nullable();
            Map(x => x.MenuName).Not.Nullable();
            Map(x => x.Price).Not.Nullable();

            Table("Menu");
        }
    }
}
