using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.IRepositories;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using News.Common;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace News.Controllers
{
   
    public class UserController : BaseController
    {
        ItblUserRepository _ItblUserRepository;
        public UserController(ItblUserRepository _tblUserRepository,HttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _ItblUserRepository = _tblUserRepository;
        }

        #region"signup"

        [AllowAnonymous]
        [IgnoreAntiforgeryToken]
        [HttpPost]
        public IActionResult Signup([FromBody]tblUser model)
        {
            try
            {

                model.Email = model.Email.Trim().ToLower();
                model.Password = model.Password.Trim();
                string password = model.Password;
                tblUser _tblUser = _ItblUserRepository.Get(x => x.Email.ToLower() == model.Email).FirstOrDefault();
                if (_tblUser != null)
                {



                    return BadRequest("Email already exists");
                }
                else
                {

                    model.Password = EncryptUtil.EncryptString(model.Password);
                    model.IsActive = true;
                    model.CreatedDate = DateTime.Now;
                    _ItblUserRepository.Add(model);
                    model.Password = password;
                }

                return Ok(model);
            }
            catch (Exception ex)
            {
                log.Fatal("signup:", ex);
                return BadRequest(ex);
            }
        }
        #endregion
    }
}
