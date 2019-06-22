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
    public class MessagesController : ApiController
    {
        IMessageService _typeService;

        public MessagesController(IMessageService typeService)
        {
            _typeService = typeService;
        }


        [Route("Message/GetAllMessage/{lang}")]
        [HttpGet]
        public MessageAPICollection GetAllMessage(int lang = 2)
        {
            var json = new MessageAPICollection();
            json.Messages = _typeService.GetAllMessage(lang);
            return json;
        }

        [Route("Message/GetAllUserMessage/{userid}/{lang}/{MessageCategoryID?}")]
        [HttpGet]
        public MessagesViewAPICollection GetAllUserMessage(int userid = 1, int lang = 2, int MessageCategoryID = 0)
        {
            var json = new MessagesViewAPICollection();
            json.Messages = _typeService.GetAllUserMessage(userid, lang, MessageCategoryID);
            return json;
        }

        [Route("Message/GetAllUnreadUserMessage/{userid}/{lang}/{MessageCategoryID?}")]
        [HttpGet]
        public MessagesViewAPICollection GetAllUnreadUserMessage(int userid = 1, int lang = 2, int MessageCategoryID = 0)
        {
            var json = new MessagesViewAPICollection();
            json.Messages = _typeService.GetAllUnreadUserMessage(userid, lang, MessageCategoryID);
            return json;
        }
        [Route("Message/GetAllReadedUserMessage/{userid}/{lang}/{MessageCategoryID?}")]
        [HttpGet]
        public MessagesViewAPICollection GetAllReadedUserMessage(int userid = 1, int lang = 2, int MessageCategoryID = 0)
        {
            var json = new MessagesViewAPICollection();
            json.Messages = _typeService.GetAllReadedUserMessage(userid, lang, MessageCategoryID);
            return json;
        }

        [Route("Message/GetAllConfirmedUserMessage/{userid}/{lang}/{MessageCategoryID?}")]
        [HttpGet]
        public MessagesViewAPICollection GetAllConfirmedUserMessage(int userid = 1, int lang = 2, int MessageCategoryID = 0)
        {
            var json = new MessagesViewAPICollection();
            json.Messages = _typeService.GetAllConfirmedUserMessage(userid, lang, MessageCategoryID);
            return json;
        }


        [Route("Message/GetAllUnConfirmedUserMessage/{userid}/{lang}/{MessageCategoryID?}")]
        [HttpGet]
        public MessagesViewAPICollection GetAllUnConfirmedUserMessage(int userid = 1, int lang = 2, int MessageCategoryID = 0)
        {
            var json = new MessagesViewAPICollection();
            json.Messages = _typeService.GetAllUnConfirmedUserMessage(userid, lang, MessageCategoryID);
            return json;
        }


        [Route("Message/UpdateMessageReadStatus/{messageid}/{lang}")]
        [HttpGet]
        public HttpResponseMessage UpdateMessageReadStatus(int messageid = 1, int lang = 2)
        {

            var state = _typeService.UpdateMessageReadStatus(messageid, lang);
            return Request.CreateResponse(HttpStatusCode.OK, state, Configuration.Formatters.JsonFormatter);

        }


        [Route("Message/UpdateMessageReceivedStatus/{messageid}/{lang}")]
        [HttpGet]
        public HttpResponseMessage UpdateMessageReceivedStatus(int messageid = 1, int lang = 2)
        {

            var state = _typeService.UpdateMessageReceivedStatus(messageid, lang);
            return Request.CreateResponse(HttpStatusCode.OK, state, Configuration.Formatters.JsonFormatter);

        }




        [Route("Message/UpdateUserMessageReadStatus/{userid}/{lang}")]
        [HttpGet]
        public HttpResponseMessage UpdateUserMessageReadStatus(int userid = 1, int lang = 2)
        {

            var state = _typeService.UpdateUserMessageReadStatus(userid, lang);
            return Request.CreateResponse(HttpStatusCode.OK, state, Configuration.Formatters.JsonFormatter);
        }



        [Route("Message/insert")]
        [HttpPost]
        public async Task<HttpResponseMessage> add([FromBody]MessageVM entity)
        {
            var id = await _typeService.InsertAsync(entity);
            return Request.CreateResponse(HttpStatusCode.OK, id, Configuration.Formatters.JsonFormatter);
        }

        [Route("Message/update")]
        [HttpPost]
        public async Task<HttpResponseMessage> edit([FromBody]MessageVM entity)
        {
            var state = await _typeService.UpdateAsync(entity);
            return Request.CreateResponse(HttpStatusCode.OK, state, Configuration.Formatters.JsonFormatter);
        }

        [Route("Message/delete/{Id}")]
        [HttpDelete]
        public async Task<HttpResponseMessage> remove(int Id)
        {
            var state = await _typeService.DeleteAsync(Id);

            return Request.CreateResponse(HttpStatusCode.OK, state, Configuration.Formatters.JsonFormatter);
        }

        [Route("Message/{Id}")]
        [HttpGet]
        public async Task<Message> getById(int Id)
        {
            var country = await _typeService.getById(Id);
            return country;
        }


        [Route("Message/count")]
        [HttpGet]
        public async Task<int> count()
        {
            var count = await _typeService.GetCount();
            return count;
        }


        [Route("Message/getPagedData")]
        [HttpGet]
        public async Task<IHttpActionResult> getPagedData(int pageNum, int pageSize)
        {
            return Ok(await _typeService.GetPageData(pageNum, pageSize));
        }


        public class MessageAPICollection
        {
            public List<MessageVM> Messages { get; set; }
        }

        public class MessagesViewAPICollection
        {
            public List<MessagesViewVM> Messages { get; set; }
        }
    }
}
