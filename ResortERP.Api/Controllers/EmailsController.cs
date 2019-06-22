using ResortERP.Core.VM;
using ResortERP.Service.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace ResortERP.Api.Controllers
{
    public class EmailsController : ApiController
    {
        IEmailsService emailService;
        public EmailsController(IEmailsService emailService)
        {
            this.emailService = emailService;
        }

        [Route("email/insert")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]EmailsVM entity)
        {
            return Ok(await emailService.InsertAsync(entity));
        }
        [Route("email/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]EmailsVM entity)
        {
            return Ok(await emailService.UpdateAsync(entity));
        }
        [Route("email/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]EmailsVM entity)
        {
            return Ok(await emailService.DeleteAsync(entity));
        }
        [Route("email/get/{pageNum}/{pageSize}")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await emailService.GetAllAsync(pageNum, pageSize));
        }

        [Route("email/getByUID/{userID}")]
        [HttpGet]
        public async Task<IHttpActionResult> getByUID(int userID)
        {
            return Ok(await emailService.GetByUID(userID));
        }

        [Route("email/getAll")]
        [HttpGet]
        public List<EmailsVM> getAll()
        {
            return emailService.GetAll();
        }

        [Route("email/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await emailService.CountAsync());
        }
    }
}