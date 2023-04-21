using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Core.Models
{
    public class GridModel<T> where  T : class
    {
        public List<T> datas { get; set; } = new List<T>();

        public int Totapage { get; set; }

        public int pageNumber { get; set; }

        public int pageSize { get; set; }

        public int TotalItem { get; set; }

    }
}
