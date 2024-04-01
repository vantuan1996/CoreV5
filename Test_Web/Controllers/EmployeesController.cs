using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Test_Library.Helper;
using Test_Library.Models;
using Test_Model.Models;
using Test_Service.Admin;

namespace Test_Web.Controllers
{
    public class EmployeesController : Controller
    {
        private IEmployeesService _EmployeService;

        public EmployeesController(IEmployeesService EmployeService)
        {
            this._EmployeService = EmployeService;
           
        }
        public async Task<IActionResult> Index(string key, string pc, int page = 1, string group = "", string selectedId = "")
        {

            var gridmodel = await _EmployeService.GetAllCustomPagingByFirst(key, pc, page, 2);         
            ViewBag.keyValue = key;
            return View(gridmodel);
        }


        public async Task<IActionResult> GetData()
        {
            string url = "http://api.isportsapi.com/sport/football/livescores?api_key=0Ls2s89uNlcmKB00";

            // Call iSport Api to get data in json format
            string rsp = sendGet(url);
            var  obj  = JsonSerializer.Deserialize<Root>(rsp)!;

            return View(obj);
        }


        private string sendGet(string url)
        {
            // Create request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            // GET request
            request.Method = "GET";
            request.ReadWriteTimeout = 5000;
            request.ContentType = "text/html;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));

            // Return content
            string retString = myStreamReader.ReadToEnd();
            return retString;
        }


        #region Thêm mới

        /// <returns></returns>     
        [HttpGet]
        public async Task<IActionResult> Create(Employees model)
        {
            model = model == null ? new Employees() : model;

            return await Task.FromResult(View(model));
        }
      

        [HttpPost]
        public async Task<IActionResult> Create(Employees model, bool SaveAndCountinue = false)
        {


            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.Id = Guid.NewGuid().ToString();
            model.CreatedAt = DateTime.Now;
            model.UpdateAt = DateTime.Now;

            //Thực hiện thêm mới
            var result = await _EmployeService.Create(model);
            if (result.isSuccess)
            {
                
                if (SaveAndCountinue)
                {
                    TempData["Success"] = result.Message;
                    return RedirectToAction("Create");
                }

                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", result.Message);
                return View(model);
            }
        }


        #endregion

        #region Cập nhật

        [HttpGet]
        public async Task<IActionResult> Update(string id, string AreaCode = "", int page = 1, string key = "")
        {
            var model = await _EmployeService.GetById(id);
            ViewBag.PN = page;
            ViewBag.AreaCodeValue = AreaCode;
            ViewBag.keyValue = key;

            return View(model);
        }

    
        [HttpPost]
        public async Task<IActionResult> Update(Employees obj, string AreaCode = "", int page = 1, string key = "")
        {
            //
            ViewBag.keyValue = key;
            ViewBag.AreaCodeValue = AreaCode;
            ViewBag.PN = page;
            //Kiểm tra
            var oldObj = await _EmployeService.GetById(obj.Id.ToString());
            if (oldObj == null)
            {
                ViewBag.Error = await LanguageHelper.GetLanguageText("MESSAGE:RECORD:NOTEXISTS");
                return View(obj);
            }



            if (!ModelState.IsValid)
            {
                return View(oldObj);
            }

            oldObj.Id = obj.Id;
            oldObj.FullName = obj.FullName;
            oldObj.Note = obj.Note;
            oldObj.Adress = obj.Adress;
            oldObj.Age = obj.Age;
            oldObj.UpdateAt = DateTime.Now;


            //Thực hiện cập nhậts
            var result = await _EmployeService.Update(oldObj);


            if (result.isSuccess)
            {
                
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", result.Message);
                return View(obj);
            }
        }

        #endregion Cập nhật

        #region Xóa

        /// <summary>
        /// Xóa
        /// </summary>
        /// <modified>
        /// Author              Date            Comments
        /// TrungNQ             01/09/2017      Tạo mới
        /// </modified>
        /// <param name="id">Id bản ghi</param>
        /// <returns></returns>

        public async Task<IActionResult> Delete(string id)
        {
            var result = await _EmployeService.DeleteById(id);
        

            return Json(result);
        }

        
        #endregion Xóa

        
    }
}
