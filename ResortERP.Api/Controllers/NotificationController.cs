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
    public class NotificationsController : ApiController
    {
        INotificationService _typeService;

        public NotificationsController(INotificationService typeService)
        {
            _typeService = typeService;
        }


        [Route("Notification/GetAllNotification/{lang}")]
        [HttpGet]
        public NotificationAPICollection GetAllNotification(int lang = 2)
        {
            var json = new NotificationAPICollection();
            json.Notifications = _typeService.GetAllNotification(lang);
            return json;
        }

        [Route("Notification/GetAllUserNotification/{userid}/{lang}/{NotifcationCategoryID?}")]
        [HttpGet]
        public NotificationsViewAPICollection GetAllUserNotification(int userid = 1, int lang = 2, int NotifcationCategoryID = 0)
        {
            var json = new NotificationsViewAPICollection();
            json.Notifications = _typeService.GetAllUserNotification(userid, lang, NotifcationCategoryID);
            return json;
        }

        [Route("Notification/GetAllUnreadUserNotification/{userid}/{lang}/{NotifcationCategoryID?}")]
        [HttpGet]
        public NotificationsViewAPICollection GetAllUnreadUserNotification(int userid = 1, int lang = 2, int NotifcationCategoryID = 0)
        {
            var json = new NotificationsViewAPICollection();
            json.Notifications = _typeService.GetAllUnreadUserNotification(userid, lang, NotifcationCategoryID);
            return json;
        }
        [Route("Notification/GetAllReadedUserNotification/{userid}/{lang}/{NotifcationCategoryID?}")]
        [HttpGet]
        public NotificationsViewAPICollection GetAllReadedUserNotification(int userid = 1, int lang = 2, int NotifcationCategoryID = 0)
        {
            var json = new NotificationsViewAPICollection();
            json.Notifications = _typeService.GetAllReadedUserNotification(userid, lang, NotifcationCategoryID);
            return json;
        }

        [Route("Notification/GetAllConfirmedUserNotification/{userid}/{lang}/{NotifcationCategoryID?}")]
        [HttpGet]
        public NotificationsViewAPICollection GetAllConfirmedUserNotification(int userid = 1, int lang = 2, int NotifcationCategoryID = 0)
        {
            var json = new NotificationsViewAPICollection();
            json.Notifications = _typeService.GetAllConfirmedUserNotification(userid, lang, NotifcationCategoryID);
            return json;
        }


        [Route("Notification/GetAllUnConfirmedUserNotification/{userid}/{lang}/{NotifcationCategoryID?}")]
        [HttpGet]
        public NotificationsViewAPICollection GetAllUnConfirmedUserNotification(int userid = 1, int lang = 2, int NotifcationCategoryID = 0)
        {
            var json = new NotificationsViewAPICollection();
            json.Notifications = _typeService.GetAllUnConfirmedUserNotification(userid, lang, NotifcationCategoryID);
            return json;
        }


        [Route("Notification/UpdateNotificationConfirmedStatus/{userid}/{lang}")]
        [HttpGet]
        public HttpResponseMessage UpdateNotificationConfirmedStatus(int userid = 1, int lang = 2)
        {

            var state = _typeService.UpdateNotificationConfirmedStatus(userid, lang);
            return Request.CreateResponse(HttpStatusCode.OK, state, Configuration.Formatters.JsonFormatter);

        }




        [Route("Notification/UpdateUserNotificationReadStatus/{userid}/{lang}")]
        [HttpGet]
        public HttpResponseMessage UpdateUserNotificationReadStatus(int userid = 1, int lang = 2)
        {

            var state = _typeService.UpdateUserNotificationReadStatus(userid, lang);
            return Request.CreateResponse(HttpStatusCode.OK, state, Configuration.Formatters.JsonFormatter);
        }



        [Route("Notification/insert")]
        [HttpPost]
        public async Task<HttpResponseMessage> add([FromBody]NotificationVM entity)
        {
            var id = await _typeService.InsertAsync(entity);
            return Request.CreateResponse(HttpStatusCode.OK, id, Configuration.Formatters.JsonFormatter);
        }

        [Route("Notification/update")]
        [HttpPost]
        public async Task<HttpResponseMessage> edit([FromBody]NotificationVM entity)
        {
            var state = await _typeService.UpdateAsync(entity);
            return Request.CreateResponse(HttpStatusCode.OK, state, Configuration.Formatters.JsonFormatter);
        }

        [Route("Notification/delete/{Id}")]
        [HttpDelete]
        public async Task<HttpResponseMessage> remove(int Id)
        {
            var state = await _typeService.DeleteAsync(Id);

            return Request.CreateResponse(HttpStatusCode.OK, state, Configuration.Formatters.JsonFormatter);
        }

        [Route("Notification/{Id}")]
        [HttpGet]
        public async Task<Notification> getById(int Id)
        {
            var country = await _typeService.getById(Id);
            return country;
        }


        [Route("Notification/count")]
        [HttpGet]
        public async Task<int> count()
        {
            var count = await _typeService.GetCount();
            return count;
        }


        [Route("Notification/getPagedData/{pageNum}/{pageSize}")]
        [HttpGet]
        public async Task<IHttpActionResult> getPagedData(int pageNum, int pageSize)
        {
            return Ok(await _typeService.GetPageData(pageNum, pageSize));
        }


        public class NotificationAPICollection
        {
            public List<NotificationVM> Notifications { get; set; }
        }

        public class NotificationsViewAPICollection
        {
            public List<NotificationsViewVM> Notifications { get; set; }
        }
    }
}
