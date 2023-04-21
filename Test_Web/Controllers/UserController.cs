using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_Library.Extensions;
using Test_Library.Helper;
using Test_Library.Security;
using Test_Model.Models;
using Test_Service.Admin;
using Test_Service.Admin.Database;

namespace Test_Web.Controllers
{
    public class UserController : Controller
    {
         private IUserService _UserService;
        private IRoleService _RoleService;
        private IMenuFunctionService _MenuFunctionService;
     //   private IHostingEnvironment _hostingEnvironment;
        public UserController (IMenuFunctionService _MenuFunctionService, IUserService _UserService, IRoleService _RoleService)
        {
            this._MenuFunctionService = _MenuFunctionService;
            this._RoleService = _RoleService;
            this._UserService = _UserService;
            
        }
        public async Task<IActionResult> Index(string key = "", int page = 1, string export = "0", string AreaCode = "")
        
        {
            var gridmodel = await _UserService.GetPaging(key, page, 5);
            ViewBag.AuthValue = await AuthHelper.CheckAuthAction("User", this.HttpContext);
            ViewBag.keyValue = key;
            //ViewBag.AuthValue = await AuthHelper.CheckAuthAction("User", this.HttpContext);
            ViewBag.AreaCodeValue = AreaCode;

            //if (export == "1")
            //{
            //    await ExportFile(this.HttpContext);

            //    //return View(gridmodel);
            ////}
            ViewBag.Roles = await _RoleService.GetAllUserRoles();
            return View(gridmodel);
        }
        [HttpGet]
        public async Task<IActionResult> Create(User_Submit model, string AreaCode = "")
        {
            model = model == null ? new User_Submit() : model;
            model.Data_Role = await _RoleService.GetAllActiveOrder();
            ViewBag.AreaCodeValue = AreaCode;

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(User_Submit model, bool SaveAndCountinue = false, string AreaCode = "", string groupids = "")
        {
            model.Data_Role = await _RoleService.GetAllActiveOrder();

            ViewBag.AreaCodeValue = AreaCode;

           

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //
            var existed = await _UserService.GetByUsername(model.Username);
            if (existed != null)
            {
                ModelState.AddModelError("Username", "Tài khoản tồn tại");
                return View(model);
            }

            if (string.IsNullOrWhiteSpace(model.Password))
            {
                model.Password = "123456";
            }
            else
            {
                if (model.Password != model.RePassword)
                {
                    ModelState.AddModelError("RePassword", "Mật khẩu không khớp");
                    return View(model);
                }
            }

            var obj = new User()
            {
                Active = model.Active,
                Id = Guid.NewGuid().ToString(),
                Password = model.Password,
                PasswordSalat = Guid.NewGuid().ToString(),
                Name = model.Name,
                Username = model.Username,
                Admin = model.isAdmin,
             
            };

            if (!string.IsNullOrWhiteSpace(model.RoleIds))
            {
                var ks = model.RoleIds.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                model.Roles = new List<string>();
                foreach (var item in ks)
                {
                    model.Roles.Add(item);
                }

                foreach (var item in model.Roles)
                {
                    var t = new UserRole()
                    {
                        Id = Guid.NewGuid().ToString(),
                        RoleId = item,
                        UserId = obj.Id
                    };

                    await _RoleService.CreateMap(t);
                }
            }

            //Mã hóa pass
            //obj.Password = CryptoHelper.EncryptPass_User(obj.Password, obj.PasswordSalat);
            obj.Password = obj.Password.PasswordHashed(obj.PasswordSalat);

            //Thực hiện thêm mới
            var result = await _UserService.Create(obj);
            if (result.isSuccess)
            {
                if (SaveAndCountinue)
                {
                    TempData["Success"] = "Thêm mới thành công";
                    return RedirectToAction("Create", new { AreaCode = AreaCode });
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
            var model = _UserService.GetCustomById(id);
            model.Data_Role = await _RoleService.GetAllActiveOrder();
            ViewBag.AreaCodeValue = AreaCode;
            return View(model);
        }
        public async Task<IActionResult> Update(User_Submit model, string AreaCode = "", string groupids = "")
        {
            model.Data_Role = await _RoleService.GetAllActiveOrder();

           
            ViewBag.AreaCodeValue = AreaCode;

         

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var oldObj = await _UserService.GetById(model.Id);
            if (oldObj == null)
            {
                ModelState.AddModelError("", "Bản ghi không tồn tại");
                return View(model);
            }

            //
            var existed = await _UserService.GetByUsername_notId(model.Username, model.Id);
            if (existed != null)
            {
                ModelState.AddModelError("Username", "Tài khoản tồn tại");
                return View(model);
            }

            oldObj.Active = model.Active;
            oldObj.Name = model.Name;
            oldObj.Username = model.Username;
            oldObj.Admin = model.isAdmin;
      

            //Kiểm tra mật khẩu mới
            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                if (model.Password != model.RePassword)
                {
                    ModelState.AddModelError("RePassword", "Mật khẩu không khớp");
                    return View(model);
                }

                //Sinh mã salat mới
                oldObj.PasswordSalat = Guid.NewGuid().ToString();

                //
                oldObj.Password = model.Password.PasswordHashed(oldObj.PasswordSalat);
            }

            //
            await _RoleService.DeleteMap(oldObj.Id);

            if (!string.IsNullOrWhiteSpace(model.RoleIds))
            {
                var ks = model.RoleIds.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                model.Roles = new List<string>();
                foreach (var item in ks)
                {
                    model.Roles.Add(item);
                }

                foreach (var item in model.Roles)
                {
                    var t = new UserRole()
                    {
                        Id = Guid.NewGuid().ToString(),
                        RoleId = item,
                        UserId = oldObj.Id
                    };

                    await _RoleService.CreateMap(t);
                }
            }

            var result = await _UserService.Update(oldObj);
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

    }
}
