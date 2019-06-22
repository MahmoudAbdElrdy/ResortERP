using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResortERP.Service.IServices;
using ResortERP.Repository;
using ResortERP.Core;
using ResortERP.Core.VM;
using System.Drawing;
using System.IO;
using SolversTeamERP.Core.VM;

namespace ResortERP.Service.Services
{
    public class AssetOperationMasterService : IAssetOperationMasterService, IDisposable
    {
        IGenericRepository<AssetOperationMaster> AssetOperationMasterRepo;
        IGenericRepository<AssetOperationDetails> AssetOperationDetailsRepo;
        IGenericRepository<AssetMaster> AssetMasterRepo;
        IGenericRepository<Currency> currencyRepo;
        IGenericRepository<Department> DepartmentRepo;
        IGenericRepository<COST_CENTERS> CostCenterRepo;
        IGenericRepository<AssetCompanyDetails> AssetCompanyDetailsRepo;
        IGenericRepository<ACCOUNTS> accountsRepo;
        IGenericRepository<ENTRY_MASTER> entryMasterRepo;
        //TODO: should replace employee table with HrPslEmployee table after integration with HR
        IGenericRepository<EMPLOYEES> employeeRepo;

        public void Dispose()
        {
            AssetOperationMasterRepo.Dispose();
            currencyRepo.Dispose();
            AssetMasterRepo.Dispose();
            DepartmentRepo.Dispose();
            CostCenterRepo.Dispose();
            AssetCompanyDetailsRepo.Dispose();
            accountsRepo.Dispose();
            entryMasterRepo.Dispose();
            employeeRepo.Dispose();
        }


        public AssetOperationMasterService(IGenericRepository<AssetOperationMaster> _AssetOperationMasterRepo, IGenericRepository<Currency> _currencyRepo,
        IGenericRepository<AssetMaster> _AssetMasterRepo, IGenericRepository<Department> _DepartmentRepo, IGenericRepository<COST_CENTERS> _CostCenterRepo,
        IGenericRepository<AssetCompanyDetails> _AssetCompanyDetailsRepo, IGenericRepository<ACCOUNTS> _accountsRepo,
        IGenericRepository<AssetOperationDetails> _AssetOperationDetailsRepo, IGenericRepository<ENTRY_MASTER> _entryMasterRepo,
        IGenericRepository<EMPLOYEES> _employeeRepo)
        {
            AssetOperationMasterRepo = _AssetOperationMasterRepo;
            currencyRepo = _currencyRepo;
            AssetMasterRepo = _AssetMasterRepo;
            DepartmentRepo = _DepartmentRepo;
            CostCenterRepo = _CostCenterRepo;
            AssetCompanyDetailsRepo = _AssetCompanyDetailsRepo;
            accountsRepo = _accountsRepo;
            AssetOperationDetailsRepo = _AssetOperationDetailsRepo;
            entryMasterRepo = _entryMasterRepo;
            employeeRepo = _employeeRepo;
        }

        public bool CheckNameAndCode(int Id, string number, string code)
        {
            return AssetOperationMasterRepo.Filter(p => p.ID != Id && (p.Number == number || p.Code == code)).Any();
        }

        public Task<bool> CheckOperationForDeletion(int Id)
        {
            return Task.Run(async () =>
            {
                if (await AssetOperationDetailsRepo.CountAsync(p => p.AssetOperationMasterID == Id) > 0 ||
                await entryMasterRepo.CountAsync(p => p.AssetOperationMasterID == Id) > 0)
                {
                    return false;
                }
                return true;
            });
            
        }

