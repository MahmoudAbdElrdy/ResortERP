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
    public class AssetOperationMasterController : ApiController
    {
        IAssetOperationMasterService _AssetOperationMasterService;
        IUserLogFileService userLogFileService;
        public AssetOperationMasterController(IAssetOperationMasterService AssetOperationMasterService, IUserLogFileService userLogFileService)
        {
            this._AssetOperationMasterService = AssetOperationMasterService;
            this.userLogFileService = userLogFileService;
        }

        [Route("AssetOperationMaster/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count(int operationType)
        {
            return Ok(await _AssetOperationMasterService.CountAsync(operationType));
        }

        [Route("AssetOperationMaster/add")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]AssetOperationMasterVM entity)
        {
            if (!_AssetOperationMasterService.CheckNameAndCode(entity.ID, entity.Number, entity.Code))
            {
                var result = await _AssetOperationMasterService.InsertAsync(entity);
                await LogData(entity.Code, result.ToString());
                if (result != 0)
                {
                    return Ok(result);
                }
            }
            return Ok(false);
        }
        [Route("AssetOperationMaster/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]AssetOperationMasterVM entity)
        {
            if (!_AssetOperationMasterService.CheckNameAndCode(entity.ID, entity.Number, entity.Code))
            {
                var result = await _AssetOperationMasterService.UpdateAsync(entity);
                await LogData(entity.Code, entity.ID.ToString());
                return Ok(result);
            }
            else
            {
                return Ok(false);
            }
        }

        [Route("AssetOperationMaster/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]AssetOperationMasterVM entity)
        {
            if (await _AssetOperationMasterService.CheckOperationForDeletion(entity.ID))
            {
                var result = await _AssetOperationMasterService.DeleteAsync(entity);
                await LogData(entity.Code, entity.ID.ToString());
                return Ok(result);
            }
            else
            {
                return Ok(false);
            }
        }

        [Route("AssetOperationMaster/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize, int operationType)
        {
            return Ok(await _AssetOperationMasterService.GetAllAsync(pageNum, pageSize, operationType));
        }

        [Route("AssetOperationMaster/getEmployeeList")]
        [HttpGet]
        public async Task<IHttpActionResult> getEmployeeList()
        {
            return Ok(await _AssetOperationMasterService.getEmployeeList());
        }
        
        [Route("AssetOperationMaster/getEmployeeAssets")]
        [HttpGet]
        public async Task<IHttpActionResult> getEmployeeAssets(int employeeId)
        {
            return Ok(await _AssetOperationMasterService.getEmployeeAssets(employeeId));
        }

        [Route("AssetOperationMaster/getAssetMasterList")]
        [HttpGet]
        public async Task<IHttpActionResult> getAssetMasterList()
        {
            return Ok(await _AssetOperationMasterService.getAssetMasterList());
        }

        [Route("AssetOperationMaster/getCostCenterList")]
        [HttpGet]
        public async Task<IHttpActionResult> getCostCenterList()
        {
            return Ok(await _AssetOperationMasterService.getCostCenterList());
        }

        [Route("AssetOperationMaster/getDepartmentList")]
        [HttpGet]
        public async Task<IHttpActionResult> getDepartmentList()
        {
            return Ok(await _AssetOperationMasterService.getDepartmentList());
        }

        [Route("AssetOperationMaster/getCompanyList")]
        [HttpGet]
        public async Task<IHttpActionResult> getCompanyList()
        {
            return Ok(await _AssetOperationMasterService.getCompanyList());
        }

        [Route("AssetOperationMaster/getCurrencyList")]
        [HttpGet]
        public async Task<IHttpActionResult> getCurrencyList()
        {
            return Ok(await _AssetOperationMasterService.getCurrencyList());
        }

        [Route("AssetOperationMaster/getAccountList")]
        [HttpGet]
        public async Task<IHttpActionResult> getAccountList()
        {
            return Ok(await _AssetOperationMasterService.getAccountList());
        }

        [Route("AssetOperationMaster/getLastCode")]
        [HttpGet]
        public string GetLastCode()
        {
            return _AssetOperationMasterService.GetLastCode();
        }

        public async Task<bool> LogData(string code = null, string id = null)
        {
            Logger logger = new Logger();
            await userLogFileService.Insert(logger.LogDataMethod(code, id));
            return true;
        }
    }
}
