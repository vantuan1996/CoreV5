using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Library.Helper
{
  public  class FunctionHelper
    {
        public static string GetLayout(string code)
        {
            var layout = "~/Views/Shared/_Layout.cshtml";

            if (!string.IsNullOrWhiteSpace(code))
            {
                layout = "~/Areas/Admin/Views/Shared/_Layout.cshtml";
            }

            return layout;
        }
    }
}
