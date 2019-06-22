using ResortERP.Core;
using ResortERP.Core.VM;
using ResortERP.Repository;
using ResortERP.Service.IServices;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ResortERP.Service.Services
{
    public class ItemsService : IItemsService, IDisposable
    {
        IGenericRepository<ITEMS> itemsRepo;
        IGenericRepository<ITEMS_UNITS> itemUnitsRepo;
        IGenericRepository<UNITS> unitsRepo;
        IGenericRepository<ItemCaliber> itemCaliberRepo;
        IGenericRepository<ITEM_PRICES> itemPricesRepo;
        ICommonRepository<User> commonRepo;
        public ItemsService(IGenericRepository<ITEMS> itemsRepoParam, IGenericRepository<ITEMS_UNITS> itemUnitsRepoParam,
            IGenericRepository<UNITS> unitsRepo, IGenericRepository<ITEM_PRICES> itemPricesRepo, ICommonRepository<User> commonRepo, IGenericRepository<ItemCaliber> itemCaliberRepo)
        {
            this.itemsRepo = itemsRepoParam;
            this.itemUnitsRepo = itemUnitsRepoParam;
            this.unitsRepo = unitsRepo;
            this.itemCaliberRepo = itemCaliberRepo;
            this.itemPricesRepo = itemPricesRepo;
            this.commonRepo = commonRepo;
        }

        public void Dispose()
        {
            itemsRepo.Dispose();
            itemUnitsRepo.Dispose();
        }


        public int Insert(ITEMSVM entity)
        {
            if (entity.Item_Base64_Photo != null)
            {
                string base64 = entity.Item_Base64_Photo;
                entity.Item_Base64_Photo = String.Format(base64);
                base64 = base64.Remove(0, base64.IndexOf("base64,") + 7);
                entity.ITEM_PIC = Convert.FromBase64String(base64);
            }
            ITEMS item = new ITEMS
            {

                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                BURCHASEDISCOUNT = entity.BURCHASEDISCOUNT,
                COMPANY_ID = entity.COMPANY_ID,
                COST_CALCULATION_TYPE = entity.COST_CALCULATION_TYPE,
                DOESTHEQUANTITYISAPARTOFBARCODE = entity.DOESTHEQUANTITYISAPARTOFBARCODE,
                EXPIRED_DATE = entity.EXPIRED_DATE,
                GROUP_ID = (int)entity.GROUP_ID,
                ITEM_AR_NAME = entity.ITEM_AR_NAME,
                ITEM_BRAND = entity.ITEM_BRAND,
                ITEM_CODE = entity.ITEM_CODE,
                ITEM_COLOR = entity.ITEM_COLOR,
                ITEM_EN_NAME = entity.ITEM_EN_NAME,
                ITEM_ID = entity.ITEM_ID,
                ITEM_MODEL = entity.ITEM_MODEL,
                ITEM_PIC = entity.ITEM_PIC,
                ITEM_REMARKS = entity.ITEM_REMARKS,
                ITEM_TYPE = entity.ITEM_TYPE,
                MIN_QTY = entity.MIN_QTY,
                PRODUCTION_DATE = entity.PRODUCTION_DATE,
                QUANTITYLENGTHATTHEBARCODE = entity.QUANTITYLENGTHATTHEBARCODE,
                QUANTITYSTARTATTHEBARCODE = entity.QUANTITYSTARTATTHEBARCODE,
                QUANTITYPARTSTARTATTHEBARCODE = entity.QUANTITYPARTSTARTATTHEBARCODE,
                QUANTITYPARTLENGTHATTHEBARCODE = entity.QUANTITYPARTLENGTHATTHEBARCODE,
                SELLEDISCOUNT = entity.SELLEDISCOUNT,
                SERIAL_IN_INPUT = entity.SERIAL_IN_INPUT,
                SERIAL_IN_OUTPUT = entity.SERIAL_IN_OUTPUT,
                SubjectToVAT = entity.SubjectToVAT,
                VATRate = entity.VATRate,
                CaliberID = entity.CaliberID,
                GlobalGoldPrice = entity.GlobalGoldPrice,
                ManufacturingWages = entity.ManufacturingWages,
                ProfitMargin = entity.ProfitMargin,
                ItemPrice = entity.ItemPrice,
                ItemWeight = entity.ItemWeight,
                ItemStatus = entity.ItemStatus,
                LowestSellingPrice = entity.LowestSellingPrice,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn,
                KiloPrice = entity.KiloPrice,
                OuncePrice = entity.OuncePrice,
                IsItemCodeRelatedByGroup = entity.IsItemCodeRelatedByGroup,
                itemGmWages= entity.itemGmWages,
                GoldAccID=entity.GoldAccID,
                ClearnessRate=entity.ClearnessRate,

                GemsWages=entity.GemsWages,
                GemsWeight=entity.GemsWeight
            };
            itemsRepo.Add(item);
            int ItemId = item.ITEM_ID;
            return ItemId;
        }
        public Task<int> InsertAsync(ITEMSVM entity)
        {
            return Task.Run<int>(() =>
            {
                string base64 = entity.Item_Base64_Photo;
                entity.Item_Base64_Photo = String.Format(base64);
                base64 = base64.Remove(0, base64.IndexOf("base64,") + 7);
                entity.ITEM_PIC = Convert.FromBase64String(base64);

                ITEMS item = new ITEMS
                {
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    BURCHASEDISCOUNT = entity.BURCHASEDISCOUNT,
                    COMPANY_ID = entity.COMPANY_ID,
                    COST_CALCULATION_TYPE = entity.COST_CALCULATION_TYPE,
                    DOESTHEQUANTITYISAPARTOFBARCODE = entity.DOESTHEQUANTITYISAPARTOFBARCODE,
                    EXPIRED_DATE = entity.EXPIRED_DATE,
                    GROUP_ID = (int)entity.GROUP_ID,
                    ITEM_AR_NAME = entity.ITEM_AR_NAME,
                    ITEM_BRAND = entity.ITEM_BRAND,
                    ITEM_CODE = entity.ITEM_CODE,
                    ITEM_COLOR = entity.ITEM_COLOR,
                    ITEM_EN_NAME = entity.ITEM_EN_NAME,
                    ITEM_ID = entity.ITEM_ID,
                    ITEM_MODEL = entity.ITEM_MODEL,
                    ITEM_PIC = entity.ITEM_PIC,
                    ITEM_REMARKS = entity.ITEM_REMARKS,
                    ITEM_TYPE = entity.ITEM_TYPE,
                    MIN_QTY = entity.MIN_QTY,
                    PRODUCTION_DATE = entity.PRODUCTION_DATE,
                    QUANTITYLENGTHATTHEBARCODE = entity.QUANTITYLENGTHATTHEBARCODE,
                    QUANTITYSTARTATTHEBARCODE = entity.QUANTITYSTARTATTHEBARCODE,
                    QUANTITYPARTSTARTATTHEBARCODE = entity.QUANTITYPARTSTARTATTHEBARCODE,
                    QUANTITYPARTLENGTHATTHEBARCODE = entity.QUANTITYPARTLENGTHATTHEBARCODE,
                    SELLEDISCOUNT = entity.SELLEDISCOUNT,
                    SERIAL_IN_INPUT = entity.SERIAL_IN_INPUT,
                    SERIAL_IN_OUTPUT = entity.SERIAL_IN_OUTPUT,
                    SubjectToVAT = entity.SubjectToVAT,
                    VATRate = entity.VATRate,
                    CaliberID = entity.CaliberID,
                    GlobalGoldPrice = entity.GlobalGoldPrice,
                    ManufacturingWages = entity.ManufacturingWages,
                    ProfitMargin = entity.ProfitMargin,
                    ItemPrice = entity.ItemPrice,
                    ItemWeight = entity.ItemWeight,
                    ItemStatus = entity.ItemStatus,
                    LowestSellingPrice = entity.LowestSellingPrice,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn,
                    KiloPrice = entity.KiloPrice,
                    OuncePrice = entity.OuncePrice,
                    IsItemCodeRelatedByGroup = entity.IsItemCodeRelatedByGroup,
                    itemGmWages = entity.itemGmWages,
                    GoldAccID = entity.GoldAccID,
                    ClearnessRate=entity.ClearnessRate,

                    GemsWages = entity.GemsWages,
                    GemsWeight = entity.GemsWeight
                };


                itemsRepo.Add(item);
                int AddedItem = item.ITEM_ID;
                return AddedItem;
            }
              );

        }
        public List<ITEMS> getByItemCaliber(int itemCaliberID)
        {
            var q = itemsRepo.GetAll().Where(a => a.CaliberID == itemCaliberID).ToList();
            return q;
        }
        
        public bool Update(ITEMSVM entity)
        {

            ITEMS item = new ITEMS
            {
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                BURCHASEDISCOUNT = entity.BURCHASEDISCOUNT,
                COMPANY_ID = entity.COMPANY_ID,
                COST_CALCULATION_TYPE = entity.COST_CALCULATION_TYPE,
                DOESTHEQUANTITYISAPARTOFBARCODE = entity.DOESTHEQUANTITYISAPARTOFBARCODE,
                EXPIRED_DATE = entity.EXPIRED_DATE,
                GROUP_ID = (int)entity.GROUP_ID,
                ITEM_AR_NAME = entity.ITEM_AR_NAME,
                ITEM_BRAND = entity.ITEM_BRAND,
                ITEM_CODE = entity.ITEM_CODE,
                ITEM_COLOR = entity.ITEM_COLOR,
                ITEM_EN_NAME = entity.ITEM_EN_NAME,
                ITEM_ID = entity.ITEM_ID,
                ITEM_MODEL = entity.ITEM_MODEL,
                ITEM_PIC = entity.ITEM_PIC,
                ITEM_REMARKS = entity.ITEM_REMARKS,
                ITEM_TYPE = entity.ITEM_TYPE,
                MIN_QTY = entity.MIN_QTY,
                PRODUCTION_DATE = entity.PRODUCTION_DATE,
                QUANTITYLENGTHATTHEBARCODE = entity.QUANTITYLENGTHATTHEBARCODE,
                QUANTITYSTARTATTHEBARCODE = entity.QUANTITYSTARTATTHEBARCODE,
                QUANTITYPARTSTARTATTHEBARCODE = entity.QUANTITYPARTSTARTATTHEBARCODE,
                QUANTITYPARTLENGTHATTHEBARCODE = entity.QUANTITYPARTLENGTHATTHEBARCODE,
                SELLEDISCOUNT = entity.SELLEDISCOUNT,
                SERIAL_IN_INPUT = entity.SERIAL_IN_INPUT,
                SERIAL_IN_OUTPUT = entity.SERIAL_IN_OUTPUT,
                SubjectToVAT = entity.SubjectToVAT,
                VATRate = entity.VATRate,
                CaliberID = entity.CaliberID,
                GlobalGoldPrice = entity.GlobalGoldPrice,
                ManufacturingWages = entity.ManufacturingWages,
                ProfitMargin = entity.ProfitMargin,
                ItemPrice = entity.ItemPrice,
                ItemWeight = entity.ItemWeight,
                ItemStatus = entity.ItemStatus,
                LowestSellingPrice = entity.LowestSellingPrice,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn,
                IsItemCodeRelatedByGroup = entity.IsItemCodeRelatedByGroup,
                itemGmWages = entity.itemGmWages,
                GoldAccID = entity.GoldAccID,
                ClearnessRate=entity.ClearnessRate,

                GemsWages = entity.GemsWages,
                GemsWeight = entity.GemsWeight
            };
            itemsRepo.Update(item, item.ITEM_ID);
            return true;


        }
        public Task<bool> UpdateAsync(ITEMSVM entity)
        {
            return Task.Run<bool>(() =>
            {
                string base64 = entity.Item_Base64_Photo;
                entity.Item_Base64_Photo = String.Format(base64);
                base64 = base64.Remove(0, base64.IndexOf("base64,") + 7);
                entity.ITEM_PIC = Convert.FromBase64String(base64);

                ITEMS item = new ITEMS
                {
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    BURCHASEDISCOUNT = entity.BURCHASEDISCOUNT,
                    COMPANY_ID = entity.COMPANY_ID,
                    COST_CALCULATION_TYPE = entity.COST_CALCULATION_TYPE,
                    DOESTHEQUANTITYISAPARTOFBARCODE = entity.DOESTHEQUANTITYISAPARTOFBARCODE,
                    EXPIRED_DATE = entity.EXPIRED_DATE,
                    GROUP_ID = (int)entity.GROUP_ID,
                    ITEM_AR_NAME = entity.ITEM_AR_NAME,
                    ITEM_BRAND = entity.ITEM_BRAND,
                    ITEM_CODE = entity.ITEM_CODE,
                    ITEM_COLOR = entity.ITEM_COLOR,
                    ITEM_EN_NAME = entity.ITEM_EN_NAME,
                    ITEM_ID = entity.ITEM_ID,
                    ITEM_MODEL = entity.ITEM_MODEL,
                    ITEM_PIC = entity.ITEM_PIC,
                    ITEM_REMARKS = entity.ITEM_REMARKS,
                    ITEM_TYPE = entity.ITEM_TYPE,
                    MIN_QTY = entity.MIN_QTY,
                    PRODUCTION_DATE = entity.PRODUCTION_DATE,
                    QUANTITYLENGTHATTHEBARCODE = entity.QUANTITYLENGTHATTHEBARCODE,
                    QUANTITYSTARTATTHEBARCODE = entity.QUANTITYSTARTATTHEBARCODE,
                    QUANTITYPARTSTARTATTHEBARCODE = entity.QUANTITYPARTSTARTATTHEBARCODE,
                    QUANTITYPARTLENGTHATTHEBARCODE = entity.QUANTITYPARTLENGTHATTHEBARCODE,
                    SELLEDISCOUNT = entity.SELLEDISCOUNT,
                    SERIAL_IN_INPUT = entity.SERIAL_IN_INPUT,
                    SERIAL_IN_OUTPUT = entity.SERIAL_IN_OUTPUT,
                    SubjectToVAT = entity.SubjectToVAT,
                    VATRate = entity.VATRate,
                    CaliberID = entity.CaliberID,
                    GlobalGoldPrice = entity.GlobalGoldPrice,
                    ManufacturingWages = entity.ManufacturingWages,
                    ProfitMargin = entity.ProfitMargin,
                    ItemPrice = entity.ItemPrice,
                    ItemWeight = entity.ItemWeight,
                    ItemStatus = entity.ItemStatus,
                    LowestSellingPrice = entity.LowestSellingPrice,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn,
                    KiloPrice = entity.KiloPrice,
                    OuncePrice = entity.OuncePrice,
                    IsItemCodeRelatedByGroup = entity.IsItemCodeRelatedByGroup,
                    itemGmWages = entity.itemGmWages,
                    GoldAccID = entity.GoldAccID,
                    ClearnessRate = entity.ClearnessRate,

                    GemsWages = entity.GemsWages,
                    GemsWeight = entity.GemsWeight
                };
                itemsRepo.Update(item, item.ITEM_ID);
                return true;
            }
              );

        }

        public bool Delete(ITEMSVM entity)
        {

            ITEMS item = new ITEMS
            {
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                BURCHASEDISCOUNT = entity.BURCHASEDISCOUNT,
                COMPANY_ID = entity.COMPANY_ID,
                COST_CALCULATION_TYPE = entity.COST_CALCULATION_TYPE,
                DOESTHEQUANTITYISAPARTOFBARCODE = entity.DOESTHEQUANTITYISAPARTOFBARCODE,
                EXPIRED_DATE = entity.EXPIRED_DATE,
                GROUP_ID = (int)entity.GROUP_ID,
                ITEM_AR_NAME = entity.ITEM_AR_NAME,
                ITEM_BRAND = entity.ITEM_BRAND,
                ITEM_CODE = entity.ITEM_CODE,
                ITEM_COLOR = entity.ITEM_COLOR,
                ITEM_EN_NAME = entity.ITEM_EN_NAME,
                ITEM_ID = entity.ITEM_ID,
                ITEM_MODEL = entity.ITEM_MODEL,
                ITEM_PIC = entity.ITEM_PIC,
                ITEM_REMARKS = entity.ITEM_REMARKS,
                ITEM_TYPE = entity.ITEM_TYPE,
                MIN_QTY = entity.MIN_QTY,
                PRODUCTION_DATE = entity.PRODUCTION_DATE,
                QUANTITYLENGTHATTHEBARCODE = entity.QUANTITYLENGTHATTHEBARCODE,
                QUANTITYSTARTATTHEBARCODE = entity.QUANTITYSTARTATTHEBARCODE,
                QUANTITYPARTSTARTATTHEBARCODE = entity.QUANTITYPARTSTARTATTHEBARCODE,
                QUANTITYPARTLENGTHATTHEBARCODE = entity.QUANTITYPARTLENGTHATTHEBARCODE,
                SELLEDISCOUNT = entity.SELLEDISCOUNT,
                SERIAL_IN_INPUT = entity.SERIAL_IN_INPUT,
                SERIAL_IN_OUTPUT = entity.SERIAL_IN_OUTPUT,
                SubjectToVAT = entity.SubjectToVAT,
                VATRate = entity.VATRate,
                CaliberID = entity.CaliberID,
                GlobalGoldPrice = entity.GlobalGoldPrice,
                ManufacturingWages = entity.ManufacturingWages,
                ProfitMargin = entity.ProfitMargin,
                ItemPrice = entity.ItemPrice,
                ItemWeight = entity.ItemWeight,
                ItemStatus = entity.ItemStatus,
                LowestSellingPrice = entity.LowestSellingPrice,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn,
                IsItemCodeRelatedByGroup = entity.IsItemCodeRelatedByGroup,
                GoldAccID = entity.GoldAccID,
                ClearnessRate = entity.ClearnessRate,

                GemsWages = entity.GemsWages,
                GemsWeight = entity.GemsWeight
            };
            itemsRepo.Delete(item, entity.ITEM_ID);
            return true;


        }
        public Task<bool> DeleteAsync(ITEMSVM entity)
        {
            return Task.Run<bool>(() =>
            {
                ITEMS item = new ITEMS
                {
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    BURCHASEDISCOUNT = entity.BURCHASEDISCOUNT,
                    COMPANY_ID = entity.COMPANY_ID,
                    COST_CALCULATION_TYPE = entity.COST_CALCULATION_TYPE,
                    DOESTHEQUANTITYISAPARTOFBARCODE = entity.DOESTHEQUANTITYISAPARTOFBARCODE,
                    EXPIRED_DATE = entity.EXPIRED_DATE,
                    GROUP_ID = (int)entity.GROUP_ID,
                    ITEM_AR_NAME = entity.ITEM_AR_NAME,
                    ITEM_BRAND = entity.ITEM_BRAND,
                    ITEM_CODE = entity.ITEM_CODE,
                    ITEM_COLOR = entity.ITEM_COLOR,
                    ITEM_EN_NAME = entity.ITEM_EN_NAME,
                    ITEM_ID = entity.ITEM_ID,
                    ITEM_MODEL = entity.ITEM_MODEL,
                    ITEM_PIC = entity.ITEM_PIC,
                    ITEM_REMARKS = entity.ITEM_REMARKS,
                    ITEM_TYPE = entity.ITEM_TYPE,
                    MIN_QTY = entity.MIN_QTY,
                    PRODUCTION_DATE = entity.PRODUCTION_DATE,
                    QUANTITYLENGTHATTHEBARCODE = entity.QUANTITYLENGTHATTHEBARCODE,
                    QUANTITYSTARTATTHEBARCODE = entity.QUANTITYSTARTATTHEBARCODE,
                    QUANTITYPARTSTARTATTHEBARCODE = entity.QUANTITYPARTSTARTATTHEBARCODE,
                    QUANTITYPARTLENGTHATTHEBARCODE = entity.QUANTITYPARTLENGTHATTHEBARCODE,
                    SELLEDISCOUNT = entity.SELLEDISCOUNT,
                    SERIAL_IN_INPUT = entity.SERIAL_IN_INPUT,
                    SERIAL_IN_OUTPUT = entity.SERIAL_IN_OUTPUT,
                    SubjectToVAT = entity.SubjectToVAT,
                    VATRate = entity.VATRate,
                    CaliberID = entity.CaliberID,
                    GlobalGoldPrice = entity.GlobalGoldPrice,
                    ManufacturingWages = entity.ManufacturingWages,
                    ProfitMargin = entity.ProfitMargin,
                    ItemPrice = entity.ItemPrice,
                    ItemWeight = entity.ItemWeight,
                    ItemStatus = entity.ItemStatus,
                    LowestSellingPrice = entity.LowestSellingPrice,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn,
                    KiloPrice = entity.KiloPrice,
                    OuncePrice = entity.OuncePrice,
                    IsItemCodeRelatedByGroup = entity.IsItemCodeRelatedByGroup,
                    GoldAccID = entity.GoldAccID,
                    ClearnessRate = entity.ClearnessRate,

                    GemsWages = entity.GemsWages,
                    GemsWeight = entity.GemsWeight
                };
                itemsRepo.Delete(item, entity.ITEM_ID);
                return true;
            }
              );

        }

        public Task<List<ITEMSVM>> GetSearchForItem(string search)
        {
            return Task.Run<List<ITEMSVM>>(() =>
            {

                var q = from p in itemsRepo.GetAll()
                        join itmUnt in itemUnitsRepo.GetAll() on p.ITEM_ID equals itmUnt.ITEM_ID
                        join unt in unitsRepo.GetAll() on itmUnt.UNIT_ID equals unt.UNIT_ID
                        join itmPrice in itemPricesRepo.GetAll() on itmUnt.ITEM_UNIT_ID equals itmPrice.ITEM_UNIT_ID
                        select new ITEMSVM
                        {
                            AddedBy = p.AddedBy,
                            AddedOn = p.AddedOn,
                            BURCHASEDISCOUNT = p.BURCHASEDISCOUNT,
                            COMPANY_ID = p.COMPANY_ID,
                            COST_CALCULATION_TYPE = p.COST_CALCULATION_TYPE,
                            DOESTHEQUANTITYISAPARTOFBARCODE = p.DOESTHEQUANTITYISAPARTOFBARCODE,
                            EXPIRED_DATE = p.EXPIRED_DATE,
                            GROUP_ID = p.GROUP_ID,
                            ITEM_AR_NAME = p.ITEM_AR_NAME,
                            ITEM_BRAND = p.ITEM_BRAND,
                            ITEM_CODE = p.ITEM_CODE,
                            ITEM_COLOR = p.ITEM_COLOR,
                            ITEM_EN_NAME = p.ITEM_EN_NAME,
                            ITEM_ID = p.ITEM_ID,
                            ITEM_MODEL = p.ITEM_MODEL,
                            ITEM_PIC = p.ITEM_PIC,
                            ITEM_REMARKS = p.ITEM_REMARKS,
                            ITEM_TYPE = p.ITEM_TYPE,
                            MIN_QTY = p.MIN_QTY,
                            PRODUCTION_DATE = p.PRODUCTION_DATE,
                            QUANTITYLENGTHATTHEBARCODE = p.QUANTITYLENGTHATTHEBARCODE,
                            QUANTITYSTARTATTHEBARCODE = p.QUANTITYSTARTATTHEBARCODE,
                            SELLEDISCOUNT = p.SELLEDISCOUNT,
                            SERIAL_IN_INPUT = p.SERIAL_IN_INPUT,
                            SERIAL_IN_OUTPUT = p.SERIAL_IN_OUTPUT,
                            UpdatedBy = p.UpdatedBy,
                            updatedOn = p.updatedOn,
                            Unit_ID = itmUnt.UNIT_ID,
                            Unit_Name_Ar = unt.UNIT_AR_NAME,
                            Unit_Trans_Rate = itmUnt.UNIT_MASTER_UNIT_ID,
                            WHOLE_PRICE = itmPrice.WHOLE_PRICE,
                            CONSUMER_PRICE = itmPrice.CONSUMER_PRICE,
                            KiloPrice = p.KiloPrice,
                            OuncePrice = p.OuncePrice,
                            IsItemCodeRelatedByGroup = p.IsItemCodeRelatedByGroup,
                            GoldAccID = p.GoldAccID,
                            ClearnessRate = p.ClearnessRate,

                            GemsWages = p.GemsWages,
                            GemsWeight = p.GemsWeight
                        };
                if (!string.IsNullOrEmpty(search))
                {
                    return q.Where(x => (x.ITEM_CODE.ToLower().Contains(search.ToLower()) || x.ITEM_AR_NAME.ToLower().Contains(search.ToLower()) || x.ITEM_EN_NAME.ToLower().Contains(search.ToLower()))).ToList();
                }
                else
                {
                    return q.ToList();
                }
            });
        }

        public List<ITEMS> GetByIDGroup(int groupId)
        {
            var q= itemsRepo.GetAll().Where(a => a.GROUP_ID == groupId).ToList();
            return q;
                //return itemsRepo.Filter(x => x.GROUP_ID == groupId).FirstOrDefault();
        }
        public Task<ITEMS> GetByID(int itemID)
        {
            return Task.Run<ITEMS>(() =>
            {
                return itemsRepo.Filter(x => x.ITEM_ID == itemID).FirstOrDefault();
            });
        }

        public Task<List<ITEMSVM>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run<List<ITEMSVM>>(() =>
            {

                int rowCount;

                var q = from p in itemsRepo.GetPaged<int>(pageNum, pageSize, p => p.ITEM_ID, false, out rowCount)
                        select new ITEMSVM
                        {
                            COMPANY_ID = p.COMPANY_ID,//
                            GROUP_ID = p.GROUP_ID,//
                            ITEM_AR_NAME = p.ITEM_AR_NAME,//
                            ITEM_CODE = p.ITEM_CODE, //
                            ITEM_EN_NAME = p.ITEM_EN_NAME,//
                            ITEM_ID = p.ITEM_ID,//
                            ITEM_REMARKS = p.ITEM_REMARKS,
                            ItemStatus =p.ItemStatus,
                            GoldAccID = p.GoldAccID
                            //
                            
                            //KiloPrice = p.KiloPrice,//
                            //OuncePrice = p.OuncePrice,//
                        };

                return q.ToList();
            });
        }

        public Task<Dictionary<string, bool>> checkValidation(string Code, string ArName, string EnName)
        {
            return Task.Run<Dictionary<string, bool>>(() =>
            {
                Dictionary<string, bool> chk = new Dictionary<string, bool>();
                chk.Add("itemArName", CheckItemArName(ArName));
                chk.Add("itemEnName", CheckItemEnName(EnName));
                chk.Add("itemCode", CheckItemCode(Code));
                return chk;
            });
        }

        bool CheckItemArName(string ArName)
        {
            return itemsRepo.Filter(x => x.ITEM_AR_NAME.ToLower() == ArName.ToLower()).Count() >= 1;
        }
        bool CheckItemEnName(string EnName)
        {
            return itemsRepo.Filter(x => x.ITEM_EN_NAME.ToLower() == EnName.ToLower()).Count() >= 1;
        }
        bool CheckItemCode(string code)
        {
            return itemsRepo.Filter(x => x.ITEM_CODE == code).Count() >= 1;
        }

        public List<ITEMSVM> GetAllItems()
        {


            var q = from p in itemsRepo.GetAll()
                    select new ITEMSVM
                    {
                        AddedBy = p.AddedBy,
                        AddedOn = p.AddedOn,
                        BURCHASEDISCOUNT = p.BURCHASEDISCOUNT,
                        COMPANY_ID = p.COMPANY_ID,
                        COST_CALCULATION_TYPE = p.COST_CALCULATION_TYPE,
                        DOESTHEQUANTITYISAPARTOFBARCODE = p.DOESTHEQUANTITYISAPARTOFBARCODE,
                        EXPIRED_DATE = p.EXPIRED_DATE,
                        GROUP_ID = p.GROUP_ID,
                        ITEM_AR_NAME = p.ITEM_AR_NAME,
                        ITEM_BRAND = p.ITEM_BRAND,
                        ITEM_CODE = p.ITEM_CODE,
                        ITEM_COLOR = p.ITEM_COLOR,
                        ITEM_EN_NAME = p.ITEM_EN_NAME,
                        ITEM_ID = p.ITEM_ID,
                        ITEM_MODEL = p.ITEM_MODEL,
                        ITEM_PIC = p.ITEM_PIC,
                        ITEM_REMARKS = p.ITEM_REMARKS,
                        ITEM_TYPE = p.ITEM_TYPE,
                        MIN_QTY = p.MIN_QTY,
                        PRODUCTION_DATE = p.PRODUCTION_DATE,
                        QUANTITYLENGTHATTHEBARCODE = p.QUANTITYLENGTHATTHEBARCODE,
                        QUANTITYSTARTATTHEBARCODE = p.QUANTITYSTARTATTHEBARCODE,
                        QUANTITYPARTSTARTATTHEBARCODE = p.QUANTITYPARTSTARTATTHEBARCODE,
                        QUANTITYPARTLENGTHATTHEBARCODE = p.QUANTITYPARTLENGTHATTHEBARCODE,
                        SELLEDISCOUNT = p.SELLEDISCOUNT,
                        SERIAL_IN_INPUT = p.SERIAL_IN_INPUT,
                        SERIAL_IN_OUTPUT = p.SERIAL_IN_OUTPUT,
                        SubjectToVAT = p.SubjectToVAT,
                        VATRate = p.VATRate,
                        CaliberID = p.CaliberID,
                        GlobalGoldPrice = p.GlobalGoldPrice,
                        ManufacturingWages = p.ManufacturingWages,
                        ProfitMargin = p.ProfitMargin,
                        ItemPrice = p.ItemPrice,
                        ItemWeight = p.ItemWeight,
                        ItemStatus = p.ItemStatus,
                        LowestSellingPrice = p.LowestSellingPrice,
                        UpdatedBy = p.UpdatedBy,
                        updatedOn = p.updatedOn,
                        KiloPrice = p.KiloPrice,
                        OuncePrice = p.OuncePrice,
                        IsItemCodeRelatedByGroup = p.IsItemCodeRelatedByGroup,
                        GoldAccID = p.GoldAccID,
                        ClearnessRate = p.ClearnessRate,

                        GemsWages = p.GemsWages,
                        GemsWeight = p.GemsWeight
                    };
            return q.ToList();
        }
        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return itemsRepo.CountAsync();
            });
        }

        public List<CustomItemUnitsVM> GetAllItemUnits(int ItemID)
        {
            //string dbname = itemsRepo.GetLoggedDatabaseName();
            List<CustomItemUnitsVM> ItemUnitsList = new List<CustomItemUnitsVM>();
            var q = from p in itemUnitsRepo.Filter(x => x.ITEM_ID == ItemID)
                    select new CustomItemUnitsVM
                    {
                        IS_DEFAULT = p.IS_DEFAULT,
                        UNIT_BARCODE = p.UNIT_BARCODE,
                        UNIT_ID = p.UNIT_ID,
                        UNIT_TRANS_RATE = p.UNIT_TRANS_RATE,
                        Operation = "Edit",
                        Item_ID = p.ITEM_ID,
                        ItemUnitID = p.ITEM_UNIT_ID,

                    };
            List<CustomItemUnitsVM> ItemUnits = q.ToList();
            foreach (var item in ItemUnits)
            {
                //Get UnitCode And UnitNameAr by UnitID
                CustomItemUnitsVM UnitDetails = GetUnitDataUsingUnitCode(item.UNIT_ID);
                if (UnitDetails != null)
                {
                    item.UNIT_CODE = UnitDetails.UNIT_CODE;
                    item.UNIT_AR_NAME = UnitDetails.UNIT_AR_NAME;
                }


                //Get ItemPrices
                CustomItemUnitsVM ItemPrices = GetItemPrices(item.ItemUnitID);
                if (ItemPrices != null)
                {
                    item.CONSUMER_PRICE = ItemPrices.CONSUMER_PRICE;
                    item.CONSUMER_PRICE = ItemPrices.CONSUMER_PRICE;
                    item.EMP_PRICE = ItemPrices.EMP_PRICE;
                    item.EXPORT_PRICE = ItemPrices.EXPORT_PRICE;
                    item.HALF_WHOLE_PRICE = ItemPrices.HALF_WHOLE_PRICE;
                    item.LAST_BUY_PRICE = ItemPrices.LAST_BUY_PRICE;
                    item.RETAIL_PRICE = ItemPrices.RETAIL_PRICE;
                    item.WHOLE_PRICE = ItemPrices.WHOLE_PRICE;
                }
                else
                {
                    item.CONSUMER_PRICE = 0;
                    item.CONSUMER_PRICE = 0;
                    item.EMP_PRICE = 0;
                    item.EXPORT_PRICE = 0;
                    item.HALF_WHOLE_PRICE = 0;
                    item.LAST_BUY_PRICE = 0;
                    item.RETAIL_PRICE = 0;
                    item.WHOLE_PRICE = 0;
                }
            }
            return ItemUnits;
        }
        public CustomItemUnitsVM GetItemPrices(int ItemUnitID)
        {
            var q = from p in itemPricesRepo.Filter(x => x.ITEM_UNIT_ID == ItemUnitID)

                    select new CustomItemUnitsVM
                    {
                        CONSUMER_PRICE = p.CONSUMER_PRICE,
                        EMP_PRICE = p.EMP_PRICE,
                        EXPORT_PRICE = p.EXPORT_PRICE,
                        HALF_WHOLE_PRICE = p.HALF_WHOLE_PRICE,
                        LAST_BUY_PRICE = p.LAST_BUY_PRICE,
                        RETAIL_PRICE = p.RETAIL_PRICE,
                        WHOLE_PRICE = p.WHOLE_PRICE
                    };
            return q.FirstOrDefault();
        }

        public CustomItemUnitsVM GetUnitDataUsingUnitCode(int UnitID)
        {
            var q = from p in unitsRepo.Filter(x => x.UNIT_ID == UnitID)

                    select new CustomItemUnitsVM
                    {
                        UNIT_CODE = p.UNIT_CODE,
                        UNIT_AR_NAME = p.UNIT_AR_NAME,
                    };
            return q.FirstOrDefault();
        }


        public int GetLastItemAdded()
        {
            int LastItemAdded = itemsRepo.GetAll().OrderByDescending(p => p.ITEM_ID).Select(r => r.ITEM_ID).First();
            return LastItemAdded;


        }

        public bool InsertItemUnits(List<CustomItemUnitsVM> ItemUnitsList, int[] DeletedItemUnits)
        {
            //Delete ItemUnits that checked as deleted
            if (DeletedItemUnits != null && DeletedItemUnits.Length > 0)
            {
                foreach (int item in DeletedItemUnits)
                {
                    ITEMS_UNITS Obj = new ITEMS_UNITS();
                    {
                        Obj.ITEM_UNIT_ID = item;
                        itemUnitsRepo.Delete(Obj, item);
                    }
                    //Delete ItemPrices
                    ITEM_PRICES ItemPricesObj = new ITEM_PRICES();
                    {
                        ItemPricesObj.ITEM_UNIT_ID = item;
                        itemPricesRepo.Delete(ItemPricesObj, item);
                    }
                }
            }
            foreach (CustomItemUnitsVM item in ItemUnitsList)
            {
                if (item.UNIT_ID > 0)
                {
                    //Insert ItemUnits for the first time 
                    if (item.Operation == "Insert")
                    {
                        //Insert Item Units
                        ITEMS_UNITS ItemsUnits = new ITEMS_UNITS();
                        ItemsUnits.ITEM_ID = item.Item_ID;
                        ItemsUnits.UNIT_BARCODE = item.UNIT_BARCODE;
                        ItemsUnits.UNIT_ID = item.UNIT_ID;
                        ItemsUnits.UNIT_TRANS_RATE = item.UNIT_TRANS_RATE;
                        ItemsUnits.IS_DEFAULT = item.IS_DEFAULT;
                        itemUnitsRepo.Add(ItemsUnits);
                        //Save ItemPrices
                        int InsertedId = ItemsUnits.ITEM_UNIT_ID;
                        ITEM_PRICES ItemPricesObj = new ITEM_PRICES();
                        ItemPricesObj.ITEM_UNIT_ID = InsertedId;
                        ItemPricesObj.CONSUMER_PRICE = item.CONSUMER_PRICE;
                        ItemPricesObj.EMP_PRICE = item.EMP_PRICE;
                        ItemPricesObj.EXPORT_PRICE = item.EXPORT_PRICE;
                        ItemPricesObj.HALF_WHOLE_PRICE = item.HALF_WHOLE_PRICE;
                        ItemPricesObj.LAST_BUY_PRICE = item.LAST_BUY_PRICE;
                        ItemPricesObj.RETAIL_PRICE = item.RETAIL_PRICE;
                        ItemPricesObj.WHOLE_PRICE = item.WHOLE_PRICE;
                        itemPricesRepo.Add(ItemPricesObj);
                    }
                    else if (item.Operation == "Edit")
                    {
                        ITEMS_UNITS ItemsUnits = new ITEMS_UNITS();
                        ItemsUnits.ITEM_ID = item.Item_ID;
                        ItemsUnits.UNIT_BARCODE = item.UNIT_BARCODE;
                        ItemsUnits.UNIT_ID = item.UNIT_ID;
                        ItemsUnits.UNIT_TRANS_RATE = item.UNIT_TRANS_RATE;
                        ItemsUnits.IS_DEFAULT = item.IS_DEFAULT;
                        ItemsUnits.ITEM_UNIT_ID = item.ItemUnitID;
                        itemUnitsRepo.Update(ItemsUnits, item.ItemUnitID);
                        //Update ItemPrices
                        ITEM_PRICES ItemPricesObj = new ITEM_PRICES();
                        ItemPricesObj.ITEM_UNIT_ID = item.ItemUnitID;
                        ItemPricesObj.CONSUMER_PRICE = item.CONSUMER_PRICE;
                        ItemPricesObj.EMP_PRICE = item.EMP_PRICE;
                        ItemPricesObj.EXPORT_PRICE = item.EXPORT_PRICE;
                        ItemPricesObj.HALF_WHOLE_PRICE = item.HALF_WHOLE_PRICE;
                        ItemPricesObj.LAST_BUY_PRICE = item.LAST_BUY_PRICE;
                        ItemPricesObj.RETAIL_PRICE = item.RETAIL_PRICE;
                        ItemPricesObj.WHOLE_PRICE = item.WHOLE_PRICE;
                        itemPricesRepo.Update(ItemPricesObj, item.ItemUnitID);
                    }
                }
            }
            return true;
        }

        public List<ITEMSVM> GetItemsByItemGroup(int GroupID)
        {
            var q = from p in itemsRepo.Filter(x => x.GROUP_ID == GroupID)
                    join iu in itemUnitsRepo.GetAll() on p.ITEM_ID equals iu.ITEM_ID into g
                    from x in g.DefaultIfEmpty()
                    select new ITEMSVM
                    {
                        AddedBy = p.AddedBy,
                        AddedOn = p.AddedOn,
                        BURCHASEDISCOUNT = p.BURCHASEDISCOUNT,
                        COMPANY_ID = p.COMPANY_ID,
                        COST_CALCULATION_TYPE = p.COST_CALCULATION_TYPE,
                        DOESTHEQUANTITYISAPARTOFBARCODE = p.DOESTHEQUANTITYISAPARTOFBARCODE,
                        EXPIRED_DATE = p.EXPIRED_DATE,
                        GROUP_ID = p.GROUP_ID,
                        ITEM_AR_NAME = p.ITEM_AR_NAME,
                        ITEM_BRAND = p.ITEM_BRAND,
                        ITEM_CODE = p.ITEM_CODE,
                        ITEM_COLOR = p.ITEM_COLOR,
                        ITEM_EN_NAME = p.ITEM_EN_NAME,
                        ITEM_ID = p.ITEM_ID,
                        ITEM_MODEL = p.ITEM_MODEL,
                        ITEM_PIC = p.ITEM_PIC,
                        ITEM_REMARKS = p.ITEM_REMARKS,
                        ITEM_TYPE = p.ITEM_TYPE,
                        MIN_QTY = p.MIN_QTY,
                        PRODUCTION_DATE = p.PRODUCTION_DATE,
                        QUANTITYLENGTHATTHEBARCODE = p.QUANTITYLENGTHATTHEBARCODE,
                        QUANTITYSTARTATTHEBARCODE = p.QUANTITYSTARTATTHEBARCODE,
                        SELLEDISCOUNT = p.SELLEDISCOUNT,
                        SERIAL_IN_INPUT = p.SERIAL_IN_INPUT,
                        SERIAL_IN_OUTPUT = p.SERIAL_IN_OUTPUT,
                        UpdatedBy = p.UpdatedBy,
                        updatedOn = p.updatedOn,
                        KiloPrice = p.KiloPrice,
                        OuncePrice = p.OuncePrice,
                        GoldAccID = p.GoldAccID,
                        Unit_Name_Ar = x == null ? "" : unitsRepo.Filter(s => s.UNIT_ID == x.UNIT_ID && x.IS_DEFAULT == true).FirstOrDefault() == null ? "" : unitsRepo.Filter(s => s.UNIT_ID == x.UNIT_ID).FirstOrDefault().UNIT_AR_NAME,
                        ItemUnitPrice = GetItemUnitPrice(Convert.ToInt32(p.ITEM_ID), 0),
                        IsItemCodeRelatedByGroup = p.IsItemCodeRelatedByGroup,
                        ClearnessRate = p.ClearnessRate,

                        GemsWages = p.GemsWages,
                        GemsWeight = p.GemsWeight
                    };

            return q.ToList();
        }

        private double? GetItemUnitPrice(int ItemCode, int SellTypeID)
        {
            //int? ItemID = itemsRepo.GetAll().Where(x => int.Parse(x.ITEM_CODE) == ItemCode).FirstOrDefault().ITEM_ID;
            double? Price = 0;
            int? ItemUnitID = itemUnitsRepo.Filter(x => x.ITEM_ID == ItemCode).FirstOrDefault() == null ? 0 : itemUnitsRepo.Filter(x => x.ITEM_ID == ItemCode).FirstOrDefault().ITEM_UNIT_ID;
            if (ItemUnitID > 0)
            {
                Price = itemPricesRepo.Filter(x => x.ITEM_UNIT_ID == ItemUnitID).FirstOrDefault() == null ? 0 :
                    SellTypeID == (int)SellTypes.Whole ? itemPricesRepo.Filter(x => x.ITEM_UNIT_ID == ItemUnitID).FirstOrDefault().WHOLE_PRICE : SellTypeID == (int)SellTypes.Half_Whole ? itemPricesRepo.Filter(x => x.ITEM_UNIT_ID == ItemUnitID).FirstOrDefault().HALF_WHOLE_PRICE : SellTypeID == (int)SellTypes.Employee ? itemPricesRepo.Filter(x => x.ITEM_UNIT_ID == ItemUnitID).FirstOrDefault().EMP_PRICE : SellTypeID == (int)SellTypes.EndUsers ? itemPricesRepo.Filter(x => x.ITEM_UNIT_ID == ItemUnitID).FirstOrDefault().LAST_BUY_PRICE : SellTypeID == (int)SellTypes.Export ? itemPricesRepo.Filter(x => x.ITEM_UNIT_ID == ItemUnitID).FirstOrDefault().EXPORT_PRICE : SellTypeID == (int)SellTypes.Last_Buy ? itemPricesRepo.Filter(x => x.ITEM_UNIT_ID == ItemUnitID).FirstOrDefault().LAST_BUY_PRICE : SellTypeID == (int)SellTypes.Retail ? itemPricesRepo.Filter(x => x.ITEM_UNIT_ID == ItemUnitID).FirstOrDefault().RETAIL_PRICE : itemPricesRepo.GetAll().Where(x => x.ITEM_UNIT_ID == ItemUnitID).FirstOrDefault().WHOLE_PRICE;
            }
            return Price;
        }

        public string GetItemNameUsingItemID(int ItemID)
        {
            string q = itemsRepo.Filter(x => x.ITEM_CODE == Convert.ToString(ItemID)).FirstOrDefault().ITEM_AR_NAME;
            return q;
        }

        public ITEMSVM GetItemDataUsingItemCode(string ItemCode, int SellTypeID)
        {
            var q = from p in itemsRepo.Filter(x => x.ITEM_CODE == ItemCode)
                    join iu in itemUnitsRepo.GetAll() on p.ITEM_ID equals iu.ITEM_ID into g
                    from x in g.DefaultIfEmpty()
                    join Y in unitsRepo.GetAll() on x.UNIT_ID equals Y.UNIT_ID into z
                    from YY in z.DefaultIfEmpty()
                    join YYY in itemCaliberRepo.GetAll() on p.CaliberID equals YYY.ID into zz
                    from YYYY in zz.DefaultIfEmpty()
                    where x.IS_DEFAULT = true
                    select new ITEMSVM
                    {
                        AddedBy = p.AddedBy,
                        AddedOn = p.AddedOn,
                        BURCHASEDISCOUNT = p.BURCHASEDISCOUNT,
                        COMPANY_ID = p.COMPANY_ID,
                        COST_CALCULATION_TYPE = p.COST_CALCULATION_TYPE,
                        DOESTHEQUANTITYISAPARTOFBARCODE = p.DOESTHEQUANTITYISAPARTOFBARCODE,
                        EXPIRED_DATE = p.EXPIRED_DATE,
                        GROUP_ID = p.GROUP_ID,
                        ITEM_AR_NAME = p.ITEM_AR_NAME,
                        ITEM_BRAND = p.ITEM_BRAND,
                        ITEM_CODE = p.ITEM_CODE,
                        ITEM_COLOR = p.ITEM_COLOR,
                        ITEM_EN_NAME = p.ITEM_EN_NAME,
                        ITEM_ID = p.ITEM_ID,
                        ITEM_MODEL = p.ITEM_MODEL,
                        //ITEM_PIC = p.ITEM_PIC,
                        ITEM_REMARKS = p.ITEM_REMARKS,
                        ITEM_TYPE = p.ITEM_TYPE,
                        MIN_QTY = p.MIN_QTY,
                        PRODUCTION_DATE = p.PRODUCTION_DATE,
                        QUANTITYLENGTHATTHEBARCODE = p.QUANTITYLENGTHATTHEBARCODE,
                        QUANTITYSTARTATTHEBARCODE = p.QUANTITYSTARTATTHEBARCODE,
                        QUANTITYPARTSTARTATTHEBARCODE = p.QUANTITYPARTSTARTATTHEBARCODE,
                        QUANTITYPARTLENGTHATTHEBARCODE = p.QUANTITYPARTLENGTHATTHEBARCODE,
                        SELLEDISCOUNT = p.SELLEDISCOUNT,
                        SERIAL_IN_INPUT = p.SERIAL_IN_INPUT,
                        SERIAL_IN_OUTPUT = p.SERIAL_IN_OUTPUT,
                        UpdatedBy = p.UpdatedBy,
                        updatedOn = p.updatedOn,
                        GoldAccID = p.GoldAccID,
                        Unit_Name_Ar = YY == null ? "" : YY.UNIT_AR_NAME == null ? "" : YY.UNIT_AR_NAME,
                        //Unit_Name_Ar = x == null ? "" : unitsRepo.GetAll().Where(s => s.UNIT_ID == x.UNIT_ID && x.IS_DEFAULT == true).FirstOrDefault() == null ? "" : unitsRepo.GetAll().Where(s => s.UNIT_ID == x.UNIT_ID).FirstOrDefault().UNIT_AR_NAME,
                        ItemUnitPrice = GetItemUnitPrice(Convert.ToInt32(p.ITEM_ID), SellTypeID),
                        CaliberID = p.CaliberID,
                        GlobalGoldPrice = p.GlobalGoldPrice,
                        ManufacturingWages = p.ManufacturingWages,
                        ProfitMargin = p.ProfitMargin,
                        ItemPrice = p.ItemPrice,
                        ItemWeight = p.ItemWeight,
                        ItemStatus = p.ItemStatus,
                        LowestSellingPrice = p.LowestSellingPrice,
                        SubjectToVAT = p.SubjectToVAT,
                        VATRate = p.VATRate,
                        ARName = YYYY.ARName,
                        LatName = YYYY.LatName,
                        ClearnessRate = YYYY.ClearnessRate,
                        KiloPrice = p.KiloPrice,
                        OuncePrice = p.OuncePrice,
                        IsItemCodeRelatedByGroup = p.IsItemCodeRelatedByGroup,
                        itemGmWages= p.itemGmWages,
                        GemsWages = p.GemsWages,
                        GemsWeight = p.GemsWeight

                    };
            return q.FirstOrDefault();
        }

        public string getLastItemCodeByGroup(int groupID)
        {
            // string LastItemCode = "";
            //LastItemCode = itemsRepo.SQLQuery<string>("exec GetMaxItemCode", (object)null).ToList().FirstOrDefault().ToString();
            // LastItemCode = itemsRepo.GetAll().OrderByDescending(x => x.ITEM_ID).FirstOrDefault().ITEM_CODE;

            var LastItemCode = itemsRepo.Filter(i => i.GROUP_ID == groupID).OrderByDescending(i => i.ITEM_ID).FirstOrDefault();
            //int IsInteger;
            //bool result = int.TryParse(LastItemCode, out IsInteger);
            //if (!result)
            //{
            //    LastItemCode = Convert.ToString(itemsRepo.GetAll().OrderByDescending(x => x.ITEM_ID).FirstOrDefault().ITEM_ID);
            //}

            var code = "0";
            if (LastItemCode != null)
            {
                if (LastItemCode.ITEM_CODE != null)
                {
                    code = LastItemCode.ITEM_CODE;
                }
                //else
                //{
                //    code = "1";
                //}
            }
            return code;
        }

        public List<ITEMSVM> SearchItems(string SearchCriteria)
        {
            int CharsCount = Regex.Matches(SearchCriteria, @"[a-zA-Z]").Count;
            bool IsArabicLetters = ContainsArabicLetters(SearchCriteria);
            if (CharsCount > 1 || IsArabicLetters)
            {
                SqlParameter SEARCH_CODEParam = new SqlParameter("@SEARCH_CODE", (object)SearchCriteria);
                SqlParameter SearchTypeParam = new SqlParameter("@SEARCH_TYPE", (object)2);
                var ItemsList = itemsRepo.SQLQuery<ITEMSVM>("exec SEARCH_ITEM  @SEARCH_CODE,@SEARCH_TYPE", SEARCH_CODEParam, SearchTypeParam).ToList<ITEMSVM>();
                return ItemsList;
            }
            else
            {
                SqlParameter SEARCH_CODEParam = new SqlParameter("@SEARCH_CODE", (object)SearchCriteria);
                SqlParameter SearchTypeParam = new SqlParameter("@SEARCH_TYPE", (object)4);
                var ItemsList = itemsRepo.SQLQuery<ITEMSVM>("exec SEARCH_ITEM_By_Code  @SEARCH_CODE,@SEARCH_TYPE WITH RECOMPILE", SEARCH_CODEParam, SearchTypeParam).ToList<ITEMSVM>();
                return ItemsList;
            }

        }



        public Task<ItemCurrentQuantityVM> GetItemCurrentQuantity(string ItemID, int StoreID)
        {

            return Task.Run<ItemCurrentQuantityVM>(() =>
            {
            SqlParameter ITEM_IDParam = new SqlParameter("@ITEM_ID", (object)ItemID);
            SqlParameter COM_STORE_IDParam = new SqlParameter("@COM_STORE_ID", (object)StoreID);
            var ItemsList = itemsRepo.SQLQuery<ItemCurrentQuantityVM>("exec GET_ITEM_CURRENT_QTY  @ITEM_ID,@COM_STORE_ID", ITEM_IDParam, COM_STORE_IDParam).ToList<ItemCurrentQuantityVM>();
            return ItemsList.FirstOrDefault();

            });
        }

      

        internal static bool ContainsArabicLetters(string text)
        {
            foreach (char character in text.ToCharArray())
            {
                if (character >= 0x600 && character <= 0x6ff)
                    return true;

                if (character >= 0x750 && character <= 0x77f)
                    return true;

                if (character >= 0xfb50 && character <= 0xfc3f)
                    return true;

                if (character >= 0xfe70 && character <= 0xfefc)
                    return true;
            }

            return false;
        }



        public string getLastItemCode()
        {
            var LastItemCode = itemsRepo.Filter(i => i.IsItemCodeRelatedByGroup == null || i.IsItemCodeRelatedByGroup == false).OrderByDescending(i => i.ITEM_ID).FirstOrDefault();
            var code = "";
            if (LastItemCode.ITEM_CODE != null)
            {
                code = LastItemCode.ITEM_CODE;
            }
            else
            {
                code = "1";
            }
            return code;
        }

        public List<ITEMS> getByItemStatus(int itemStatusID)
        {
                var q = itemsRepo.GetAll().Where(a => a.ItemStatus == itemStatusID).ToList();
                return q;
      
        }

       
    }
}
