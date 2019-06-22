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
    public class AddressPartsSettingsController : ApiController
    {
        IAddressPartsSettingsService addressPartService;

        public AddressPartsSettingsController(IAddressPartsSettingsService _addressPartService)
        {
            this.addressPartService = _addressPartService;
        }

        [Route("addressPart/get")]
        [HttpGet]
        public async Task<IHttpActionResult> getAllAddressParts()
        {
            return Ok(await addressPartService.getAddressPartsAsync());
        }

        [Route("addressPart/count")]
        [HttpGet]
        public async Task<IHttpActionResult> countAddressPart()
        {
            return Ok(await addressPartService.countAddressPartsAsync());
        }


        [Route("addressPart/insert")]
        [HttpPost]
        public async Task<IHttpActionResult> insertAddressPart(AddressPartsSettingsVM addressPart)
        {
            return Ok(await addressPartService.insertAddressPartsSync(addressPart));
        }

        [Route("addressPart/update")]
        [HttpPost]
        public async Task<IHttpActionResult> updateAddressPart(AddressPartsSettingsVM addressPart)
        {
            return Ok(await addressPartService.insertAddressPartsSync(addressPart));
        }

        [Route("addressPart/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> deleteAddressPart(AddressPartsSettingsVM addressPart)
        {
            return Ok(await addressPartService.deleteAddressPartsSync(addressPart));
        }


        [Route("addressPart/getByName")]
        [HttpGet]
        public async Task<IHttpActionResult> getByUId(string UID)
        {
            return Ok(await addressPartService.getAddrPartsByUID(UID));
        }
    }
}
