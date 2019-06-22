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

namespace ResortERP.Service.Services
{
    public class AssetMasterService : IAssetMasterService, IDisposable
    {
        IGenericRepository<AssetMaster> assetMasterRepo;
        IGenericRepository<AssetDepreciationDetails> AssetDepreciationDetailsRepo;
        IGenericRepository<AssetOperationDetails> AssetOperationDetailsRepo;
        IGenericRepository<Currency> currencyRepo;
        IGenericRepository<NATIONS> nationsRepo;
        IGenericRepository<AssetType> AssetTypeRepo;
        IGenericRepository<AssetStatus> AssetStatusRepo;
        IGenericRepository<AssetLifeSpanUnit> AssetLifeSpanUnitRepo;
        IGenericRepository<AssetDepreciationType> AssetDepreciationTypeRepo;
        IGenericRepository<AssetCompanyDetails> AssetCompanyDetailsRepo;
        IGenericRepository<ACCOUNTS> accountsRepo;
        IGenericRepository<AssetGroup> AssetGroupRepo;
        IGenericRepository<Department> departmentRepo;

        public void Dispose()
        {
            assetMasterRepo.Dispose();
            currencyRepo.Dispose();
            nationsRepo.Dispose();
            AssetTypeRepo.Dispose();
            AssetStatusRepo.Dispose();
            AssetLifeSpanUnitRepo.Dispose();
            AssetDepreciationTypeRepo.Dispose();
            AssetCompanyDetailsRepo.Dispose();
            accountsRepo.Dispose();
            AssetDepreciationDetailsRepo.Dispose();
            AssetGroupRepo.Dispose();
            departmentRepo.Dispose();
            AssetOperationDetailsRepo.Dispose();
        }


        public AssetMasterService(IGenericRepository<AssetMaster> _assetMasterRepo, IGenericRepository<Currency> _currencyRepo,
        IGenericRepository<NATIONS> _nationsRepo, IGenericRepository<AssetType> _AssetTypeRepo, IGenericRepository<AssetStatus> _AssetStatusRepo,
        IGenericRepository<AssetLifeSpanUnit> _AssetLifeSpanUnitRepo, IGenericRepository<AssetDepreciationType> _AssetDepreciationTypeRepo,
        IGenericRepository<AssetCompanyDetails> _AssetCompanyDetailsRepo, IGenericRepository<ACCOUNTS> _accountsRepo,
        IGenericRepository<AssetDepreciationDetails> _AssetDepreciationDetailsRepo, IGenericRepository<AssetGroup> _AssetGroupRepo,
        IGenericRepository<Department> _departmentRepo, IGenericRepository<AssetOperationDetails> _AssetOperationMasterRepo)
        {
            assetMasterRepo = _assetMasterRepo;
            currencyRepo = _currencyRepo;
            nationsRepo = _nationsRepo;
            AssetTypeRepo = _AssetTypeRepo;
            AssetStatusRepo = _AssetStatusRepo;
            AssetLifeSpanUnitRepo = _AssetLifeSpanUnitRepo;
            AssetDepreciationTypeRepo = _AssetDepreciationTypeRepo;
            AssetCompanyDetailsRepo = _AssetCompanyDetailsRepo;
            accountsRepo = _accountsRepo;
            AssetDepreciationDetailsRepo = _AssetDepreciationDetailsRepo;
            AssetGroupRepo = _AssetGroupRepo;
            departmentRepo = _departmentRepo;
            AssetOperationDetailsRepo = _AssetOperationMasterRepo;
        }

        public bool CheckNameAndCode(int Id, string aRName, string latName, string code)
        {
            return assetMasterRepo.Filter(p => p.ID != Id && ( p.ARName == aRName || p.LatName == latName || p.Code == code)).Any();
        }