        public Task<List<AssetOperationMasterVM>> GetAllAsync(int pageNum, int pageSize, int operationType)
        {
            return Task.Run(() =>
            {
                int rowCount;
                var AssetOperationMasterList = from operation in AssetOperationMasterRepo.GetPaged(pageNum, pageSize, p=>p.AssetOperationTypeID == operationType,b => b.ID, false, out rowCount).ToList()
                                               join d in AssetOperationDetailsRepo.GetAll() on operation.ID equals d.AssetOperationMasterID into details
                                               join e in entryMasterRepo.GetAll() on operation.ID equals e.AssetOperationMasterID into entry
                                               from entryMaster in entry.DefaultIfEmpty()
                                               select new AssetOperationMasterVM
                                               {
                                                   ID = operation.ID,
                                                   Code = operation.Code,
                                                   Number = operation.Number,
                                                   CurrencyID = operation.CurrencyID,
                                                   CurrencyRate = operation.CurrencyRate,
                                                   DepartmentID = operation.DepartmentID,
                                                   FromAccountID = operation.FromAccountID,
                                                   AssetOperationTypeID = operation.AssetOperationTypeID,
                                                   CostCenterID = operation.CostCenterID,
                                                   DistributorID = operation.DistributorID,
                                                   OperationStatment = operation.OperationStatment,
                                                   StartDate = operation.StartDate,
                                                   SellerAccountID = operation.SellerAccountID,
                                                   ToAccountID = operation.ToAccountID,
                                                   AddedBy = operation.AddedBy,
                                                   AddedOn = operation.AddedOn,
                                                   UpdatedBy = operation.UpdatedBy,
                                                   UpdatedOn = operation.UpdatedOn,
                                                   Notes = operation.Notes,
                                                   ENTRY_ID = entryMaster == null ? 0 : entryMaster.ENTRY_ID,
                                                   OperationDetails = details.Select(p => new AssetOperationDetailsVM
                                                   {
                                                       AddedBy = p.AddedBy,
                                                       AddedOn = p.AddedOn,
                                                       AssetDepreciationSum = p.AssetDepreciationSum,
                                                       AssetEliminationSum = p.AssetEliminationSum,
                                                       AssetExtrasSum = p.AssetExtrasSum,
                                                       AssetMasterID = p.AssetMasterID,
                                                       FromDepartmentID = p.FromDepartmentID,
                                                       FromEmployeeID = p.FromEmployeeID,
                                                       ToDepartmentID = p.ToDepartmentID,
                                                       ToEmployeeID = p.ToEmployeeID,
                                                       Value = p.Value,
                                                       //AssetOperationMasterID = p.AssetOperationMasterID,
                                                       CostCenterID = p.CostCenterID,
                                                       DistributorID = p.DistributorID,
                                                       //ID = p.ID,
                                                       Notes = p.Notes,
                                                       RowNumber = p.RowNumber,
                                                       SellerAccountID = p.SellerAccountID,
                                                       UpdatedBy = p.UpdatedBy,
                                                       UpdatedOn = p.UpdatedOn,
                                                   }).ToList()
                                               };
                return AssetOperationMasterList.ToList();
            });
        }

        //public Task<AssetDepreciationDetailsVM> getAssetDepreciationDetails(int AssetOperationMasterId)
        //{
        //    return Task.Run(() =>
        //    {
        //        return AssetDepreciationDetailsRepo.FilterAsync(p => p.AssetOperationMasterID == AssetOperationMasterId).Result.AsEnumerable()
        //        .Select(p => new AssetDepreciationDetailsVM
        //        {
        //            ID = p.ID,
        //            //Active = p.Active,
        //            AddedBy = p.AddedBy,
        //            AddedOn = p.AddedOn,
        //            AssetDepreciationTypeID = p.AssetDepreciationTypeID,
        //            //Position = p.Position,
        //            AssetLifeSpan = p.AssetLifeSpan,
        //            AssetLifeSpanUnitID = p.AssetLifeSpanUnitID,
        //            AssetOperationMasterID = p.AssetOperationMasterID,
        //            AssetScrapValue = p.AssetScrapValue,
        //            CurrencyID = p.CurrencyID,
        //            CurrencyRate = p.CurrencyRate,
        //            CurrentAssetValue = p.CurrentAssetValue,
        //            DepreciationStartDate = p.DepreciationStartDate,
        //            DepreciationTotals = p.DepreciationTotals,
        //            ExclusionsValue = p.ExclusionsValue,
        //            ExtraValue = p.ExtraValue,
        //            InitialAssetScrapValue = p.InitialAssetScrapValue,
        //            Notes = p.Notes,
        //            NotSubjectToDepreciation = p.NotSubjectToDepreciation,
        //            NotSubjectToRevaluation = p.NotSubjectToRevaluation,
        //            UpdatedBy = p.UpdatedBy,
        //            UpdatedOn = p.UpdatedOn,
        //        }).FirstOrDefault();
        //    });
        //}

