using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ResortERP.Service.IServices;
using System.Threading.Tasks;
using ResortERP.Core.VM;
using ResortERP.Repository;

namespace ResortERP.Api.Controllers
{
    [Logger]
    public class AddressController : ApiController
    {
        IAddressService addressService;
        IEmployeeService employeeService;
        ICompanyStoresService companyStoresService;
        ICustomersService customersService;
        IBankService bankService;
        ICompanyBranchesService companyBranchesService;
        IUserLogFileService userLogFileService;
        public AddressController(IAddressService addressService, IEmployeeService employeeService, 
            ICompanyStoresService companyStoresService, ICustomersService customersService,
            IBankService bankService, ICompanyBranchesService companyBranchesService, IUserLogFileService userLogFileService)
        {
            this.addressService = addressService;
            this.employeeService = employeeService;
            this.companyStoresService = companyStoresService;
            this.customersService = customersService;
            this.bankService = bankService;
            this.companyBranchesService = companyBranchesService;
            this.userLogFileService = userLogFileService;
        }


        //nations
        [Route("address/getNations")]
        [HttpGet]
        public async Task<IHttpActionResult> getNation(int pageNum, int pageSize)
        {
            return Ok(await addressService.getAllNationsAsync(pageNum, pageSize));
        }


        [Route("address/addNation")]
        [HttpPost]
        public async Task<IHttpActionResult> addNation([FromBody] NATIONVM entity)
        {
            var result = await addressService.insertNationAsync(entity);
            await LogData(null, result.ToString());
            if (result != 0)
            {
                return Ok(true);
            }
            return Ok(false);

        }


        [Route("address/updateNation")]
        [HttpPost]
        public async Task<IHttpActionResult> updateNation([FromBody] NATIONVM entity)
        {
            var result = await addressService.updatetNationAsync(entity);
            await LogData(null,entity.NATION_ID.ToString());
            return Ok(result);

        }


        [Route("address/deleteNation")]
        [HttpPost]
        public async Task<IHttpActionResult> deleteNation([FromBody] NATIONVM entity)
        {
            var q = employeeService.getByNationID(entity.NATION_ID);
            var q1 = companyStoresService.getByNationID(entity.NATION_ID);
            var q2 = customersService.getByNationID(entity.NATION_ID);
            var q3= bankService.getByNationID(entity.NATION_ID);
            var q4 = addressService.getNationGov(entity.NATION_ID);
            var q5 = companyBranchesService.getByNationID(entity.NATION_ID);
            if (q.Count == 0 && q1.Count == 0 && q2.Count == 0 && q3.Count == 0 && q4.Count == 0 && q5.Count == 0)
            {
                var result = await addressService.deletetNationAsync(entity);
                await LogData(null,entity.NATION_ID.ToString());
                return Ok(result);
            }

            else
                return Ok(false);
            //return Ok(await addressService.deletetNationAsync(entity));
        }


        [Route("address/countNation")]
        [HttpGet]
        public async Task<IHttpActionResult> countNation()
        {
            return Ok(await addressService.countNationAsync());
        }


        [Route("address/getNationByID")]
        [HttpGet]
        public NATIONVM getNationByID(int nationID)
        {
            return addressService.gatNationByID(nationID);
        }







        //governs
        [Route("address/getGoverns")]
        [HttpGet]
        public async Task<IHttpActionResult> getGovern(int pageNum, int pageSize)
        {
            return Ok(await addressService.getAllGovernAsync(pageNum, pageSize));
        }


        [Route("address/addGovern")]
        [HttpPost]
        public async Task<IHttpActionResult> addGovern([FromBody] GOVERNORATEVM entity)
        {
            var result = await addressService.insertGovernAsync(entity);
            await LogData(null, result.ToString());
            if (result != 0)
            {
                return Ok(true);
            }
            return Ok(false);
        }


        [Route("address/updateGovern")]
        [HttpPost]
        public async Task<IHttpActionResult> updateGovern([FromBody] GOVERNORATEVM entity)
        {
            var result = await addressService.updatetGovernAsync(entity);
            await LogData(null,entity.GOV_ID.ToString());
            return Ok(result);

        }


        [Route("address/deleteGovern")]
        [HttpPost]
        public async Task<IHttpActionResult> deleteGovern([FromBody] GOVERNORATEVM entity)
        {
            var q = employeeService.getByGOVID(entity.GOV_ID);
            var q1 = companyStoresService.getByGOVID(entity.GOV_ID);
            var q2 = customersService.getByGOVID(entity.GOV_ID);
            var q3=bankService.getByGOVID(entity.GOV_ID);
            var q4 = addressService.getGovTown(entity.GOV_ID);
            var q5 = companyBranchesService.getByGOVID(entity.GOV_ID);
            
            if (q.Count == 0 && q1.Count == 0 && q2.Count == 0 && q3.Count == 0 && q4.Count == 0 && q5.Count == 0)
            {
                var result = await addressService.deletetGovernAsync(entity);
               await LogData(null,entity.GOV_ID.ToString());
                return Ok(result);
            }
            else
                return Ok(false);
            //return Ok(await addressService.deletetGovernAsync(entity));
        }


