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
    public class AssetCompanyDetailsController : ApiController
    {
        IAssetCompanyDetailsService AssetCompanyDetailsService;
        IItemsService itemsService;
        IUnit_ItemsService AssetCompanyDetailsItems;
        IUserLogFileService userLogFileService;
        public AssetCompanyDetailsController(IAssetCompanyDetailsService AssetCompanyDetailsService, IItemsService itemsService, IUnit_ItemsService AssetCompanyDetailsItems, IUserLogFileService userLogFileService)
        {
            this.AssetCompanyDetailsService = AssetCompanyDetailsService;
            this.itemsService = itemsService;
            this.AssetCompanyDetailsItems = AssetCompanyDetailsItems;
            this.userLogFileService = userLogFileService;
        }

        [Route("AssetCompanyDetails/insert")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]AssetCompanyDetailsVM entity)
        {
            var result = await AssetCompanyDetailsService.InsertAsync(entity);
            await LogData(entity.Code,result.ToString());
            if (result != 0)
            {
                return Ok(true);
            }
            return Ok(false);
        }
        [Route("AssetCompanyDetails/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]AssetCompanyDetailsVM entity)
        {
            var result = await AssetCompanyDetailsService.UpdateAsync(entity);
            await LogData(entity.Code, entity.ID.ToString());
            return Ok(result);
        }
        [Route("AssetCompanyDetails/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]AssetCompanyDetailsVM entity)
        {
                var result = await AssetCompanyDetailsService.DeleteAsync(entity);
                await LogData(entity.Code, entity.ID.ToString());
                return Ok(result);

            //return Ok(await AssetCompanyDetailsService.DeleteAsync(entity));
        }
        [Route("AssetCompanyDetails/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await AssetCompanyDetailsService.GetAllAsync(pageNum, pageSize));
        }

        [Route("AssetCompanyDetails/getById")]
        [HttpGet]
        public async Task<IHttpActionResult> getById(int Id)
        {
            return Ok(await AssetCompanyDetailsService.getById(Id));
        }

        [Route("AssetCompanyDetails/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await AssetCompanyDetailsService.CountAsync());
        }


        [Route("AssetCompanyDetails/getLastCode")]
        [HttpGet]
        public string GetLastCode()
        {
            return AssetCompanyDetailsService.GetLastCode();
        }

        public async Task<bool> LogData(string code=null,string id=null)
        {
            Logger logger = new Logger();
            await userLogFileService.Insert(logger.LogDataMethod(code,id));
            return true;
        }
    }
}