using Knjiznica.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Data
{
    public interface IBookData
    {
        IEnumerable<Book> GetBook(BookSearchModel searchModel);
        IEnumerable<Book> GetBookByTitle(string name);

        Book GetById(int id);
        Book Update(Book updatedBook);
        Book Add(Book newBook);
        Book Delete(int id);
        int Commit();
    }
}
