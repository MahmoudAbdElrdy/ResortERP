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
    public class ItemsGroupsService : IItemsGroupsService, IDisposable
    {
        IGenericRepository<ITEMS_GROUPS> itemsGroupsRepo;
        IGenericRepository<ITEMS> itemsRepo;
        public ItemsGroupsService(IGenericRepository<ITEMS_GROUPS> itemsGroupsRepo, IGenericRepository<ITEMS> itemsRepo)
        {
            this.itemsGroupsRepo = itemsGroupsRepo;
            this.itemsRepo = itemsRepo;
        }

        public void Dispose()
        {
            itemsGroupsRepo.Dispose();
        }


        public bool Insert(ItemsGroupsVM entity)
        {
            ITEMS_GROUPS ig = new ITEMS_GROUPS
            {
                GROUP_APPEAREONTHESALEPOINTBILL = entity.AppearOnSalePoint,
                GROUP_AR_NAME = entity.GroupARName,
                GROUP_CODE = entity.GroupCode,
                GROUP_EN_NAME = entity.GroupENName,
                GROUP_ID = entity.GroupID,
                GROUP_MASTER_ID = entity.GroupMasterID,
                CaliberID = entity.CaliberID,
                DOESTHEQUANTITYISAPARTOFBARCODE = entity.DOESTHEQUANTITYISAPARTOFBARCODE,
                QUANTITYLENGTHATTHEBARCODE = entity.QUANTITYLENGTHATTHEBARCODE,
                QUANTITYSTARTATTHEBARCODE = entity.QUANTITYSTARTATTHEBARCODE,
                QUANTITYPARTSTARTATTHEBARCODE = entity.QUANTITYPARTSTARTATTHEBARCODE,
                QUANTITYPARTLENGTHATTHEBARCODE = entity.QUANTITYPARTLENGTHATTHEBARCODE,
                COST_CALCULATION_TYPE = entity.COST_CALCULATION_TYPE,
                GROUP_REMARKS = entity.GroupRemarks,
                ItemStatusID= entity.ItemStatusID,
                GoldAccID = entity.GoldAccID
            };
            itemsGroupsRepo.Add(ig);
            return true;
        }

        public Task<int> InsertAsync(ItemsGroupsVM entity)
        {
            return Task.Run<int>(() =>
            {
                ITEMS_GROUPS ig = new ITEMS_GROUPS
                {
                    GROUP_APPEAREONTHESALEPOINTBILL = entity.AppearOnSalePoint,
                    GROUP_AR_NAME = entity.GroupARName,
                    GROUP_CODE = entity.GroupCode,
                    GROUP_EN_NAME = entity.GroupENName,
                    GROUP_ID = entity.GroupID,
                    GROUP_MASTER_ID = entity.GroupMasterID,
                    CaliberID = entity.CaliberID,
                    DOESTHEQUANTITYISAPARTOFBARCODE = entity.DOESTHEQUANTITYISAPARTOFBARCODE,
                    QUANTITYLENGTHATTHEBARCODE = entity.QUANTITYLENGTHATTHEBARCODE,
                    QUANTITYSTARTATTHEBARCODE = entity.QUANTITYSTARTATTHEBARCODE,
                    QUANTITYPARTSTARTATTHEBARCODE = entity.QUANTITYPARTSTARTATTHEBARCODE,
                    QUANTITYPARTLENGTHATTHEBARCODE = entity.QUANTITYPARTLENGTHATTHEBARCODE,
                    COST_CALCULATION_TYPE = entity.COST_CALCULATION_TYPE,
                    GROUP_REMARKS = entity.GroupRemarks,
                    ItemStatusID = entity.ItemStatusID,
                    GoldAccID = entity.GoldAccID
                };
                itemsGroupsRepo.Add(ig);
                return ig.GROUP_ID;
            }
              );

        }

        public bool Update(ItemsGroupsVM entity)
        {

            ITEMS_GROUPS ig = new ITEMS_GROUPS
            {
                GROUP_APPEAREONTHESALEPOINTBILL = entity.AppearOnSalePoint,
                GROUP_AR_NAME = entity.GroupARName,
                GROUP_CODE = entity.GroupCode,
                GROUP_EN_NAME = entity.GroupENName,
                GROUP_ID = entity.GroupID,
                GROUP_MASTER_ID = entity.GroupMasterID,
                CaliberID = entity.CaliberID,
                DOESTHEQUANTITYISAPARTOFBARCODE = entity.DOESTHEQUANTITYISAPARTOFBARCODE,
                QUANTITYLENGTHATTHEBARCODE = entity.QUANTITYLENGTHATTHEBARCODE,
                QUANTITYSTARTATTHEBARCODE = entity.QUANTITYSTARTATTHEBARCODE,
                QUANTITYPARTSTARTATTHEBARCODE = entity.QUANTITYPARTSTARTATTHEBARCODE,
                QUANTITYPARTLENGTHATTHEBARCODE = entity.QUANTITYPARTLENGTHATTHEBARCODE,
                COST_CALCULATION_TYPE = entity.COST_CALCULATION_TYPE,
                GROUP_REMARKS = entity.GroupRemarks,
                ItemStatusID = entity.ItemStatusID,
                GoldAccID = entity.GoldAccID
            };
            itemsGroupsRepo.Update(ig, ig.GROUP_ID);
            return true;


        }
        public Task<bool> UpdateAsync(ItemsGroupsVM entity)
        {
            return Task.Run<bool>(() =>
            {
                ITEMS_GROUPS ig = new ITEMS_GROUPS
                {
                    GROUP_APPEAREONTHESALEPOINTBILL = entity.AppearOnSalePoint,
                    GROUP_AR_NAME = entity.GroupARName,
                    GROUP_CODE = entity.GroupCode,
                    GROUP_EN_NAME = entity.GroupENName,
                    GROUP_ID = entity.GroupID,
                    GROUP_MASTER_ID = entity.GroupMasterID,
                    CaliberID = entity.CaliberID,
                    DOESTHEQUANTITYISAPARTOFBARCODE = entity.DOESTHEQUANTITYISAPARTOFBARCODE,
                    QUANTITYLENGTHATTHEBARCODE = entity.QUANTITYLENGTHATTHEBARCODE,
                    QUANTITYSTARTATTHEBARCODE = entity.QUANTITYSTARTATTHEBARCODE,
                    QUANTITYPARTSTARTATTHEBARCODE = entity.QUANTITYPARTSTARTATTHEBARCODE,
                    QUANTITYPARTLENGTHATTHEBARCODE = entity.QUANTITYPARTLENGTHATTHEBARCODE,
                    COST_CALCULATION_TYPE = entity.COST_CALCULATION_TYPE,
                    GROUP_REMARKS = entity.GroupRemarks,
                    ItemStatusID = entity.ItemStatusID,
                    GoldAccID = entity.GoldAccID
                };
                itemsGroupsRepo.Update(ig, ig.GROUP_ID);
                return true;
            }
              );

        }

        public bool Delete(ItemsGroupsVM entity)
        {
            ITEMS_GROUPS ig = new ITEMS_GROUPS
            {
                GROUP_APPEAREONTHESALEPOINTBILL = entity.AppearOnSalePoint,
                GROUP_AR_NAME = entity.GroupARName,
                GROUP_CODE = entity.GroupCode,
                GROUP_EN_NAME = entity.GroupENName,
                GROUP_ID = entity.GroupID,
                GROUP_MASTER_ID = entity.GroupMasterID,
                CaliberID = entity.CaliberID,
                DOESTHEQUANTITYISAPARTOFBARCODE = entity.DOESTHEQUANTITYISAPARTOFBARCODE,
                QUANTITYLENGTHATTHEBARCODE = entity.QUANTITYLENGTHATTHEBARCODE,
                QUANTITYSTARTATTHEBARCODE = entity.QUANTITYSTARTATTHEBARCODE,
                QUANTITYPARTSTARTATTHEBARCODE = entity.QUANTITYPARTSTARTATTHEBARCODE,
                QUANTITYPARTLENGTHATTHEBARCODE = entity.QUANTITYPARTLENGTHATTHEBARCODE,
                COST_CALCULATION_TYPE = entity.COST_CALCULATION_TYPE,
                GROUP_REMARKS = entity.GroupRemarks,
                ItemStatusID = entity.ItemStatusID,
                GoldAccID = entity.GoldAccID
            };
            itemsGroupsRepo.Delete(ig, entity.GroupID);
            return true;


        }
        public Task<bool> DeleteAsync(ItemsGroupsVM entity)
        {
            return Task.Run<bool>(() =>
            {
                ITEMS_GROUPS ig = new ITEMS_GROUPS
                {
                    GROUP_APPEAREONTHESALEPOINTBILL = entity.AppearOnSalePoint,
                    GROUP_AR_NAME = entity.GroupARName,
                    GROUP_CODE = entity.GroupCode,
                    GROUP_EN_NAME = entity.GroupENName,
                    GROUP_ID = entity.GroupID,
                    GROUP_MASTER_ID = entity.GroupMasterID,
                    CaliberID = entity.CaliberID,
                    DOESTHEQUANTITYISAPARTOFBARCODE = entity.DOESTHEQUANTITYISAPARTOFBARCODE,
                    QUANTITYLENGTHATTHEBARCODE = entity.QUANTITYLENGTHATTHEBARCODE,
                    QUANTITYSTARTATTHEBARCODE = entity.QUANTITYSTARTATTHEBARCODE,
                    QUANTITYPARTSTARTATTHEBARCODE = entity.QUANTITYPARTSTARTATTHEBARCODE,
                    QUANTITYPARTLENGTHATTHEBARCODE = entity.QUANTITYPARTLENGTHATTHEBARCODE,
                    COST_CALCULATION_TYPE = entity.COST_CALCULATION_TYPE,
                    GROUP_REMARKS = entity.GroupRemarks,
                    ItemStatusID = entity.ItemStatusID,
                    GoldAccID = entity.GoldAccID
                };
                itemsGroupsRepo.Delete(ig, entity.GroupID);
                return true;
            });
        }

        //public List<ItemsGuidVM> getForTree()
        //{
        //    var q = itemsGroupsRepo.GetAll();
        //    var q_Child = itemsRepo.GetAll();
        //    List<ItemsGuidVM> result = new List<ItemsGuidVM>();
        //    foreach (var item in q)
        //    {
        //        ItemsGuidVM itemObj = new ItemsGuidVM()
        //        {
        //            id = item.GROUP_ID,
        //            text = item.GROUP_CODE + " - " + item.GROUP_AR_NAME,

        //        };

        //        foreach (var itemChild in q_Child)
        //        {
        //            if (item.GROUP_ID == itemChild.GROUP_ID)
        //            {
        //                itemObj.children.Add(new childItem()
        //                {
        //                    id = itemChild.ITEM_ID,
        //                    text = itemChild.ITEM_CODE + " - " + itemChild.ITEM_AR_NAME
        //                });
        //                //result.
        //            }

        //        }
        //        result.Add(itemObj);
        //    }
        //    return result;
        //}

        public List<ItemsGuidVM> getForTree()
        {
            var q = buildItemsTree();
            var q_Child = itemsRepo.GetAll();
            List<ItemsGuidVM> result = new List<ItemsGuidVM>();
            foreach (var item in q)
            {
                ItemsGuidVM itemObj = item;

                foreach (var itemChild in q_Child)
                {
                    if (item.id == itemChild.GROUP_ID)
                    {
                        itemObj.children.Add(new ItemsGuidVM()
                        {
                            id = itemChild.ITEM_ID,
                            text = itemChild.ITEM_CODE + " - " + itemChild.ITEM_AR_NAME
                        });
                        //result.
                    }

                }
                result.Add(itemObj);
            }
            return result;
        }

        public Task<List<ItemsGroupsVM>> GetAllAsync(int pageNum, int pageSize)
        {

            return Task.Run<List<ItemsGroupsVM>>(() =>
            {

                int rowCount;
                var q = from p in itemsGroupsRepo.GetPaged<int>(pageNum, pageSize, p => p.GROUP_ID, false, out rowCount)
                        join e1 in itemsGroupsRepo.GetPaged<int>(pageNum, pageSize, p => p.GROUP_ID, false, out rowCount) on p.GROUP_MASTER_ID equals e1.GROUP_ID into g
                        from y in g.DefaultIfEmpty()
                        select new ItemsGroupsVM
                        {
                            AppearOnSalePoint = p.GROUP_APPEAREONTHESALEPOINTBILL,
                            GroupARName = p.GROUP_AR_NAME,
                            GroupCode = p.GROUP_CODE,
                            GroupENName = p.GROUP_EN_NAME,
                            GroupID = p.GROUP_ID,
                            GroupMasterID = p.GROUP_MASTER_ID,
                            CaliberID = p.CaliberID,
                            DOESTHEQUANTITYISAPARTOFBARCODE = p.DOESTHEQUANTITYISAPARTOFBARCODE,
                            QUANTITYLENGTHATTHEBARCODE = p.QUANTITYLENGTHATTHEBARCODE,
                            QUANTITYSTARTATTHEBARCODE = p.QUANTITYSTARTATTHEBARCODE,
                            QUANTITYPARTSTARTATTHEBARCODE = p.QUANTITYPARTSTARTATTHEBARCODE,
                            QUANTITYPARTLENGTHATTHEBARCODE = p.QUANTITYPARTLENGTHATTHEBARCODE,
                            COST_CALCULATION_TYPE = p.COST_CALCULATION_TYPE,
                            GroupMasterName = y.GROUP_AR_NAME,
                            GroupRemarks = p.GROUP_REMARKS,
                            ItemStatusID = p.ItemStatusID,
                            GoldAccID = p.GoldAccID
                        };



                return q.ToList();




                // return new List<ItemsGroupsVM>();
            });


        }

        public List<ItemsGroupsVM> GetAllItemGroups()
        {
            var q = from p in itemsGroupsRepo.GetAll()
                    select new ItemsGroupsVM
                    {
                        AppearOnSalePoint = p.GROUP_APPEAREONTHESALEPOINTBILL,
                        GroupARName = p.GROUP_AR_NAME,
                        GroupCode = p.GROUP_CODE,
                        GroupENName = p.GROUP_EN_NAME,
                        GroupID = p.GROUP_ID,
                        GroupMasterID = p.GROUP_MASTER_ID,
                        CaliberID = p.CaliberID,
                        DOESTHEQUANTITYISAPARTOFBARCODE = p.DOESTHEQUANTITYISAPARTOFBARCODE,
                        QUANTITYLENGTHATTHEBARCODE = p.QUANTITYLENGTHATTHEBARCODE,
                        QUANTITYSTARTATTHEBARCODE = p.QUANTITYSTARTATTHEBARCODE,
                        QUANTITYPARTSTARTATTHEBARCODE = p.QUANTITYPARTSTARTATTHEBARCODE,
                        QUANTITYPARTLENGTHATTHEBARCODE = p.QUANTITYPARTLENGTHATTHEBARCODE,
                        COST_CALCULATION_TYPE = p.COST_CALCULATION_TYPE,
                        GroupRemarks = p.GROUP_REMARKS,
                        ItemStatusID = p.ItemStatusID,
                        GoldAccID = p.GoldAccID
                    };
            return q.ToList();
        }

        public List<ItemsGroupsVM> GetAllItemGroupsPos()
        {
            var q = from p in itemsGroupsRepo.GetAll().Where(x => x.GROUP_APPEAREONTHESALEPOINTBILL == true)
                    select new ItemsGroupsVM
                    {
                        AppearOnSalePoint = p.GROUP_APPEAREONTHESALEPOINTBILL,
                        GroupARName = p.GROUP_AR_NAME,
                        GroupCode = p.GROUP_CODE,
                        GroupENName = p.GROUP_EN_NAME,
                        GroupID = p.GROUP_ID,
                        GroupMasterID = p.GROUP_MASTER_ID,
                        CaliberID = p.CaliberID,
                        DOESTHEQUANTITYISAPARTOFBARCODE = p.DOESTHEQUANTITYISAPARTOFBARCODE,
                        QUANTITYLENGTHATTHEBARCODE = p.QUANTITYLENGTHATTHEBARCODE,
                        QUANTITYSTARTATTHEBARCODE = p.QUANTITYSTARTATTHEBARCODE,
                        QUANTITYPARTSTARTATTHEBARCODE = p.QUANTITYPARTSTARTATTHEBARCODE,
                        QUANTITYPARTLENGTHATTHEBARCODE = p.QUANTITYPARTLENGTHATTHEBARCODE,
                        COST_CALCULATION_TYPE = p.COST_CALCULATION_TYPE,
                        GroupRemarks = p.GROUP_REMARKS,
                        ItemStatusID = p.ItemStatusID,
                        GoldAccID = p.GoldAccID
                    };
            return q.ToList();
        }

        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return itemsGroupsRepo.CountAsync();
            });
        }

        //public List<ITEMS_GROUPS> GetById(int id)
        //{
        //    return itemsGroupsRepo.GetAll().Where(a => a.GROUP_ID == id).ToList();
        //}

        public Task<ItemsGroupsVM> getItemGroupByID(int GroupID)
        {
            return Task.Run<ItemsGroupsVM>(() =>
            {
                var q = from g in itemsGroupsRepo.Filter(g => g.GROUP_ID == GroupID)
                        select new ItemsGroupsVM
                        {
                            AppearOnSalePoint = g.GROUP_APPEAREONTHESALEPOINTBILL,
                            GroupARName = g.GROUP_AR_NAME,
                            GroupCode = g.GROUP_CODE,
                            GroupENName = g.GROUP_EN_NAME,
                            GroupID = g.GROUP_ID,
                            GroupMasterID = g.GROUP_MASTER_ID,
                            CaliberID = g.CaliberID,
                            DOESTHEQUANTITYISAPARTOFBARCODE = g.DOESTHEQUANTITYISAPARTOFBARCODE,
                            QUANTITYLENGTHATTHEBARCODE = g.QUANTITYLENGTHATTHEBARCODE,
                            QUANTITYSTARTATTHEBARCODE = g.QUANTITYSTARTATTHEBARCODE,
                            QUANTITYPARTSTARTATTHEBARCODE = g.QUANTITYPARTSTARTATTHEBARCODE,
                            QUANTITYPARTLENGTHATTHEBARCODE = g.QUANTITYPARTLENGTHATTHEBARCODE,
                            COST_CALCULATION_TYPE = g.COST_CALCULATION_TYPE,
                            ItemStatusID = g.ItemStatusID,
                            GoldAccID = g.GoldAccID

                        };
                return q.FirstOrDefault();
            });
        }


        public string GetLastCode()
        {
            var lastCode = itemsGroupsRepo.GetAll().OrderByDescending(i => i.GROUP_ID).FirstOrDefault();
            return lastCode.GROUP_CODE;
        }
        public ItemsGroupsVM GetByID(int itemID)
        {
                var q = from p in itemsGroupsRepo.GetAll()
                        select new ItemsGroupsVM
                        {
                            AppearOnSalePoint = p.GROUP_APPEAREONTHESALEPOINTBILL,
                            GroupARName = p.GROUP_AR_NAME,
                            GroupCode = p.GROUP_CODE,
                            GroupENName = p.GROUP_EN_NAME,
                            GroupID = p.GROUP_ID,
                            GroupMasterID = p.GROUP_MASTER_ID,
                            CaliberID = p.CaliberID,
                            DOESTHEQUANTITYISAPARTOFBARCODE = p.DOESTHEQUANTITYISAPARTOFBARCODE,
                            QUANTITYLENGTHATTHEBARCODE = p.QUANTITYLENGTHATTHEBARCODE,
                            QUANTITYSTARTATTHEBARCODE = p.QUANTITYSTARTATTHEBARCODE,
                            QUANTITYPARTSTARTATTHEBARCODE = p.QUANTITYPARTSTARTATTHEBARCODE,
                            QUANTITYPARTLENGTHATTHEBARCODE = p.QUANTITYPARTLENGTHATTHEBARCODE,
                            COST_CALCULATION_TYPE = p.COST_CALCULATION_TYPE,
                            GroupRemarks = p.GROUP_REMARKS,
                            ItemStatusID = p.ItemStatusID,
                            GoldAccID = p.GoldAccID
                        };
            var res= q.Where(x => x.GroupID == itemID).FirstOrDefault();
            return res;

        }


        public void GetTreeView(List<ItemsGuidVM> list, ItemsGuidVM current, ref List<ItemsGuidVM> result)
        {
            // get child of current items

            var childs = list.Where(a => a.PARENT_ACC_ID == current.id);
            current.children = new List<ItemsGuidVM>();
            current.children.AddRange(childs);
            foreach (var i in childs)
            {
                GetTreeView(list, i, ref result);
            }
        }

        public List<ItemsGuidVM> Buildtree(List<ItemsGuidVM> list)
        {
            List<ItemsGuidVM> result = new List<ItemsGuidVM>();
            // find top levels items
            var toplevels = list.Where(a => a.PARENT_ACC_ID == list.OrderBy(b => b.PARENT_ACC_ID).FirstOrDefault().PARENT_ACC_ID);
            result.AddRange(toplevels);
            foreach (var i in toplevels)
            {
                GetTreeView(list, i, ref result);
            }
            return result;
        }

        public List<ItemsGuidVM> buildItemsTree()
        {
            List<ItemsGuidVM> treelist = new List<ItemsGuidVM>();
            var q = from a in itemsGroupsRepo.GetAll()
                    select new ItemsGuidVM
                    {
                        id = a.GROUP_ID,
                        text = a.GROUP_CODE + " - " + a.GROUP_AR_NAME,
                        PARENT_ACC_ID = a.GROUP_MASTER_ID,
                        data = new ItemsDataGuidVM { data = 10}
                    };


            if (q.ToList().Count > 0)
            {
                treelist = Buildtree(q.ToList());
            }
            return treelist;
        }


    }
}