        [Route("address/countGovern")]
        [HttpGet]
        public async Task<IHttpActionResult> countGovern()
        {
            return Ok(await addressService.countGovernAsync());
        }


        [Route("address/getGovByID")]
        [HttpGet]
        public GOVERNORATEVM getGovByID(int govID)
        {
            return addressService.gatGovByID(govID);
        }




        //towns

        [Route("address/getTowns")]
        [HttpGet]
        public async Task<IHttpActionResult> getTown(int pageNum, int pageSize)
        {
            return Ok(await addressService.getAllTownsAsync(pageNum, pageSize));
        }


        [Route("address/addTown")]
        [HttpPost]
        public async Task<IHttpActionResult> addTown([FromBody] TOWNVM entity)
        {
            var result = await addressService.insertTownAsync(entity);
            await LogData(null, result.ToString());
            if (result != 0)
            {
                return Ok(true);
            }
            return Ok(false);
        }


        [Route("address/updateTown")]
        [HttpPost]
        public async Task<IHttpActionResult> updateTown([FromBody] TOWNVM entity)
        {
            var result = await addressService.updatetTownAsync(entity);
            await LogData(null, entity.TOWN_ID.ToString());
            return Ok(result);

        }


        [Route("address/deleteTown")]
        [HttpPost]
        public async Task<IHttpActionResult> deleteTown([FromBody] TOWNVM entity)
        {
            var q = employeeService.getByTownID(entity.TOWN_ID);
            var q1 = companyStoresService.getByTownID(entity.TOWN_ID);
            var q2 = customersService.getByTownID(entity.TOWN_ID);
            var q3=bankService.getByTownID(entity.TOWN_ID);
            var q4 = addressService.getTownVillage(entity.TOWN_ID);
            var q5 = companyBranchesService.getByTownID(entity.TOWN_ID);
            
            if (q.Count == 0 && q1.Count == 0 && q2.Count == 0 && q3.Count == 0 && q4.Count == 0 && q5.Count == 0)
            {
                var result = await addressService.deletetTownAsync(entity);
                await LogData(null,entity.TOWN_ID.ToString());
                return Ok(result);
            }

            else
                return Ok(false);
            //return Ok(await addressService.deletetTownAsync(entity));
        }


        [Route("address/countTown")]
        [HttpGet]
        public async Task<IHttpActionResult> countTown()
        {
            return Ok(await addressService.countTownAsync());
        }


        [Route("address/getTownByID")]
        [HttpGet]
        public TOWNVM getTownByID(int townID)
        {
            return addressService.getTownByID(townID);
        }




        //village

        [Route("address/getVillages")]
        [HttpGet]
        public async Task<IHttpActionResult> getVillages(int pageNum, int pageSize)
        {
            return Ok(await addressService.getAllVillageAsync(pageNum, pageSize));
        }


        [Route("address/addVillage")]
        [HttpPost]
        public async Task<IHttpActionResult> addVillage([FromBody] VILLAGEVM entity)
        {
            var result = await addressService.insertVillageAsync(entity);
            await LogData(null, result.ToString());
            if (result != 0)
            {
                return Ok(true);
            }
            return Ok(false);  
        }


        [Route("address/updateVillage")]
        [HttpPost]
        public async Task<IHttpActionResult> updateVillage([FromBody] VILLAGEVM entity)
        {
            var result = await addressService.updatetVillageAsync(entity);
            await LogData(null,entity.VILLAGE_ID.ToString());
            return Ok(result);

        }


        [Route("address/deleteVillage")]
        [HttpPost]
        public async Task<IHttpActionResult> deleteVillage([FromBody] VILLAGEVM entity)
        {
            var q = employeeService.getByVilID(entity.VILLAGE_ID);
            var q1 = companyStoresService.getByVilID(entity.VILLAGE_ID);
            var q2 = customersService.getByVilID(entity.VILLAGE_ID);
            var q3=bankService.getByVilID(entity.VILLAGE_ID);
            var q4 = companyBranchesService.getByVilID(entity.VILLAGE_ID);
            
            if (q.Count == 0 && q1.Count == 0 && q2.Count == 0 && q3.Count == 0 && q4.Count == 0)
            {
                var result = await addressService.deletetVillageAsync(entity);
                await LogData(null,entity.VILLAGE_ID.ToString());
                return Ok(result);
            }

            else
                return Ok(false);
            //return Ok(await addressService.deletetVillageAsync(entity));
        }


        [Route("address/countVillage")]
        [HttpGet]
        public async Task<IHttpActionResult> countVillage()
        {
            return Ok(await addressService.countVillageAsync());
        }


        [Route("address/getVillByID")]
        [HttpGet]
        public VILLAGEVM getVillByID(int villID)
        {
            return addressService.getVillageByID(villID);
        }

        public async Task<bool> LogData(string code = null, string id = null)
        {
            Logger logger = new Logger();
            await userLogFileService.Insert(logger.LogDataMethod(code,id));
            return true;
        }

    }
}
