using System.Collections.Generic;
using Knjiznica.Core;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace Library.Data
{
    public class SqlBookData : IBookData
    {
        private readonly LibraryDbContext db;
        public SqlBookData(LibraryDbContext db)
        {
            this.db = db;
        }
        public Book Add(Book newBook)
        {
            db.Add(newBook);
            return newBook;
        }

        public int Commit()
        {
            return db.SaveChanges();
        }

        public Book Delete(int id)
        {
            var book = GetById(id);
            if(book != null)
            {
                db.Books.Remove(book);
            }
            return book;
        }

        public IEnumerable<Book> GetBook(BookSearchModel searchModel)
        {
            var query = from b in db.Books
                    orderby b.Naslov
                    select b;

            var result = from b in db.Books
                         select b;

            var results3 =
                query
                    .Concat(Enumerable.Repeat(new Book { ISBN = "eafaef", Naslov = "Juš Lesjak Autobiogaphy" }, 1))
                    .OrderBy(b => b.Naslov);

            if (searchModel != null)
            {
                if (!string.IsNullOrEmpty(searchModel.ISBN)) {
                    var newquery = from b in db.Books
                                   where b.ISBN.StartsWith(searchModel.ISBN)
                                   select b;

                    // Either find one and set it as result
                    // Or find none and empty the result query
                    result = newquery;
                }

                if (!string.IsNullOrEmpty(searchModel.Naslov)) {
                    var newquery = from b in db.Books
                            where b.Naslov.StartsWith(searchModel.Naslov)
                            select b;

                    // If there are any matches, append them uniquely to result query
                    if (newquery != null)
                    {
                        result = result.Union(newquery);
                    }
                }

                if (!string.IsNullOrEmpty(searchModel.Opis)) {
                    var newquery = from b in db.Books
                            where b.Opis.StartsWith(searchModel.Opis)
                            select b;
                    if (newquery != null)
                    {
                        result = result.Union(newquery);
                    }
                }

                if (!string.IsNullOrEmpty(searchModel.Avtor))
                {
                    var newquery = from b in db.Books
                                   where b.Avtor.StartsWith(searchModel.Avtor)
                                   select b;
                    if (newquery != null)
                    {
                        result = result.Union(newquery);
                    }
                }

                if (!string.IsNullOrEmpty(searchModel.Oznaka)) {
                    var newquery = from b in db.Books
                            where b.Oznaka.StartsWith(searchModel.Oznaka)
                            select b;

                    if (newquery != null)
                    {
                        result = result.Union(newquery);
                    }
                }

                if (!string.IsNullOrEmpty(searchModel.Izdajatelj)) {
                    var newquery = from b in db.Books
                            where b.Izdajatelj.StartsWith(searchModel.Izdajatelj)
                            select b;

                    if (newquery != null)
                    {
                        result = result.Union(newquery);
                    }
                }

                if (!string.IsNullOrEmpty(searchModel.LetoIzdaje))
                {
                    try
                    {
                        var searchInt = Int32.Parse(searchModel.LetoIzdaje);
                        var newquery = from b in db.Books
                                       where b.LetoIzdaje == searchInt
                                       select b;

                        if (newquery != null)
                        {
                            result = result.Union(newquery);
                        }

                    } catch(FormatException)
                    {
                        Console.WriteLine("Wrong form of string to int");
                    }
                    
                }
            };

            var orderedResult = from b in result
                                orderby b.Naslov
                                select b;

            return orderedResult;
        }

        public IEnumerable<Book> GetBookByTitle(string name)
        {
            var query = from b in db.Books
                        where b.Naslov.StartsWith(name) || string.IsNullOrEmpty(name)
                        orderby b.Naslov
                        select b;
            return query;
        }

        public Book GetById(int id)
        {
            return db.Books.Find(id);
        }

        public Book Update(Book updatedBook)
        {
            var entity = db.Books.Attach(updatedBook);
            entity.State = EntityState.Modified;
            return updatedBook;
        }
    }
}
