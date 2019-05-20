using Knjiznica.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Data
{
    public class LibraryDbContext : DbContext
    {

        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options)
        {

        }
        public DbSet<Book> Books { get; set; }
    }
}
