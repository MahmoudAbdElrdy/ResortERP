using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResortERP.Core.VM;
using ResortERP.Core;

namespace ResortERP.Service.IServices
{
   public interface IAddressService
    {
        Task<List<NATIONVM>> getAllNationsAsync(int pageNum, int pageSize);
        Task<short> insertNationAsync(NATIONVM entity);
        Task<bool> updatetNationAsync(NATIONVM entity);
        Task<bool> deletetNationAsync(NATIONVM entity);
        Task<int> countNationAsync();
        NATIONVM gatNationByID(int nationID);


        Task<List<GOVERNORATEVM>> getAllGovernAsync(int pageNum, int pageSize);
        Task<short> insertGovernAsync(GOVERNORATEVM entity);
        Task<bool> updatetGovernAsync(GOVERNORATEVM entity);
        Task<bool> deletetGovernAsync(GOVERNORATEVM entity);
        Task<int> countGovernAsync();
        GOVERNORATEVM gatGovByID(int govID);


        Task<List<TOWNVM>> getAllTownsAsync(int pageNum, int pageSize);
        Task<int> insertTownAsync(TOWNVM entity);
        Task<bool> updatetTownAsync(TOWNVM entity);
        Task<bool> deletetTownAsync(TOWNVM entity);
        Task<int> countTownAsync();
        TOWNVM getTownByID(int townID);


        Task<List<VILLAGEVM>> getAllVillageAsync(int pageNum, int pageSize);
        Task<long> insertVillageAsync(VILLAGEVM entity);
        Task<bool> updatetVillageAsync(VILLAGEVM entity);
        Task<bool> deletetVillageAsync(VILLAGEVM entity);
        Task<int> countVillageAsync();
        VILLAGEVM getVillageByID(int villageID);

        List<VILLAGES> getTownVillage(int townId);
        List<GOVERNORATES> getNationGov(int nationId);
        List<TOWNS> getGovTown(int govId);

    }
}
