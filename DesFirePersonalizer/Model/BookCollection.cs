using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesFirePersonalizer.Model
{
    class BookCollection
    {
        public virtual long Id { get; set; }
        public virtual IList<Book> BookList{ get; set; }
        public virtual bool availability { get; set; }

        public BookCollection()
        {
            BookList = new List<Book>();
        }

        public virtual void addBook(Book aBook)
        {
            BookList.Add(aBook);
        }

        public virtual void removeBook(Book aBook)
        {
            BookList.Remove(aBook);
        }

        public virtual Book getABook(string bookId)
        {
            foreach (Book b in BookList)
            {
                if (string.Equals(b.BookID, bookId, StringComparison.OrdinalIgnoreCase))
                    return b;
            }

            return null;
        }
    }
}
