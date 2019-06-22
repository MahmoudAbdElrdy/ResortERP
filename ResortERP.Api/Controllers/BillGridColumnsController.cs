using ResortERP.Core.VM;
using ResortERP.Repository;
using ResortERP.Service.IServices;
using System.Threading.Tasks;
using System.Web.Http;

namespace ResortERP.Api.Controllers
{
    [Logger]
    public class BillGridColumnsController : ApiController
    {
        IBillGridColumnsService billGridColumnsService;
        IUserLogFileService userLogFileService;
        public BillGridColumnsController(IBillGridColumnsService billGridColumnsService, IUserLogFileService userLogFileService)
        {
            this.billGridColumnsService = billGridColumnsService;
            this.userLogFileService = userLogFileService;
        }

        [Route("billGridColumns/insert")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]BILL_GRID_COLUMNSVM entity)
        {
            var result = await billGridColumnsService.InsertAsync(entity);
           //await LogData(null,result.ToString());
            if (result != 0)
            {
                return Ok(true);
            }
            return Ok(false);
        }
        [Route("billGridColumns/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]BILL_GRID_COLUMNSVM entity)
        {
            var result = await billGridColumnsService.UpdateAsync(entity);
            //await LogData(null,entity.BILL_GRID_ID.ToString());
            return Ok(result);
        }
        [Route("billGridColumns/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]BILL_GRID_COLUMNSVM entity)
        {
            var result = await billGridColumnsService.DeleteAsync(entity);
            //await LogData(null,entity.BILL_GRID_ID.ToString());
            return Ok(result);
        }
        [Route("billGridColumns/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await billGridColumnsService.GetAllAsync(pageNum, pageSize));
        }

        [Route("billGridColumns/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await billGridColumnsService.CountAsync());
        }

        [Route("billGridColumns/getBysettingId")]
        [HttpGet]
        public async Task<IHttpActionResult> GetBySettingID(int settingID)
        {

            return Ok(await billGridColumnsService.GetBySettingID(settingID));
        }

        public async Task<bool> LogData(string code = null,string id=null,string notes=null)
        {
            Logger logger = new Logger();
            //await userLogFileService.Insert(logger.LogDataMethod(code,id,notes));
            return true;
        }

    }
}