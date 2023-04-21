using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Test_Library.Helper
{
    public class SqlHelper
    {
        public static List<T> ExcuteCommandToList<T>(string connect, string command)
        {
            List<T> list = new List<T>();
          var   k = new SqlConnection(connect);
            using (SqlConnection conn = k)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(command, conn);
                var dt = cmd.ExecuteReader();
                list = DataReaderMapToList<T>(dt);
                conn.Close();
            }
            return list;
        }

        private static List<T> DataReaderMapToList<T>(SqlDataReader dt)
        {
            List<T> list = new List<T>();
            T obj = default(T);
            while (dt.Read())
            {
                obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (!object.Equals(dt[prop.Name], DBNull.Value))
                    {
                        prop.SetValue(obj, dt[prop.Name], null);
                    }
                }
                list.Add(obj);
            }
            return list;
        
         
        }

        public static T ExcuteCommandToModel<T>(string connect, string command)
        {
            var list = new List<T>();

            var k = new SqlConnection(connect);

            using (SqlConnection conn = k)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(command, conn);

                var dt = cmd.ExecuteReader();

                list = DataReaderMapToList<T>(dt);

                conn.Close();
            }

            return list.FirstOrDefault();
        }

    }
}
