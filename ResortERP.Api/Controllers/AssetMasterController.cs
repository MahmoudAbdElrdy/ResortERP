using ResortERP.Core.VM;
using ResortERP.Service;
using ResortERP.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using ResortERP.Core;
using ResortERP.Repository;

namespace ResortERP.Api.Controllers
{
    [Logger]
    public class AssetMasterController : ApiController
    {
        IAssetMasterService _AssetMasterService;
        IUserLogFileService userLogFileService;
        public AssetMasterController(IAssetMasterService AssetMasterService, IUserLogFileService userLogFileService)
        {
            this._AssetMasterService = AssetMasterService;
            this.userLogFileService = userLogFileService;
        }

        [Route("assetMaster/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await _AssetMasterService.CountAsync());
        }

        [Route("assetMaster/add")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]AssetMasterVM entity)
        {
            if (!_AssetMasterService.CheckNameAndCode(entity.ID, entity.ARName, entity.LatName, entity.Code))
            {
                var result = await _AssetMasterService.InsertAsync(entity);
                await LogData(entity.Code, result.ToString());
                if (result != 0)
                {
                    return Ok(true);
                }
            }
            return Ok(false);
        }
        [Route("assetMaster/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]AssetMasterVM entity)
        {
            if (!_AssetMasterService.CheckNameAndCode(entity.ID, entity.ARName, entity.LatName, entity.Code))
            {
                var result = await _AssetMasterService.UpdateAsync(entity);
                await LogData(entity.Code, entity.ID.ToString());
                return Ok(result);
            }
            else
            {
                return Ok(false);
            }
        }

        [Route("assetMaster/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]AssetMasterVM entity)
        {
            if (await _AssetMasterService.CheckAssetMasterForDeletion(entity.ID))
            {
                var result = await _AssetMasterService.DeleteAsync(entity);
                await LogData(entity.Code, entity.ID.ToString());
                return Ok(result);
            }
            else
            {
                return Ok(false);
            }
        }

        [Route("assetMaster/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await _AssetMasterService.GetAllAsync(pageNum, pageSize));
        }
        
        [Route("assetMaster/getAssetDepreciationDetails")]
        [HttpGet]
        public async Task<IHttpActionResult> getAssetDepreciationDetails(int AssetMasterId)
        {
            return Ok(await _AssetMasterService.getAssetDepreciationDetails(AssetMasterId));
        }

        [Route("assetMaster/getAssetTypeList")]
        [HttpGet]
        public async Task<IHttpActionResult> getAssetTypeList()
        {
            return Ok(await _AssetMasterService.getAssetTypeList());
        }

        [Route("assetMaster/getAssetGroupList")]
        [HttpGet]
        public async Task<IHttpActionResult> getAssetGroupList()
        {
            return Ok(await _AssetMasterService.getAssetGroupList());
        }

        [Route("assetMaster/getAssetStatusList")]
        [HttpGet]
        public async Task<IHttpActionResult> getAssetStatusList()
        {
            return Ok(await _AssetMasterService.getAssetStatusList());
        }

        [Route("assetMaster/getOriginNationList")]
        [HttpGet]
        public async Task<IHttpActionResult> getOriginNationList()
        {
            return Ok(await _AssetMasterService.getOriginNationList());
        }

        [Route("assetMaster/getCompanyList")]
        [HttpGet]
        public async Task<IHttpActionResult> getCompanyList()
        {
            return Ok(await _AssetMasterService.getCompanyList());
        }

        [Route("assetMaster/getAssetDepreciationTypeList")]
        [HttpGet]
        public async Task<IHttpActionResult> getAssetDepreciationTypeList()
        {
            return Ok(await _AssetMasterService.getAssetDepreciationTypeList());
        }

        [Route("assetMaster/getAssetLifeSpanUnitList")]
        [HttpGet]
        public async Task<IHttpActionResult> getAssetLifeSpanUnitList()
        {
            return Ok(await _AssetMasterService.getAssetLifeSpanUnitList());
        }

        [Route("assetMaster/getCurrencyList")]
        [HttpGet]
        public async Task<IHttpActionResult> getCurrencyList()
        {
            return Ok(await _AssetMasterService.getCurrencyList());
        }

        [Route("assetMaster/getAccountList")]
        [HttpGet]
        public async Task<IHttpActionResult> getAccountList()
        {
            return Ok(await _AssetMasterService.getAccountList());
        }
        
        [Route("assetMaster/getDepartmentList")]
        [HttpGet]
        public async Task<IHttpActionResult> getDepartmentList()
        {
            return Ok(await _AssetMasterService.getDepartmentList());
        }

        [Route("assetMaster/getLastCode")]
        [HttpGet]
        public string GetLastCode()
        {
            return _AssetMasterService.GetLastCode();
        }

        public async Task<bool> LogData(string code = null, string id = null)
        {
            Logger logger = new Logger();
            await userLogFileService.Insert(logger.LogDataMethod(code, id));
            return true;
        }
    }
}
