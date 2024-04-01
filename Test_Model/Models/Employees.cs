using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Model.Models
{
    [Table("Employees")]
    public class Employees
    {
        [Key]
        public string Id { get; set; }

        public string FullName { get; set; }

        public int Age { get; set; }

        public string Note { get; set; }

        public string CreatedBy { get; set; }
 
        public DateTime CreatedAt { get; set; }

        public string Adress { get; set; }

        public string UpdateBy { get; set; }

        public DateTime UpdateAt { get; set; }
    }
}
