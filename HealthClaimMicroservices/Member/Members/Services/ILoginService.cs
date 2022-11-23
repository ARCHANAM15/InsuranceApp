using Members.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Members.Services
{
    public interface ILoginService
    {
        Task<string> Login(LoginTbl login, bool Isregister);
        Task<string> Register(LoginTbl login, bool Isregister);
        LoginTbl AuthenticateUser(LoginTbl login, bool IsRegister);
        string GenerateToken(LoginTbl login);
        dynamic UserData(UserDataModel userDataModel);
    }
}
