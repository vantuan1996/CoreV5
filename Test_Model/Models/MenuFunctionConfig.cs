using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Model.Models
{
    [Table("MenuFunctionConfig")]
    public class MenuFunctionConfig
    {
        [Key]
        public string Id { get; set; }

        public string MenuFunctionId { get; set; }
    }
}