        public Task<int> InsertAsync(AssetOperationMasterVM entity)
        {
            return Task.Run(() =>
            {
                // insert asset master
                AssetOperationMaster AssetOperationMaster = new AssetOperationMaster
                {
                    ID = entity.ID,
                    Code = entity.Code,
                    Number = entity.Number,
                    CurrencyID = entity.CurrencyID,
                    CurrencyRate = entity.CurrencyRate,
                    DepartmentID = entity.DepartmentID,
                    FromAccountID = entity.FromAccountID,
                    AssetOperationTypeID = entity.AssetOperationTypeID,
                    CostCenterID = entity.CostCenterID,
                    DistributorID = entity.DistributorID,
                    OperationStatment = entity.OperationStatment,
                    StartDate = entity.StartDate,
                    SellerAccountID = entity.SellerAccountID,
                    ToAccountID = entity.ToAccountID,
                    AddedBy = entity.AddedBy,
                    AddedOn = DateTime.Now,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedOn = entity.UpdatedOn,
                    Notes = entity.Notes
                };
                AssetOperationMasterRepo.Add(AssetOperationMaster);

                // Insert asset operation details
                List<AssetOperationDetails> AssetOperationDetails = entity.OperationDetails.Select(p => new AssetOperationDetails
                {
                    AddedBy = p.AddedBy,
                    AddedOn = DateTime.Now,
                    AssetDepreciationSum = p.AssetDepreciationSum,
                    AssetEliminationSum = p.AssetEliminationSum,
                    AssetExtrasSum = p.AssetExtrasSum,
                    AssetMasterID = p.AssetMasterID,
                    FromDepartmentID = p.FromDepartmentID,
                    FromEmployeeID = p.FromEmployeeID,
                    ToDepartmentID = p.ToDepartmentID,
                    ToEmployeeID = p.ToEmployeeID,
                    Value = p.Value,
                    AssetOperationMasterID = AssetOperationMaster.ID,
                    CostCenterID = p.CostCenterID,
                    DistributorID = p.DistributorID,
                    ID = p.ID,
                    Notes = p.Notes,
                    RowNumber = p.RowNumber,
                    SellerAccountID = p.SellerAccountID,
                    UpdatedBy = p.UpdatedBy,
                    UpdatedOn = p.UpdatedOn,
                }).ToList();
                AssetOperationDetailsRepo.AddRange(AssetOperationDetails);
                if(AssetOperationMaster.AssetOperationTypeID == 7)
                {
                    ChangeAssetMasterLocations(entity.OperationDetails);
                }
                else if (AssetOperationMaster.AssetOperationTypeID == 10)
                {
                    SetAssignedAssetsAsReceived(entity.OperationDetails);
                }
                return AssetOperationMaster.ID;
            });

        }

        private void SetAssignedAssetsAsReceived(List<AssetOperationDetailsVM> operationDetails)
        {
            foreach (var item in operationDetails)
            {
                if (item.AssetMasterID != 0 && item.FromEmployeeID != null && item.FromEmployeeID != 0 && item.assignedAssetOperationId != null &&
                    item.assignedAssetOperationId != 0)
                {
                    var operation = AssetOperationDetailsRepo.GetByID(item.assignedAssetOperationId);
                    if (operation != null)
                    {
                        operation.IsAssetDeliveredFromEmployee = true;
                        AssetOperationDetailsRepo.Update(operation, operation.ID);
                    }
                }
            }
        }

        private void ChangeAssetMasterLocations(List<AssetOperationDetailsVM> operationDetails)
        {
            foreach (var item in operationDetails)
            {
                if(item.AssetMasterID != 0 && item.ToDepartmentID != null && item.ToDepartmentID!= 0)
                {
                    var assetMaster = AssetMasterRepo.GetByID(item.AssetMasterID);
                    if (assetMaster != null)
                    {
                        assetMaster.DepartmentID = item.ToDepartmentID;
                        AssetMasterRepo.Update(assetMaster, assetMaster.ID);
                    }
                }
            }
        }

