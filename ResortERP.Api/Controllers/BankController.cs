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

namespace ResortERP.Api.Controllers
{
    [Logger]
    public class BankController : ApiController
    {
        IBankService bankService;
        IUserLogFileService userLogFileService;
        public BankController(IBankService _bankService, IUserLogFileService userLogFileService)
        {
            this.bankService = _bankService;
            this.userLogFileService = userLogFileService;
        }

        [Route("bank/gatBanks")]
        [HttpGet]
        public async Task<IHttpActionResult> getAllBanks(int pageNum, int pageSize)
        {
            return Ok(await bankService.getBankAsync(pageNum, pageSize));
        }

        [Route("bank/getById")]
        [HttpGet]
        public async Task<IHttpActionResult> getById(int ID)
        {
            return Ok(await bankService.getById(ID));
        }

        [Route("bank/countBanks")]
        [HttpGet]
        public async Task<IHttpActionResult> countBanks()
        {
            return Ok(await bankService.countBankAsync());
        }


        [Route("bank/insertBank")]
        [HttpPost]
        public async Task<IHttpActionResult> insertBank(BankVM bank)
        {
            var result = await bankService.insertBankSync(bank);
            await LogData(bank.Code.ToString(), result.ToString());
            if (result != 0)
            {
                return Ok(true);
            }
            return Ok(false);
  
        }

        [Route("bank/updateBank")]
        [HttpPost]
        public async Task<IHttpActionResult> updateBank(BankVM bank)
        {
            var result = await bankService.updateBankSync(bank);
            await LogData(bank.Code.ToString(),bank.ID.ToString());
            return Ok(result);

        }

        [Route("bank/deleteBank")]
        [HttpPost]
        public async Task<IHttpActionResult> deleteBank(BankVM bank)
        {
            var result = await bankService.deleteBankSync(bank);
            await LogData(bank.Code.ToString(),bank.ID.ToString());
            return Ok(result);

        }



        [Route("bank/getLastCode")]
        [HttpGet]
        public string GetLastCode()
        {
            return bankService.GetLastCode();
        }

        public async Task<bool> LogData(string code = null, string id = null)
        {
            Logger logger = new Logger();
            await userLogFileService.Insert(logger.LogDataMethod(code,id));
            return true;
        }
    }
}
