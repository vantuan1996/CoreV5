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
    public class UserService : IUserService
    {
        private IUserRepository _UserRepository;
        private IUserRoleRepository _UserRoleRepository;
        public UserService(IUserRepository _UserRepository , IUserRoleRepository _UserRoleRepository)
        {
            this._UserRoleRepository = _UserRoleRepository;
            this._UserRepository = _UserRepository;
        }
        public async Task<MessageReport> Create(User model)
        {
            return await _UserRepository.Add(model);
        }

        public async Task<MessageReport> Delete(string id)
        {

            var result = new MessageReport(false, await LanguageHelper.GetLanguageText("MESSAGEREPORT:ERR"));

            var obj = GetById(id);
            if (obj.Result != null)
            {
                return await _UserRepository.Remove(obj.Result);
            }
            else
            {
                result = new MessageReport(false, await LanguageHelper.GetLanguageText("MESSAGEREPORT:NON_RECORD"));
            }

            return await Task.FromResult(result);
        }

        public async Task<List<User>> GetAll()
        {
            var query = from n in _UserRepository.Table
                        where n.Active == true
                        select n;
            return await Task.FromResult(query.ToList());

        }

        public Task<IEnumerable<User>> GetAllActiveByListId(string ids)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetById(string id)
        {
            return await _UserRepository.GetOneById(id);
        }

        public async Task<User> GetByUsername(string username)
      
        {
            var query = from n in _UserRepository.Table
                        where n.Username == username
                        select n;

            return await  Task.FromResult( query.FirstOrDefault());
        }

        public async Task<User> GetByUsername_notId(string username, string id)
        {
            var query = from n in _UserRepository.Table
                        where n.Username == username && n.Id != id
                        select n;

            return await Task.FromResult(query.FirstOrDefault());
        }

        public User_Submit GetCustomById(string id)
        {
            var model = new User_Submit();

            var obj = _UserRepository.GetOneById(id).Result;
            if (obj != null)
            {
                model = GetCustomByModel(obj);
            }

            return model;
        }

        public User_Submit GetCustomByModel(User model)
        {
            var obj = new User_Submit()
            {
                Id = model.Id,
                Active = model.Active,
                Name = model.Name,
                Roles = new List<string>(),
                Username = model.Username,
                isAdmin = model.Admin,
                Avatar = model.UserAvatar,
               
            };

            obj.Roles = (from n in _UserRoleRepository.Table
                         where n.UserId == model.Id
                         select n.RoleId).ToList();

            return obj;
        }

        public  async Task<GridModel<User>> GetPaging(string key, int pageNumber, int pageSize)
        {
            var sb = new StringBuilder();
            sb.AppendLine("SELECT * FROM (");
            sb.AppendLine("SELECT ROW_NUMBER () OVER ( ORDER BY [Name] desc) as RowNumber,a.*");
            sb.AppendLine("FROM(");
            sb.AppendLine("  select * from [User]");
            if (!string.IsNullOrEmpty(key))
            {
                sb.AppendLine(string.Format("WHERE ([Name] LIKE '%{0}%' OR [Username] LIKE '%{0}%')", key));
            }
            sb.AppendLine(")as a");
            sb.AppendLine(") as C1");
            //var model = GridModelHelper<User>.GetPage(pageList.ToList(), pageNumber, pageSize, pageList.TotalItemCount, pageList.PageCount);
            sb.AppendLine(string.Format("WHERE RowNumber BETWEEN (({0}-1) * {1} + 1) AND ({0} * {1})", pageNumber, pageSize));
            var listData = DatabaseHelper.ExcuteCommandToList<User>(sb.ToString());


            // Tính tổng
            sb.Clear();
            sb.AppendLine("SELECT COUNT(*) TotalCount");

            sb.AppendLine("FROM [User]");
            if (!string.IsNullOrEmpty(key))
            {
                sb.AppendLine(string.Format("WHERE ([Name] LIKE '%{0}%' OR [Username] LIKE '%{0}%')", key));
            }
            var _total = DatabaseHelper.ExcuteCommandToModel<TotalPagingModel>(sb.ToString());

            var model = GridModelHelper<User>.GetPage(listData, pageNumber, pageSize, _total.TotalCount);
            return await Task.FromResult(model);
        }

        public  Task<MessageReport> Login(AuthModel model, out User user)
        {


            var result = new MessageReport(false, "Có lỗi xảy ra");

            try
            {
                //Kiểm tra username
                var objUser =  GetByUsername(model.Username);
                if (objUser == null)
                {
                    user = null;
                    result = new MessageReport(false, "Tài khoản không tồn tại");
                    return  Task.FromResult(result);
                }

                //Giải mã
                //var pass = CryptoHelper.DecryptPass_User(objUser.Password, objUser.PasswordSalat);
                var pass = objUser.Result. Password;

                // Check mật khẩu
                if (pass != model.Password.PasswordHashed(objUser.Result. PasswordSalat))
                {
                    user = null;
                    result = new MessageReport(false, "Mật khẩu không khớp");
                    return  Task.FromResult(result);
                }
                //Gán lại user
                user = (User)objUser;
                result = new MessageReport(true, "Đăng nhập thành công");
            }
            catch (Exception ex)
            {

                user = null;
                result = new MessageReport(false, ex.Message);
            }
            return  Task.FromResult(result);
        }

        public async Task<MessageReport> SignIn(AuthModel_LowSecurity model)
        {
            var result = new MessageReport(false, "Có lỗi xảy ra");
            try
            {
                //Kiểm tra tk
                var objUser = await GetByUsername(model.Username);
                if (objUser == null)
                {
                    result = new MessageReport(false, "Tài khoản không tồn tại");
                    return await Task.FromResult(result);
                }
                //Kiểm tra trạng thái
                if (objUser.Active == false)
                {
                    result = new MessageReport(false, "Tài khoản đang bị khóa");
                    return await Task.FromResult(result);
                }
                var pass = model.Password.PasswordHashed(objUser.PasswordSalat);
                if (objUser.Password != pass)
                {
                    result = new MessageReport(false, "Mật khẩu không khớp");
                    return await Task.FromResult(result);
                }

                //Tạo token
                var token = ApiHelper.GenerateJSON_MobileToken(objUser.Id);

                result = new MessageReport(true, token);
            }
            catch (Exception ex)
            {

                result = new MessageReport(false, string.Format("Message: {0} - Details: {1}", ex.Message, ex.InnerException != null ? ex.InnerException.Message : ""));
            }
            return await Task.FromResult(result);
        }

        public async Task<MessageReport> Update(User model)
        {
            var result = new MessageReport(false, "Có lỗi xảy ra");
            try
            {
                //Kiểm tra tk
                var objUser = await GetByUsername(model.Username);
                if (objUser == null)
                {
                    result = new MessageReport(false, "Tài khoản không tồn tại");
                    return await Task.FromResult(result);
                }
                //Kiểm tra trạng thái
                if (objUser.Active == false)
                {
                    result = new MessageReport(false, "Tài khoản đang bị khóa");
                    return await Task.FromResult(result);
                }
                var pass = model.Password.PasswordHashed(objUser.PasswordSalat);
            }
            catch (Exception)
            {

                throw;
            }
            return await _UserRepository.Update(model);
        }

     
    }
}
