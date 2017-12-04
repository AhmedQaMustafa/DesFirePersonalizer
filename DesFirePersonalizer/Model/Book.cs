
namespace DesFirePersonalizer.Model
{
    class Book
    {
        public virtual long Id { get; set; }
        public virtual string BookID { get; set; }
        public virtual string Title { get; set; }
        public virtual string Author { get; set; }
        public virtual string Year { get; set; }
        public virtual bool Availability { get;  set;}
    }
}
