using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Test_Library.Configs;
using Test_Model.Models;
using Test_Service.Admin.Database;

namespace Test_Web.Apis
{
    [Authorize(Policy = ApiConfig.Auth_Bearer_Mobile)]
    [Route("api/[controller]")]
    [ApiController]
  
    public class TEMP_BAO_CAO_TONG_HONGController : ControllerBase
    {
        private ITEMP_BAO_CAO_TONG_HONGService _TEMP_BAO_CAO_TONG_HONG;
        public TEMP_BAO_CAO_TONG_HONGController (ITEMP_BAO_CAO_TONG_HONGService _TEMP_BAO_CAO_TONG_HONG)
        {
            this._TEMP_BAO_CAO_TONG_HONG = _TEMP_BAO_CAO_TONG_HONG;
        }
        [AllowAnonymous]
        [HttpPost("byList")]
        public async Task<string> GetList([FromBody] Params value)
        {
            var data = await _TEMP_BAO_CAO_TONG_HONG.GetAllByFirst(value.key, value.fromdate, value.todate, value.pcid, value.phuongthuc, value.page, value.sizePerPage);
            var obj  = JsonSerializer.Serialize(data);
            return await Task.FromResult(obj);
        }
    }
}
