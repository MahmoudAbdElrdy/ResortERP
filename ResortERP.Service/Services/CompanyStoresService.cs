using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResortERP.Core.VM;
using ResortERP.Repository;
using ResortERP.Core;
using ResortERP.Service.IServices;

namespace ResortERP.Service.Services
{
    public class CompanyStoresService : ICompanyStoresService, IDisposable
    {
        IGenericRepository<COMPANY_STORES> companyStoreRepo;
        IGenericRepository<EMPLOYEES> employeeRepo;
        IGenericRepository<COMPANY_BRANCHES> companyBranchesRepo;

        public CompanyStoresService(IGenericRepository<COMPANY_STORES> companyStoreRepo, IGenericRepository<EMPLOYEES> employeeRepo, IGenericRepository<COMPANY_BRANCHES> companyBranchesRepo)
        {
            this.companyStoreRepo = companyStoreRepo;
            this.employeeRepo = employeeRepo;
            this.companyBranchesRepo = companyBranchesRepo;

        }

        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return companyStoreRepo.CountAsync();
            });
        }

        public bool Delete(CompanyStoresVM entity)
        {
            COMPANY_STORES Ct = new COMPANY_STORES
            {
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                COM_BRN_ID = entity.COM_BRN_ID,
                COM_MASTER_STORE_ID = entity.COM_MASTER_STORE_ID,
                COM_PRINTER_NAME = entity.COM_PRINTER_NAME,
                COM_STORE_ADDR_REMARKS = entity.COM_STORE_ADDR_REMARKS,
                COM_STORE_AR_ABRV = entity.COM_STORE_AR_ABRV,
                COM_STORE_AR_NAME = entity.COM_STORE_AR_NAME,
                COM_STORE_CODE = entity.COM_STORE_CODE,
                COM_STORE_EN_ABRV = entity.COM_STORE_EN_ABRV,
                COM_STORE_EN_NAME = entity.COM_STORE_EN_NAME,
                COM_STORE_ID = entity.COM_STORE_ID,
                COM_STORE_REMARKS = entity.COM_STORE_REMARKS,
                Disable = entity.Disable,
                EMP_ID = entity.EMP_ID,
                GOV_ID = entity.GOV_ID,
                NATION_ID = entity.NATION_ID,
                TOWN_ID = entity.TOWN_ID,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn,
                VILLAGE_ID = entity.VILLAGE_ID
            };
            companyStoreRepo.Delete(Ct, entity.COM_STORE_ID);
            return true;
        }

        public Task<bool> DeleteAsync(CompanyStoresVM entity)
        {
            return Task.Run<bool>(() =>
            {
                COMPANY_STORES Ct = new COMPANY_STORES
                {
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    COM_BRN_ID = entity.COM_BRN_ID,
                    COM_MASTER_STORE_ID = entity.COM_MASTER_STORE_ID,
                    COM_PRINTER_NAME = entity.COM_PRINTER_NAME,
                    COM_STORE_ADDR_REMARKS = entity.COM_STORE_ADDR_REMARKS,
                    COM_STORE_AR_ABRV = entity.COM_STORE_AR_ABRV,
                    COM_STORE_AR_NAME = entity.COM_STORE_AR_NAME,
                    COM_STORE_CODE = entity.COM_STORE_CODE,
                    COM_STORE_EN_ABRV = entity.COM_STORE_EN_ABRV,
                    COM_STORE_EN_NAME = entity.COM_STORE_EN_NAME,
                    COM_STORE_ID = entity.COM_STORE_ID,
                    COM_STORE_REMARKS = entity.COM_STORE_REMARKS,
                    Disable = entity.Disable,
                    EMP_ID = entity.EMP_ID,
                    GOV_ID = entity.GOV_ID,
                    NATION_ID = entity.NATION_ID,
                    TOWN_ID = entity.TOWN_ID,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn,
                    VILLAGE_ID = entity.VILLAGE_ID
                };
                companyStoreRepo.Delete(Ct, entity.COM_STORE_ID);
                return true;
            });
        }

        public void Dispose()
        {
            companyStoreRepo.Dispose();
        }
        public List<COMPANY_STORES> getCompBranByStoresID(int compBranId)
        {
            var q = companyStoreRepo.GetAll().Where(a => a.COM_BRN_ID == compBranId).ToList();
            return q;
        }
        

        public Task<List<CompanyStoresVM>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run<List<CompanyStoresVM>>(() =>
            {
                int rowCount;
                var q = from p in companyStoreRepo.GetPaged<int>(pageNum, pageSize, p => p.COM_STORE_ID, false, out rowCount)
                        join pp in companyStoreRepo.GetPaged<int>(pageNum, pageSize, p => p.COM_STORE_ID, false, out rowCount) on p.COM_MASTER_STORE_ID equals pp.COM_STORE_ID into gg
                        from comStor in gg.DefaultIfEmpty()
                        join cb in companyBranchesRepo.GetPaged<int>(pageNum, pageSize, p => p.COM_BRN_ID, false, out rowCount) on p.COM_BRN_ID equals cb.COM_BRN_ID into cBran
                        from x in cBran.DefaultIfEmpty()
                        join ee in employeeRepo.GetPaged<int>(pageNum, pageSize, p => p.EMP_ID, false, out rowCount) on p.EMP_ID equals ee.EMP_ID into emp
                        from xx in emp.DefaultIfEmpty()
                        select new CompanyStoresVM
                        {
                            AddedBy = p.AddedBy,
                            AddedOn = p.AddedOn,
                            COM_BRN_ID = p.COM_BRN_ID,
                            COM_MASTER_STORE_ID = p.COM_MASTER_STORE_ID,
                            COM_PRINTER_NAME = p.COM_PRINTER_NAME,
                            COM_STORE_ADDR_REMARKS = p.COM_STORE_ADDR_REMARKS,
                            COM_STORE_AR_ABRV = p.COM_STORE_AR_ABRV,
                            COM_STORE_AR_NAME = p.COM_STORE_AR_NAME,
                            COM_STORE_CODE = p.COM_STORE_CODE,
                            COM_STORE_EN_ABRV = p.COM_STORE_EN_ABRV,
                            COM_STORE_EN_NAME = p.COM_STORE_EN_NAME,
                            COM_STORE_ID = p.COM_STORE_ID,
                            COM_STORE_REMARKS = p.COM_STORE_REMARKS,
                            Disable = p.Disable,
                            EMP_ID = p.EMP_ID,
                            GOV_ID = p.GOV_ID,
                            NATION_ID = p.NATION_ID,
                            TOWN_ID = p.TOWN_ID,
                            UpdatedBy = p.UpdatedBy,
                            updatedOn = p.updatedOn,
                            VILLAGE_ID = p.VILLAGE_ID,
                            CompanyBranchAR = x.COM_BRN_AR_NAME,
                            StoreManagerNameAr = xx.EMP_AR_NAME,
                            CompanyMainStore = comStor.COM_STORE_AR_NAME
                        };
                return q.ToList();
            });
        }

        public Task<List<CompanyStoresVM>> GetSearchForStore(string search)
        {
            return Task.Run<List<CompanyStoresVM>>(() =>
            {
                var q = from p in companyStoreRepo.GetAll()
                        join cb in companyBranchesRepo.GetAll() on p.COM_BRN_ID equals cb.COM_BRN_ID into cBran
                        from x in cBran.DefaultIfEmpty()
                        join ee in employeeRepo.GetAll() on p.EMP_ID equals ee.EMP_ID into emp
                        from xx in emp.DefaultIfEmpty()
                        select new CompanyStoresVM
                        {
                            AddedBy = p.AddedBy,
                            AddedOn = p.AddedOn,
                            COM_BRN_ID = p.COM_BRN_ID,
                            COM_MASTER_STORE_ID = p.COM_MASTER_STORE_ID,
                            COM_PRINTER_NAME = p.COM_PRINTER_NAME,
                            COM_STORE_ADDR_REMARKS = p.COM_STORE_ADDR_REMARKS,
                            COM_STORE_AR_ABRV = p.COM_STORE_AR_ABRV,
                            COM_STORE_AR_NAME = p.COM_STORE_AR_NAME,
                            COM_STORE_CODE = p.COM_STORE_CODE,
                            COM_STORE_EN_ABRV = p.COM_STORE_EN_ABRV,
                            COM_STORE_EN_NAME = p.COM_STORE_EN_NAME,
                            COM_STORE_ID = p.COM_STORE_ID,
                            COM_STORE_REMARKS = p.COM_STORE_REMARKS,
                            Disable = p.Disable,
                            EMP_ID = p.EMP_ID,
                            GOV_ID = p.GOV_ID,
                            NATION_ID = p.NATION_ID,
                            TOWN_ID = p.TOWN_ID,
                            UpdatedBy = p.UpdatedBy,
                            updatedOn = p.updatedOn,
                            VILLAGE_ID = p.VILLAGE_ID,
                            CompanyBranchAR = x.COM_BRN_AR_NAME,
                            StoreManagerNameAr = (xx != null) ? xx.EMP_AR_NAME : ""
                        };
                if (!string.IsNullOrEmpty(search))
                {
                    return q.Where(x => (x.COM_STORE_CODE.ToLower().Contains(search.ToLower()) || x.COM_STORE_AR_NAME.ToLower().Contains(search.ToLower()) || x.COM_STORE_EN_NAME.ToLower().Contains(search.ToLower()))).ToList();
                }
                else
                {
                    return q.ToList();
                }
            });
        }


        public List<CompanyStoresVM> GetAll()
        {
            var q = from p in companyStoreRepo.GetAll()
                    select new CompanyStoresVM
                    {
                        AddedBy = p.AddedBy,
                        AddedOn = p.AddedOn,
                        COM_BRN_ID = p.COM_BRN_ID,
                        COM_MASTER_STORE_ID = p.COM_MASTER_STORE_ID,
                        COM_PRINTER_NAME = p.COM_PRINTER_NAME,
                        COM_STORE_ADDR_REMARKS = p.COM_STORE_ADDR_REMARKS,
                        COM_STORE_AR_ABRV = p.COM_STORE_AR_ABRV,
                        COM_STORE_AR_NAME = p.COM_STORE_AR_NAME,
                        COM_STORE_CODE = p.COM_STORE_CODE,
                        COM_STORE_EN_ABRV = p.COM_STORE_EN_ABRV,
                        COM_STORE_EN_NAME = p.COM_STORE_EN_NAME,
                        COM_STORE_ID = p.COM_STORE_ID,
                        COM_STORE_REMARKS = p.COM_STORE_REMARKS,
                        Disable = p.Disable,
                        EMP_ID = p.EMP_ID,
                        GOV_ID = p.GOV_ID,
                        NATION_ID = p.NATION_ID,
                        TOWN_ID = p.TOWN_ID,
                        UpdatedBy = p.UpdatedBy,
                        updatedOn = p.updatedOn,
                        VILLAGE_ID = p.VILLAGE_ID
                    };
            return q.ToList();
        }

        public CompanyStoresVM GetById(int COM_STORE_ID)
        {
            var q = from p in companyStoreRepo.GetAll().Where(a=>a.COM_STORE_ID== COM_STORE_ID)
                    select new CompanyStoresVM
                    {
                        AddedBy = p.AddedBy,
                        AddedOn = p.AddedOn,
                        COM_BRN_ID = p.COM_BRN_ID,
                        COM_MASTER_STORE_ID = p.COM_MASTER_STORE_ID,
                        COM_PRINTER_NAME = p.COM_PRINTER_NAME,
                        COM_STORE_ADDR_REMARKS = p.COM_STORE_ADDR_REMARKS,
                        COM_STORE_AR_ABRV = p.COM_STORE_AR_ABRV,
                        COM_STORE_AR_NAME = p.COM_STORE_AR_NAME,
                        COM_STORE_CODE = p.COM_STORE_CODE,
                        COM_STORE_EN_ABRV = p.COM_STORE_EN_ABRV,
                        COM_STORE_EN_NAME = p.COM_STORE_EN_NAME,
                        COM_STORE_ID = p.COM_STORE_ID,
                        COM_STORE_REMARKS = p.COM_STORE_REMARKS,
                        Disable = p.Disable,
                        EMP_ID = p.EMP_ID,
                        GOV_ID = p.GOV_ID,
                        NATION_ID = p.NATION_ID,
                        TOWN_ID = p.TOWN_ID,
                        UpdatedBy = p.UpdatedBy,
                        updatedOn = p.updatedOn,
                        VILLAGE_ID = p.VILLAGE_ID
                    };
            return q.FirstOrDefault();
        }


        public bool Insert(CompanyStoresVM entity)
        {
            COMPANY_STORES Ct = new COMPANY_STORES
            {
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                COM_BRN_ID = entity.COM_BRN_ID,
                COM_MASTER_STORE_ID = entity.COM_MASTER_STORE_ID,
                COM_PRINTER_NAME = entity.COM_PRINTER_NAME,
                COM_STORE_ADDR_REMARKS = entity.COM_STORE_ADDR_REMARKS,
                COM_STORE_AR_ABRV = entity.COM_STORE_AR_ABRV,
                COM_STORE_AR_NAME = entity.COM_STORE_AR_NAME,
                COM_STORE_CODE = entity.COM_STORE_CODE,
                COM_STORE_EN_ABRV = entity.COM_STORE_EN_ABRV,
                COM_STORE_EN_NAME = entity.COM_STORE_EN_NAME,
                COM_STORE_ID = entity.COM_STORE_ID,
                COM_STORE_REMARKS = entity.COM_STORE_REMARKS,
                Disable = entity.Disable,
                EMP_ID = entity.EMP_ID,
                GOV_ID = entity.GOV_ID,
                NATION_ID = entity.NATION_ID,
                TOWN_ID = entity.TOWN_ID,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn,
                VILLAGE_ID = entity.VILLAGE_ID
            };
            companyStoreRepo.Add(Ct);
            return true;
        }

        public Task<short> InsertAsync(CompanyStoresVM entity)
        {
            return Task.Run<short>(() =>
            {
                COMPANY_STORES Ct = new COMPANY_STORES
                {
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    COM_BRN_ID = entity.COM_BRN_ID,
                    COM_MASTER_STORE_ID = entity.COM_MASTER_STORE_ID,
                    COM_PRINTER_NAME = entity.COM_PRINTER_NAME,
                    COM_STORE_ADDR_REMARKS = entity.COM_STORE_ADDR_REMARKS,
                    COM_STORE_AR_ABRV = entity.COM_STORE_AR_ABRV,
                    COM_STORE_AR_NAME = entity.COM_STORE_AR_NAME,
                    COM_STORE_CODE = entity.COM_STORE_CODE,
                    COM_STORE_EN_ABRV = entity.COM_STORE_EN_ABRV,
                    COM_STORE_EN_NAME = entity.COM_STORE_EN_NAME,
                    COM_STORE_ID = entity.COM_STORE_ID,
                    COM_STORE_REMARKS = entity.COM_STORE_REMARKS,
                    Disable = entity.Disable,
                    EMP_ID = entity.EMP_ID,
                    GOV_ID = entity.GOV_ID,
                    NATION_ID = entity.NATION_ID,
                    TOWN_ID = entity.TOWN_ID,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn,
                    VILLAGE_ID = entity.VILLAGE_ID
                };
                companyStoreRepo.Add(Ct);
                return Ct.COM_STORE_ID;
            });
        }

        public bool Update(CompanyStoresVM entity)
        {
            COMPANY_STORES Ct = new COMPANY_STORES
            {
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                COM_BRN_ID = entity.COM_BRN_ID,
                COM_MASTER_STORE_ID = entity.COM_MASTER_STORE_ID,
                COM_PRINTER_NAME = entity.COM_PRINTER_NAME,
                COM_STORE_ADDR_REMARKS = entity.COM_STORE_ADDR_REMARKS,
                COM_STORE_AR_ABRV = entity.COM_STORE_AR_ABRV,
                COM_STORE_AR_NAME = entity.COM_STORE_AR_NAME,
                COM_STORE_CODE = entity.COM_STORE_CODE,
                COM_STORE_EN_ABRV = entity.COM_STORE_EN_ABRV,
                COM_STORE_EN_NAME = entity.COM_STORE_EN_NAME,
                COM_STORE_ID = entity.COM_STORE_ID,
                COM_STORE_REMARKS = entity.COM_STORE_REMARKS,
                Disable = entity.Disable,
                EMP_ID = entity.EMP_ID,
                GOV_ID = entity.GOV_ID,
                NATION_ID = entity.NATION_ID,
                TOWN_ID = entity.TOWN_ID,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn,
                VILLAGE_ID = entity.VILLAGE_ID
            };
            companyStoreRepo.Update(Ct, Ct.COM_STORE_ID);
            return true;
        }

        public Task<short> UpdateAsync(CompanyStoresVM entity)
        {
            return Task.Run<short>(() =>
            {
                COMPANY_STORES Ct = new COMPANY_STORES
                {
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    COM_BRN_ID = entity.COM_BRN_ID,
                    COM_MASTER_STORE_ID = entity.COM_MASTER_STORE_ID,
                    COM_PRINTER_NAME = entity.COM_PRINTER_NAME,
                    COM_STORE_ADDR_REMARKS = entity.COM_STORE_ADDR_REMARKS,
                    COM_STORE_AR_ABRV = entity.COM_STORE_AR_ABRV,
                    COM_STORE_AR_NAME = entity.COM_STORE_AR_NAME,
                    COM_STORE_CODE = entity.COM_STORE_CODE,
                    COM_STORE_EN_ABRV = entity.COM_STORE_EN_ABRV,
                    COM_STORE_EN_NAME = entity.COM_STORE_EN_NAME,
                    COM_STORE_ID = entity.COM_STORE_ID,
                    COM_STORE_REMARKS = entity.COM_STORE_REMARKS,
                    Disable = entity.Disable,
                    EMP_ID = entity.EMP_ID,
                    GOV_ID = entity.GOV_ID,
                    NATION_ID = entity.NATION_ID,
                    TOWN_ID = entity.TOWN_ID,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn,
                    VILLAGE_ID = entity.VILLAGE_ID
                };
                companyStoreRepo.Update(Ct, Ct.COM_STORE_ID);
                return Ct.COM_STORE_ID;
            });
        }

        public Task<List<CompanyStoresVM>> GetSearch(string search)
        {
            return Task.Run<List<CompanyStoresVM>>(() =>
            {
                if (search != "" && search != null)
                    return companyStoreRepo.Filter(X => (X.COM_STORE_CODE == search || X.COM_STORE_AR_NAME == search || X.COM_STORE_EN_NAME == search)).Select(entity => new CompanyStoresVM()
                    {
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        COM_BRN_ID = entity.COM_BRN_ID,
                        COM_MASTER_STORE_ID = entity.COM_MASTER_STORE_ID,
                        COM_PRINTER_NAME = entity.COM_PRINTER_NAME,
                        COM_STORE_ADDR_REMARKS = entity.COM_STORE_ADDR_REMARKS,
                        COM_STORE_AR_ABRV = entity.COM_STORE_AR_ABRV,
                        COM_STORE_AR_NAME = entity.COM_STORE_AR_NAME,
                        COM_STORE_CODE = entity.COM_STORE_CODE,
                        COM_STORE_EN_ABRV = entity.COM_STORE_EN_ABRV,
                        COM_STORE_EN_NAME = entity.COM_STORE_EN_NAME,
                        COM_STORE_ID = entity.COM_STORE_ID,
                        COM_STORE_REMARKS = entity.COM_STORE_REMARKS,
                        Disable = entity.Disable,
                        EMP_ID = entity.EMP_ID,
                        GOV_ID = entity.GOV_ID,
                        NATION_ID = entity.NATION_ID,
                        TOWN_ID = entity.TOWN_ID,
                        UpdatedBy = entity.UpdatedBy,
                        updatedOn = entity.updatedOn,
                        VILLAGE_ID = entity.VILLAGE_ID
                    }).ToList();
                else
                {
                    return companyStoreRepo.GetAll().Select(entity => new CompanyStoresVM()
                    {
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        COM_BRN_ID = entity.COM_BRN_ID,
                        COM_MASTER_STORE_ID = entity.COM_MASTER_STORE_ID,
                        COM_PRINTER_NAME = entity.COM_PRINTER_NAME,
                        COM_STORE_ADDR_REMARKS = entity.COM_STORE_ADDR_REMARKS,
                        COM_STORE_AR_ABRV = entity.COM_STORE_AR_ABRV,
                        COM_STORE_AR_NAME = entity.COM_STORE_AR_NAME,
                        COM_STORE_CODE = entity.COM_STORE_CODE,
                        COM_STORE_EN_ABRV = entity.COM_STORE_EN_ABRV,
                        COM_STORE_EN_NAME = entity.COM_STORE_EN_NAME,
                        COM_STORE_ID = entity.COM_STORE_ID,
                        COM_STORE_REMARKS = entity.COM_STORE_REMARKS,
                        Disable = entity.Disable,
                        EMP_ID = entity.EMP_ID,
                        GOV_ID = entity.GOV_ID,
                        NATION_ID = entity.NATION_ID,
                        TOWN_ID = entity.TOWN_ID,
                        UpdatedBy = entity.UpdatedBy,
                        updatedOn = entity.updatedOn,
                        VILLAGE_ID = entity.VILLAGE_ID
                    }).ToList(); ;
                }
            });
        }
        public List<StoresGuidVM> getForTree()
        {
            var q = buildItemsTree();
            //var q_Child = companyStoreRepo.GetAll();
            //List<StoresGuidVM> result = new List<StoresGuidVM>();
            //foreach (var item in q)
            //{
            //    StoresGuidVM itemObj = item;

            //    foreach (var itemChild in q_Child)
            //    {
            //        if (item.id == itemChild.COM_MASTER_STORE_ID)
            //        {
            //            itemObj.children.Add(new StoresGuidVM()
            //            {
            //                id = itemChild.COM_STORE_ID,
            //                text = itemChild.COM_STORE_CODE + " - " + itemChild.COM_STORE_AR_NAME
            //            });
            //            //result.
            //        }

            //    }
            //    result.Add(itemObj);
            //}
            return q;
        }
        public void GetTreeView(List<StoresGuidVM> list, StoresGuidVM current, ref List<StoresGuidVM> result)
        {
            // get child of current items

            var childs = list.Where(a => a.PARENT_ACC_ID == current.id);
            current.children = new List<StoresGuidVM>();
            current.children.AddRange(childs);
            foreach (var i in childs)
            {
                GetTreeView(list, i, ref result);
            }
        }

        public List<StoresGuidVM> Buildtree(List<StoresGuidVM> list)
        {
            List<StoresGuidVM> result = new List<StoresGuidVM>();
            // find top levels items
            var toplevels = list.Where(a => a.PARENT_ACC_ID == list.OrderBy(b => b.PARENT_ACC_ID).FirstOrDefault().PARENT_ACC_ID);
            result.AddRange(toplevels);
            foreach (var i in toplevels)
            {
                GetTreeView(list, i, ref result);
            }
            return result;
        }

        public List<StoresGuidVM> buildItemsTree()
        {
            List<StoresGuidVM> treelist = new List<StoresGuidVM>();
            var q = from a in companyStoreRepo.GetAll()
                    select new StoresGuidVM
                    {
                        id = a.COM_STORE_ID,
                        text = a.COM_STORE_CODE + " - " + a.COM_STORE_AR_NAME,
                        PARENT_ACC_ID = a.COM_MASTER_STORE_ID,
                        data = new StoresDataGuidVM { data = 10 }
                    };


            if (q.ToList().Count > 0)
            {
                treelist = Buildtree(q.ToList());
            }
            return treelist;
        }
        //public List<StoresGuidVM> getForTree()
        //{
        //    var q = buildStoresTree();
        //    //var q_Child = companyStoreRepo.GetAll();
        //    //List<StoresGuidVM> result = new List<StoresGuidVM>();
        //    //foreach (var item in q)
        //    //{
        //    //    StoresGuidVM itemObj = item;

        //    //    foreach (var itemChild in q_Child)
        //    //    {
        //    //        if (item.id == itemChild.COM_MASTER_STORE_ID)
        //    //        {
        //    //            itemObj.children.Add(new StoresGuidVM()
        //    //            {
        //    //                id = itemChild.COM_STORE_ID,
        //    //                text = itemChild.COM_STORE_CODE + " - " + itemChild.COM_STORE_AR_NAME
        //    //            });
        //    //            //result.
        //    //        }

        //    //    }
        //    //    result.Add(itemObj);
        //    //}
        //    return q;
        //}

        //public List<StoresGuidVM> buildStoresTree()
        //{
        //    List<StoresGuidVM> treelist = new List<StoresGuidVM>();
        //    var q = from a in companyStoreRepo.GetAll()
        //            select new StoresGuidVM
        //            {
        //                id = a.COM_STORE_ID,
        //                text = a.COM_STORE_CODE + " - " + a.COM_STORE_AR_NAME,
        //                PARENT_ACC_ID = a.COM_MASTER_STORE_ID,
        //                data = new StoresDataGuidVM { data = 10 }
        //            };


        //    if (q.ToList().Count > 0)
        //    {
        //        treelist = Buildtree(q.ToList());
        //    }
        //    return treelist;
        //}

        //public List<StoresGuidVM> Buildtree(List<StoresGuidVM> list)
        //{
        //    List<StoresGuidVM> result = new List<StoresGuidVM>();
        //    // find top levels items
        //    var toplevels = list.Where(a => a.PARENT_ACC_ID == list.OrderBy(b => b.PARENT_ACC_ID).FirstOrDefault().PARENT_ACC_ID);
        //    result.AddRange(toplevels);
        //    foreach (var i in toplevels)
        //    {
        //        GetTreeView(list, i, ref result);
        //    }
        //    return result;
        //}

        //public void GetTreeView(List<StoresGuidVM> list, StoresGuidVM current, ref List<StoresGuidVM> result)
        //{
        //    // get child of current items

        //    var childs = list.Where(a => a.PARENT_ACC_ID == current.id);
        //    current.children = new List<StoresGuidVM>();
        //    current.children.AddRange(childs);
        //    foreach (var i in childs)
        //    {
        //        GetTreeView(list, i, ref result);
        //    }
        //}
        public string getLastCode()
        {
            var lastCode = companyStoreRepo.GetAll().OrderByDescending(c => c.COM_STORE_ID).FirstOrDefault();
            return lastCode.COM_STORE_CODE;
        }

        public List<COMPANY_STORES> getByNationID(int nationId)
        {
            var q = companyStoreRepo.GetAll().Where(a => a.NATION_ID == nationId).ToList();
            return q;
        }
        public List<COMPANY_STORES> getByGOVID(int govId)
        {
            var q = companyStoreRepo.GetAll().Where(a => a.GOV_ID == govId).ToList();
            return q;
        }
        public List<COMPANY_STORES> getByTownID(int townId)
        {
            var q = companyStoreRepo.GetAll().Where(a => a.TOWN_ID == townId).ToList();
            return q;
        }
        public List<COMPANY_STORES> getByVilID(long villageId)
        {
            var q = companyStoreRepo.GetAll().Where(a => a.VILLAGE_ID == villageId).ToList();
            return q;
        }

    }
}
