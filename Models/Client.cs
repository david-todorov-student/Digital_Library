using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DigitalLibrary.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        [MinLength(1)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        [Display(Name = "Number of books taken")]
        public int NumBooks { get; set; }
        public virtual ICollection<Book> Books { get; set; }

        public Client()
        {
            Books = new List<Book>();
        }

        public Client(int id, string fullName)
        {
            Id = id;
            fullName = fullName;
            NumBooks = 0;
            Books = new List<Book>();
        }
    }
}