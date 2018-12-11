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
   
    public class PermissionController : BaseController
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
        public PermissionController(ItblUserLogRepository _tblUserLogRepository,ItblUserRoleRepository _tblUserRoleRepository,ItblUserRepository _tblUserRepository,ItblRolePageRepository _tblRolePageRepository,HttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _ItblUserRepository = _tblUserRepository;
            _ItblUserRoleRepository = _tblUserRoleRepository;
            _ItblRolePageRepository = _tblRolePageRepository;
            _ItblUserLogRepository = _tblUserLogRepository;
        }
        #endregion
        #region"Get Permission By Role"
        [HttpGet]
        [ActionName("GetPermission")]
        public bool GetPermission(string PageName, int FKRole)
        {
            try
            {
                bool retVal = false;
                if (PageName.ToUpper().Equals("SKIP"))
                {
                    retVal = true;
                }
                else
                {                 
                  
                    var result = _ItblRolePageRepository.Get(x => x.FKRole == FKRole && x.IsActive == true&&x.tblPage.PageName==PageName).FirstOrDefault();

                    if (result != null)
                    {
                        retVal = true;
                    }                
            }
                return retVal;
            }
            catch (Exception ex)
            {
                log.Fatal("GetPermission:" + ex);
                return false;
            }
        }

        #endregion
        
    }
}
