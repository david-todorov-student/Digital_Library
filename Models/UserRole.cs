using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalLibrary.Models
{
    public class UserRole
    {

        public string Email { get; set; }
        public string selectedRole { get; set; }
        public List<string> roles { get; set; }
        public UserRole()
        {
            roles = new List<string>();
        }
    }
}