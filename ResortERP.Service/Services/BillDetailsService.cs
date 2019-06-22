using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResortERP.Core.VM;
using ResortERP.Repository;
using ResortERP.Core;
using ResortERP.Service.IServices;
using System.Data.SqlClient;

namespace ResortERP.Service.Services
{
    public class BillDetailsService : IBillDetailsService, IDisposable
    {
        IGenericRepository<BILL_DETAILS> billDetailsMRepo;
        IGenericRepository<BILL_MASTER> billMasterMRepo;
        IGenericRepository<ITEMS> itemsRepo;
        IGenericRepository<UNITS> unitsRepo;
        IGenericRepository<ITEMS_UNITS> itemUnitsRepo;


        public BillDetailsService(IGenericRepository<BILL_DETAILS> billDetailsMRepo, IGenericRepository<BILL_MASTER> billMasterMRepo, IGenericRepository<ITEMS> itemsRepo,
        IGenericRepository<ITEMS_UNITS> itemUnitsRepo, IGenericRepository<UNITS> unitsRepo)
        {
            this.billDetailsMRepo = billDetailsMRepo;
            this.billMasterMRepo = billMasterMRepo;
            this.itemsRepo = itemsRepo;
            this.unitsRepo = unitsRepo;
            this.itemUnitsRepo = itemUnitsRepo;
        }

        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return billDetailsMRepo.CountAsync();
            });
        }

        public bool Delete(BILL_DETAILSVM entity)
        {
            BILL_DETAILS bd = new BILL_DETAILS
            {
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                BILL_DETAILS_REMARKS = entity.BILL_DETAILS_REMARKS,
                BILL_ID = entity.BILL_ID,
                BILL_INVENTORY_VALUE = entity.BILL_INVENTORY_VALUE,
                Disable = entity.Disable,
                DISC_RATE = entity.DISC_RATE,
                DISC_VALUE = entity.DISC_VALUE,
                EMP_ID = entity.EMP_ID,
                EXPIRED_DATE = entity.EXPIRED_DATE,
                EXTRA_RATE = entity.EXTRA_RATE,
                EXTRA_VALUE = entity.EXTRA_VALUE,
                GIFT = entity.GIFT,
                GRID_ROW_NUMBER = entity.GRID_ROW_NUMBER,
                ITEM_ID = entity.ITEM_ID,
                PRODUCTION_DATE = entity.PRODUCTION_DATE,
                QTY = entity.QTY,
                SERVICE_ID = entity.SERVICE_ID,
                UNIT_ID = entity.UNIT_ID,
                UNIT_PRICE = entity.UNIT_PRICE,
                CaliberID = entity.CaliberID,
                UpdatedBy = entity.UpdatedBy,
                UpdatedOn = entity.UpdatedOn,

                ItemWeight = entity.ItemWeight,
                ManufacturingWages = entity.ManufacturingWages,
                itemGmWages = entity.itemGmWages,
                CostCenterID = entity.CostCenterID,
                VATRate = entity.VATRate,
                SubjectToVAT = entity.SubjectToVAT,
                VatValue = entity.VatValue,

                WagesDiscRate = entity.WagesDiscRate,
                WagesDiscValue = entity.WagesDiscValue,

                ActualItemWeight = entity.ActualItemWeight,

                TaxTotal = entity.TaxTotal,
                IsExemptOfTax = entity.IsExemptOfTax,
                ClearnessRate = entity.ClearnessRate
            };
            object[] keys = new object[2] { entity.BILL_ID, entity.GRID_ROW_NUMBER };
            billDetailsMRepo.DeleteComposite(bd, keys);
            return true;
        }

        public Task<bool> DeleteAsync(BILL_DETAILSVM entity)
        {
            return Task.Run<bool>(() =>
            {
                BILL_DETAILS bd = new BILL_DETAILS
                {
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    BILL_DETAILS_REMARKS = entity.BILL_DETAILS_REMARKS,
                    BILL_ID = entity.BILL_ID,
                    BILL_INVENTORY_VALUE = entity.BILL_INVENTORY_VALUE,
                    Disable = entity.Disable,
                    DISC_RATE = entity.DISC_RATE,
                    DISC_VALUE = entity.DISC_VALUE,
                    EMP_ID = entity.EMP_ID,
                    EXPIRED_DATE = entity.EXPIRED_DATE,
                    EXTRA_RATE = entity.EXTRA_RATE,
                    EXTRA_VALUE = entity.EXTRA_VALUE,
                    GIFT = entity.GIFT,
                    GRID_ROW_NUMBER = entity.GRID_ROW_NUMBER,
                    ITEM_ID = entity.ITEM_ID,
                    PRODUCTION_DATE = entity.PRODUCTION_DATE,
                    QTY = entity.QTY,
                    SERVICE_ID = entity.SERVICE_ID,
                    UNIT_ID = entity.UNIT_ID,
                    UNIT_PRICE = entity.UNIT_PRICE,
                    CaliberID = entity.CaliberID,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedOn = entity.UpdatedOn,
                    ItemWeight = entity.ItemWeight,
                    ManufacturingWages = entity.ManufacturingWages,
                    itemGmWages = entity.itemGmWages,
                    CostCenterID = entity.CostCenterID,

                    VATRate = entity.VATRate,
                    SubjectToVAT = entity.SubjectToVAT,
                    VatValue = entity.VatValue,

                    WagesDiscRate = entity.WagesDiscRate,
                    WagesDiscValue = entity.WagesDiscValue,

                    ActualItemWeight = entity.ActualItemWeight,
                    TaxTotal = entity.TaxTotal,
                    IsExemptOfTax = entity.IsExemptOfTax,

                    ClearnessRate = entity.ClearnessRate
                };
                object[] keys = new object[2] { entity.BILL_ID, entity.GRID_ROW_NUMBER };
                billDetailsMRepo.DeleteComposite(bd, keys);
                return true;
            });
        }

        public void Dispose()
        {
            billDetailsMRepo.Dispose();
        }

        public Task<List<BILL_DETAILSVM>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run<List<BILL_DETAILSVM>>(() =>
            {
                int rowCount;
                var q = from entity in billDetailsMRepo.GetPaged<long>(pageNum, pageSize, p => p.BILL_ID, false, out rowCount)
                        select new BILL_DETAILSVM
                        {
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            BILL_DETAILS_REMARKS = entity.BILL_DETAILS_REMARKS,
                            BILL_ID = entity.BILL_ID,
                            BILL_INVENTORY_VALUE = entity.BILL_INVENTORY_VALUE,
                            Disable = entity.Disable,
                            DISC_RATE = entity.DISC_RATE,
                            DISC_VALUE = entity.DISC_VALUE,
                            EMP_ID = entity.EMP_ID,
                            EXPIRED_DATE = entity.EXPIRED_DATE,
                            EXTRA_RATE = entity.EXTRA_RATE,
                            EXTRA_VALUE = entity.EXTRA_VALUE,
                            GIFT = entity.GIFT,
                            GRID_ROW_NUMBER = entity.GRID_ROW_NUMBER,
                            ITEM_ID = entity.ITEM_ID,
                            PRODUCTION_DATE = entity.PRODUCTION_DATE,
                            QTY = entity.QTY,
                            SERVICE_ID = entity.SERVICE_ID,
                            UNIT_ID = entity.UNIT_ID,
                            UNIT_PRICE = entity.UNIT_PRICE,
                            CaliberID = entity.CaliberID,
                            UpdatedBy = entity.UpdatedBy,
                            UpdatedOn = entity.UpdatedOn,
                            ItemWeight = entity.ItemWeight,
                            ManufacturingWages = entity.ManufacturingWages,
                            itemGmWages = entity.itemGmWages,
                            CostCenterID = entity.CostCenterID,

                            VATRate = entity.VATRate,
                            SubjectToVAT = entity.SubjectToVAT,
                            VatValue = entity.VatValue,

                            WagesDiscRate = entity.WagesDiscRate,
                            WagesDiscValue = entity.WagesDiscValue,
                            ActualItemWeight = entity.ActualItemWeight,
                            TaxTotal = entity.TaxTotal,
                            IsExemptOfTax = entity.IsExemptOfTax,

                            ClearnessRate = entity.ClearnessRate
                        };
                return q.ToList();
            });
        }

        public bool Insert(BILL_DETAILSVM entity)
        {
            BILL_DETAILS bd = new BILL_DETAILS
            {
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                BILL_DETAILS_REMARKS = entity.BILL_DETAILS_REMARKS,
                BILL_ID = entity.BILL_ID,
                BILL_INVENTORY_VALUE = entity.BILL_INVENTORY_VALUE,
                Disable = entity.Disable,
                DISC_RATE = entity.DISC_RATE,
                DISC_VALUE = entity.DISC_VALUE,
                EMP_ID = entity.EMP_ID,
                EXPIRED_DATE = entity.EXPIRED_DATE,
                EXTRA_RATE = entity.EXTRA_RATE,
                EXTRA_VALUE = entity.EXTRA_VALUE,
                GIFT = entity.GIFT,
                GRID_ROW_NUMBER = entity.GRID_ROW_NUMBER,
                ITEM_ID = entity.ITEM_ID,
                PRODUCTION_DATE = entity.PRODUCTION_DATE,
                QTY = entity.QTY,
                SERVICE_ID = entity.SERVICE_ID,
                UNIT_ID = entity.UNIT_ID,
                UNIT_PRICE = entity.UNIT_PRICE,
                CaliberID = entity.CaliberID,
                UpdatedBy = entity.UpdatedBy,
                UpdatedOn = entity.UpdatedOn,
                ItemWeight = entity.ItemWeight,
                ManufacturingWages = entity.ManufacturingWages,
                itemGmWages = entity.itemGmWages,
                CostCenterID = entity.CostCenterID,

                VATRate = entity.VATRate,
                SubjectToVAT = entity.SubjectToVAT,
                VatValue = entity.VatValue,

                WagesDiscRate = entity.WagesDiscRate,
                WagesDiscValue = entity.WagesDiscValue,

                ActualItemWeight = entity.ActualItemWeight,
                TaxTotal = entity.TaxTotal,
                IsExemptOfTax = entity.IsExemptOfTax,

                ClearnessRate = entity.ClearnessRate
            };
            billDetailsMRepo.Add(bd);
            return true;
        }

        public Task<bool> InsertAsync(BILL_DETAILSVM entity)
        {
            return Task.Run<bool>(() =>
            {
                BILL_DETAILS bd = new BILL_DETAILS
                {
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    BILL_DETAILS_REMARKS = entity.BILL_DETAILS_REMARKS,
                    BILL_ID = entity.BILL_ID,
                    BILL_INVENTORY_VALUE = entity.BILL_INVENTORY_VALUE,
                    Disable = entity.Disable,
                    DISC_RATE = entity.DISC_RATE,
                    DISC_VALUE = entity.DISC_VALUE,
                    EMP_ID = entity.EMP_ID,
                    EXPIRED_DATE = entity.EXPIRED_DATE,
                    EXTRA_RATE = entity.EXTRA_RATE,
                    EXTRA_VALUE = entity.EXTRA_VALUE,
                    GIFT = entity.GIFT,
                    GRID_ROW_NUMBER = entity.GRID_ROW_NUMBER,
                    ITEM_ID = entity.ITEM_ID,
                    PRODUCTION_DATE = entity.PRODUCTION_DATE,
                    QTY = entity.QTY,
                    SERVICE_ID = entity.SERVICE_ID,
                    UNIT_ID = entity.UNIT_ID,
                    UNIT_PRICE = entity.UNIT_PRICE,
                    CaliberID = entity.CaliberID,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedOn = entity.UpdatedOn,
                    ItemWeight = entity.ItemWeight,
                    ManufacturingWages = entity.ManufacturingWages,
                    itemGmWages = entity.itemGmWages,
                    CostCenterID = entity.CostCenterID,
                    VatValue = entity.VatValue,
                    VATRate = entity.VATRate,
                    SubjectToVAT = entity.SubjectToVAT,

                    WagesDiscRate = entity.WagesDiscRate,
                    WagesDiscValue = entity.WagesDiscValue,

                    ActualItemWeight = entity.ActualItemWeight,
                    TaxTotal = entity.TaxTotal,
                    IsExemptOfTax = entity.IsExemptOfTax,

                    ClearnessRate = entity.ClearnessRate
                };
                billDetailsMRepo.Add(bd);
                return true;
            });
        }

        public bool Update(BILL_DETAILSVM entity)
        {
            BILL_DETAILS bd = new BILL_DETAILS
            {
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                BILL_DETAILS_REMARKS = entity.BILL_DETAILS_REMARKS,
                BILL_ID = entity.BILL_ID,
                BILL_INVENTORY_VALUE = entity.BILL_INVENTORY_VALUE,
                Disable = entity.Disable,
                DISC_RATE = entity.DISC_RATE,
                DISC_VALUE = entity.DISC_VALUE,
                EMP_ID = entity.EMP_ID,
                EXPIRED_DATE = entity.EXPIRED_DATE,
                EXTRA_RATE = entity.EXTRA_RATE,
                EXTRA_VALUE = entity.EXTRA_VALUE,
                GIFT = entity.GIFT,
                GRID_ROW_NUMBER = entity.GRID_ROW_NUMBER,
                ITEM_ID = entity.ITEM_ID,
                PRODUCTION_DATE = entity.PRODUCTION_DATE,
                QTY = entity.QTY,
                SERVICE_ID = entity.SERVICE_ID,
                UNIT_ID = entity.UNIT_ID,
                UNIT_PRICE = entity.UNIT_PRICE,
                CaliberID = entity.CaliberID,
                UpdatedBy = entity.UpdatedBy,
                UpdatedOn = entity.UpdatedOn,
                ItemWeight = entity.ItemWeight,
                ManufacturingWages = entity.ManufacturingWages,
                itemGmWages = entity.itemGmWages,
                CostCenterID = entity.CostCenterID,

                VATRate = entity.VATRate,
                SubjectToVAT = entity.SubjectToVAT,
                VatValue = entity.VatValue,

                WagesDiscRate = entity.WagesDiscRate,
                WagesDiscValue = entity.WagesDiscValue,

                ActualItemWeight = entity.ActualItemWeight,
                TaxTotal = entity.TaxTotal,
                IsExemptOfTax = entity.IsExemptOfTax,

                ClearnessRate = entity.ClearnessRate
            };
            object[] keys = new object[2] { bd.BILL_ID, bd.GRID_ROW_NUMBER };
            billDetailsMRepo.UpdateComposite(bd, keys);
            return true;
        }

        public Task<bool> UpdateAsync(BILL_DETAILSVM entity)
        {
            return Task.Run<bool>(() =>
            {
                BILL_DETAILS bd = new BILL_DETAILS
                {
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    BILL_DETAILS_REMARKS = entity.BILL_DETAILS_REMARKS,
                    BILL_ID = entity.BILL_ID,
                    BILL_INVENTORY_VALUE = entity.BILL_INVENTORY_VALUE,
                    Disable = entity.Disable,
                    DISC_RATE = entity.DISC_RATE,
                    DISC_VALUE = entity.DISC_VALUE,
                    EMP_ID = entity.EMP_ID,
                    EXPIRED_DATE = entity.EXPIRED_DATE,
                    EXTRA_RATE = entity.EXTRA_RATE,
                    EXTRA_VALUE = entity.EXTRA_VALUE,
                    GIFT = entity.GIFT,
                    GRID_ROW_NUMBER = entity.GRID_ROW_NUMBER,
                    ITEM_ID = entity.ITEM_ID,
                    PRODUCTION_DATE = entity.PRODUCTION_DATE,
                    QTY = entity.QTY,
                    SERVICE_ID = entity.SERVICE_ID,
                    UNIT_ID = entity.UNIT_ID,
                    UNIT_PRICE = entity.UNIT_PRICE,
                    CaliberID = entity.CaliberID,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedOn = entity.UpdatedOn,
                    ItemWeight = entity.ItemWeight,
                    ManufacturingWages = entity.ManufacturingWages,
                    itemGmWages = entity.itemGmWages,
                    CostCenterID = entity.CostCenterID,

                    VATRate = entity.VATRate,
                    SubjectToVAT = entity.SubjectToVAT,
                    VatValue = entity.VatValue,

                    WagesDiscRate = entity.WagesDiscRate,
                    WagesDiscValue = entity.WagesDiscValue,

                    ActualItemWeight = entity.ActualItemWeight,
                    TaxTotal = entity.TaxTotal,
                    IsExemptOfTax = entity.IsExemptOfTax,

                    ClearnessRate = entity.ClearnessRate
                };
                object[] keys = new object[2] { bd.BILL_ID, bd.GRID_ROW_NUMBER };
                billDetailsMRepo.UpdateComposite(bd, keys);
                return true;
            });
        }

        public bool InsertBillDetails(List<BILL_DETAILSVM> BillDetails)
        {
            long Bill_ID = Convert.ToInt32(BillDetails[0].BILL_ID);
            if (Bill_ID < 1)
            {
                Bill_ID = billMasterMRepo.GetAll().OrderByDescending(m => m.BILL_ID).FirstOrDefault().BILL_ID;
            }

            //Delete All 
            {
                List<BILL_DETAILS> ShouldBeDeleted = billDetailsMRepo.Filter(x => x.BILL_ID == Bill_ID).ToList<BILL_DETAILS>();

                foreach (BILL_DETAILS item in ShouldBeDeleted)
                {
                    object[] keys = new object[2] { Bill_ID, item.GRID_ROW_NUMBER };
                    billDetailsMRepo.DeleteComposite(item, keys);
                    //new { Bill_ID = item.BILL_ID, GRID_ROW_NUMBER = item.GRID_ROW_NUMBER }
                }
            }

            //Insert All Again
            foreach (BILL_DETAILSVM item in BillDetails)
            {
                if (item.ITEM_ID > 0)
                {
                    BILL_DETAILS Bill_DetailObj = new BILL_DETAILS();
                    Bill_DetailObj.AddedBy = item.AddedBy;
                    Bill_DetailObj.AddedOn = item.AddedOn;
                    Bill_DetailObj.BILL_DETAILS_REMARKS = item.BILL_DETAILS_REMARKS;
                    Bill_DetailObj.BILL_ID = Bill_ID;
                    Bill_DetailObj.BILL_INVENTORY_VALUE = item.BILL_INVENTORY_VALUE;
                    Bill_DetailObj.Disable = item.Disable;
                    Bill_DetailObj.DISC_RATE = item.DISC_RATE;
                    Bill_DetailObj.DISC_VALUE = item.DISC_VALUE;

                    Bill_DetailObj.EXPIRED_DATE = item.EXPIRED_DATE;
                    Bill_DetailObj.EXTRA_RATE = item.EXTRA_VALUE;
                    Bill_DetailObj.EXTRA_VALUE = item.EXTRA_VALUE;
                    Bill_DetailObj.GIFT = item.GIFT;
                    Bill_DetailObj.GRID_ROW_NUMBER = item.GRID_ROW_NUMBER;
                    Bill_DetailObj.ITEM_ID = item.ITEM_ID;
                    Bill_DetailObj.PRODUCTION_DATE = item.PRODUCTION_DATE;
                    Bill_DetailObj.QTY = item.QTY;
                    Bill_DetailObj.SERVICE_ID = item.SERVICE_ID;
                    Bill_DetailObj.UNIT_ID = Convert.ToInt16(GetUnitIDByItemID(item.ITEM_ID));
                    Bill_DetailObj.UNIT_PRICE = item.UNIT_PRICE;
                    Bill_DetailObj.CaliberID = item.CaliberID;
                    Bill_DetailObj.UpdatedBy = item.UpdatedBy;
                    Bill_DetailObj.UpdatedOn = item.UpdatedOn;
                    Bill_DetailObj.EMP_ID = item.EMP_ID;
                    Bill_DetailObj.ItemWeight = item.ItemWeight;
                    Bill_DetailObj.ManufacturingWages = item.ManufacturingWages;
                    Bill_DetailObj.itemGmWages = item.itemGmWages;
                    Bill_DetailObj.CostCenterID = item.CostCenterID;

                    Bill_DetailObj.VATRate = item.VATRate;
                    Bill_DetailObj.SubjectToVAT = item.SubjectToVAT;
                    Bill_DetailObj.VatValue = item.VatValue;

                    Bill_DetailObj.WagesDiscRate = item.WagesDiscRate;
                    Bill_DetailObj.WagesDiscValue = item.WagesDiscValue;

                    Bill_DetailObj.ActualItemWeight = item.ActualItemWeight;
                    Bill_DetailObj.TaxTotal = item.TaxTotal;
                    Bill_DetailObj.IsExemptOfTax = item.IsExemptOfTax;

                    Bill_DetailObj.ClearnessRate = item.ClearnessRate;

                    billDetailsMRepo.Add(Bill_DetailObj);
                }
            }

            return true;
        }


        private int? GetUnitIDByItemID(int ItemID)
        {

            //ITEMS item = itemsRepo.Filter(x => x.ITEM_CODE == ItemCode.ToString()).FirstOrDefault();
            //int? ItemID = item.ITEM_ID;
            ITEMS_UNITS itemUnit = itemUnitsRepo.Filter(x => x.ITEM_ID == ItemID && x.IS_DEFAULT == true).FirstOrDefault();
            int? UnitID;
            if (itemUnit == null)
            {
                UnitID = 0;
            }
            else
            {
                UnitID = itemUnit.UNIT_ID;
            }

            return UnitID;
        }


        public List<BILL_DETAILSVM> GetAllBillDetailsByBill_ID(long Bill_ID)
        {
            SqlParameter BILL_IDParam = new SqlParameter("@BILL_ID", (object)Bill_ID);
            var BillDetailsList = billDetailsMRepo.SQLQuery<BILL_DETAILSVM>("exec Get_BILL_MASTER_ByBillId  @BILL_ID", BILL_IDParam).ToList<BILL_DETAILSVM>();
            return BillDetailsList;

            //var q = from p in billDetailsMRepo.Filter(x => x.BILL_ID == Bill_ID)
            //        join i in itemsRepo.GetAll() on p.ITEM_ID equals int.Parse(i.ITEM_CODE) into x
            //        from m in x.DefaultIfEmpty()
            //        join Y in unitsRepo.GetAll() on p.UNIT_ID equals Y.UNIT_ID into z
            //        from YY in z.DefaultIfEmpty()
            //            //join y in itemUnitsRepo.GetAll() on m.ITEM_ID equals y.ITEM_ID into l
            //            //from k in l.DefaultIfEmpty()
            //        select new BILL_DETAILSVM
            //        {
            //            AddedBy = p.AddedBy,
            //            AddedOn = p.AddedOn,
            //            BILL_DETAILS_REMARKS = p.BILL_DETAILS_REMARKS,
            //            BILL_ID = p.BILL_ID,
            //            BILL_INVENTORY_VALUE = p.BILL_INVENTORY_VALUE,
            //            Disable = p.Disable,
            //            DISC_RATE = p.DISC_RATE,
            //            DISC_VALUE = p.DISC_VALUE,
            //            EMP_ID = p.EMP_ID,
            //            EXPIRED_DATE = p.EXPIRED_DATE,
            //            EXTRA_RATE = p.EXTRA_RATE,
            //            EXTRA_VALUE = p.EXTRA_VALUE,
            //            GIFT = p.GIFT,
            //            GRID_ROW_NUMBER = p.GRID_ROW_NUMBER,
            //            ITEM_ID = p.ITEM_ID,
            //            PRODUCTION_DATE = p.PRODUCTION_DATE,
            //            QTY = p.QTY,
            //            SERVICE_ID = p.SERVICE_ID,
            //            UNIT_ID = p.UNIT_ID,
            //            UNIT_PRICE = p.UNIT_PRICE,
            //            UpdatedBy = p.UpdatedBy,
            //            UpdatedOn = p.UpdatedOn,
            //            ITEM_AR_NAME = m == null ? "" : m.ITEM_AR_NAME,
            //            UnitNameAr = YY == null ? "" : YY.UNIT_AR_NAME == null ? "" : YY.UNIT_AR_NAME
            //            //UnitNameAr = unitsRepo.GetAll().Where(y => y.UNIT_ID == p.UNIT_ID).FirstOrDefault() == null ? "" : unitsRepo.GetAll().Where(y => y.UNIT_ID == p.UNIT_ID).FirstOrDefault().UNIT_AR_NAME
            //        };
            //return q.ToList();
        }
    }
}