        public Task<List<AssetMasterVM>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run(() =>
            {
                int rowCount;
                var assetMasterList = from asset in assetMasterRepo.GetPaged(pageNum, pageSize, b => b.ID, false, out rowCount).ToList()
                                     join g in AssetGroupRepo.GetAll() on asset.AssetGroupID equals g.ID into x
                                     from assetGroup in x.DefaultIfEmpty()
                                     select new AssetMasterVM
                                     {
                                         ID = asset.ID,
                                         Code = asset.Code,
                                         ARName = asset.ARName,
                                         LatName = asset.LatName,
                                         AssetGroupID = asset.AssetGroupID,
                                         AssetAccountID = asset.AssetAccountID,
                                         AssetBrand = asset.AssetBrand,
                                         AssetColor = asset.AssetColor,
                                         AssetHeight = asset.AssetHeight,
                                         AssetLocation = asset.AssetLocation,
                                         AssetModel = asset.AssetModel,
                                         AssetScreenNumber = asset.AssetScreenNumber,
                                         AssetStatusID = asset.AssetStatusID,
                                         AssetTypeID = asset.AssetTypeID,
                                         AssetWeight = asset.AssetWeight,
                                         AssetWidth = asset.AssetWidth,
                                         Barcode = asset.Barcode,
                                         ManufactureCompanyID = asset.ManufactureCompanyID,
                                         OriginNationID = asset.OriginNationID,
                                         SupplierCompanyID = asset.SupplierCompanyID,
                                         DepreciationAccountID = asset.DepreciationAccountID,
                                         TotalDepreciationAccountID = asset.TotalDepreciationAccountID,
                                         ExpensesAccountID = asset.ExpensesAccountID,
                                         CapitalProfitAccountID = asset.CapitalProfitAccountID,
                                         CapitalLossAccountID = asset.CapitalLossAccountID,
                                         AppraisalExcessAccountID = asset.AppraisalExcessAccountID,
                                         ApprasialDeficitAccountID = asset.ApprasialDeficitAccountID,
                                         AssetPhoto = asset.AssetPhoto,
                                         AddedBy = asset.AddedBy,
                                         AddedOn = asset.AddedOn,
                                         UpdatedBy = asset.UpdatedBy,
                                         UpdatedOn = asset.UpdatedOn,
                                         Notes = asset.Notes,
                                         DepartmentID = asset.DepartmentID,

                                         ManufactureDate= asset.ManufactureDate,
                                         WarrantyNumber = asset.WarrantyNumber,
                                         WarrantyStartDate = asset.WarrantyStartDate,
                                         WarrantyEndDate = asset.WarrantyEndDate,
                                         ReceivingDate = asset.ReceivingDate,
                                         ReceivingNotes = asset.ReceivingNotes,
                                         ContractNumber = asset.ContractNumber,

                                         ContractDate = asset.ContractDate,
                                         PurchasingDate = asset.PurchasingDate,
                                         CustomsStatment = asset.CustomsStatment,
                                         CustomsStatmentDate = asset.CustomsStatmentDate,

                                         ShippingNumber = asset.ShippingNumber,
                                         AssetShippingMethodID = asset.AssetShippingMethodID,
                                         ShippingDate = asset.ShippingDate,
                                         ImportLicenseNumber = asset.ImportLicenseNumber,
                                         ShippingDestination = asset.ShippingDestination,
                                         ShippingArrivalDate = asset.ShippingArrivalDate,
                                         //Active = asset.Active,
                                         //Position = asset.Position,
                                         AssetGroup = assetGroup == null ? "" : assetGroup.ARName,
                                     };
                return assetMasterList.ToList();
            });
        }

