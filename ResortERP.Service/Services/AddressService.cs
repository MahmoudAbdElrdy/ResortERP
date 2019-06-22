using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResortERP.Service.IServices;
using ResortERP.Repository;
using ResortERP.Core;
using ResortERP.Core.VM;

namespace ResortERP.Service.Services
{
    public class AddressService : IAddressService, IDisposable
    {
        IGenericRepository<NATIONS> nationsRepo;
        IGenericRepository<GOVERNORATES> govRepo;
        IGenericRepository<TOWNS> townRepo;
        IGenericRepository<VILLAGES> villageRepo;
        public void Dispose()
        {
            nationsRepo.Dispose();
        }



        //, IGenericRepository<GOVERNORATES> govRepo, IGenericRepository<TOWNS> townRepo, IGenericRepository<VILLAGES> villageRepo
        public AddressService(IGenericRepository<NATIONS> nationsRepo, IGenericRepository<GOVERNORATES> govRepo, IGenericRepository<TOWNS> townRepo, IGenericRepository<VILLAGES> villageRepo)
        {
            this.nationsRepo = nationsRepo;
            this.govRepo = govRepo;
            this.townRepo = townRepo;
            this.villageRepo = villageRepo;
        }

        public Task<List<NATIONVM>> getAllNationsAsync(int pageNum, int pageSize)
        {
            return Task.Run(() =>
            {

                int rowCount;
                var nationList = from nat in nationsRepo.GetPaged(pageNum, pageSize, n => n.NATION_ID, false, out rowCount)
                                 select new NATIONVM
                                 {
                                     NATION_ID = nat.NATION_ID,
                                     NATIONALITY_EN_NAME = nat.NATIONALITY_EN_NAME,
                                     NATIONALITY_AR_NAME = nat.NATIONALITY_AR_NAME,
                                     NATION_AR_NAME = nat.NATION_AR_NAME,
                                     NATION_EN_NAME = nat.NATION_EN_NAME,
                                     UpdatedBy = nat.UpdatedBy,
                                     updatedOn = nat.updatedOn,
                                     AddedBy = nat.AddedBy,
                                     AddedOn = nat.AddedOn,
                                     Disable = nat.Disable,

                                 };
                return nationList.ToList();
            });
        }


        public Task<short> insertNationAsync(NATIONVM entity)
        {
            return Task.Run(() =>
            {
                NATIONS nation = new NATIONS();
                {
                    nation.NATION_ID = entity.NATION_ID;
                    nation.NATIONALITY_EN_NAME = entity.NATIONALITY_EN_NAME;
                    nation.NATIONALITY_AR_NAME = entity.NATIONALITY_AR_NAME;
                    nation.NATION_AR_NAME = entity.NATION_AR_NAME;
                    nation.NATION_EN_NAME = entity.NATION_EN_NAME;
                    nation.UpdatedBy = entity.UpdatedBy;
                    nation.updatedOn = entity.updatedOn;
                    nation.AddedOn = entity.AddedOn;
                    nation.Disable = entity.Disable;
                    nation.AddedBy = entity.AddedBy;

                };
                nationsRepo.Add(nation);
                return nation.NATION_ID;

            });
           
        }


        public Task<bool> updatetNationAsync(NATIONVM entity)
        {
            return Task.Run(() =>
            {
                NATIONS nation = new NATIONS();
                {
                    nation.NATION_ID = entity.NATION_ID;
                    nation.NATIONALITY_EN_NAME = entity.NATIONALITY_EN_NAME;
                    nation.NATIONALITY_AR_NAME = entity.NATIONALITY_AR_NAME;
                    nation.NATION_AR_NAME = entity.NATION_AR_NAME;
                    nation.NATION_EN_NAME = entity.NATION_EN_NAME;
                    nation.UpdatedBy = entity.UpdatedBy;
                    nation.updatedOn = entity.updatedOn;
                    nation.AddedOn = entity.AddedOn;
                    nation.Disable = entity.Disable;
                    nation.AddedBy = entity.AddedBy;

                };
                nationsRepo.Update(nation, nation.NATION_ID);
                return true;
            });
        }


