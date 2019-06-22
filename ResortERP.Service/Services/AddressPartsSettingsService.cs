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
    public class AddressPartsSettingsService : IAddressPartsSettingsService, IDisposable
    {

        IGenericRepository<AddressPartsSettings> partsRepo;

        public AddressPartsSettingsService(IGenericRepository<AddressPartsSettings> _partsRepo)
        {
            this.partsRepo = _partsRepo;
        }

        public void Dispose()
        {
            partsRepo.Dispose();
        }


        public Task<List<AddressPartsSettingsVM>> getAddressPartsAsync()
        {
            return Task.Run(() =>
            {

                var addressParts = from p in partsRepo.GetAll()

                                   select new AddressPartsSettingsVM
                                   {

                                       AddressPartID = p.AddressPartID,
                                       AddressPart1 = p.AddressPart1,
                                       AddressPart2 = p.AddressPart2,
                                       AddressPart3 = p.AddressPart3,
                                       AddressPart4 = p.AddressPart4,
                                       UID = p.UID
                                   };
                return addressParts.ToList();
            });
        }

        public Task<bool> insertAddressPartsSync(AddressPartsSettingsVM entity)
        {
            return Task.Run(() =>
            {
                AddressPartsSettings addressPart = new AddressPartsSettings();
                {
                    addressPart.AddressPartID = entity.AddressPartID;
                    addressPart.AddressPart1 = entity.AddressPart1;
                    addressPart.AddressPart2 = entity.AddressPart2;
                    addressPart.AddressPart3 = entity.AddressPart3;
                    addressPart.AddressPart4 = entity.AddressPart4;
                    addressPart.UID = entity.UID;
                };

                partsRepo.Add(addressPart);
                return true;
            });

        }

        public Task<bool> updateAddressPartsSync(AddressPartsSettingsVM entity)
        {
            return Task.Run(() =>
            {
                AddressPartsSettings addressPart = new AddressPartsSettings();
                {
                    addressPart.AddressPartID = entity.AddressPartID;
                    addressPart.AddressPart1 = entity.AddressPart1;
                    addressPart.AddressPart2 = entity.AddressPart2;
                    addressPart.AddressPart3 = entity.AddressPart3;
                    addressPart.AddressPart4 = entity.AddressPart4;
                };

                partsRepo.Update(addressPart, addressPart.AddressPartID);
                return true;
            });

        }

        public Task<bool> deleteAddressPartsSync(AddressPartsSettingsVM entity)
        {
            return Task.Run(() =>
            {
                if (entity != null)
                {
                    AddressPartsSettings addressPart = new AddressPartsSettings();
                    {

                        addressPart.AddressPartID = entity.AddressPartID;
                        addressPart.AddressPart1 = entity.AddressPart1;
                        addressPart.AddressPart2 = entity.AddressPart2;
                        addressPart.AddressPart3 = entity.AddressPart3;
                        addressPart.AddressPart4 = entity.AddressPart4;
                    };

                    partsRepo.Delete(addressPart, addressPart.AddressPartID);
                }
                return true;
            });

        }

        public Task<int> countAddressPartsAsync()
        {
            return Task.Run(() =>
            {
                return partsRepo.CountAsync();
            });
        }



        public Task<AddressPartsSettingsVM> getAddrPartsByUID(string userName)
        {
            return Task.Run(() =>
            {

                var addressParts = from p in partsRepo.GetAll().Where(p => p.UID == userName)

                                   select new AddressPartsSettingsVM
                                   {

                                       AddressPartID = p.AddressPartID,
                                       AddressPart1 = p.AddressPart1,
                                       AddressPart2 = p.AddressPart2,
                                       AddressPart3 = p.AddressPart3,
                                       AddressPart4 = p.AddressPart4,
                                       UID = p.UID
                                   };
                return addressParts.FirstOrDefault();
            });
        }
    }
}
