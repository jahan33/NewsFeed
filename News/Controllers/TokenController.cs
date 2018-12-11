using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DataAccess.IRepositories;
using DataAccess.Models;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using News.Common;
using News.ViewModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace News.Controllers
{
    [Produces("application/json")]
    [Route("Token")]
    public class TokenController : BaseController
    {
        #region"Field"
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private ItblRolePageRepository _ItblRolePageRepository;        
        private ItblUserRepository _ItblUserRepository;
        private ItblUserRoleRepository _ItblUserRoleRepository;
        private ItblUserLogRepository _ItblUserLogRepository;
        private List<tblUserLog> lastLogin = new List<tblUserLog>();
        #endregion
        #region"Constructor"
        public TokenController(ItblUserLogRepository _tblUserLogRepository, ItblUserRoleRepository _tblUserRoleRepository, ItblUserRepository _tblUserRepository, ItblRolePageRepository _tblRolePageRepository, HttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _ItblUserRepository = _tblUserRepository;
            _ItblUserRoleRepository = _tblUserRoleRepository;
            _ItblRolePageRepository = _tblRolePageRepository;
            _ItblUserLogRepository = _tblUserLogRepository;
        }
        #endregion
        #region "Login"
       
        [AllowAnonymous]
        [IgnoreAntiforgeryToken]
        [HttpPost]
        public IActionResult CreateToken(LoginModel loginModel)
        {
            try
            {
                IActionResult response = Unauthorized();
                var user = Authenticate(loginModel);
                if (user.Users != null)
                {
                    int currentRole = 0;
                    List<tblPage> menu = new List<tblPage>();
                    var roles = _ItblUserRoleRepository.Get(x => x.IsActive == true && x.FKUser == user.Users.PKUser).Select(s => s.tblRole).Where(w => w.IsActive == true)
                        //.OrderBy(s => s.Priority)
                        .ToList();
                    if (roles != null && roles.Count > 0)
                    {
                        var DefaultRole = _ItblUserRoleRepository.Get(x => x.FKUser == user.Users.PKUser && x.IsActive == true && x.IsActive == true).Select(s => s.tblRole).Where(w => w.IsActive == true).FirstOrDefault();
                        if (DefaultRole != null)
                        {
                            currentRole = DefaultRole.PKRole;
                        }
                        else
                        {
                            currentRole = roles[0].PKRole;
                        }


                        //   menu = _loginRepository.GetMenuByRole(currentRole, user.Users.Default_Module_Seq);

                    }
                    var tokenString = BuildToken(user);
                    user.Password = null;
                    user.Users.Password = null;

                    response = Ok(new { token = tokenString, Roles = roles, CurrentRole = currentRole, User = user, Menu = menu, HeaderList = context.HttpContext.Items, FKUser = user.Users.PKUser });
                }
                else
                {

                    response = BadRequest(user.ErrorMessage);
                }

                return response;
            }
            catch (Exception ex)
            {
                log.Fatal("Token:", ex);
                return BadRequest(ex);
            }
        }


        private string BuildToken(LoginModel user)
        {

            var claims = new[] {
        new Claim(JwtRegisteredClaimNames.Sub, user.Users.FullName),
       // new Claim(JwtRegisteredClaimNames.Email, user.Users.Email),
        //new Claim(JwtRegisteredClaimNames.Birthdate, user.FKCompany.ToString()),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AccessKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(Issuer,
              Issuer,
              claims,
              expires: DateTime.Now.AddDays(1),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private LoginModel Authenticate(LoginModel model)
        {
            LoginModel user = null;
            user = loginUser(model);

            return user;
        }

        public LoginModel loginUser(LoginModel model)
        {
            LoginModel _LoginModel = new LoginModel();

            try
            {
                List<tblUserRole> _tblUserRole = new List<tblUserRole>();
                bool FirstLog = false;
                tblUser _tblUser = new tblUser();

                tblUserLog _tblUserLog = new tblUserLog();
                model.Username = model.Username.Trim();
                model.Password = model.Password.Trim();
                model.Password = EncryptUtil.EncryptString(model.Password);


                model.Username = model.Username.ToLower();



                _tblUser = _ItblUserRepository.Get(x => x.Email.ToLower() == model.Username && x.Password == model.Password && x.IsActive == true && x.IsBlocked == false).FirstOrDefault();


                if (_tblUser != null)
                {
                    _tblUser.tblUserRole = null;
                    _tblUser.InvalidAttempt = 0;
                    _ItblUserRepository.Update(_tblUser);
                    List<tblRole> _tblUserRole2 = _ItblUserRoleRepository.Get(x => x.FKUser == _tblUser.PKUser && x.IsActive == true).Select(s =>
                           s.tblRole
                         ).ToList();

                    foreach (var data in _tblUserRole2)
                    {
                        tblUserRole temp = new tblUserRole();
                        temp.tblRole = data;
                        temp.FKRole = data.PKRole;

                        _tblUserRole.Add(temp);
                    }


                    _tblUserLog.LoginSuccess = true;
                    _tblUserLog.FKUser = _tblUser.PKUser;

                    lastLogin = _ItblUserLogRepository.Get(x => x.UserName == _tblUser.Email && x.LoginSuccess == true).ToList();
                    if (lastLogin != null && lastLogin.Count > 0 && _tblUser.IsChangePassword == true)
                    {
                        FirstLog = true;

                    }
                    else
                    {
                        FirstLog = false;
                    }



                }

                else
                {

                    _tblUser = _ItblUserRepository.Get(x => x.Email.ToLower() == model.Username && x.Password == model.Password && x.IsActive == true && x.IsBlocked == true).FirstOrDefault();


                    if (_tblUser != null)
                    {
                        _LoginModel.ErrorMessage = "Your account has been blocked. Please contact to the administrator!";
                    }
                    else
                    {

                        _tblUser = _ItblUserRepository.Get(x => x.Email.ToLower() == model.Username).FirstOrDefault();


                        if (_tblUser != null)
                        {
                            _tblUser.tblUserRole = null;
                            _tblUserLog.FKUser = _tblUser.PKUser;
                            _tblUser.InvalidAttempt = _tblUser.InvalidAttempt + 1;
                            _ItblUserRepository.Update(_tblUser);
                        }
                        _tblUserLog.LoginSuccess = false;
                        _LoginModel.ErrorMessage = "Invalid Username Or Password";
                    }
                }


                _tblUserLog.UserName = model.Username;
                _tblUserLog.Password = model.Password;
                _tblUserLog.LoginTime = DateTime.Now;
                _tblUserLog.IPAddress = context.HttpContext.Connection.RemoteIpAddress.ToString();
                _tblUserLog.Origin = model.Origin;
                _tblUserLog.IsActive = true;
                _ItblUserLogRepository.Add(_tblUserLog);
                if (_tblUser != null && _tblUser.InvalidAttempt > 3)
                {
                    tblUser _tblUser1 = new tblUser();

                    _tblUser1 = _ItblUserRepository.Get(x => x.Email.ToLower() == model.Username).FirstOrDefault();



                    if (_tblUser1 != null)
                    {
                        _tblUser1.IsBlocked = true;
                        _tblUser.IsBlocked = true;
                        _tblUser.tblUserRole = null;
                        _ItblUserRepository.Update(_tblUser1);
                    }


                }

                if (_tblUser != null)
                {
                    _tblUser.IsActive = FirstLog;
                    _tblUser.tblUserRole = _tblUserRole;
                    if (_tblUserLog.LoginSuccess == false)
                    {
                        _tblUser = null;
                    }
                    else
                    {
                        _tblUser.Password = null;
                    }
                }

                _LoginModel.Users = _tblUser;
                if (_LoginModel.ErrorMessage != null && _LoginModel.ErrorMessage.Length > 0)
                {
                    _LoginModel.Users = null;
                }
                return _LoginModel;

            }
            catch (Exception ex)
            {
                _LoginModel.ErrorMessage = ex.Message;
                log.Fatal("login:", ex);
                throw ex;
            }
        }

        #endregion
    }
}
