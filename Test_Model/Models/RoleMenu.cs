using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Model.Models
{
    [Table("RoleMenu")]
    public class RoleMenu
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string RoleId { get; set; }

        [Required]
        public string MenuId { get; set; }
    }
}