        public Task<AssetDepreciationDetailsVM> getAssetDepreciationDetails(int assetMasterId)
        {
            return Task.Run(() =>
            {
                return AssetDepreciationDetailsRepo.FilterAsync(p => p.AssetMasterID == assetMasterId).Result.AsEnumerable()
                .Select(p => new AssetDepreciationDetailsVM
                {
                    ID = p.ID,
                    //Active = p.Active,
                    AddedBy = p.AddedBy,
                    AddedOn = p.AddedOn,
                    AssetDepreciationTypeID = p.AssetDepreciationTypeID,
                    //Position = p.Position,
                    AssetLifeSpan = p.AssetLifeSpan,
                    AssetLifeSpanUnitID = p.AssetLifeSpanUnitID,
                    AssetMasterID = p.AssetMasterID,
                    AssetScrapValue = p.AssetScrapValue,
                    CurrencyID = p.CurrencyID,
                    CurrencyRate = p.CurrencyRate,
                    CurrentAssetValue = p.CurrentAssetValue,
                    DepreciationStartDate = p.DepreciationStartDate,
                    DepreciationTotals = p.DepreciationTotals,
                    ExclusionsValue = p.ExclusionsValue,
                    ExtraValue = p.ExtraValue,
                    InitialAssetScrapValue = p.InitialAssetScrapValue,
                    Notes = p.Notes,
                    NotSubjectToDepreciation = p.NotSubjectToDepreciation,
                    NotSubjectToRevaluation = p.NotSubjectToRevaluation,
                    UpdatedBy = p.UpdatedBy,
                    UpdatedOn = p.UpdatedOn,
                }).FirstOrDefault();
            });
        }        

        public Task<int> InsertAsync(AssetMasterVM entity)
        {
            return Task.Run(() =>
            {
                if (entity.Asset_Base64_Photo != null)
                {
                    string base64 = entity.Asset_Base64_Photo;
                    entity.Asset_Base64_Photo = String.Format(base64);
                    base64 = base64.Remove(0, base64.IndexOf("base64,") + 7);
                    entity.AssetPhoto = Convert.FromBase64String(base64);
                }

                // insert asset master
                AssetMaster assetMaster = new AssetMaster
                {
                    ID = entity.ID,
                    Code = entity.Code,
                    ARName = entity.ARName,
                    LatName = entity.LatName,
                    AssetGroupID = entity.AssetGroupID,
                    AssetAccountID = entity.AssetAccountID,
                    AssetBrand = entity.AssetBrand,
                    AssetColor = entity.AssetColor,
                    AssetHeight = entity.AssetHeight,
                    AssetLocation = entity.AssetLocation,
                    AssetModel = entity.AssetModel,
                    AssetScreenNumber = entity.AssetScreenNumber,
                    AssetStatusID = entity.AssetStatusID,
                    AssetTypeID = entity.AssetTypeID,
                    AssetWeight = entity.AssetWeight,
                    AssetWidth = entity.AssetWidth,
                    Barcode = entity.Barcode,
                    ManufactureCompanyID = entity.ManufactureCompanyID,
                    OriginNationID = entity.OriginNationID,
                    SupplierCompanyID = entity.SupplierCompanyID,
                    DepreciationAccountID = entity.DepreciationAccountID,
                    TotalDepreciationAccountID = entity.TotalDepreciationAccountID,
                    ExpensesAccountID = entity.ExpensesAccountID,
                    CapitalProfitAccountID = entity.CapitalProfitAccountID,
                    CapitalLossAccountID = entity.CapitalLossAccountID,
                    AppraisalExcessAccountID = entity.AppraisalExcessAccountID,
                    ApprasialDeficitAccountID = entity.ApprasialDeficitAccountID,
                    AssetPhoto = entity.AssetPhoto,
                    AddedBy = entity.AddedBy,
                    AddedOn = DateTime.Now,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedOn = entity.UpdatedOn,
                    Notes = entity.Notes,
                    DepartmentID = entity.DepartmentID,
                    ManufactureDate=entity.ManufactureDate,
                    WarrantyNumber = entity.WarrantyNumber,
                    WarrantyStartDate = entity.WarrantyStartDate,
                    WarrantyEndDate = entity.WarrantyEndDate,
                    ReceivingDate = entity.ReceivingDate,
                    ReceivingNotes = entity.ReceivingNotes,
                    ContractNumber = entity.ContractNumber,

                    ContractDate = entity.ContractDate,
                    PurchasingDate = entity.PurchasingDate,
                    CustomsStatment = entity.CustomsStatment,
                    CustomsStatmentDate = entity.CustomsStatmentDate,

                    ShippingNumber = entity.ShippingNumber,
                    AssetShippingMethodID = entity.AssetShippingMethodID,
                    ShippingDate = entity.ShippingDate,
                    ImportLicenseNumber = entity.ImportLicenseNumber,
                    ShippingDestination = entity.ShippingDestination,
                    ShippingArrivalDate = entity.ShippingArrivalDate,
                    //Active = entity.Active,
                    //Position = entity.Position
                };
                assetMasterRepo.Add(assetMaster);

                // Insert asset depreciation details
                AssetDepreciationDetails assetDepreciationDetails = new AssetDepreciationDetails
                {
                    //Active = entity.Depreciation.Active,
                    AddedBy = entity.Depreciation.AddedBy,
                    AddedOn = DateTime.Now,
                    AssetDepreciationTypeID = entity.Depreciation.AssetDepreciationTypeID,
                    //Position = entity.Depreciation.Position,
                    AssetLifeSpan = entity.Depreciation.AssetLifeSpan,
                    AssetLifeSpanUnitID = entity.Depreciation.AssetLifeSpanUnitID,
                    AssetMasterID = assetMaster.ID,
                    AssetScrapValue = entity.Depreciation.AssetScrapValue,
                    CurrencyID = entity.Depreciation.CurrencyID,
                    CurrencyRate = entity.Depreciation.CurrencyRate,
                    CurrentAssetValue = entity.Depreciation.CurrentAssetValue,
                    DepreciationStartDate = entity.Depreciation.DepreciationStartDate,
                    DepreciationTotals = entity.Depreciation.DepreciationTotals,
                    ExclusionsValue = entity.Depreciation.ExclusionsValue,
                    ExtraValue = entity.Depreciation.ExtraValue,
                    InitialAssetScrapValue = entity.Depreciation.InitialAssetScrapValue,
                    Notes = entity.Depreciation.Notes,
                    NotSubjectToDepreciation = entity.Depreciation.NotSubjectToDepreciation,
                    NotSubjectToRevaluation = entity.Depreciation.NotSubjectToRevaluation,
                    UpdatedBy = entity.Depreciation.UpdatedBy,
                    UpdatedOn = entity.Depreciation.UpdatedOn,
                };
                AssetDepreciationDetailsRepo.Add(assetDepreciationDetails);
                return assetMaster.ID;
            });

        }

