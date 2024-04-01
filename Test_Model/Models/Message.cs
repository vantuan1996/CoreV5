using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Model.Models
{
    [Table("Message")]
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
    }
    public class obj
    {
     
        public string sender { get; set; }
        public string receiver { get; set; }
        public string message { get; set; }
   
    }
}
