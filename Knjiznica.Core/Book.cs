using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Knjiznica.Core
{
    public class Book
    {
        public int Id { get; set; }
        [Required, StringLength(13)]
        public string ISBN { get; set; } // "^\\d{3}-\\d{1}-\\d{4}-\\d{4}-\\d{1}$"
        [Required]
        public string Naslov { get; set; }
        public string Opis { get; set; }
        public string Avtor { get; set; } // string[] Avtor {get; set;}
        public string Oznaka { get; set; }
        public string Izdajatelj { get; set; }
        public int LetoIzdaje { get; set; }
    }
}
