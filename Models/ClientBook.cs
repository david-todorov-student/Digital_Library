using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalLibrary.Models
{
    public class ClientBook
    {
        public Book Book { get; set; }
        public Client Client { get; set; }
        public List<Client> Clients { get; set; }
        public int BookId { get; set; }
        public int ClientId { get; set; }

    }
}