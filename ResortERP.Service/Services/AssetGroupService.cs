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
    public class AssetGroupService : IAssetGroupService, IDisposable
    {
        IGenericRepository<AssetGroup> assetGroupRepo;
        public void Dispose()
        {
            assetGroupRepo.Dispose();
        }


        public AssetGroupService(IGenericRepository<AssetGroup> _assetGroupRepo, IGenericRepository<Currency> _currencyRepo)
        {
            this.assetGroupRepo = _assetGroupRepo;

        }

        public Task<List<AssetGroupVM>> getAssetGroupAsync(int pageNum, int pageSize)
        {
            return Task.Run(() =>
            {
                int rowCount;
                var assetGroupList = from g in assetGroupRepo.GetPaged(pageNum, pageSize, b => b.ID, false, out rowCount)                                    
                                     select new AssetGroupVM
                                     {
                                         ID = g.ID,
                                         Code = g.Code,
                                         ARName = g.ARName,
                                         LatName = g.LatName,
                                         AssetGroupID = g.AssetGroupID,
                                         AssetGroupAccountID = g.AssetGroupAccountID,
                                         DepreciationAccountID = g.DepreciationAccountID,
                                         TotalDepreciationAccountID = g.TotalDepreciationAccountID,
                                         ExpensesAccountID = g.ExpensesAccountID,
                                         CapitalProfitAccountID = g.CapitalProfitAccountID,
                                         CapitalLossAccountID = g.CapitalLossAccountID,
                                         AppraisalExcessAccountID = g.AppraisalExcessAccountID,
                                         ApprasialDeficitAccountID = g.ApprasialDeficitAccountID,
                                         AddedBy = g.AddedBy,
                                         AddedOn = g.AddedOn,
                                         UpdatedBy = g.UpdatedBy,
                                         UpdatedOn = g.UpdatedOn,
                                         Notes = g.Notes,
                                         Active = g.Active,
                                         Position = g.Position,
                                     };
                return assetGroupList.ToList();
            });
        }


        public Task<List<AssetGroupVM>> getAll()
        {
            return Task.Run(() =>
            {               
                var assetGroupList = from g in assetGroupRepo.GetAll()
                                     select new AssetGroupVM
                                     {
                                         ID = g.ID,
                                         Code = g.Code,
                                         ARName = g.ARName,
                                         LatName = g.LatName,
                                         AssetGroupID = g.AssetGroupID,
                                         AssetGroupAccountID = g.AssetGroupAccountID,
                                         DepreciationAccountID = g.DepreciationAccountID,
                                         TotalDepreciationAccountID = g.TotalDepreciationAccountID,
                                         ExpensesAccountID = g.ExpensesAccountID,
                                         CapitalProfitAccountID = g.CapitalProfitAccountID,
                                         CapitalLossAccountID = g.CapitalLossAccountID,
                                         AppraisalExcessAccountID = g.AppraisalExcessAccountID,
                                         ApprasialDeficitAccountID = g.ApprasialDeficitAccountID,
                                         AddedBy = g.AddedBy,
                                         AddedOn = g.AddedOn,
                                         UpdatedBy = g.UpdatedBy,
                                         UpdatedOn = g.UpdatedOn,
                                         Notes = g.Notes,
                                         Active = g.Active,
                                         Position = g.Position,
                                     };
                return assetGroupList.ToList();
            });
        }

        public Task<AssetGroupVM> getById(int ID)
        {
            return Task.Run(() =>
            {
                var assetGroupList = from g in assetGroupRepo.GetAll()
                                     where g.ID == ID
                                     select new AssetGroupVM

                                     {
                                         ID = g.ID,
                                         Code = g.Code,
                                         ARName = g.ARName,
                                         LatName = g.LatName,
                                         AssetGroupID = g.AssetGroupID,
                                         AssetGroupAccountID = g.AssetGroupAccountID,
                                         DepreciationAccountID = g.DepreciationAccountID,
                                         TotalDepreciationAccountID = g.TotalDepreciationAccountID,
                                         ExpensesAccountID = g.ExpensesAccountID,
                                         CapitalProfitAccountID = g.CapitalProfitAccountID,
                                         CapitalLossAccountID = g.CapitalLossAccountID,
                                         AppraisalExcessAccountID = g.AppraisalExcessAccountID,
                                         ApprasialDeficitAccountID = g.ApprasialDeficitAccountID,
                                         AddedBy = g.AddedBy,
                                         AddedOn = g.AddedOn,
                                         UpdatedBy = g.UpdatedBy,
                                         UpdatedOn = g.UpdatedOn,
                                         Notes = g.Notes,
                                         Active = g.Active,
                                         Position = g.Position,
                                     };
                return assetGroupList.FirstOrDefault();
            });
        }


        public Task<int> insertAssetGroupSync(AssetGroupVM entity)
        {
            return Task.Run(() =>
            {
                AssetGroup group = new AssetGroup();
                {
                    group.ID = entity.ID;
                    group.Code = entity.Code;
                    group.ARName = entity.ARName;
                    group.LatName = entity.LatName;
                    group.AssetGroupID = entity.AssetGroupID;
                    group.AssetGroupAccountID = entity.AssetGroupAccountID;
                    group.DepreciationAccountID = entity.DepreciationAccountID;
                    group.TotalDepreciationAccountID = entity.TotalDepreciationAccountID;
                    group.ExpensesAccountID = entity.ExpensesAccountID;
                    group.CapitalProfitAccountID = entity.CapitalProfitAccountID;
                    group.CapitalLossAccountID = entity.CapitalLossAccountID;
                    group.AppraisalExcessAccountID = entity.AppraisalExcessAccountID;
                    group.ApprasialDeficitAccountID = entity.ApprasialDeficitAccountID;
                    group.AddedBy = entity.AddedBy;
                    group.AddedOn = entity.AddedOn;
                    group.UpdatedBy = entity.UpdatedBy;
                    group.UpdatedOn = entity.UpdatedOn;
                    group.Notes = entity.Notes;
                    group.Active = entity.Active;
                    group.Position = entity.Position;
                };
                assetGroupRepo.Add(group);
                return group.ID;
            });

        }

        public Task<bool> updateAssetGroupSync(AssetGroupVM entity)
        {
            return Task.Run(() =>
            {
                AssetGroup group = new AssetGroup();
                {
                    group.ID = entity.ID;
                    group.Code = entity.Code;
                    group.ARName = entity.ARName;
                    group.LatName = entity.LatName;
                    group.AssetGroupID = entity.AssetGroupID;
                    group.AssetGroupAccountID = entity.AssetGroupAccountID;
                    group.DepreciationAccountID = entity.DepreciationAccountID;
                    group.TotalDepreciationAccountID = entity.TotalDepreciationAccountID;
                    group.ExpensesAccountID = entity.ExpensesAccountID;
                    group.CapitalProfitAccountID = entity.CapitalProfitAccountID;
                    group.CapitalLossAccountID = entity.CapitalLossAccountID;
                    group.AppraisalExcessAccountID = entity.AppraisalExcessAccountID;
                    group.ApprasialDeficitAccountID = entity.ApprasialDeficitAccountID;
                    group.AddedBy = entity.AddedBy;
                    group.AddedOn = entity.AddedOn;
                    group.UpdatedBy = entity.UpdatedBy;
                    group.UpdatedOn = entity.UpdatedOn;
                    group.Notes = entity.Notes;
                    group.Active = entity.Active;
                    group.Position = entity.Position;
                };

                assetGroupRepo.Update(group, group.ID);
                return true;
            });

        }

        public Task<bool> deleteAssetGroupSync(AssetGroupVM entity)
        {
            return Task.Run(() =>
            {
                AssetGroup group = new AssetGroup();
                {
                    group.ID = entity.ID;
                    group.Code = entity.Code;
                    group.ARName = entity.ARName;
                    group.LatName = entity.LatName;
                    group.AssetGroupID = entity.AssetGroupID;
                    group.AssetGroupAccountID = entity.AssetGroupAccountID;
                    group.DepreciationAccountID = entity.DepreciationAccountID;
                    group.TotalDepreciationAccountID = entity.TotalDepreciationAccountID;
                    group.ExpensesAccountID = entity.ExpensesAccountID;
                    group.CapitalProfitAccountID = entity.CapitalProfitAccountID;
                    group.CapitalLossAccountID = entity.CapitalLossAccountID;
                    group.AppraisalExcessAccountID = entity.AppraisalExcessAccountID;
                    group.ApprasialDeficitAccountID = entity.ApprasialDeficitAccountID;
                    group.AddedBy = entity.AddedBy;
                    group.AddedOn = entity.AddedOn;
                    group.UpdatedBy = entity.UpdatedBy;
                    group.UpdatedOn = entity.UpdatedOn;
                    group.Notes = entity.Notes;
                    group.Active = entity.Active;
                    group.Position = entity.Position;
                };

                assetGroupRepo.Delete(group, group.ID);
                return true;
            });

        }

        public Task<int> countGroupAssetAsync()
        {
            return Task.Run(() =>
            {
                return assetGroupRepo.CountAsync();
            });
        }

        public string GetLastCode()
        {
            var lastCode = assetGroupRepo.GetAll().OrderByDescending(b => b.ID).FirstOrDefault();
            return lastCode.Code.ToString();
        }

    }
}