        public Task<bool> UpdateAsync(AssetMasterVM entity)
        {
            return Task.Run(() =>
            {
                if (entity.Asset_Base64_Photo != null)
                {
                    string base64 = entity.Asset_Base64_Photo;
                    entity.Asset_Base64_Photo = String.Format(base64);
                    base64 = base64.Remove(0, base64.IndexOf("base64,") + 7);
                    entity.AssetPhoto = Convert.FromBase64String(base64);
                }

                AssetMaster assetMaster = new AssetMaster
                {
                    ID = entity.ID,
                    Code = entity.Code,
                    ARName = entity.ARName,
                    LatName = entity.LatName,
                    AssetGroupID = entity.AssetGroupID,
                    AssetAccountID = entity.AssetAccountID,
                    AssetBrand = entity.AssetBrand,
                    AssetColor = entity.AssetColor,
                    AssetHeight = entity.AssetHeight,
                    AssetLocation = entity.AssetLocation,
                    AssetModel = entity.AssetModel,
                    AssetScreenNumber = entity.AssetScreenNumber,
                    AssetStatusID = entity.AssetStatusID,
                    AssetTypeID = entity.AssetTypeID,
                    AssetWeight = entity.AssetWeight,
                    AssetWidth = entity.AssetWidth,
                    Barcode = entity.Barcode,
                    ManufactureCompanyID = entity.ManufactureCompanyID,
                    OriginNationID = entity.OriginNationID,
                    SupplierCompanyID = entity.SupplierCompanyID,
                    DepreciationAccountID = entity.DepreciationAccountID,
                    TotalDepreciationAccountID = entity.TotalDepreciationAccountID,
                    ExpensesAccountID = entity.ExpensesAccountID,
                    CapitalProfitAccountID = entity.CapitalProfitAccountID,
                    CapitalLossAccountID = entity.CapitalLossAccountID,
                    AppraisalExcessAccountID = entity.AppraisalExcessAccountID,
                    ApprasialDeficitAccountID = entity.ApprasialDeficitAccountID,
                    AssetPhoto = entity.AssetPhoto,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedOn = DateTime.Now,
                    Notes = entity.Notes,
                    DepartmentID = entity.DepartmentID,
                    ManufactureDate = entity.ManufactureDate,
                    WarrantyNumber = entity.WarrantyNumber,
                    WarrantyStartDate = entity.WarrantyStartDate,
                    WarrantyEndDate = entity.WarrantyEndDate,
                    ReceivingDate = entity.ReceivingDate,
                    ReceivingNotes = entity.ReceivingNotes,
                    ContractNumber = entity.ContractNumber,

                    ContractDate = entity.ContractDate,
                    PurchasingDate = entity.PurchasingDate,
                    CustomsStatment = entity.CustomsStatment,
                    CustomsStatmentDate = entity.CustomsStatmentDate,

                    ShippingNumber = entity.ShippingNumber,
                    AssetShippingMethodID = entity.AssetShippingMethodID,
                    ShippingDate = entity.ShippingDate,
                    ImportLicenseNumber = entity.ImportLicenseNumber,
                    ShippingDestination = entity.ShippingDestination,
                    ShippingArrivalDate = entity.ShippingArrivalDate,
                    //Active = entity.Active,
                    //Position = entity.Position
                };
                assetMasterRepo.Update(assetMaster, assetMaster.ID);

                AssetDepreciationDetails assetDepreciationDetails = new AssetDepreciationDetails
                {
                    ID = entity.Depreciation.ID,
                    //Active = entity.Depreciation.Active,
                    AddedBy = entity.Depreciation.AddedBy,
                    AddedOn = entity.Depreciation.AddedOn,
                    AssetDepreciationTypeID = entity.Depreciation.AssetDepreciationTypeID,
                    //Position = entity.Depreciation.Position,
                    AssetLifeSpan = entity.Depreciation.AssetLifeSpan,
                    AssetLifeSpanUnitID = entity.Depreciation.AssetLifeSpanUnitID,
                    AssetMasterID = assetMaster.ID,
                    AssetScrapValue = entity.Depreciation.AssetScrapValue,
                    CurrencyID = entity.Depreciation.CurrencyID,
                    CurrencyRate = entity.Depreciation.CurrencyRate,
                    CurrentAssetValue = entity.Depreciation.CurrentAssetValue,
                    DepreciationStartDate = entity.Depreciation.DepreciationStartDate,
                    DepreciationTotals = entity.Depreciation.DepreciationTotals,
                    ExclusionsValue = entity.Depreciation.ExclusionsValue,
                    ExtraValue = entity.Depreciation.ExtraValue,
                    InitialAssetScrapValue = entity.Depreciation.InitialAssetScrapValue,
                    Notes = entity.Depreciation.Notes,
                    NotSubjectToDepreciation = entity.Depreciation.NotSubjectToDepreciation,
                    NotSubjectToRevaluation = entity.Depreciation.NotSubjectToRevaluation,
                    UpdatedBy = entity.Depreciation.UpdatedBy,
                    UpdatedOn = DateTime.Now
                };
                AssetDepreciationDetailsRepo.Update(assetDepreciationDetails, assetDepreciationDetails.ID);
                return true;
            });
        }

