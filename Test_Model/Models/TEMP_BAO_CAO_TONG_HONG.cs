using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Model.Models
{
    [Table("TEMP_BAO_CAO_TONG_HONG")]
  public  class TEMP_BAO_CAO_TONG_HONG
    {
        [Key]
        public  string MA_DM { get; set; }
        public  string TEN_DM { get; set; }
        public  string TEN_HE_THONG { get; set; }
        public  string PHUONG_THUC { get; set; }
        public  string DON_VI_NGSP { get; set; }
        public  string DON_VI_LSGP { get; set; }
        public  int SO_MA { get; set; }


    }
    public class Report_All
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItem { get; set; }
        public int TotalPage { get; set; }
        public List<TEMP_BAO_CAO_TONG_HONG> data { get; set; }
    }  
    
    public class Params
    {
        public string key { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        public string pcid { get; set; }  
        public string phuongthuc { get; set; }
        public int page { get; set; } = 1;
        public int sizePerPage { get; set; } = 2;
      
    }

}
