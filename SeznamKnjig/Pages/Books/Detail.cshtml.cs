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
    public class DetailModel : PageModel
    {
        private readonly IBookData bookData;

        [TempData]
        public string Message { get; set; }

        public Book Book { set; get; }

        public DetailModel(IBookData bookData)
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
    }
}