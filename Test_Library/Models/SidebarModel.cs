using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_Model.Models;

namespace Test_Library.Models
{
   public class SidebarModel
    {
        public string Id { get; set; } = "";

        public string ControllerName { get; set; } = "";

        public string ActionName { get; set; } = "";

        public List<MenuFunction> Data { get; set; } = new List<MenuFunction>();

        public List<MenuFunction> Data_Child { get; set; } = new List<MenuFunction>();

        public MenuFunction CurrentView { get; set; } = new MenuFunction();

        public string Breadcrumb { get; set; } = "";

        public string AreaCode { get; set; } = "";
    }
}
