using ResortERP.Core.VM;
using ResortERP.Repository;
using ResortERP.Service.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace ResortERP.Api.Controllers
{
    [Logger]
    public class TelephoneCatController : ApiController
    {
        ITelephoneCatService teleCatService;
        IUserLogFileService _userLogFileService;
        public TelephoneCatController(ITelephoneCatService teleCatService, IUserLogFileService _userLogFileService)
        {
            this.teleCatService = teleCatService;
            this._userLogFileService = _userLogFileService;
        }

        [Route("teleCat/insert")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]TelephoneCatVM entity)
        {
            var result = await teleCatService.InsertAsync(entity);
            await LogData(null,result.ToString());
            if (result != 0)
            {
                return Ok(true);
            }
            return Ok(false);
        }
        [Route("teleCat/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]TelephoneCatVM entity)
        {
            var result = await teleCatService.UpdateAsync(entity);
            await LogData(null,entity.TELE_CAT_ID.ToString());
            return Ok(result);
        }
        [Route("teleCat/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]TelephoneCatVM entity)
        {
            var result = await teleCatService.DeleteAsync(entity);
            await LogData(null,entity.TELE_CAT_ID.ToString());
            return Ok(result);
        }
        [Route("teleCat/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await teleCatService.GetAllAsync(pageNum, pageSize));
        }

        [Route("teleCat/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await teleCatService.CountAsync());
        }
        [Route("teleCat/getAll")]
        [HttpGet]
        public List<TelephoneCatVM> getAll()
        {
            return teleCatService.GetAll();
        }



        [Route("telephoneCat/getByID")]
        [HttpGet]
        public TelephoneCatVM getByID(int typeID)
        {
            return teleCatService.GetByID(typeID);
        }

        public async Task<bool> LogData(string code = null,string id=null)
        {
            Logger logger = new Logger();
            await _userLogFileService.Insert(logger.LogDataMethod(code,id));
            return true;
        }
    }
}