        public Task<bool> UpdateAsync(AssetOperationMasterVM entity)
        {
            return Task.Run(() =>
            {
                AssetOperationMaster AssetOperationMaster = new AssetOperationMaster
                {
                    ID = entity.ID,
                    Code = entity.Code,
                    Number = entity.Number,
                    CurrencyID = entity.CurrencyID,
                    CurrencyRate = entity.CurrencyRate,
                    DepartmentID = entity.DepartmentID,
                    FromAccountID = entity.FromAccountID,
                    AssetOperationTypeID = entity.AssetOperationTypeID,
                    CostCenterID = entity.CostCenterID,
                    DistributorID = entity.DistributorID,
                    OperationStatment = entity.OperationStatment,
                    StartDate = entity.StartDate,
                    SellerAccountID = entity.SellerAccountID,
                    ToAccountID = entity.ToAccountID,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedOn = DateTime.Now,
                    Notes = entity.Notes
                };
                AssetOperationMasterRepo.Update(AssetOperationMaster, AssetOperationMaster.ID);

                var detailsToDelete = AssetOperationDetailsRepo.Filter(p => p.AssetOperationMasterID == entity.ID);
                AssetOperationDetailsRepo.DeleteRange(detailsToDelete);

                // Insert asset operation details
                List<AssetOperationDetails> AssetOperationDetails = entity.OperationDetails.Select(p => new AssetOperationDetails
                {
                    AddedBy = p.AddedBy,
                    AddedOn = DateTime.Now,
                    AssetDepreciationSum = p.AssetDepreciationSum,
                    AssetEliminationSum = p.AssetEliminationSum,
                    AssetExtrasSum = p.AssetExtrasSum,
                    AssetMasterID = p.AssetMasterID,
                    FromDepartmentID = p.FromDepartmentID,
                    FromEmployeeID = p.FromEmployeeID,
                    ToDepartmentID = p.ToDepartmentID,
                    ToEmployeeID = p.ToEmployeeID,
                    Value = p.Value,
                    AssetOperationMasterID = AssetOperationMaster.ID,
                    RowNumber = p.RowNumber,
                    CostCenterID = p.CostCenterID,
                    DistributorID = p.DistributorID,
                    ID = p.ID,
                    Notes = p.Notes,
                    SellerAccountID = p.SellerAccountID,
                    UpdatedBy = p.UpdatedBy,
                    UpdatedOn = DateTime.Now,
                }).ToList();
                AssetOperationDetailsRepo.AddRange(AssetOperationDetails);
                if (AssetOperationMaster.AssetOperationTypeID == 7)
                {
                    ChangeAssetMasterLocations(entity.OperationDetails);
                }
                else if (AssetOperationMaster.AssetOperationTypeID == 10)
                {
                    SetAssignedAssetsAsReceived(entity.OperationDetails);
                }
                return true;
            });
        }

        public Task<bool> DeleteAsync(AssetOperationMasterVM entity)
        {
            return Task.Run(() =>
            {
                AssetOperationMasterRepo.Delete(new AssetOperationMaster(), entity.ID);
                var detailsToDelete = AssetOperationDetailsRepo.Filter(p => p.AssetOperationMasterID == entity.ID);
                AssetOperationDetailsRepo.DeleteRange(detailsToDelete);
                return true;
            });
        }

        public Task<int> CountAsync(int operationType)
        {
            return Task.Run(() =>
            {
                return AssetOperationMasterRepo.CountAsync(p => p.AssetOperationTypeID == operationType);
            });
        }

        public string GetLastCode()
        {
            var lastCode = AssetOperationMasterRepo.GetAll().OrderByDescending(b => b.ID).FirstOrDefault();
            return lastCode == null ? "0" : lastCode.Code.ToString();
        }

