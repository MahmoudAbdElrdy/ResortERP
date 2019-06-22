using ResortERP.Core.VM;
using ResortERP.Repository;
using ResortERP.Service;
using ResortERP.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ResortERP.Api.Controllers
{
    [Logger]
    public class TelephoneTypeController : ApiController
    {
        ITelephoneTypeService teleTypeService;
        IUserLogFileService _userLogFileService;
        public TelephoneTypeController(ITelephoneTypeService teleTypeService, IUserLogFileService _userLogFileService)
        {
            this.teleTypeService = teleTypeService;
            this._userLogFileService = _userLogFileService;
        }

        [Route("telephoneType/insert")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]TelephoneTypesVM entity)
        {
            var result = await teleTypeService.InsertAsync(entity);
            await LogData(null,result.ToString());
            if (result != 0)
            {
                return Ok(true);
            }
            return Ok(false);
        }
        [Route("telephoneType/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]TelephoneTypesVM entity)
        {
            var result = await teleTypeService.UpdateAsync(entity);
            await LogData(null,entity.TELE_TYPE_ID.ToString());
            return Ok(result);
        }
        [Route("telephoneType/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]TelephoneTypesVM entity)
        {
            var result = await teleTypeService.DeleteAsync(entity);
            await LogData(null,entity.TELE_TYPE_ID.ToString());
            return Ok(result);
        }
        [Route("telephoneType/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await teleTypeService.GetAllAsync(pageNum, pageSize));
        }

        [Route("telephoneType/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await teleTypeService.CountAsync());
        }
        [Route("telephoneType/getAll")]
        [HttpGet]
        public List<TelephoneTypesVM> getAll()
        {
            return teleTypeService.GetAll();
        }
        public async Task<bool> LogData(string code = null,string id=null)
        {
            Logger logger = new Logger();
            await _userLogFileService.Insert(logger.LogDataMethod(code,id));
            return true;
        }
        //[Route("telephoneType/getByID")]
        //[HttpGet]
        //public TelephoneTypesVM getByID( int typeID)
        //{
        //    return teleTypeService.GetByID(typeID);
        //}
    }
}
