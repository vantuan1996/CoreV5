﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Library.Models
{
   public class AuthPartialModel
    {
        public AuthActionModel Auth_Value { get; set; }

        public object model { get; set; }

        public string ControllerName { get; set; } = "";

        public string ActionName { get; set; } = "";

        public string RecordId { get; set; } = "";

        public string FunctionName { get; set; } = "Index";

        public string AreaCode { get; set; } = "";
    }
}
