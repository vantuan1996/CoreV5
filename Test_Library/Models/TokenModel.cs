using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Library.Models
{
    public class TokenModel
    {
        public string Identifier { get; set; }

        public int Expires_In { get; set; }

        public string Token { get; set; }
    }
}
