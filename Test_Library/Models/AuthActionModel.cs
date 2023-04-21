using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Library.Models
{
  public  class AuthActionModel
    {
        public int Create_Auth { get; set; } = 1; // 0 - No, 1 - Yes

        public int Update_Auth { get; set; } = 0; // 0 - No, 1 - Yes

        public int Delete_Auth { get; set; } = 0; // 0 - No, 1 - Yes
    }
}
