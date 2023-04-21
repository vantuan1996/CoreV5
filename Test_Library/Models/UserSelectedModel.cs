using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_Model.Models;

namespace Test_Library.Models
{
    public class UserSelectedModel
    {
        public List<string> Selected { get; set; } = new List<string>();

        public List<Role> Data_Role { get; set; } = new List<Role>();
    }

    public class RoleSelectedModel
    {
        public List<string> Selected { get; set; } = new List<string>();

        public List<MenuFunction> Data_Tree { get; set; } = new List<MenuFunction>();

        public List<MenuFunction> Data_Child { get; set; } = new List<MenuFunction>();
    }
    public class MenuFunctionTreeModel
    {
        public List<MenuFunction> Data_All { get; set; }

        public List<MenuFunction> Data_Child { get; set; }

        public string AreaCode { get; set; }
    }

}
