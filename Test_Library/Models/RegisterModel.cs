using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Library.Models
{
  public  class RegisterModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string RePassword { get; set; }

        [Required]
        public string Name { get; set; }

        public bool isAny { get; set; }
    }
}
