using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Knjiznica.Core;
using Library.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SeznamKnjig.Pages.Books
{
    public class ListModel : PageModel
    {
        private readonly IBookData bookData;

        public IEnumerable<Book> Books { get; set; }

        [BindProperty(SupportsGet = true)]
        public BookSearchModel SearchModel { get; set; }
        public string SearchTerm { get; set; }

        public ListModel(IBookData bookData)
        {
            this.bookData = bookData;
        }

        public void OnGet(string SearchTerm)
        {
            SearchModel.ISBN = SearchTerm;
            SearchModel.Naslov = SearchTerm;
            SearchModel.Opis = SearchTerm;
            SearchModel.Avtor = SearchTerm;
            SearchModel.Oznaka = SearchTerm;
            SearchModel.Izdajatelj = SearchTerm;
            SearchModel.LetoIzdaje = SearchTerm;
            Console.WriteLine("SEARCHMODEL " + SearchModel.Naslov);

            Books = bookData.GetBook(SearchModel);
        }
    }
}
