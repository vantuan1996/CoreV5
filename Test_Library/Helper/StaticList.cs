using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_Library.Models;

namespace Test_Library.Helper
{
   public class StaticList
    {
        public static async Task<List<SelectListModel>> ListLanguage()
        {

            var list = new List<SelectListModel> {
                                         new SelectListModel { ItemValue = "vi", ItemText = await LanguageHelper.GetLanguageText("LANGUAGE:VNA")},
                                         new SelectListModel { ItemValue = "en", ItemText = await LanguageHelper.GetLanguageText("LANGUAGE:English")},
                                    };
            return list;
        }


        public static List<SelectListModel_MenuGroup> GroupMenuList()
        {
            var list = new List<SelectListModel_MenuGroup> {
                                         new SelectListModel_MenuGroup { ItemValue = "12878956", ItemText = "Core", ItemIndex = 3, ItemCode = "PK_", Icon = "/Content/Image/sy-parking-icon.png", Color = "infobox-blue", AreaName = "Admin", Layout = "~/Views/Shared/_Layout.cshtml", Label = "label-success"}
                                    };

            return list;
        }

        /// <summary>
        /// Danh sách loại menu
        /// </summary>
        /// <returns>List<SelectListModel></returns>
        public static List<SelectListModel> MenuType()
        {
            var list = new List<SelectListModel> {
                                        new SelectListModel { ItemValue = "1", ItemText = "Menu"},
                                        new SelectListModel { ItemValue = "2", ItemText = "Function"}
                                    };
            return list;
        }

    }
}
