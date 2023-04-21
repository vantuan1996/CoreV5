using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Library.Models
{
    public class SessionModel
    {
        public string UserId { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

        public bool isAdmin { get; set; }

        public string Avatar { get; set; }
    }
}