        public Task<List<AssetMasterDropDownMenu>> getAssetMasterList()
        {

            var x = Task.Run(() =>
            {
                return (from asset in AssetMasterRepo.GetAll()
                        join a in accountsRepo.GetAll() on asset.AssetAccountID equals a.ACC_ID into y
                        from account in y.DefaultIfEmpty()
                        join d in DepartmentRepo.GetAll() on asset.DepartmentID equals d.DEPT_ID into dep
                        from department in dep.DefaultIfEmpty()
                        select new AssetMasterDropDownMenu
                        {
                            OptionValue = asset.ID,
                            OptionText = asset.ARName,
                            AccountId = account == null ? 0 : account.ACC_ID,
                            DepartmentId = department == null ? 0 : department.DEPT_ID,
                            DepartmentName = department == null ? "" : department.DEPT_AR_NAME
                        }).ToList();
            });
            return x;
        }

        public Task<List<DropDownMenuOptionsVM>> getCostCenterList()
        {
            return Task.Run(() =>
            {
                return CostCenterRepo.GetAll().Select(p => new DropDownMenuOptionsVM
                {
                    OptionValue = p.COST_CENTER_ID,
                    OptionText = p.COST_CENTER_AR_NAME,
                    OptionTextEn = p.COST_CENTER_EN_NAME
                }).ToList();
            });
        }

        public Task<List<DropDownMenuOptionsVM>> getDepartmentList()
        {
            return Task.Run(() =>
            {
                return DepartmentRepo.GetAll().Select(p => new DropDownMenuOptionsVM
                {
                    OptionValue = p.DEPT_ID,
                    OptionText = p.DEPT_AR_NAME,
                    OptionTextEn = p.DEPT_EN_NAME
                }).ToList();
            });
        }

        public Task<List<DropDownMenuOptionsVM>> getCompanyList()
        {
            return Task.Run(() =>
            {
                return AssetCompanyDetailsRepo.GetAll().Select(p => new DropDownMenuOptionsVM
                {
                    OptionValue = p.ID,
                    OptionText = p.ARName,
                    OptionTextEn = p.ENName
                }).ToList();
            });
        }

        public Task<List<DropDownMenuOptionsVM>> getCurrencyList()
        {
            return Task.Run(() =>
            {
                return currencyRepo.GetAll().Select(p => new DropDownMenuOptionsVM
                {
                    OptionValue = p.CURRENCY_ID,
                    OptionText = p.CURRENCY_AR_NAME,
                    OptionTextEn = p.CURRENCY_EN_NAME
                }).ToList();
            });
        }

        public Task<List<DropDownMenuOptionsVM>> getAccountList()
        {
            return Task.Run(() =>
            {
                return accountsRepo.GetAll().Select(p => new DropDownMenuOptionsVM
                {
                    OptionValue = p.ACC_ID,
                    OptionText = p.ACC_AR_NAME,
                    OptionTextEn = p.ACC_EN_NAME
                }).ToList();
            });
        }

        public Task<List<DropDownMenuOptionsVM>> getEmployeeList()
        {
            return Task.Run(() =>
            {
                return employeeRepo.GetAll().Select(p => new DropDownMenuOptionsVM
                {
                    OptionValue = p.EMP_ID,
                    OptionText = p.EMP_AR_NAME,
                    OptionTextEn = p.EMP_EN_NAME
                }).ToList();
            });
        }

        public Task<List<EmployeeAssetsVM>> getEmployeeAssets(int employeeId)
        {
            return Task.Run(() =>
            {
                var employeeAssets = from assetMaster in AssetOperationMasterRepo.GetAll()
                                     join assignedAssets in AssetOperationDetailsRepo.GetAll() on assetMaster.ID equals assignedAssets.AssetOperationMasterID
                                     where assetMaster.AssetOperationTypeID == 9 && assignedAssets.ToEmployeeID == employeeId &&
                                     assignedAssets.IsAssetDeliveredFromEmployee != true
                                     join asset in AssetMasterRepo.GetAll() on assignedAssets.AssetMasterID equals asset.ID
                                     select new EmployeeAssetsVM
                                     {
                                         AssetMasterID = assignedAssets.AssetMasterID,
                                         FromEmployeeID = (int)assignedAssets.ToEmployeeID,
                                         AssetName = asset == null ? "" : asset.ARName,
                                         assignedAssetOperationId = assignedAssets.ID
                                     };
                return employeeAssets.ToList();
            });
        }
    }
}
