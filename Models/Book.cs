using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DigitalLibrary.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        [Display(Name = "PDF Link")]
        public string Url { get; set; }
        [Display(Name = "Number of copies")]
        public int numOfCopies { get; set; }
        [Display(Name = "Number of copies left")]
        public int numOfCopiesLeft { get; set; }
        public virtual ICollection<Client> Clients { get; set; }

        public Book()
        {
            Clients = new List<Client>();
        }

        public Book(int id, string title, string author, int numOfCopies)
        {
            Id = id;
            Title = title;
            Author = author;
            this.numOfCopies = numOfCopies;
            this.numOfCopiesLeft = numOfCopies;
            Clients = new List<Client>();
        }

        public Book(int id, string title, string author, string url)
        {
            Id = id;
            Title = title;
            Author = author;
            Url = url;
            numOfCopies = 0;
        }
    }
}