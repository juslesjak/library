using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Knjiznica.Core;
using Library.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.Books
{
    public class DeleteModel : PageModel
    {
        private readonly IBookData bookData;
        public Book Book { get; set; }

        public DeleteModel(IBookData bookData)
        {
            this.bookData = bookData;
        }
        public IActionResult OnGet(int bookId)
        {
            Book = bookData.GetById(bookId);
            if(Book == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }

        public IActionResult OnPost(int bookId)
        {
            var book = bookData.Delete(bookId);
            bookData.Commit();

            if(book == null)
            {
                return RedirectToPage("./NotFound");
            }

            TempData["Message"] = $"Knjiga uspešno zbrisana:  {book.Naslov}";
            return RedirectToPage("./List");
        }
    }
}