        public Task<bool> DeleteAsync(AssetMasterVM entity)
        {
            return Task.Run(() =>
            {
                assetMasterRepo.Delete(new AssetMaster(),entity.ID);
                var depreciation = AssetDepreciationDetailsRepo.Filter(p => p.AssetMasterID == entity.ID).FirstOrDefault();
                if (depreciation != null)
                {
                    AssetCompanyDetailsRepo.Delete(new AssetCompanyDetails(), depreciation.ID);
                }
                return true;
            });
        }

        public Task<int> CountAsync()
        {
            return Task.Run(() =>
            {
                return assetMasterRepo.CountAsync();
            });
        }

        public string GetLastCode()
        {
            var lastCode = assetMasterRepo.GetAll().OrderByDescending(b => b.ID).FirstOrDefault();
            return lastCode == null ? "0" : lastCode.Code.ToString();
        }

        public Task<List<DropDownMenuOptionsVM>> getAssetTypeList()
        {

            var x = Task.Run(() =>
            {
                return AssetTypeRepo.GetAll().Select(p => new DropDownMenuOptionsVM
                {
                    OptionValue = p.ID,
                    OptionText = p.ARName,
                    OptionTextEn = p.LatName
                }).ToList();
            });
            return x;
        }

        public Task<List<DropDownMenuOptionsVM>> getAssetStatusList()
        {
            return Task.Run(() =>
            {
                return AssetStatusRepo.GetAll().Select(p => new DropDownMenuOptionsVM
                {
                    OptionValue = p.ID,
                    OptionText = p.ARName,
                    OptionTextEn = p.LatName
                }).ToList();
            });
        }

