using FluentNHibernate.Mapping;

namespace DesFirePersonalizer.Mapping
{
    class BookMapping:ClassMap<Model.Book>
    {
        public BookMapping()
        {
            Id(x => x.Id).GeneratedBy.Increment().Not.Nullable(); ;
            Map(x => x.BookID).Unique().Not.Nullable();
            Map(x => x.Title).Not.Nullable();
            Map(x => x.Author).Not.Nullable();
            Map(x => x.Year).Not.Nullable();
            Map(x => x.Availability).Not.Nullable().Access.Property().Default("1");

            Table("Book");
        }
    }
}
