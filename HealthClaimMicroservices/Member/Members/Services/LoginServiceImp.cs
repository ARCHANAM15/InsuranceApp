using Members.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Members.Services
{
    public class LoginServiceImp:ILoginService
    {
        HealthCareDBContext db;

        private IConfiguration _config;

        public LoginServiceImp(IConfiguration config, HealthCareDBContext _db)
        {
            _config = config;
            db = _db;
        }

        public LoginTbl AuthenticateUser(LoginTbl login, bool IsRegister)
        {
            if (IsRegister)
            {
                db.LoginTbls.Add(login);
                db.SaveChanges();
                return login;
            }
            else
            {
                if (db.LoginTbls.Any(x => x.Email == login.Email && x.Password == login.Password))
                {
                    return db.LoginTbls.Where(x => x.Email == login.Email && x.Password == login.Password).FirstOrDefault();
                }
                else
                {
                    return null;
                }
            }
        }

        public string GenerateToken(LoginTbl login)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var token = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, login.Email),
                    new Claim(ClaimTypes.Role, login.UserType),
                   new Claim(ClaimTypes.Name, login.Id.ToString()),
                    new Claim(ClaimTypes.Name, login.UserName),
                }),
                Expires = DateTime.Now.AddMinutes(120),
                SigningCredentials = credentials
            };
            var TokenHandler = new JwtSecurityTokenHandler();
            var tokenGenerated = TokenHandler.CreateToken(token);
            return TokenHandler.WriteToken(tokenGenerated).ToString();
        }

        public async Task<string> Login(LoginTbl user, bool Isregister)
        {
            try
            {
                var userdata = AuthenticateUser(user, Isregister);
                if (user != null)
                {
                    var tokenString = GenerateToken(userdata);
                    return tokenString;
                }
                else
                {
                    return "Invalid data";
                }
            }
            catch (Exception ex)
            {
                return "Something went wrong";
            }
        }

        public async Task<string> Register(LoginTbl user, bool Isregister)
        {
            string str = string.Empty;
            var tokenString = str;
            dynamic userdata = null;
            try
            {

                var member = db.LoginTbls?.Where(x => x.Email == user.Email).FirstOrDefault();
                if (member != null)
                {
                    tokenString = "already";
                }
                else
                {
                    if (user != null)
                    {
                        registrationData registrationData = new registrationData();
                        registrationData.UserName = user.UserName;
                        registrationData.Email = user.Email;
                        registrationData.Password = user.Password;
                        registrationData.UserType = user.UserType;
                        userdata = AuthenticateUser(user, Isregister);

                    }
                    if (userdata != null)
                    {
                        tokenString = GenerateToken(userdata);
                        return tokenString;
                    }
                }
                return tokenString;


            }
            catch (Exception ex)
            {
                return "Something went wrong";
            }
        }
        public dynamic UserData(UserDataModel userDataModel)
        {
            dynamic data= db.LoginTbls?.Where(x => x.Email == userDataModel.Email).FirstOrDefault();
            return data;
        }
    }
}
