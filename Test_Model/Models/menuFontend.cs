using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Model.Models
{
    [Table("menuFontend")]
    public class menuFontend
    {
        public string Id { get; set; }

        public string _tag { get; set; }

        public string name { get; set; }

        public string route { get; set; }

        public string parentId { get; set; }

        public string hethong { get; set; }

    }

    public class menuFontend_Custom
    {
        public string Id { get; set; }

        public string _tag { get; set; }

        public string name { get; set; }

        public string route { get; set; }

        public string parentId { get; set; }

        public string hethong { get; set; }

        public List<menuFontend> _children { get; set; }



    }
}
