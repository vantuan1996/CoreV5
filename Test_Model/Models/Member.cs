using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Model.Models
{
  
    [Table("Member")]
    public class Member
    {
        [Key]
        public int PersonID { get; set; }

        public string Name { get; set; }

        [Required]
        //[StringLength(100)]
        public string Username { get; set; }

        //[StringLength(500)]
        //[Column(TypeName = "varchar")]
        public string Password { get; set; }

        //[StringLength(500)]
        //[Column(TypeName = "varchar")]
        public string PasswordSalat { get; set; }

        public Nullable<DateTime> DateCreated { get; set; }

        public static explicit operator Member(Task<Member> v)
        {
            throw new NotImplementedException();
        }
    }
}