        public Task<bool> deletetNationAsync(NATIONVM entity)
        {
            return Task.Run(() =>
            {
                NATIONS nation = new NATIONS();
                {
                    nation.NATION_ID = entity.NATION_ID;
                    nation.NATIONALITY_EN_NAME = entity.NATIONALITY_EN_NAME;
                    nation.NATIONALITY_AR_NAME = entity.NATIONALITY_AR_NAME;
                    nation.NATION_AR_NAME = entity.NATION_AR_NAME;
                    nation.NATION_EN_NAME = entity.NATION_EN_NAME;
                    nation.UpdatedBy = entity.UpdatedBy;
                    nation.updatedOn = entity.updatedOn;
                    nation.AddedOn = entity.AddedOn;
                    nation.Disable = entity.Disable;
                    nation.AddedBy = entity.AddedBy;

                };
                nationsRepo.Delete(nation, nation.NATION_ID);
                return true;
            });
        }

        public Task<int> countNationAsync()
        {
            return Task.Run(() =>
            {
                return nationsRepo.CountAsync();
            });
        }


        public NATIONVM gatNationByID(int nationID)
        {
            var nation = from nat in nationsRepo.GetAll().Where(n => n.NATION_ID == nationID)
                         select new NATIONVM
                         {
                             NATION_ID = nat.NATION_ID,
                             NATIONALITY_EN_NAME = nat.NATIONALITY_EN_NAME,
                             NATIONALITY_AR_NAME = nat.NATIONALITY_AR_NAME,
                             NATION_AR_NAME = nat.NATION_AR_NAME,
                             NATION_EN_NAME = nat.NATION_EN_NAME,
                             UpdatedBy = nat.UpdatedBy,
                             updatedOn = nat.updatedOn,
                             AddedBy = nat.AddedBy,
                             AddedOn = nat.AddedOn,
                             Disable = nat.Disable,

                         };
            return nation.FirstOrDefault();

        }







        public Task<List<GOVERNORATEVM>> getAllGovernAsync(int pageNum, int pageSize)
        {
            return Task.Run(() =>
            {

                int rowCount;
                var govnList = from gov in govRepo.GetPaged(pageNum, pageSize, g => g.GOV_ID, false, out rowCount)
                               join nat in nationsRepo.GetPaged(pageNum, pageSize, g => g.NATION_ID, false, out rowCount) on gov.NATION_ID equals nat.NATION_ID into g
                               from y in g.DefaultIfEmpty()
                               select new GOVERNORATEVM
                               {
                                   GOV_ID = gov.GOV_ID,
                                   GOV_EN_NAME = gov.GOV_EN_NAME,
                                   GOV_AR_NAME = gov.GOV_AR_NAME,
                                   NATION_ID = gov.NATION_ID,
                                   UpdatedBy = gov.UpdatedBy,
                                   updatedOn = gov.updatedOn,
                                   AddedBy = gov.AddedBy,
                                   AddedOn = gov.AddedOn,
                                   Disable = gov.Disable,
                                   NationName = y.NATION_AR_NAME,
                               };
                return govnList.ToList();
            });
        }


        public Task<short> insertGovernAsync(GOVERNORATEVM entity)
        {
            return Task.Run(() =>
            {
                GOVERNORATES gov = new GOVERNORATES();
                {
                    gov.GOV_ID = entity.GOV_ID;
                    gov.GOV_EN_NAME = entity.GOV_EN_NAME;
                    gov.GOV_AR_NAME = entity.GOV_AR_NAME;
                    gov.NATION_ID = entity.NATION_ID;
                    gov.UpdatedBy = entity.UpdatedBy;
                    gov.updatedOn = entity.updatedOn;
                    gov.AddedOn = entity.AddedOn;
                    gov.Disable = entity.Disable;
                    gov.AddedBy = entity.AddedBy;

                };
                govRepo.Add(gov);
                return gov.GOV_ID;
            });
        }


        public Task<bool> updatetGovernAsync(GOVERNORATEVM entity)
        {
            return Task.Run(() =>
            {
                GOVERNORATES gov = new GOVERNORATES();
                {
                    gov.GOV_ID = entity.GOV_ID;
                    gov.GOV_EN_NAME = entity.GOV_EN_NAME;
                    gov.GOV_AR_NAME = entity.GOV_AR_NAME;
                    gov.NATION_ID = entity.NATION_ID;
                    gov.UpdatedBy = entity.UpdatedBy;
                    gov.updatedOn = entity.updatedOn;
                    gov.AddedOn = entity.AddedOn;
                    gov.Disable = entity.Disable;
                    gov.AddedBy = entity.AddedBy;

                };
                govRepo.Update(gov, gov.GOV_ID);
                return true;
            });
        }


