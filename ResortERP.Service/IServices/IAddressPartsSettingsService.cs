using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface IAddressPartsSettingsService
    {
        Task<List<AddressPartsSettingsVM>> getAddressPartsAsync();
        Task<bool> insertAddressPartsSync(AddressPartsSettingsVM entity);
        Task<bool> updateAddressPartsSync(AddressPartsSettingsVM entity);
        Task<bool> deleteAddressPartsSync(AddressPartsSettingsVM entity);
        Task<int> countAddressPartsAsync();
        Task<AddressPartsSettingsVM> getAddrPartsByUID(string userName);
    }
}
