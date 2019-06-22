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
    public class ResourcesController : ApiController
    {

        IResourcesService resourcesService;
        IResourcesTranslationService resourcesTranslationService;

        public ResourcesController(IResourcesService resourcesService, IResourcesTranslationService resourcesTranslationService)
        {
            this.resourcesService = resourcesService;
            this.resourcesTranslationService = resourcesTranslationService;
        }

        [Route("resources/getByResourceName")]
        [HttpGet]
        public Resources getByResourceName(string ResourceName)
        {
            return resourcesService.GetByResourceName(ResourceName);
        }

        [Route("resources/getAll")]
        [HttpGet]
        public string getAll()
        {
            var cacheKey1 = HttpRuntime.Cache.Get("resources");

            var cacheKey2 = HttpRuntime.Cache.Get("resourcesTranslation");

            if (cacheKey1 == null || cacheKey2 == null)
            {
                var resources = resourcesService.GetAll();
                HttpRuntime.Cache.Insert("resources", resources);

                var resourcesTranslation = resourcesTranslationService.GetAll();
                HttpRuntime.Cache.Insert("resourcesTranslation", resourcesTranslation);

                var res = new { r = resources, rt = resourcesTranslation };

                return Newtonsoft.Json.JsonConvert.SerializeObject(res);
            }
            else
            {
                var resources = ((List<Resources>)cacheKey1);

                var resourcesTranslation = ((List<ResourcesTranslationVM>)cacheKey2);

                var res = new { r = resources, rt = resourcesTranslation };

                return Newtonsoft.Json.JsonConvert.SerializeObject(res);
            }

        }

    }
}
