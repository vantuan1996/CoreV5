using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_Core.Models;

namespace Test_Library.Helper
{
  public  class GridModelHelper <T> where T : class
    {
        public static GridModel<T>  GetPage (List<T> data ,int currentPage, int itemPerPage, int TotalItemCount, int pageCount)
        {
            var pageModel = new GridModel<T>
            {
                datas = data,
                pageNumber = currentPage,
                pageSize = itemPerPage,
                TotalItem = TotalItemCount,
                Totapage = pageCount

            };
            return pageModel;
        }
        public static GridModel<T> GetPage(List<T> list, int currentPage, int itemPerPage, int TotalItemCount)
        {
            var PageCount = TotalItemCount > 0
                        ? (int)Math.Ceiling(TotalItemCount / (double)itemPerPage)
                        : 0;

            var PageModel = new GridModel<T>
            {
                datas = list,
                pageNumber = currentPage,
                pageSize = itemPerPage,
                Totapage = PageCount,
                TotalItem = TotalItemCount,
            };

            return PageModel;
        }
    }
}
