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
    public class AcountsController : ApiController
    {
        IAcountsService _AcountsService;
        IUserLogFileService userLogFileService;
        public AcountsController(IAcountsService AcountsService, IUserLogFileService userLogFileService)
        {
            this._AcountsService = AcountsService;
            this.userLogFileService = userLogFileService;
        }

        [Route("Acounts/add")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]AccountVM entity)
        {
            var result = await _AcountsService.InsertAsync(entity);
            await LogData(entity.ACC_CODE, result.ToString());
            if (result != 0)
            {
                return Ok(true);
            }
            return Ok(false);

        }
        [Route("Acounts/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]AccountVM entity)
        {
            var result = await _AcountsService.UpdateAsync(entity);
            await LogData(entity.ACC_CODE, entity.ACC_ID.ToString());
            return Ok(result);

        }

        [Route("Acounts/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]AccountVM entity)
        {
            var result = await _AcountsService.DeleteAsync(entity);
            await LogData(entity.ACC_CODE, entity.ACC_ID.ToString());
            return Ok(result);

        }

        [Route("Acounts/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await _AcountsService.GetAllAsync(pageNum, pageSize));
        }

        [Route("Acounts/getByID")]
        [HttpGet]
        public async Task<IHttpActionResult> getByID(int AccountID)
        {
            return Ok(await _AcountsService.GetByAccountID(AccountID));
        }

        [Route("Acounts/getByIDTree")]
        [HttpGet]
        public async Task<IHttpActionResult> getByIDTree(int AccountID)
        {
            return Ok(await _AcountsService.GetByAccountIDTree(AccountID));
        }

        [Route("Acounts/getSearchForAccount")]
        [HttpGet]
        public async Task<IHttpActionResult> getSearchForAccount(string accountName, int pageNumA, int pageSizeA)
        {
            return Ok(await _AcountsService.getSearchForAccount(accountName, pageNumA, pageSizeA));
        }


        [Route("Acounts/getSearch")]
        [HttpGet]
        public async Task<IHttpActionResult> GetSearch(string search)
        {
            return Ok(await _AcountsService.GetSearch(search));
        }


        [Route("Acounts/getSearchCount")]
        [HttpGet]
        public async Task<IHttpActionResult> getSearchCount(string accountName)
        {
            return Ok(await _AcountsService.CountofSearchAsync(accountName));
        }


        [Route("Acounts/getAll")]
        [HttpGet]
        public List<AccountVM> getAll()
        {
            return _AcountsService.GetAll();
        }

        [Route("Acounts/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await _AcountsService.CountAsync());
        }
        [Route("Acounts/GetAllCustomerAccounts")]
        [HttpGet]
        public List<AccountVM> GetAllCustomerAccounts(int Type = 1)
        {
            return _AcountsService.GetAllCustomerAccounts(Type);
        }


        [Route("Acounts/GetAllBoxAccounts")]
        [HttpGet]
        public List<AccountVM> GetAllBoxAccounts()
        {
            return _AcountsService.GetAllBoxAccounts();
        }

        [Route("Acounts/GetAllGoldBoxAccounts")]
        [HttpGet]
        public List<AccountVM> GetAllGoldBoxAccounts()
        {
            return _AcountsService.GetAllGoldBoxAccounts();
        }


        [Route("Accounts/searchAccounts")]
        [HttpGet]
        public List<AccountVM> SearchAccount(string SearchCriteria)
        {
            return _AcountsService.SearchItems(SearchCriteria);
        }


        [Route("Accounts/GetAccountDataByCode")]
        [HttpGet]
        public AccountVM GetAccountDataByCode(string AccountCode, int Type)
        {
            return _AcountsService.GetAccountDataByCode(AccountCode, Type);
        }


        [Route("Accounts/count")]
        [HttpGet]
        public Task<int> countAccounts()
        {
            return _AcountsService.CountAsync();
        }


        [Route("Accounts/getLastCode")]
        [HttpGet]
        public string GetLastCode()
        {
            return _AcountsService.GetLastCode();
        }


        [Route("Accounts/getCodeWithoutParent")]
        [HttpGet]
        public string getCodeWithoutParent()
        {
            return _AcountsService.GetCodeWithoutParent();
        }


        [Route("Accounts/getAccountByCode")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAccountbyCode(string AccountCode)
        {
            return Ok(await _AcountsService.GetByAccountcode(AccountCode));
        }


        [Route("Accounts/getParentDataByID")]
        [HttpGet]
        public AccountVM getParentDataByID(int parentID)
        {
            return _AcountsService.GetParentDataByID(parentID);
        }


        [Route("Acounts/checkAccountIfUsed")]
        [HttpGet]
        public string checkAccountIfUsed(int accountID)
        {
            return _AcountsService.checkAccountIfUsed(accountID);
        }

        //[Route("Acounts/getAccTree")]
        //[HttpGet]
        //public IHttpActionResult getAccTree()
        //{
        //    return Ok(_AcountsService.getTreeGuid());
        //}

        [Route("Acounts/buildTree")]
        [HttpGet]
        public IHttpActionResult buildTree()
        {
            return Ok(_AcountsService.buildAccTree());
        }


        [Route("Acounts/getAllBankAccounts")]
        [HttpGet]
        public List<AccountVM> getAllBankAccounts()
        {
            return _AcountsService.getAllBankAccounts();
        }


        [Route("Acounts/GetAccountBoxByTypesForEntry")]
        [HttpGet]
        public List<AccountVM> GetAccountBoxByTypesForEntry(int EntryType)
        {
            return _AcountsService.GetAccountBoxByTypesForEntry(EntryType);
        }

        [Route("Acounts/GetAccountBoxByTypesForBill")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAccountBoxByTypesForBill(int BillType, string AccType, int CompBran)
        {
            return Ok(await _AcountsService.GetAccountBoxByTypesForBill(BillType, AccType, CompBran));
        }

        // /////////////////////////////
        [Route("Acounts/GetGoldBoxByTypesForBill")]
        [HttpGet]
        public async Task<IHttpActionResult> GetGoldBoxByTypesForBill(string AccType, int CompBran)
        {
            return Ok(await _AcountsService.GetGoldBoxByTypesForBill(AccType, CompBran));
        }





        //// ////////////////////////////

        [Route("Acounts/GetDefaultAccountsOfBillSettings")]
        [HttpGet]
        public List<ACCOUNTS> GetDefaultAccountsOfBillSettings(string AccType)
        {
            return _AcountsService.GetDefaultAccountsOfBillSettings(AccType);
        }


        [Route("Accounts/GetAllMainAccounts")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllMainAccounts()
        {
            return Ok(await _AcountsService.GetAllMainAccounts());
        }
        public async Task<bool> LogData(string code = null, string id = null)
        {
            Logger logger = new Logger();
            await userLogFileService.Insert(logger.LogDataMethod(code, id));
            return true;
        }



        [Route("Accounts/GetCustomerAccountsForComplexEntry")]
        [HttpGet]
        public List<AccountVM> GetCustomerAccountsForComplexEntry()
        {
            return _AcountsService.GetCustomerAccountsForComplexEntry();
        }


        [Route("Accounts/SearchAccountsForDepitGrid")]
        [HttpGet]
        public List<AccountVM> SearchAccountsForDepitGrid(string SearchCriteria, int EntryTypeID)
        {
            return _AcountsService.SearchAccountsForDepitGrid(SearchCriteria, EntryTypeID);
        }


        [Route("Accounts/SearchAccountsForCreditGrid")]
        [HttpGet]
        public List<AccountVM> SearchAccountsForCreditGrid(string SearchCriteria, int EntryTypeID)
        {
            return _AcountsService.SearchAccountsForCreditGrid(SearchCriteria, EntryTypeID);
        }

        [Route("Accounts/GetAccountsFilteredByType")]
        [HttpGet]
        public List<ACCOUNTS> GetAccountsFilteredByType(string AccountType)
        {
            return _AcountsService.GetAccountsFilteredByType(AccountType);
        }
    }
}
