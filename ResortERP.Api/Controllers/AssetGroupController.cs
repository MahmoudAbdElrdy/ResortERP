using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ResortERP.Service.IServices;
using ResortERP.Core.VM;
using System.Threading.Tasks;
using ResortERP.Repository;
using ResortERP.Core;

namespace ResortERP.Api.Controllers
{
    [Logger]
    public class AssetGroupController : ApiController
    {
        IAssetGroupService assetGroupService;
        IUserLogFileService userLogFileService;
        public AssetGroupController(IAssetGroupService _assetGroupService, IUserLogFileService userLogFileService)
        {
            this.assetGroupService = _assetGroupService;
            this.userLogFileService = userLogFileService;
        }

        [Route("assetGroup/getPaging")]
        [HttpGet]
        public async Task<IHttpActionResult> getAllAssetGroup(int pageNum, int pageSize)
        {
            return Ok(await assetGroupService.getAssetGroupAsync(pageNum, pageSize));
        }

        [Route("assetGroup/AssetGroupsList")]
        [HttpGet]
        public async Task<IHttpActionResult> AssetGroupsList()
        {
            return Ok(await assetGroupService.getAll());
        }


        [Route("assetGroup/getById")]
        [HttpGet]
        public async Task<IHttpActionResult> getById(int ID)
        {
            return Ok(await assetGroupService.getById(ID));
        }

        [Route("assetGroup/count")]
        [HttpGet]
        public async Task<IHttpActionResult> countBanks()
        {
            return Ok(await assetGroupService.countGroupAssetAsync());
        }


        [Route("assetGroup/insert")]
        [HttpPost]
        public async Task<IHttpActionResult> insertBank(AssetGroupVM assetGroup)
        {
            var result = await assetGroupService.insertAssetGroupSync(assetGroup);
            await LogData(assetGroup.Code.ToString(), result.ToString());
            if (result != 0)
            {
                return Ok(true);
            }
            return Ok(false);
  
        }

        [Route("assetGroup/update")]
        [HttpPost]
        public async Task<IHttpActionResult> updateBank(AssetGroupVM assetGroup)
        {
            var result = await assetGroupService.updateAssetGroupSync(assetGroup);
            await LogData(assetGroup.Code.ToString(),assetGroup.ID.ToString());
            return Ok(result);

        }

        [Route("assetGroup/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> deleteBank(AssetGroupVM assetGroup)
        {
            var result = await assetGroupService.deleteAssetGroupSync(assetGroup);
            await LogData(assetGroup.Code.ToString(), assetGroup.ID.ToString());
            return Ok(result);

        }



        [Route("assetGroup/getLastCode")]
        [HttpGet]
        public string GetLastCode()
        {
            return assetGroupService.GetLastCode();
        }



        [Route("assetGroup/getAll")]
        [HttpGet]
        public async Task<IHttpActionResult> getAll(int pageNum, int pageSize)
        {
            return Ok(await assetGroupService.getAssetGroupAsync(pageNum, pageSize));
        }


        public async Task<bool> LogData(string code = null, string id = null)
        {
            Logger logger = new Logger();
            await userLogFileService.Insert(logger.LogDataMethod(code,id));
            return true;
        }
    }
}