        public Task<bool> deletetGovernAsync(GOVERNORATEVM entity)
        {
            return Task.Run(() =>
            {
                GOVERNORATES gov = new GOVERNORATES();
                {
                    gov.GOV_ID = entity.GOV_ID;
                    gov.GOV_EN_NAME = entity.GOV_EN_NAME;
                    gov.GOV_AR_NAME = entity.GOV_AR_NAME;
                    gov.NATION_ID = entity.NATION_ID;
                    gov.UpdatedBy = entity.UpdatedBy;
                    gov.updatedOn = entity.updatedOn;
                    gov.AddedOn = entity.AddedOn;
                    gov.Disable = entity.Disable;
                    gov.AddedBy = entity.AddedBy;

                };
                govRepo.Delete(gov, gov.GOV_ID);
                return true;
            });
        }


        public Task<int> countGovernAsync()
        {
            return Task.Run(() =>
            {
                return govRepo.CountAsync();
            });
        }

        public GOVERNORATEVM gatGovByID(int govID)
        {

            var govn = from gov in govRepo.GetAll().Where(g => g.GOV_ID == govID)

                       select new GOVERNORATEVM
                       {
                           GOV_ID = gov.GOV_ID,
                           GOV_EN_NAME = gov.GOV_EN_NAME,
                           GOV_AR_NAME = gov.GOV_AR_NAME,
                           NATION_ID = gov.NATION_ID,
                           UpdatedBy = gov.UpdatedBy,
                           updatedOn = gov.updatedOn,
                           AddedBy = gov.AddedBy,
                           AddedOn = gov.AddedOn,
                           Disable = gov.Disable,

                       };
            return govn.FirstOrDefault();


        }









        public Task<List<TOWNVM>> getAllTownsAsync(int pageNum, int pageSize)
        {
            return Task.Run(() =>
            {

                int rowCount;
                //var townList = from town in townRepo.GetPaged(pageNum, pageSize, t => t.TOWN_ID, false, out rowCount)
                //               join gov in govRepo.GetPaged(pageNum, pageSize, g => g.GOV_ID, false, out rowCount) on town.GOV_ID equals gov.GOV_ID into g
                //               from y in g.DefaultIfEmpty()
                //               //join nat in nationsRepo.GetPaged(pageNum, pageSize, g => g.NATION_ID, false, out rowCount) on g1.NATION_ID equals nat.NATION_ID into group2
                //               //from g2 in group2.DefaultIfEmpty()
                //               select new TOWNVM
                //               {
                //                   TOWN_ID = town.TOWN_ID,
                //                   TOWN_EN_NAME = town.TOWN_EN_NAME,
                //                   TOWN_AR_NAME = town.TOWN_AR_NAME,
                //                   UpdatedBy = town.UpdatedBy,
                //                   updatedOn = town.updatedOn,
                //                   AddedBy = town.AddedBy,
                //                   AddedOn = town.AddedOn,
                //                   Disable = town.Disable,
                //                   GOV_ID = town.GOV_ID,
                //                   GovName = y.GOV_AR_NAME,
                //                   //NationName = g2.NATION_AR_NAME,
                //                   //NationId = g2.NATION_ID
                //               };





                var townList = from town in townRepo.GetPaged(pageNum, pageSize, g => g.TOWN_ID, false, out rowCount)
                               join gov in govRepo.GetPaged(pageNum, pageSize, g => g.GOV_ID, false, out rowCount) on town.GOV_ID equals gov.GOV_ID into g
                               from y in g.DefaultIfEmpty()
                               select new TOWNVM
                               {
                                   TOWN_ID = town.TOWN_ID,
                                   TOWN_EN_NAME = town.TOWN_EN_NAME,
                                   TOWN_AR_NAME = town.TOWN_AR_NAME,
                                   UpdatedBy = town.UpdatedBy,
                                   updatedOn = town.updatedOn,
                                   AddedBy = town.AddedBy,
                                   AddedOn = town.AddedOn,
                                   Disable = town.Disable,
                                   GOV_ID = town.GOV_ID,
                                   GovName = y.GOV_AR_NAME,
                                   //NationName = g2.NATION_AR_NAME,
                                   //NationId = g2.NATION_ID

                               };
                return townList.ToList();
            });
        }


