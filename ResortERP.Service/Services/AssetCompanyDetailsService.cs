using ResortERP.Core;
using ResortERP.Core.VM;
using ResortERP.Repository;
using ResortERP.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.Services
{
    public class AssetCompanyDetailsService : IAssetCompanyDetailsService, IDisposable
    {
        IGenericRepository<AssetCompanyDetails> AssetCompanyDetailsRepo;
        public AssetCompanyDetailsService(IGenericRepository<AssetCompanyDetails> AssetCompanyDetailsRepo)
        {
            this.AssetCompanyDetailsRepo = AssetCompanyDetailsRepo;
        }

        public void Dispose()
        {
            AssetCompanyDetailsRepo.Dispose();
        }


        public bool Insert(AssetCompanyDetailsVM entity)
        {
            AssetCompanyDetails ig = new AssetCompanyDetails
            {
                ID = entity.ID,
                CompanyNationID = entity.CompanyNationID,
                CompanyGovernerateID = entity.CompanyGovernerateID,
                CompanyTownID = entity.CompanyTownID,
                CompanyVillageID = entity.CompanyVillageID,
                CompanyAddressDetails = entity.CompanyAddressDetails,
                CompanyPhone = entity.CompanyPhone,
                CompanyMobile = entity.CompanyMobile,
                CompanyFax = entity.CompanyFax,
                CompanyEmail = entity.CompanyEmail,
                CompanySite = entity.CompanySite,
                Type = entity.Type,
                Notes = entity.Notes,
                AddedBy = entity.AddedBy,
                AddedOn = DateTime.Now,
                UpdatedBy = entity.UpdatedBy,
                UpdatedOn = entity.UpdatedOn,
                Active = entity.Active,
                Position = entity.Position,
                Code = entity.Code,
                ENName = entity.ENName,
                ARName = entity.ARName,
            };
            AssetCompanyDetailsRepo.Add(ig);
            return true;
        }

        public Task<int> InsertAsync(AssetCompanyDetailsVM entity)
        {
            return Task.Run<int>(() =>
            {
                AssetCompanyDetails ig = new AssetCompanyDetails
                {
                    ID = entity.ID,
                    CompanyNationID = entity.CompanyNationID,
                    CompanyGovernerateID = entity.CompanyGovernerateID,
                    CompanyTownID = entity.CompanyTownID,
                    CompanyVillageID = entity.CompanyVillageID,
                    CompanyAddressDetails = entity.CompanyAddressDetails,
                    CompanyPhone = entity.CompanyPhone,
                    CompanyMobile = entity.CompanyMobile,
                    CompanyFax = entity.CompanyFax,
                    CompanyEmail = entity.CompanyEmail,
                    CompanySite = entity.CompanySite,
                    Type = entity.Type,
                    Notes = entity.Notes,
                    AddedBy = entity.AddedBy,
                    AddedOn = DateTime.Now,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedOn = entity.UpdatedOn,
                    Active = entity.Active,
                    Position = entity.Position,
                    Code = entity.Code,
                    ENName = entity.ENName,
                    ARName = entity.ARName,
                };
                AssetCompanyDetailsRepo.Add(ig);
                return ig.ID;
            });
        }

        public bool Update(AssetCompanyDetailsVM entity)
        {
            AssetCompanyDetails ig = new AssetCompanyDetails
            {
                ID = entity.ID,
                CompanyNationID = entity.CompanyNationID,
                CompanyGovernerateID = entity.CompanyGovernerateID,
                CompanyTownID = entity.CompanyTownID,
                CompanyVillageID = entity.CompanyVillageID,
                CompanyAddressDetails = entity.CompanyAddressDetails,
                CompanyPhone = entity.CompanyPhone,
                CompanyMobile = entity.CompanyMobile,
                CompanyFax = entity.CompanyFax,
                CompanyEmail = entity.CompanyEmail,
                CompanySite = entity.CompanySite,
                Type = entity.Type,
                Notes = entity.Notes,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                UpdatedBy = entity.UpdatedBy,
                UpdatedOn = DateTime.Now,
                Active = entity.Active,
                Position = entity.Position,
                Code = entity.Code,
                ENName = entity.ENName,
                ARName = entity.ARName,
            };
            AssetCompanyDetailsRepo.Update(ig, ig.ID);
            return true;
        }
        public Task<bool> UpdateAsync(AssetCompanyDetailsVM entity)
        {
            return Task.Run<bool>(() =>
            {
                AssetCompanyDetails ig = new AssetCompanyDetails
                {
                    ID = entity.ID,
                    CompanyNationID = entity.CompanyNationID,
                    CompanyGovernerateID = entity.CompanyGovernerateID,
                    CompanyTownID = entity.CompanyTownID,
                    CompanyVillageID = entity.CompanyVillageID,
                    CompanyAddressDetails = entity.CompanyAddressDetails,
                    CompanyPhone = entity.CompanyPhone,
                    CompanyMobile = entity.CompanyMobile,
                    CompanyFax = entity.CompanyFax,
                    CompanyEmail = entity.CompanyEmail,
                    CompanySite = entity.CompanySite,
                    Type = entity.Type,
                    Notes = entity.Notes,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedOn = DateTime.Now,
                    Active = entity.Active,
                    Position = entity.Position,
                    Code = entity.Code,
                    ENName = entity.ENName,
                    ARName = entity.ARName,
                };
                AssetCompanyDetailsRepo.Update(ig, ig.ID);
                return true;
            });
        }

        public bool Delete(AssetCompanyDetailsVM entity)
        {
            AssetCompanyDetails ig = new AssetCompanyDetails
            {
                ID = entity.ID,
                CompanyNationID = entity.CompanyNationID,
                CompanyGovernerateID = entity.CompanyGovernerateID,
                CompanyTownID = entity.CompanyTownID,
                CompanyVillageID = entity.CompanyVillageID,
                CompanyAddressDetails = entity.CompanyAddressDetails,
                CompanyPhone = entity.CompanyPhone,
                CompanyMobile = entity.CompanyMobile,
                CompanyFax = entity.CompanyFax,
                CompanyEmail = entity.CompanyEmail,
                CompanySite = entity.CompanySite,
                Type = entity.Type,
                Notes = entity.Notes,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                UpdatedBy = entity.UpdatedBy,
                UpdatedOn = entity.UpdatedOn,
                Active = entity.Active,
                Position = entity.Position,
                Code = entity.Code,
                ENName = entity.ENName,
                ARName = entity.ARName,
            };
            AssetCompanyDetailsRepo.Delete(ig, entity.ID);
            return true;
        }

        public Task<bool> DeleteAsync(AssetCompanyDetailsVM entity)
        {
            return Task.Run<bool>(() =>
            {
                AssetCompanyDetails ig = new AssetCompanyDetails
                {
                    ID = entity.ID,
                    CompanyNationID = entity.CompanyNationID,
                    CompanyGovernerateID = entity.CompanyGovernerateID,
                    CompanyTownID = entity.CompanyTownID,
                    CompanyVillageID = entity.CompanyVillageID,
                    CompanyAddressDetails = entity.CompanyAddressDetails,
                    CompanyPhone = entity.CompanyPhone,
                    CompanyMobile = entity.CompanyMobile,
                    CompanyFax = entity.CompanyFax,
                    CompanyEmail = entity.CompanyEmail,
                    CompanySite = entity.CompanySite,
                    Type = entity.Type,
                    Notes = entity.Notes,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedOn = entity.UpdatedOn,
                    Active = entity.Active,
                    Position = entity.Position,
                    Code = entity.Code,
                    ENName = entity.ENName,
                    ARName = entity.ARName,
                };
                AssetCompanyDetailsRepo.Delete(ig, entity.ID);
                return true;
            });
        }


        public Task<AssetCompanyDetailsVM> getById(int id)
        {
            return Task.Run<AssetCompanyDetailsVM>(() =>
            {
                var q = from p in AssetCompanyDetailsRepo.GetAll().Where(a => a.ID == id)
                        select new AssetCompanyDetailsVM
                        {
                            ID = p.ID,
                            CompanyNationID = p.CompanyNationID,
                            CompanyGovernerateID = p.CompanyGovernerateID,
                            CompanyTownID = p.CompanyTownID,
                            CompanyVillageID = p.CompanyVillageID,
                            CompanyAddressDetails = p.CompanyAddressDetails,
                            CompanyPhone = p.CompanyPhone,
                            CompanyMobile = p.CompanyMobile,
                            CompanyFax = p.CompanyFax,
                            CompanyEmail = p.CompanyEmail,
                            CompanySite = p.CompanySite,
                            Type = p.Type,
                            Notes = p.Notes,
                            AddedBy = p.AddedBy,
                            AddedOn = p.AddedOn,
                            UpdatedBy = p.UpdatedBy,
                            UpdatedOn = p.UpdatedOn,
                            Active = p.Active,
                            Position = p.Position,
                            Code = p.Code,
                            ENName = p.ENName,
                            ARName = p.ARName,
                        };
                return q.FirstOrDefault();
            });
        }
        public Task<List<AssetCompanyDetailsVM>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run<List<AssetCompanyDetailsVM>>(() =>
            {
                int rowCount;
                var q = from p in AssetCompanyDetailsRepo.GetPaged<int>(pageNum, pageSize, p => p.ID, false, out rowCount)
                        select new AssetCompanyDetailsVM
                        {
                            ID = p.ID,
                            CompanyNationID = p.CompanyNationID,
                            CompanyGovernerateID = p.CompanyGovernerateID,
                            CompanyTownID = p.CompanyTownID,
                            CompanyVillageID = p.CompanyVillageID,
                            CompanyAddressDetails = p.CompanyAddressDetails,
                            CompanyPhone = p.CompanyPhone,
                            CompanyMobile = p.CompanyMobile,
                            CompanyFax = p.CompanyFax,
                            CompanyEmail = p.CompanyEmail,
                            CompanySite = p.CompanySite,
                            Type = p.Type,
                            Notes = p.Notes,
                            AddedBy = p.AddedBy,
                            AddedOn = p.AddedOn,
                            UpdatedBy = p.UpdatedBy,
                            UpdatedOn = p.UpdatedOn,
                            Active = p.Active,
                            Position = p.Position,
                            Code = p.Code,
                            ENName = p.ENName,
                            ARName = p.ARName,
                        };
                return q.ToList();
            });
        }

        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return AssetCompanyDetailsRepo.CountAsync();
            });
        }

        public string GetLastCode()
        {
            var Code = AssetCompanyDetailsRepo.GetAll().OrderByDescending(u => u.ID).FirstOrDefault();
            //var x = Code.OrderByDescending(u => u.UNIT_ID).ToList();
            //   var y =x.FirstOrDefault() ;
            string lastCode = "0";
            if (Code != null)
            {
                lastCode = Code.Code;
            }

            return lastCode;
        }
    }
}
