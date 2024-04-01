using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_Core.Models;
using Test_Data.Repository;
using Test_Library.Extensions;
using Test_Library.Helper;
using Test_Library.Models;
using Test_Model.Models;
using X.PagedList;
namespace Test_Service.Admin.Database.SQLSERVER
{
    public class MemberService : IMemberService
    {
        private IMemberRepository _MemberRepository;
   
        public MemberService(IMemberRepository _MemberRepository)
        {
            
            this._MemberRepository = _MemberRepository;
        }

        public async Task<MessageReport> Create(Member model)
        {
            return await _MemberRepository.Add(model);
        }

        public async Task<List<Member>> GetAll()
        {
            var query = from n in _MemberRepository.Table                    
                        select n;
            return await Task.FromResult(query.ToList());
        }

        public Task<MessageReport> Login(AuthModel model, out Member user)
        {

            var result = new MessageReport(false, "Có lỗi xảy ra");

            try
            {
                //Kiểm tra username
                var objUser = GetByUsername(model.Username);
                if (objUser == null)
                {
                    user = null;
                    result = new MessageReport(false, "Tài khoản không tồn tại");
                    return Task.FromResult(result);
                }

                //Giải mã
                //var pass = CryptoHelper.DecryptPass_User(objUser.Password, objUser.PasswordSalat);
                var pass = objUser.Result.Password;

                // Check mật khẩu
                if (pass != model.Password.PasswordHashed(objUser.Result.PasswordSalat))
                {
                    user = null;
                    result = new MessageReport(false, "Mật khẩu không khớp");
                    return Task.FromResult(result);
                }
                //Gán lại user
                user = (Member)objUser.Result;
                result = new MessageReport(true, "Đăng nhập thành công");
            }
            catch (Exception ex)
            {

                user = null;
                result = new MessageReport(false, ex.Message);
            }
            return Task.FromResult(result);
        }

        private async Task<Member> GetByUsername(string username)
        {
            var query = from n in _MemberRepository.Table
                        where n.Username == username
                        select n;

            return await Task.FromResult(query.FirstOrDefault());
        }
    }
}
