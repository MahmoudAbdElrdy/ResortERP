using ResortERP.Core.VM;
using ResortERP.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ResortERP.Api.Controllers
{
    public class CurrencyCategoriesController : ApiController
    {

        ICurrencyCategoriesService catService;

        public CurrencyCategoriesController(ICurrencyCategoriesService _catService)
        {
            this.catService = _catService;
        }

        [Route("category/gatCat")]
        [HttpGet]
        public async Task<IHttpActionResult> getAllCategories(int pageNum, int pageSize)
        {
            return Ok(await catService.getCatAsync(pageNum, pageSize));
        }

        [Route("category/countCat")]
        [HttpGet]
        public async Task<IHttpActionResult> countBanks()
        {
            return Ok(await catService.countCatAsync());
        }


        [Route("category/insertCat")]
        [HttpPost]
        public async Task<IHttpActionResult> insertBank(CurrencyCategoriesVM cat)
        {
            return Ok(await catService.insertCatSync(cat));
        }

        [Route("category/updateCat")]
        [HttpPost]
        public async Task<IHttpActionResult> updateBank(CurrencyCategoriesVM cat)
        {
            return Ok(await catService.updateCatSync(cat));
        }

        [Route("category/deleteCat")]
        [HttpPost]
        public async Task<IHttpActionResult> deleteBank(CurrencyCategoriesVM cat)
        {
            return Ok(await catService.deleteCatSync(cat));
        }



        [Route("category/gatByCurrId")]
        [HttpGet]
        public  List<CurrencyCategoriesVM> getAllByCurrId(int currId)
        {
            return catService.getbyCurrId(currId);
        }


        [Route("category/gatByCurrIdCatId")]
        [HttpGet]
        public string getByCurrIdCatId(int currId, int catId)
        {
            return catService.getbyCurrIdCatId(currId, catId);
        }



        [Route("category/gatLastCode")]
        [HttpGet]
        public string getLastCode()
        {
            return catService.GetLastCode();
        }
    }
}
