using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_Core.Models;
using Test_Library.Models;
using Test_Model.Models;

namespace Test_Service.Admin
{
    public interface IMemberService
    {
        Task<List<Member>> GetAll();
        Task<MessageReport> Login(AuthModel model, out Member member);
        Task<MessageReport> Create(Member model);
    }
}
