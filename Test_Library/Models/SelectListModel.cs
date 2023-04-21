using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Library.Models
{
    public class SelectListModel
    {
        public string ItemValue { get; set; }

        public string ItemText { get; set; }
    }
    public class SelectListModel_Chosen
    {
        public string IdSelectList { get; set; }

        public List<SelectListModel> Data { get; set; }

        public string Selecteds { get; set; }

        public bool isMultiSelect { get; set; }

        public string Placeholder { get; set; }
    }
    public class SelectListModel_MenuGroup
    {
        public string ItemValue { get; set; }

        public string ItemText { get; set; }
        public int ItemIndex { get; set; }

        public string ItemCode { get; set; }
        public string Icon { get; set; }

        public string Color { get; set; }

        public string AreaName { get; set; }
        public string Layout { get; set; }

        public string Label { get; set; } = "";

    }
}
