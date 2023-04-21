using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_Library.Helper;
using Test_Library.Models;
using Test_Model.Models;
using Test_Service.Admin.Database;

namespace Test_Web.Controllers
{
    public class MenuFunctionController : Controller
    {
        private IMenuFunctionService _MenuFunctionService;
        public MenuFunctionController(IMenuFunctionService _MenuFunctionService)
        {
            this._MenuFunctionService = _MenuFunctionService;
        }
        public async Task<IActionResult> Index(string AreaCode = "")
        {
            var data = await _MenuFunctionService.GetAllActive(AreaCode);
            ViewBag.AreaCodeValue = AreaCode;

            return View(data.ToList());
        }

        [HttpGet]
        public async Task<IActionResult> Create(MenuFunction_Submit model, string AreaCode = "")
        {

            model = model == null ? new MenuFunction_Submit() : model;
            model.Active = true;

            ViewBag.Data_MenuFunction = await GetMenuList(AreaCode);
            ViewBag.Data_MenuType = StaticList.MenuType();
            ViewBag.AreaCodeValue = AreaCode;

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(MenuFunction_Submit model, bool SaveAndCountinue = false, string AreaCode = "")
        {
            ViewBag.Data_MenuFunction = await GetMenuList(AreaCode);
            ViewBag.Data_MenuType = StaticList.MenuType();
            ViewBag.AreaCodeValue = AreaCode;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (string.IsNullOrWhiteSpace(model.MenuName))
            {
                ModelState.AddModelError("MenuName", "Hãy nhập tên");
                return View(model);
            }

            //Gán giá trị
            var id = Guid.NewGuid().ToString();

            model.ControllerName = !string.IsNullOrWhiteSpace(model.ControllerName) ? model.ControllerName.Trim() : string.Format("Controller_{0}", id);
            model.ActionName = !string.IsNullOrWhiteSpace(model.ActionName) ? model.ActionName.Trim() : string.Format("Action_{0}", id);

            var obj = new MenuFunction()
            {
                Id = id,
                MenuName = model.MenuName,
                ControllerName = model.ControllerName,
                ActionName = model.ActionName,
                Icon = model.Icon,
                MenuType = model.MenuType,
                ParentId = string.IsNullOrWhiteSpace(model.ParentId) ? "0" : model.ParentId,
                Active = model.Active,
                OrderNumber = model.OrderNumber,
                MenuGroupListId = "12878956"
            };

            //Thực hiện thêm mới
            var result = await _MenuFunctionService.Create(obj);
            if (result.isSuccess)
            {
                if (SaveAndCountinue)
                {
                    TempData["Success"] = "Thêm mới thành công";
                    return RedirectToAction("Create", new { ControllerName = obj.ControllerName, ParentId = obj.ParentId, MenuType = obj.MenuType, AreaCode = AreaCode });
                }

                return RedirectToAction("Index", new { AreaCode = AreaCode });
            }
            else
            {
                ModelState.AddModelError("", result.Message);
                return View(obj);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Update(string id, string AreaCode = "")
        {
            var model = await _MenuFunctionService.GetCustomById(id);

            ViewBag.Data_MenuFunction = await GetMenuList(AreaCode);
            ViewBag.Data_MenuType = StaticList.MenuType();
            ViewBag.AreaCodeValue = AreaCode;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(MenuFunction_Submit model, string AreaCode = "")
        {
            ViewBag.Data_MenuFunction = await GetMenuList(AreaCode);
            ViewBag.Data_MenuType = StaticList.MenuType();
            ViewBag.AreaCodeValue = AreaCode;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var oldObj = await _MenuFunctionService.GetById(model.Id);
            if (oldObj == null)
            {
                ModelState.AddModelError("", "Bản ghi không tồn tại");
                return View(model);
            }

            model.ControllerName = !string.IsNullOrWhiteSpace(model.ControllerName) ? model.ControllerName.Trim() : string.Format("Controller_{0}", model.Id);
            model.ActionName = !string.IsNullOrWhiteSpace(model.ActionName) ? model.ActionName.Trim() : string.Format("Action_{0}", model.Id);

            oldObj.MenuName = model.MenuName;
            oldObj.ControllerName = model.ControllerName.Trim();
            oldObj.ActionName = model.ActionName.Trim();
            oldObj.ParentId = string.IsNullOrWhiteSpace(model.ParentId) ? "0" : model.ParentId;
            oldObj.Active = model.Active;
            oldObj.Icon = model.Icon;
            oldObj.MenuType = model.MenuType;
            oldObj.OrderNumber = model.OrderNumber;


            var result = await _MenuFunctionService.Update(oldObj);
            if (result.isSuccess)
            {
                return RedirectToAction("Index", new { AreaCode = AreaCode });
            }
            else
            {
                ModelState.AddModelError("", result.Message);
                return View(model);
            }
        }


        private async Task<List<SelectListModel>> GetMenuList(string areaCode)
        {
            var list = new List<SelectListModel>();
            var obj = _MenuFunctionService.GetAllCustomActiveOrder(areaCode);
            var lstParent = obj.Result.Where(n => n.ParentId.Equals("0")).ToList();
            foreach (var item in lstParent.OrderBy(n => n.OrderNumber))
            {
                list.Add(new SelectListModel() { ItemValue = item.Id, ItemText = item.MenuName });
                var child = obj.Result.Where(n => n.ParentId == item.Id.ToString()).ToList();
                if (child.Any())
                {
                    Children(obj.Result.ToList(), list, child, item);
                  
                }
                list.Add(new SelectListModel() { ItemValue = "", ItemText = "------------------" });

            }
            return await Task.FromResult( list); 
        }

        private void Children(List<MenuFunction_Submit> listMenu, List<SelectListModel> list, List<MenuFunction_Submit> child, MenuFunction_Submit itemMenu)
        {
            if (child.Any())
            {
                foreach (var item in child)
                {
                     list.Add(new SelectListModel() { ItemValue = item.Id, ItemText = itemMenu.MenuName + "\\" + item.MenuName });
                    var  menuchild = listMenu.Where(n => n.ParentId == item.Id).ToList();
                    if (menuchild.Any())
                    {
                        item.MenuName = itemMenu.MenuName + " \\ " + item.MenuName;
                        Children(listMenu, list, menuchild, item);
                    }
                  
                }
               
            }
         
        }
       
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _MenuFunctionService.Delete(id);

            return Json(result);
        }
    }
}
