using ResortERP.Core.VM;
using ResortERP.Repository;
using ResortERP.Service.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace ResortERP.Api.Controllers
{
    [Logger]
    public class TelephoneController : ApiController
    {
        ITelephoneService teleService;
        IUserLogFileService _userLogFileService;
        public TelephoneController(ITelephoneService teleService, IUserLogFileService _userLogFileService)
        {
            this.teleService = teleService;
            this._userLogFileService = _userLogFileService;
        }

        [Route("telephone/insert")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]TelephoneVM entity)
        {
            var result = await teleService.InsertAsync(entity);
            await LogData(null,result.ToString());
            if (result != 0)
            {
                return Ok(true);
            }
            return Ok(false);
        }

        [Route("telephone/insertList")]
        [HttpPost]
        public async Task<bool> addList([FromBody]List<TelephoneVM> entity)
        {
            var result =await teleService.InsertListAsync(entity);
            if (result == null)
            {
                return (false);
            }
            else
            {
                foreach(var item in result)
                {
                    await LogData(null,item.TELE_ID.ToString());
                }
             
            }
            return true;
        }


        [Route("telephone/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]TelephoneVM entity)
        {
            var result = await teleService.UpdateAsync(entity);
            await LogData(null,entity.TELE_ID.ToString());
            return Ok(result);
        }
        [Route("telephone/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]TelephoneVM entity)
        {
            var result = await teleService.DeleteAsync(entity);
            await LogData(null,entity.TELE_ID.ToString());
            return Ok(result);

        }


        [Route("telephone/deleteByTeleID")]
        [HttpPost]
        public async Task<bool> deleteByTeleID([FromBody]TelephoneVM entity)
        {
            var result = teleService.DeleteAllByTeleID(entity);
            await LogData(null,entity.TELE_ID.ToString());
            return result;

        }


        [Route("telephone/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await teleService.GetAllAsync(pageNum, pageSize));
        }

        [Route("telephone/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await teleService.CountAsync());
        }
        [Route("telephone/getAll")]
        [HttpGet]
        public List<TelephoneVM> getAll()
        {
            return teleService.GetAll();
        }
        [Route("telephone/getby")]
        [HttpGet]
        public List<TelephoneVM> getBy(int ContactID, int typeID)
        {
            return teleService.GetBy(ContactID, typeID);
        }

        [Route("telephone/getByType")]
        [HttpGet]
        public List<TelephoneVM> getBytype( int typeID)
        {
            return teleService.GetByType(typeID);
        }

        public async Task<bool> LogData(string code = null,string id=null)
        {
            Logger logger = new Logger();
            await _userLogFileService.Insert(logger.LogDataMethod(code,id));
            return true;
        }
    }
}
