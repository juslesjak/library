using Knjiznica.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Data
{
    public class InMemoryBookData : IBookData
    {
        readonly List<Book> books;

        public InMemoryBookData()
        {
            books = new List<Book>()
            {
                new Book { Id= 1, ISBN = "978-3-16-148410-1", Naslov = "Karavana Smrti", Opis = "Potopisna avantura", Oznaka = "Potopis", Izdajatelj = "Littera", LetoIzdaje = 1995 },
                new Book { Id= 2, ISBN = "978-3-16-148410-2", Naslov = "V golobnjaku memory", Opis = "Potopisna avantura", Oznaka = "Potopis", Izdajatelj = "Littera", LetoIzdaje = 1995 }

            };
        }

        public Book GetById(int id)
        {
            return books.SingleOrDefault(b => b.Id == id);
        }

        public IEnumerable<Book> GetBook(BookSearchModel searchModel)
        {
            var result = books.AsQueryable();
            if (searchModel != null)
            {
                if (!string.IsNullOrEmpty(searchModel.ISBN))
                    result = result.Where(b => b.ISBN == searchModel.ISBN); ;
                if (!string.IsNullOrEmpty(searchModel.Naslov))
                    result = result.Where(b => b.Naslov == searchModel.Naslov);
                if (!string.IsNullOrEmpty(searchModel.Opis))
                    result = result.Where(b => b.Opis == searchModel.Opis);
                if (!string.IsNullOrEmpty(searchModel.Oznaka))
                    result = result.Where(b => b.Oznaka == searchModel.Oznaka);
                if (!string.IsNullOrEmpty(searchModel.Izdajatelj))
                    result = result.Where(b => b.Izdajatelj == searchModel.Izdajatelj);
            };

            return result;
        }

        public Book Add(Book newBook)
        {
            books.Add(newBook);
            newBook.Id = books.Max(b => b.Id) + 1;
            return newBook;
        }

        public Book Update(Book updatedBook)
        {
            var book = books.SingleOrDefault(b => b.Id == updatedBook.Id);
            if(book != null)
            {
                book.ISBN = updatedBook.ISBN;
                book.Naslov = updatedBook.Naslov;
                book.Opis = updatedBook.Opis;
                book.Oznaka = updatedBook.Oznaka;
                book.Izdajatelj = updatedBook.Izdajatelj;
                book.LetoIzdaje = updatedBook.LetoIzdaje;
            }

            return book;
        }

        public int Commit()
        {
            return 0;
        }

        public Book Delete(int id)
        {
            var book = books.FirstOrDefault(r => r.Id == id);
            if (book != null)
            {
                books.Remove(book);
            }
            return book;
        }
    }
}
