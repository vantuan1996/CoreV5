using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Library.Helper
{
    public class DatabaseHelper
    {
        public static List<T> ExcuteCommandToList<T>(string command, string db = "DefaultConnection")
        {
            List<T> list = new List<T>();
            var connect = AppSettingHelper.GetStringFromFileJson("connectstring", "ConnectionStrings:" + db).Result;
            return SqlHelper.ExcuteCommandToList<T>(connect, command);
           
        }
        public static T ExcuteCommandToModel<T>(string command, string db = "DefaultConnection")
        {
            var list = new List<T>();

            //Chuyển db
            var connect = AppSettingHelper.GetStringFromFileJson("connectstring", "ConnectionStrings:" + db).Result;


            return SqlHelper.ExcuteCommandToModel<T>(connect, command);
        }
    }
}
