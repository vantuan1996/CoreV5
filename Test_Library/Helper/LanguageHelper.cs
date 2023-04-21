﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Library.Helper
{
    public class LanguageHelper
    {
        private static string lang;

        public static async Task<string> GetLanguageText(string path)
        {

            //var region = await AppSettingHelper.GetStringFromAppSetting("Languages");
            var region = String.IsNullOrEmpty(lang) ? "vi" : lang;
            path = $"{path}:{region}";
            var text = await AppSettingHelper.GetStringFromFileJson("Languages/languages", path);

            text = string.IsNullOrWhiteSpace(text) ? "" : text;

            return text;
        }

        public static async Task<string> GetMenuLanguageText(string path)
        {
            // var region = await AppSettingHelper.GetStringFromAppSetting("Languages");
            var region = String.IsNullOrEmpty(lang) ? "vi" : lang;
            path = $"{path}:{region}";
            var text = await AppSettingHelper.GetStringFromFileJson("Languages/menu-languages", path);

            text = string.IsNullOrWhiteSpace(text) ? "" : text;

            return text;
        }

        public static Task<string> GetLang(string language)
        {
            lang = language;
            return null;
        }
    }
}