        public Task<int> insertTownAsync(TOWNVM entity)
        {
            return Task.Run(() =>
            {
                TOWNS town = new TOWNS();
                {
                    town.TOWN_ID = entity.TOWN_ID;
                    town.TOWN_EN_NAME = entity.TOWN_EN_NAME;
                    town.TOWN_AR_NAME = entity.TOWN_AR_NAME;
                    town.GOV_ID = entity.GOV_ID;
                    town.UpdatedBy = entity.UpdatedBy;
                    town.updatedOn = entity.updatedOn;
                    town.AddedOn = entity.AddedOn;
                    town.Disable = entity.Disable;
                    town.AddedBy = entity.AddedBy;

                };
                townRepo.Add(town);
                return town.TOWN_ID;
            });
        }


        public Task<bool> updatetTownAsync(TOWNVM entity)
        {
            return Task.Run(() =>
            {
                TOWNS town = new TOWNS();
                {
                    town.TOWN_ID = entity.TOWN_ID;
                    town.TOWN_EN_NAME = entity.TOWN_EN_NAME;
                    town.TOWN_AR_NAME = entity.TOWN_AR_NAME;
                    town.GOV_ID = entity.GOV_ID;
                    town.UpdatedBy = entity.UpdatedBy;
                    town.updatedOn = entity.updatedOn;
                    town.AddedOn = entity.AddedOn;
                    town.Disable = entity.Disable;
                    town.AddedBy = entity.AddedBy;

                };
                townRepo.Update(town, town.TOWN_ID);
                return true;
            });
        }


        public Task<bool> deletetTownAsync(TOWNVM entity)
        {
            return Task.Run(() =>
            {
                TOWNS town = new TOWNS();
                {
                    town.TOWN_ID = entity.TOWN_ID;
                    town.TOWN_EN_NAME = entity.TOWN_EN_NAME;
                    town.TOWN_AR_NAME = entity.TOWN_AR_NAME;
                    town.GOV_ID = entity.GOV_ID;
                    town.UpdatedBy = entity.UpdatedBy;
                    town.updatedOn = entity.updatedOn;
                    town.AddedOn = entity.AddedOn;
                    town.Disable = entity.Disable;
                    town.AddedBy = entity.AddedBy;

                };
                townRepo.Delete(town, town.TOWN_ID);
                return true;
            });
        }

        public Task<int> countTownAsync()
        {
            return Task.Run(() =>
            {
                return townRepo.CountAsync();
            });
        }

        public TOWNVM getTownByID(int townID)
        {
            var townList = from town in townRepo.GetAll().Where(t => t.TOWN_ID == townID)

                           select new TOWNVM
                           {
                               TOWN_ID = town.TOWN_ID,
                               TOWN_EN_NAME = town.TOWN_EN_NAME,
                               TOWN_AR_NAME = town.TOWN_AR_NAME,
                               UpdatedBy = town.UpdatedBy,
                               updatedOn = town.updatedOn,
                               AddedBy = town.AddedBy,
                               AddedOn = town.AddedOn,
                               Disable = town.Disable,
                               GOV_ID = town.GOV_ID,


                           };
            return townList.FirstOrDefault();

        }









        public Task<List<VILLAGEVM>> getAllVillageAsync(int pageNum, int pageSize)
        {
            return Task.Run(() =>
            {

                int rowCount;
                var villageList = from village in villageRepo.GetPaged(pageNum, pageSize, t => t.TOWN_ID, false, out rowCount)
                                  join town in townRepo.GetPaged(pageNum, pageSize, t => t.TOWN_ID, false, out rowCount) on village.TOWN_ID equals town.TOWN_ID into group1
                                  from g1 in group1.DefaultIfEmpty()
                                  join gov in govRepo.GetPaged(pageNum, pageSize, g => g.NATION_ID, false, out rowCount) on g1.GOV_ID equals gov.GOV_ID into group2
                                  from g2 in group2.DefaultIfEmpty()
                                  join nat in nationsRepo.GetPaged(pageNum, pageSize, g => g.NATION_ID, false, out rowCount) on g2.NATION_ID equals nat.NATION_ID into group3
                                  from g3 in group2.DefaultIfEmpty()
                                  select new VILLAGEVM
                                  {
                                      VILLAGE_ID = village.VILLAGE_ID,
                                      VILLAGE_AR_NAME = village.VILLAGE_AR_NAME,
                                      VILLAGE_EN_NAME = village.VILLAGE_EN_NAME,
                                      UpdatedBy = village.UpdatedBy,
                                      updatedOn = village.updatedOn,
                                      AddedBy = village.AddedBy,
                                      AddedOn = village.AddedOn,
                                      Disable = village.Disable,
                                      TOWN_ID = village.TOWN_ID,
                                      TownName = g1.TOWN_AR_NAME,
                                      GovId = g2.GOV_ID,
                                      GovName = g2.GOV_AR_NAME,
                                      NationId = g3.NATION_ID,
                                      NationName = g3.GOV_AR_NAME,
                                  };
                return villageList.ToList();
            });
        }

