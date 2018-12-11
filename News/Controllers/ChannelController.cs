using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.IRepositories;
using DataAccess.Models;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using News.ViewModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace News.Controllers
{

    public class ChannelController : BaseController
    {
        #region"Field"
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private ItblChannelRepository _ItblChannelRepository;
        private ItblChannelSubscribeRepository _ItblChannelSubscribeRepository;
        #endregion
        #region"Constructor"
        public ChannelController(ItblChannelSubscribeRepository _tblChannelSubscribeRepository,ItblChannelRepository _tblChannelRepository, HttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _ItblChannelRepository = _tblChannelRepository;
            _ItblChannelSubscribeRepository = _tblChannelSubscribeRepository;
        }
        #endregion
        #region "Get Channel"
        [HttpGet]
        [ActionName("GetChannel")]
        public IActionResult GetChannel(int PKChannel)
        {
            try
            {
                tblChannel _tblChannel = _ItblChannelRepository.GetSingle(x => x.PKChannel == PKChannel);
                return Ok(_tblChannel);
            }
            catch (Exception ex)
            {
                log.Fatal("GetChannel:", ex);
                return BadRequest(ex.ToString());
            }

        }

        [HttpPost]
        [ActionName("GetChannel")]
        public IActionResult GetChannel([FromBody]NewsSearchModel model)
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
                if (model.pageSize < 1)
                {
                    model.pageSize = 6;
                }
                IQueryable<tblChannel> searchResult = _ItblChannelRepository.Get(x => x.IsActive == true && (x.ChannelName.Contains(model.search) || x.ChannelDescription.Contains(model.search)));
               
                long total = searchResult.Count();
                List<tblChannel> _ChannelList = searchResult.Skip((model.index - 1) * model.pageSize).Take(model.pageSize).ToList();
                result.Add(new
                {
                    Total = total,
                    ChannelList = _ChannelList
                });
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Fatal("GetChannel:", ex);
                return BadRequest(ex.ToString());
            }

        }
        [HttpGet]
        [ActionName("GetAllChannel")]
        public IActionResult GetAllChannel(int PKUser)
        {
            try
            {


             List<int?> channelIDs=   _ItblChannelSubscribeRepository.Get(x => x.IsActive == true && x.FKUser == PKUser).Select(s => s.FKChannel).ToList();

               var _ChannelList = _ItblChannelRepository.Get(x => x.IsActive == true).Select(s=>new {
                   s.PKChannel,
                   s.ChannelName,
                   s.ChannelDescription,
                  IsSubscribe =channelIDs.Contains( s.PKChannel)
               }).ToList();
               
                return Ok(_ChannelList);
            }
            catch (Exception ex)
            {
                log.Fatal("GetAllChannel:", ex);
                return BadRequest(ex.ToString());
            }

        }
        #endregion
        #region"Subscribe News"
        [Authorize]
        [HttpPost]
        [ActionName("SubscribeChannel")]
        public IActionResult SubscribeChannel([FromBody]tblChannelSubscribe model)
        {
            try
            {
              
                    tblChannelSubscribe _tblChannelSubscribe = _ItblChannelSubscribeRepository.Get(x => x.FKChannel == model.FKChannel && x.FKUser == model.FKUser && x.IsActive == true).FirstOrDefault();
                    if (_tblChannelSubscribe == null)
                    {
                        model.IsActive = true;
                        model.CreatedBy = Convert.ToString(model.FKUser);
                        model.CreatedDate = DateTime.Now;
                        _ItblChannelSubscribeRepository.Add(model);
                    }
                    else
                    {
                        _tblChannelSubscribe.IsActive = false;
                        _tblChannelSubscribe.ModifiedBy = Convert.ToString(model.FKUser);
                        _tblChannelSubscribe.ModifiedDate = DateTime.Now;
                        _ItblChannelSubscribeRepository.Update(_tblChannelSubscribe);
                        model = _tblChannelSubscribe;
                    }
                    return Ok(model);
                }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());

            }
        }
        #endregion


    }
}
