using ResortERP.Core.VM;
using ResortERP.Repository;
using ResortERP.Service.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace ResortERP.Api.Controllers
{
    [Logger]
    public class EntryMasterController : ApiController
    {
        IEntryMasterService entryMasterService;
        IUserLogFileService userLogFileService;
        public EntryMasterController(IEntryMasterService entryMasterService, IUserLogFileService userLogFileService)
        {
            this.entryMasterService = entryMasterService;
            this.userLogFileService = userLogFileService;
        }

        [Route("entryMaster/insert")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]Entry_MasterVM entity)
        {
            var result = await entryMasterService.InsertAsync(entity);
            await LogData("?" + entity.ENTRY_SETTING_ID + "?" + result, result.ToString(), entity.ENTRY_NUMBER);
            if (result != 0)
            {
                return Ok(true);
            }
            return Ok(false);

        }


        [Route("entryMaster/InsertMasterDetails")]
        [HttpPost]
        public async Task<long> InsertEntryMasterDetails(EntryMasterDetails entity)
        {
            var result = entryMasterService.InsertEntryMasterDetails(entity);
            if (entity.EntryMaster.BILL_ID == null && entity.EntryMaster.ENTRY_SETTING_ID != 130)
            {
                if(entity.EntryMaster.IsLog != false)
                {
                    await LogData("?" + entity.EntryMaster.ENTRY_SETTING_ID + "?" + result, result.ToString(), entity.EntryMaster.ENTRY_NUMBER, entity.EntryMaster.EditReason);
                }
            }
                
            return result;
            // return (entryMasterService.InsertEntryMasterDetails(entity));
        }
        [Route("entryMaster/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]Entry_MasterVM entity)
        {
            var result = await entryMasterService.UpdateAsync(entity);
            await LogData("?" + entity.ENTRY_SETTING_ID + "?" + entity.ENTRY_ID, entity.ENTRY_ID.ToString(), entity.ENTRY_NUMBER, entity.EditReason);
            return Ok(result);
        }
        [Route("entryMaster/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]Entry_MasterVM entity)
        {
            var result = await entryMasterService.DeleteAsync(entity);
            await LogData("?" + entity.ENTRY_SETTING_ID + "?" + entity.ENTRY_ID, entity.ENTRY_ID.ToString(), entity.ENTRY_NUMBER);
            return Ok(result);

        }
        [Route("entryMaster/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int entryType, int pageNum, int pageSize)
        {
            return Ok(await entryMasterService.GetAllAsync(entryType, pageNum, pageSize));
        }





        [Route("entryMaster/getAll")]
        [HttpGet]
        public List<Entry_MasterVM> getAll()
        {
            return entryMasterService.GetAll();
        }

        [Route("entryMaster/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count(int entryType)
        {
            return Ok(await entryMasterService.CountAsync(entryType));
        }


        [Route("entryMaster/GetLastEntryNumber")]
        [HttpGet]
        public long GetLastBillNumber(int settingID)
        {
            return entryMasterService.GetLastEntryNumber(settingID);
        }


        


        [Route("entryMaster/GetDetailsByEntryId")]
        [HttpGet]
        public List<Entry_DetailsVM> GetDetailsByEntryId(long EntryId)
        {
            return entryMasterService.GetDetailsByEntryId(EntryId);
        }



        [Route("entryMaster/InsertEntryBill")]
        [HttpPost]
        public async Task<IHttpActionResult> InsertEntryreturnID(EntryMasterDetails entity)
        {
            return Ok(await entryMasterService.InsertEntryBill(entity));
            // await LogData("?" + entity.EntryMaster.ENTRY_SETTING_ID + "?" + result, result.ToString(), entity.EntryMaster.ENTRY_NUMBER);
            //   return result;
        }

        [Route("entryMaster/InsertEntryAsset")]
        [HttpPost]
        public async Task<IHttpActionResult> InsertEntryAsset(EntryMasterDetails entity)
        {
            return Ok(await entryMasterService.InsertEntryAsset(entity));
        }


        [Route("entryMaster/getEntryByBillID")]
        [HttpGet]
        public EntryMasterDetails getEntryByBillID(long billID)
        {
            return entryMasterService.GetEntryByBillID(billID);
        }


        [Route("entryMaster/getEntryMasterByBill")]
        [HttpGet]
        public Entry_MasterVM getEntryMasyterByBill(long billID)
        {
            return entryMasterService.GetEntryMasterByBill(billID);
        }

        [Route("entryMaster/getEntryMasterByAssetOperation")]
        [HttpGet]
        public Entry_MasterVM getEntryMasterByAssetOperation(long assetOperationId)
        {
            return entryMasterService.getEntryMasterByAssetOperation(assetOperationId);
        }

        [Route("entryMaster/getEntryByEntryNumber")]
        [HttpGet]
        public async Task<IHttpActionResult> getEntryByEntryNumber(int entryNumber, int entryType)
        {
            return Ok(await entryMasterService.getEntryByEntryNumber(entryNumber, entryType));
        }



        [Route("entryMaster/checkEntryByEntryNumber")]
        [HttpGet]
        public async Task<IHttpActionResult> checkEntryByEntryNumber(int entryNumber, int entryType)
        {
            return Ok(await entryMasterService.CheckEntryByEntryNumber(entryNumber, entryType));
        }





        [Route("entryMaster/getDailyEntryByMsterID")]
        [HttpGet]
        public async Task<IHttpActionResult> getDailyEntryByMsterID(int masterID)
        {
            return Ok(await entryMasterService.getDailyEntryByMsterID(masterID));
        }



        [Route("entryMaster/getEntryByEntryID")]
        [HttpGet]
        public async Task<IHttpActionResult> getEntryByEntryID(int masterID)
        {
            return Ok(await entryMasterService.getEntryByEntryID(masterID));
        }





        [Route("entryMaster/getEntryByAccountID")]
        [HttpGet]
        public Entry_MasterVM getEntryByAccountID(int accountID)
        {
            return entryMasterService.getEntryByaccountID(accountID);
        }



        [Route("entryMaster/getEntryByCustID")]
        [HttpGet]
        public Entry_MasterVM getEntryByCustID(int customerID)
        {
            return entryMasterService.getEntryByCustID(customerID);
        }



        [Route("entryMaster/updateEntryOfBill")]
        [HttpPost]
        public async Task<IHttpActionResult> updateEntryOfBill(EntryMasterDetails entity)
        {
            var result = await entryMasterService.updateEntryOfBill(entity);
            await LogData("?" + entity.EntryMaster.ENTRY_SETTING_ID + "?" + result, result.ToString(), entity.EntryMaster.ENTRY_NUMBER);
            return Ok(result);

        }

        [Route("entryMaster/deleteEntryOfBill")]
        [HttpGet]
        public async Task<IHttpActionResult> deleteEntryOfBill(int billID)
        {
            var result = await entryMasterService.deleteEntryOfBill(billID);
            await LogData(null, result.ToString(), billID.ToString());
            return Ok(result);
        }

        [Route("entryMaster/getNotPostedEntries")]
        [HttpPost]
        public List<Entry_MasterVM> getNotPostedEntries([FromBody]EntryPostingSearchVM searchObject)
        {
            return entryMasterService.getNotPostedEntries(searchObject);
        }

        [Route("entryMaster/SetEntriesPosted")]
        [HttpPost]
        public bool SetEntriesPosted([FromBody]List<int> entryIds)
        {
            return entryMasterService.SetEntriesPosted(entryIds);
        }

        public async Task<bool> LogData(string code = null, string id = null, string notes = null, string editR = null)
        {
            Logger logger = new Logger();
            await userLogFileService.Insert(logger.LogDataMethod(code, id, notes, editR));
            return true;
        }
    }
}