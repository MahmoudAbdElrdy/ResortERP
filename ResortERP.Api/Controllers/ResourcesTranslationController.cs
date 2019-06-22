using ResortERP.Core;
using ResortERP.Core.VM;
using ResortERP.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;


namespace ResortERP.Api.Controllers
{
    public class ResourcesTranslationController : ApiController
    {
        IResourcesTranslationService resourcesTranslationService;

        public ResourcesTranslationController(IResourcesTranslationService resourcesTranslationService)
        {
            this.resourcesTranslationService = resourcesTranslationService;
        }

        [Route("resourcesTranslation/getAll")]
        [HttpGet]
        public List<ResourcesTranslationVM> getAll()
        {
            var cacheKey = HttpRuntime.Cache.Get("resourcesTranslation");

            if (cacheKey == null)
            {
                var resourcesTranslation = resourcesTranslationService.GetAll();
                HttpRuntime.Cache.Insert("resourcesTranslation", resourcesTranslation);

                return resourcesTranslation;
            }
            else
            {
                return ((List<ResourcesTranslationVM>)cacheKey);
            }
                
            //return resourcesTranslationService.GetAll();
        }

        [Route("resourcesTranslation/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]ResourcesTranslation entity)
        {

            HttpRuntime.Cache.Remove("resourcesTranslation");

            var item = resourcesTranslationService.GetByID(2438);

            item.Notes = "test test test";

            return Ok(await resourcesTranslationService.UpdateAsync(item));
        }

        [Route("resourcesTranslation/updateById")]
        [HttpPost]
        public async Task<IHttpActionResult> updateById([FromBody]ResourcesTranslation entity)
        {

            HttpRuntime.Cache.Remove("resourcesTranslation");

            return Ok(await resourcesTranslationService.UpdateAsyncByID(entity));
        }

    }
}
