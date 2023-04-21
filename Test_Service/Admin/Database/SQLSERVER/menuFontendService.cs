using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_Data.Repository;
using Test_Model.Models;

namespace Test_Service.Admin.Database.SQLSERVER
{
    public class menuFontendService : ImenuFontendService
    {
        private ImenuFontendRepository _menuFontendRepository;
        public menuFontendService(ImenuFontendRepository _menuFontendRepository)
        {
            this._menuFontendRepository = _menuFontendRepository;
        }
      
        public async Task<menuFontend_Custom> getAllByParentId(string name)
        {
            var listObjMenu = new menuFontend_Custom();
            var menuObjParent = from n in _menuFontendRepository.Table
                                where n.parentId == "0" && n.hethong == name
                                select n;

            foreach (var item in menuObjParent.ToList())
            {
                var child = (from n in _menuFontendRepository.Table
                             where n.parentId == item.Id
                             select n).ToList();

                listObjMenu.Id = item.Id;
                listObjMenu._tag = item._tag;
                listObjMenu.name = item.name;
                listObjMenu.route = item.route;
                listObjMenu.parentId = item.parentId;
                listObjMenu._children = child;

            }
            return await Task.FromResult( listObjMenu);
        }
    }
}
