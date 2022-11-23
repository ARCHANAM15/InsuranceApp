using Members.Models;
using Members.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Members.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        ILoginService loginService;
        public LoginController(ILoginService _loginService)
        {
            loginService = _loginService;
        }
        [HttpPost]
        [Route("login-user")]
        public IActionResult Login(LoginTbl user)
        {
            try
            {
                
                IActionResult response = Unauthorized();

                var userdata = loginService.AuthenticateUser(user, false);
                if (user != null)
                {
                    var tokenString = loginService.GenerateToken(userdata);
                    response = Ok(new { token = tokenString, role = userdata.UserType });
                }
                return response;
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpPost]
        [Route("register-user")]
        public IActionResult Register(LoginTbl user)
        {
            try
            {

                IActionResult response = Unauthorized();
                var userdata = loginService.Register(user, true);
                if (user != null)
                {
                    response = Ok(new { token = userdata.Result, role = user.UserType, userName = user.UserName });
                }
                return response;
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }

        }

        [HttpPost]
        [Route("user-data")]
        public IActionResult userData([FromBody] UserDataModel user)
        {
            try
            {
                dynamic data = loginService.UserData(user);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }

        }
        
            
    }
    

   
        

}


