using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.IRepositories;
using DataAccess.Models;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using News.ViewModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace News.Controllers
{
 
    public class NewsController : BaseController
    {
        #region"Field"
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private ItblNewsRepository _ItblNewsRepository;
        private ItblNewsSubscribeRepository _ItblNewsSubscribeRepository;
        private ItblChannelSubscribeRepository _ItblChannelSubscribeRepository;
        #endregion
        #region"Constructor"
        public NewsController(ItblChannelSubscribeRepository _tblChannelSubscribeRepository,ItblNewsSubscribeRepository _tblNewsSubscribeRepository,ItblNewsRepository _tblNewsRepository,HttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _ItblNewsRepository = _tblNewsRepository;
            _ItblNewsSubscribeRepository = _tblNewsSubscribeRepository;
            _ItblChannelSubscribeRepository = _tblChannelSubscribeRepository;
        }
        #endregion
        #region "Get News"
        [HttpGet]
        [ActionName("GetNews")]
        public IActionResult GetNews(int PKNews)
        {
            try
            {
                tblNews _tblNews = _ItblNewsRepository.GetSingle(x => x.PKNews == PKNews);
                return Ok(_tblNews);
            }
            catch(Exception ex)
            {
                log.Fatal("GetNews:", ex);
                return BadRequest(ex.ToString());
            }

        }

        [HttpPost]
        [ActionName("GetNews")]
        public IActionResult GetNews([FromBody]NewsSearchModel model)
        {
            try
            {
                ArrayList result = new ArrayList();
                if (model.search == null)
                {
                    model.search = "";
                }
                if (model.index < 1)
                {
                    model.index = 1;
                }
                if (model.pageSize <6)
                {
                    model.pageSize = 6;
                }
                IQueryable<tblNews> searchResult = _ItblNewsRepository.Get(x =>x.IsActive==true&&( x.Title.Contains(model.search) || x.Description.Contains(model.search) || x.Content.Contains(model.search)));
                if (model.fkChannel != null && model.fkChannel > 0)
                {
                    searchResult = searchResult.Where(w => w.FKChannel == model.fkChannel);
                }
               
                List<int?>newsIDs=   _ItblNewsSubscribeRepository.Get(x => x.IsActive==true && x.FKUser == model.PKUser).Select(s => s.FKNews).ToList();
                if (model.isSubscribe == true)
                {
                    searchResult = searchResult.Where(o=> newsIDs.Contains( o.PKNews));
                }
                long total= searchResult.Count();
                var _NewsList = searchResult.Skip((model.index-1)*model.pageSize).Take(model.pageSize).Select(s=> new{
                    s.PKNews,
                    s.Author,
                    s.Title,
                    s.Description,
                    s.Content,
                    s.FKChannel,
                    ChannelName= s.tblChannel.ChannelName,
                    IsSubscribeChannel= s.tblChannel.tblChannelSubscribe.Count(c=>c.FKUser==model.PKUser&&c.IsActive==true)>0,
                    s.ImageURL,
                    s.CreatedDate,
                    IsSubscribe= newsIDs.Contains(s.PKNews)
                }).ToList();
                result.Add(new
                {
                    Total = total,
                    NewsList = _NewsList
                });
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Fatal("GetNews:", ex);
                return BadRequest(ex.ToString());
            }

        }

        #endregion
        #region"Subscribe News"
        [HttpPost]
        [ActionName("SubscribeNews")]
        public IActionResult SubscribeNews([FromBody]tblNewsSubscribe model)
        {
            try
            {
                tblNewsSubscribe _tblNewsSubscribe = _ItblNewsSubscribeRepository.Get(x => x.FKNews == model.FKNews && x.FKUser == model.FKUser && x.IsActive == true).FirstOrDefault();
                if (_tblNewsSubscribe == null)
                {
                    model.IsActive = true;
                    model.CreatedBy = Convert.ToString(model.FKUser);
                    model.CreatedDate = DateTime.Now;
                    _ItblNewsSubscribeRepository.Add(model);
                }
                else
                {
                    _tblNewsSubscribe.IsActive = false;
                    _tblNewsSubscribe.ModifiedBy = Convert.ToString( model.FKUser);
                    _tblNewsSubscribe.ModifiedDate = DateTime.Now;
                    _ItblNewsSubscribeRepository.Update(_tblNewsSubscribe);
                    model = _tblNewsSubscribe;
                }
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());

            }
        }
        [NonAction]
        public bool IsSubscribeChannel(int? PKChannel=0,int? PKUser=0)
        {
            try
            {
                bool isSubscribe = false;
                tblChannelSubscribe _tblChannelSubscribe = _ItblChannelSubscribeRepository.Get(x => x.FKUser == PKUser && x.FKChannel == PKUser && x.IsActive == true).FirstOrDefault();
                if (_tblChannelSubscribe != null)
                {
                    isSubscribe = true;
                }
                return isSubscribe;
            }
            catch(Exception ex)
            {
                log.Fatal("IsSubscribeChannel", ex);
                throw ex;
            }
        }
                #endregion


            }
}
