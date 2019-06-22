using ResortERP.Core;
using ResortERP.Core.VM;
using ResortERP.Service.IServices;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ResortERP.Api.Controllers
{
    public class NotificationTypesController : ApiController
    {
        INotificationTypeService _typeService;

        public NotificationTypesController(INotificationTypeService typeService)
        {
            _typeService = typeService;
        }


        [Route("NotificationType/GetAllNotificationType/{country}/{lang}")]
        [HttpGet]
        public NotificationTypeAPICollection GetAllNotificationType(int country = 1, int lang = 2)
        {
            var json = new NotificationTypeAPICollection();
            json.NotificationTypes = _typeService.GetAllNotificationType(country, lang);
            return json;
        }

        [Route("NotificationType/insert")]
        [HttpPost]
        public async Task<HttpResponseMessage> add([FromBody]NotificationTypeVM entity)
        {
            var id = await _typeService.InsertAsync(entity);
            return Request.CreateResponse(HttpStatusCode.OK, id, Configuration.Formatters.JsonFormatter);
        }

        [Route("NotificationType/update")]
        [HttpPost]
        public async Task<HttpResponseMessage> edit([FromBody]NotificationTypeVM entity)
        {
            var state = await _typeService.UpdateAsync(entity);
            return Request.CreateResponse(HttpStatusCode.OK, state, Configuration.Formatters.JsonFormatter);
        }

        [Route("NotificationType/delete/{Id}")]
        [HttpDelete]
        public async Task<HttpResponseMessage> remove(int Id)
        {
            var state = await _typeService.DeleteAsync(Id);

            return Request.CreateResponse(HttpStatusCode.OK, state, Configuration.Formatters.JsonFormatter);
        }

        [Route("NotificationType/{Id}")]
        [HttpGet]
        public async Task<NotificationType> getById(int Id)
        {
            var country = await _typeService.getById(Id);
            return country;
        }


        [Route("NotificationType/count/{countryId}")]
        [HttpGet]
        public async Task<int> count(int countryId)
        {
            var count = await _typeService.GetCount(countryId);
            return count;
        }


        [Route("NotificationType/getPagedData")]
        [HttpGet]
        public async Task<IHttpActionResult> getPagedData(int pageNum, int pageSize, int countryId)
        {
            return Ok(await _typeService.GetPageData(pageNum, pageSize, countryId));
        }


        public class NotificationTypeAPICollection
        {
            public List<NotificationTypeVM> NotificationTypes { get; set; }
        }
    }
}
