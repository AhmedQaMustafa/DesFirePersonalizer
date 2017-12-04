using DesFirePersonalizer.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesFirePersonalizer.Mapping
{
    class BookCollectionMapping :  ClassMap<BookCollection>
    {        
        public BookCollectionMapping()
        {
            Id(x => x.Id);
            Map(x => x.availability).Not.Nullable().Access.Property().Default("1"); ;
            HasMany(x => x.BookList).Cascade.All();

            Table("BookCollection");
        }
    }
}