        public Task<long> insertVillageAsync(VILLAGEVM entity)
        {
            return Task.Run(() =>
            {
                VILLAGES village = new VILLAGES();
                {
                    village.VILLAGE_ID = entity.VILLAGE_ID;
                    village.VILLAGE_EN_NAME = entity.VILLAGE_EN_NAME;
                    village.VILLAGE_AR_NAME = entity.VILLAGE_AR_NAME;
                    village.TOWN_ID = entity.TOWN_ID;
                    village.UpdatedBy = entity.UpdatedBy;
                    village.updatedOn = entity.updatedOn;
                    village.AddedOn = entity.AddedOn;
                    village.Disable = entity.Disable;
                    village.AddedBy = entity.AddedBy;

                };
                villageRepo.Add(village);
                return village.VILLAGE_ID;
            });
        }


        public Task<bool> updatetVillageAsync(VILLAGEVM entity)
        {
            return Task.Run(() =>
            {
                VILLAGES village = new VILLAGES();
                {
                    village.VILLAGE_ID = entity.VILLAGE_ID;
                    village.VILLAGE_EN_NAME = entity.VILLAGE_EN_NAME;
                    village.VILLAGE_AR_NAME = entity.VILLAGE_AR_NAME;
                    village.TOWN_ID = entity.TOWN_ID;
                    village.UpdatedBy = entity.UpdatedBy;
                    village.updatedOn = entity.updatedOn;
                    village.AddedOn = entity.AddedOn;
                    village.Disable = entity.Disable;
                    village.AddedBy = entity.AddedBy;

                };
                villageRepo.Update(village, village.VILLAGE_ID);
                return true;
            });
        }


        public Task<bool> deletetVillageAsync(VILLAGEVM entity)
        {
            return Task.Run(() =>
            {
                VILLAGES village = new VILLAGES();
                {
                    village.VILLAGE_ID = entity.VILLAGE_ID;
                    village.VILLAGE_EN_NAME = entity.VILLAGE_EN_NAME;
                    village.VILLAGE_AR_NAME = entity.VILLAGE_AR_NAME;
                    village.TOWN_ID = entity.TOWN_ID;
                    village.UpdatedBy = entity.UpdatedBy;
                    village.updatedOn = entity.updatedOn;
                    village.AddedOn = entity.AddedOn;
                    village.Disable = entity.Disable;
                    village.AddedBy = entity.AddedBy;

                };
                villageRepo.Delete(village, village.VILLAGE_ID);
                return true;
            });
        }


        public Task<int> countVillageAsync()
        {
            return Task.Run(() =>
            {
                return villageRepo.CountAsync();
            });
        }

        public VILLAGEVM getVillageByID(int villageID)
        {
            var villageList = from village in villageRepo.GetAll().Where(v => v.VILLAGE_ID == villageID)
                              select new VILLAGEVM
                              {
                                  VILLAGE_ID = village.VILLAGE_ID,
                                  VILLAGE_AR_NAME = village.VILLAGE_AR_NAME,
                                  VILLAGE_EN_NAME = village.VILLAGE_EN_NAME,
                                  UpdatedBy = village.UpdatedBy,
                                  updatedOn = village.updatedOn,
                                  AddedBy = village.AddedBy,
                                  AddedOn = village.AddedOn,
                                  Disable = village.Disable,
                                  TOWN_ID = village.TOWN_ID,

                              };
            return villageList.FirstOrDefault();
        }

        public List<VILLAGES> getTownVillage(int townId)
        {
            var q=villageRepo.GetAll().Where(a => a.TOWN_ID == townId);
            return q.ToList();
        }

        public List<TOWNS> getGovTown(int govId)
        {
            var q = townRepo.GetAll().Where(a => a.GOV_ID == govId);
            return q.ToList();
        }

        public List<GOVERNORATES> getNationGov(int nationId)
        {
            var q = govRepo.GetAll().Where(a => a.NATION_ID == nationId);
            return q.ToList();
        }

    }



}