        public Task<List<DropDownMenuOptionsVM>> getOriginNationList()
        {
            return Task.Run(() =>
            {
                return nationsRepo.GetAll().Select(p => new DropDownMenuOptionsVM
                {
                    OptionValue = p.NATION_ID,
                    OptionText = p.NATION_AR_NAME,
                    OptionTextEn = p.NATION_EN_NAME
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

        public Task<List<DropDownMenuOptionsVM>> getAssetDepreciationTypeList()
        {
            return Task.Run(() =>
            {
                return AssetDepreciationTypeRepo.GetAll().Select(p => new DropDownMenuOptionsVM
                {
                    OptionValue = p.ID,
                    OptionText = p.ARName,
                    OptionTextEn = p.LatName
                }).ToList();
            });
        }

        public Task<List<DropDownMenuOptionsVM>> getAssetLifeSpanUnitList()
        {
            return Task.Run(() =>
            {
                return AssetLifeSpanUnitRepo.GetAll().Select(p => new DropDownMenuOptionsVM
                {
                    OptionValue = p.ID,
                    OptionText = p.ARName,
                    OptionTextEn = p.LatName
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
        
        public Task<List<DropDownMenuOptionsVM>> getDepartmentList()
        {
            return Task.Run(() =>
            {
                return departmentRepo.GetAll().Select(p => new DropDownMenuOptionsVM
                {
                    OptionValue = p.DEPT_ID,
                    OptionText = p.DEPT_AR_NAME,
                    OptionTextEn = p.DEPT_EN_NAME
                }).ToList();
            });
        }

        public Task<List<DropDownMenuOptionsVM>> getAssetGroupList()
        {
            return Task.Run(() =>
            {
                return AssetGroupRepo.GetAll().Select(p => new DropDownMenuOptionsVM
                {
                    OptionValue = p.ID,
                    OptionText = p.ARName,
                    OptionTextEn = p.LatName
                }).ToList();
            });
        }

        public Task<bool> CheckAssetMasterForDeletion(int Id)
        {
            return Task.Run(async () =>
            {
                int Count = await AssetOperationDetailsRepo.CountAsync(p => p.AssetMasterID == Id);
                if (Count > 0)
                {
                    return false;
                }
                return true;
            });
        }
    }
}
