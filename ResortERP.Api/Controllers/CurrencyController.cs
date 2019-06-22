using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ResortERP.Core.VM;
using ResortERP.Repository;
using ResortERP.Service;
using ResortERP.Service.IServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Http;

namespace ResortERP.Api.Controllers
{
    [Logger]
    public class CurrencyController : ApiController
    {
        ICurrencyService currencyService;
        IBillMasterService billMasterService;
        IEntryMasterService entryMasterService;
        IBankService bankService;
        IUserLogFileService userLogFileService;
        public CurrencyController(ICurrencyService currencyService,IBillMasterService billMasterService, IEntryMasterService entryMasterService, IBankService bankService, IUserLogFileService userLogFileService)
        {
            this.currencyService = currencyService;
            this.billMasterService = billMasterService;
            this.entryMasterService = entryMasterService;
            this.bankService = bankService;
            this.userLogFileService = userLogFileService;
        }

        [Route("currency/insert")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]CurrencyVM entity)
        {
            var result= await currencyService.InsertAsync(entity);
            await LogData(entity.CURRENCY_CODE, result.ToString());
            if (result != 0)
            {
                return Ok(true);
            }
            return Ok(false);

        }

        [Route("currency/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]CurrencyVM entity)
        {
            var result = await currencyService.UpdateAsync(entity);
            await LogData(entity.CURRENCY_CODE,entity.CURRENCY_ID.ToString());
            return Ok(result);

        }
        [Route("currency/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]CurrencyVM entity)
        {
            var q1 = billMasterService.getByCurrency(entity.CURRENCY_ID);
            var q = entryMasterService.getByCurrency(entity.CURRENCY_ID);
            var q2 = bankService.getByCurrency(entity.CURRENCY_ID);
            if (q.Count == 0 && q1.Count == 0 && q2.Count == 0)
            {
                var result = await currencyService.DeleteAsync(entity);
                await LogData(entity.CURRENCY_CODE, entity.CURRENCY_ID.ToString());
                return Ok(result);
            }
            else
                return Ok(false);
            //return Ok(await currencyService.DeleteAsync(entity));
        }
        [Route("currency/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await currencyService.GetAllAsync(pageNum, pageSize));
        }

        [Route("currency/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await currencyService.CountAsync());
        }
        [Route("currency/getAll")]
        [HttpGet]
        public List<CurrencyVM> getAll()
        {
            return currencyService.GetAll();
        }
        [Route("currency/getByID")]
        [HttpGet]
        public string getBy(int currencyID)
        {
            return currencyService.GetBy(currencyID);
        }

        [Route("currency/getSearchResult")]
        [HttpPost]
        public async Task<IHttpActionResult> GetSearchResult(CurObj obj)
        {
            return Ok(await currencyService.GetSearchResultAsync(obj.currency, obj.pageNum, obj.pageSize));
        }

        [Route("currency/getCurrencyRate")]
        [HttpGet]
        public async Task<IHttpActionResult> GetCurrencyRate(int CurrencyID)
        {
            return Ok(await currencyService.GetCurrencyRate(CurrencyID));
        }


        [Route("currency/exportReport")]
        [HttpGet]
        public HttpResponseMessage ExportReport()
        {
            string EmailTosend = WebUtility.UrlDecode("m.tigerWhite@gmail.com");
            List<CurrencyVM> model = new List<CurrencyVM>();
            var rd = new ReportDocument();

            foreach (var details in getAll())
            {
                CurrencyVM obj = new CurrencyVM();
                obj.CURRENCY_ID = details.CURRENCY_ID;
                obj.CURRENCY_AR_NAME = details.CURRENCY_AR_NAME ?? "";
                obj.CURRENCY_EN_NAME = details.CURRENCY_EN_NAME ?? "";
                obj.AddedBy = details.AddedBy ?? "0";
                obj.AddedOn = details.AddedOn ?? DateTime.Now;
                obj.CURRENCY_CODE = details.CURRENCY_CODE ?? "";
                obj.CURRENCY_FIXED_RATE = details.CURRENCY_FIXED_RATE ?? 0;
                obj.CURRENCY_RATE = details.CURRENCY_RATE;
                obj.CURRENCY_SUB_AR_NAME = details.CURRENCY_SUB_AR_NAME ?? "";
                obj.CURRENCY_SUB_EN_NAME = details.CURRENCY_SUB_EN_NAME ?? "";
                obj.CURRENCY_SYMBOL_AR_NAME = details.CURRENCY_SYMBOL_AR_NAME ?? "";
                obj.CURRENCY_SYMBOL_EN_NAME = details.CURRENCY_SYMBOL_EN_NAME ?? "";
                obj.Disable = details.Disable ?? false;
                obj.SUB_TO_CURRENCY_TRANS = details.SUB_TO_CURRENCY_TRANS;
                obj.UpdatedBy = details.UpdatedBy ?? "0";
                obj.updatedOn = details.updatedOn ?? DateTime.Now;
                model.Add(obj);

            }

            rd.Load(Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/Reports/reports"), "Test.rpt"));
            ConnectionInfo connectInfo = new ConnectionInfo()
            {
                ServerName = "LocalHost",
                DatabaseName = "Smart2",
                UserID = "sa",
                Password = "Smart_No1"
            };
            rd.SetDatabaseLogon("sa", "Smart_No1");
            foreach (Table tbl in rd.Database.Tables)
            {
                tbl.LogOnInfo.ConnectionInfo = connectInfo;
                tbl.ApplyLogOnInfo(tbl.LogOnInfo);
            }
            rd.SetDataSource(model.ToList());

            using (var stream = rd.ExportToStream(ExportFormatType.PortableDocFormat))
            {
                SmtpClient smtp = new SmtpClient
                {
                    Port = 587,
                    UseDefaultCredentials = true,
                    Host = "smtp.gmail.com",
                    EnableSsl = true
                };

                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("mail@mail.com", "Password");
                var message = new System.Net.Mail.MailMessage("mail@mail.com", EmailTosend, "Test Details", "Hi Please check your Mail  and find the attachement.");
                message.Attachments.Add(new Attachment(stream, "Test.pdf"));

                smtp.Send(message);
            }

            var Message = string.Format("Report Created and sended to your Mail.");
            HttpResponseMessage response1 = Request.CreateResponse(HttpStatusCode.OK, Message);
            return response1;
        }



        [Route("currency/getLastCode")]
        [HttpGet]
        public string GetLastCode()
        {
            return currencyService.GetLastCode();
        }


        [Route("currency/getByCurrId")]
        [HttpGet]
        public async Task<IHttpActionResult> GetByCurrId(int CurrId)
        {
            return Ok(await currencyService.GetByCurrId(CurrId));
        }

        public async Task<bool> LogData(string code = null,string id=null)
        {
            Logger logger = new Logger();
            await userLogFileService.Insert(logger.LogDataMethod(code,id));
            return true;
        }

    }
    public class CurObj
    {
        public CurrencyVM currency { get; set; }
        public int pageNum { get; set; }
        public int pageSize { get; set; }
    }
}
