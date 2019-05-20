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
    public class EditModel : PageModel
    {
        private readonly IBookData bookData;
        [BindProperty]
        public Book Book { get; set; }

        public EditModel(IBookData bookData)
        {
            this.bookData = bookData;
        }
        public IActionResult OnGet(int? bookId)
        {
            if (bookId.HasValue)
            {
                Book = bookData.GetById(bookId.Value);
            }
            else
            {
                Book = new Book();
            }
            if(Book == null)
            {
                return RedirectToPage("./NotFound");
            }

            return Page();

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (Book.Id > 0)
            {
                bookData.Update(Book);
                TempData["Message"] = $"Knjiga uspešno urejena: {@Book.Naslov} ({@Book.LetoIzdaje})";

            }
            else
            {
                bookData.Add(Book);
                TempData["Message"] = $"Knjiga uspešno dodana: {@Book.Naslov} ({@Book.LetoIzdaje})";

            }
            bookData.Commit();
            return RedirectToPage("./Detail", new { bookId = Book.Id });
        }
    }
}