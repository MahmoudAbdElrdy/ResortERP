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
    public class CostCentersService : ICostCentersService, IDisposable
    {
        IGenericRepository<COST_CENTERS> costCentersRepo;

        public CostCentersService(IGenericRepository<COST_CENTERS> costCentersRepo)
        {
            this.costCentersRepo = costCentersRepo;
        }

        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return costCentersRepo.CountAsync();
            });
        }

        public bool Delete(CostCentersVM entity)
        {
            COST_CENTERS cur = new COST_CENTERS()
            {
                COST_CENTER_AR_NAME = entity.COST_CENTER_AR_NAME,
                COST_CENTER_CODE = entity.COST_CENTER_CODE,
                COST_CENTER_EN_NAME = entity.COST_CENTER_EN_NAME,
                COST_CENTER_ID = entity.COST_CENTER_ID,
                COST_CENTER_MASTER_ID = entity.COST_CENTER_MASTER_ID,
                COST_CENTER_REMARKS = entity.COST_CENTER_REMARKS,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                Disable = entity.Disable,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn
            };
            costCentersRepo.Delete(cur, entity.COST_CENTER_ID);
            return true;
        }

        public Task<bool> DeleteAsync(CostCentersVM entity)
        {
            return Task.Run<bool>(() =>
            {
                COST_CENTERS cur = new COST_CENTERS()
                {
                    COST_CENTER_AR_NAME = entity.COST_CENTER_AR_NAME,
                    COST_CENTER_CODE = entity.COST_CENTER_CODE,
                    COST_CENTER_EN_NAME = entity.COST_CENTER_EN_NAME,
                    COST_CENTER_ID = entity.COST_CENTER_ID,
                    COST_CENTER_MASTER_ID = entity.COST_CENTER_MASTER_ID,
                    COST_CENTER_REMARKS = entity.COST_CENTER_REMARKS,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    Disable = entity.Disable,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn,
                    OpeningExpenses=entity.OpeningExpenses,
                    OpeningIncome=entity.OpeningIncome,
                    ExpectedExpenses=entity.ExpectedExpenses,
                    ExpectedIncome=entity.ExpectedIncome,
                };
                costCentersRepo.Delete(cur, entity.COST_CENTER_ID);
                return true;
            });
        }

        public void Dispose()
        {
            costCentersRepo.Dispose();
        }

        public List<CostCentersVM> GetAll()
        {
            var q = from entity in costCentersRepo.GetAll()
                    select new CostCentersVM
                    {
                        COST_CENTER_AR_NAME = entity.COST_CENTER_AR_NAME,
                        COST_CENTER_CODE = entity.COST_CENTER_CODE,
                        COST_CENTER_EN_NAME = entity.COST_CENTER_EN_NAME,
                        COST_CENTER_ID = entity.COST_CENTER_ID,
                        COST_CENTER_MASTER_ID = entity.COST_CENTER_MASTER_ID,
                        COST_CENTER_REMARKS = entity.COST_CENTER_REMARKS,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        Disable = entity.Disable,
                        UpdatedBy = entity.UpdatedBy,
                        updatedOn = entity.updatedOn
                    };
            return q.ToList();
        }
        public CostCentersVM getById(int COST_CENTER_ID)
        {
            var q = from entity in costCentersRepo.GetAll().Where(a=>a.COST_CENTER_ID== COST_CENTER_ID)
                    select new CostCentersVM
                    {
                        COST_CENTER_AR_NAME = entity.COST_CENTER_AR_NAME,
                        COST_CENTER_CODE = entity.COST_CENTER_CODE,
                        COST_CENTER_EN_NAME = entity.COST_CENTER_EN_NAME,
                        COST_CENTER_ID = entity.COST_CENTER_ID,
                        COST_CENTER_MASTER_ID = entity.COST_CENTER_MASTER_ID,
                        COST_CENTER_REMARKS = entity.COST_CENTER_REMARKS,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        Disable = entity.Disable,
                        UpdatedBy = entity.UpdatedBy,
                        updatedOn = entity.updatedOn
                    };
            return q.FirstOrDefault();
        }

        public Task<List<CostCentersVM>> GetSearchForCostCenter(string Search)
        {
            return Task.Run<List<CostCentersVM>>(() =>
            {
                var q = from entity in costCentersRepo.GetAll()
                        select new CostCentersVM
                        {
                            COST_CENTER_AR_NAME = entity.COST_CENTER_AR_NAME,
                            COST_CENTER_CODE = entity.COST_CENTER_CODE,
                            COST_CENTER_EN_NAME = entity.COST_CENTER_EN_NAME,
                            COST_CENTER_ID = entity.COST_CENTER_ID,
                            COST_CENTER_MASTER_ID = entity.COST_CENTER_MASTER_ID,
                            COST_CENTER_REMARKS = entity.COST_CENTER_REMARKS,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            Disable = entity.Disable,
                            UpdatedBy = entity.UpdatedBy,
                            updatedOn = entity.updatedOn,
                            OpeningExpenses = entity.OpeningExpenses,
                            OpeningIncome = entity.OpeningIncome,
                            ExpectedExpenses = entity.ExpectedExpenses,
                            ExpectedIncome = entity.ExpectedIncome,
                        };
                if (!string.IsNullOrEmpty(Search))
                {
                    return q.Where(x => (x.COST_CENTER_CODE.ToLower().Contains(Search.ToLower()) || x.COST_CENTER_AR_NAME.ToLower().Contains(Search.ToLower()) || x.COST_CENTER_EN_NAME.ToLower().Contains(Search.ToLower()))).ToList();
                }
                else
                {
                    return q.ToList();
                }
            });

        }

        public Task<List<CostCentersVM>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run(() =>
            {
                int rowCount;
                var q = from entity in costCentersRepo.GetPaged<int>(pageNum, pageSize, p => p.COST_CENTER_ID, false, out rowCount)
                        join c in costCentersRepo.GetPaged<int>(pageNum, pageSize, c => c.COST_CENTER_ID, false, out rowCount) on entity.COST_CENTER_MASTER_ID equals c.COST_CENTER_ID into g
                        from y in g.DefaultIfEmpty()

                        select new CostCentersVM
                        {
                            COST_CENTER_AR_NAME = entity.COST_CENTER_AR_NAME,
                            COST_CENTER_CODE = entity.COST_CENTER_CODE,
                            COST_CENTER_EN_NAME = entity.COST_CENTER_EN_NAME,
                            COST_CENTER_ID = entity.COST_CENTER_ID,
                            COST_CENTER_MASTER_ID = entity.COST_CENTER_MASTER_ID,
                            COST_CENTER_REMARKS = entity.COST_CENTER_REMARKS,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            Disable = entity.Disable,
                            UpdatedBy = entity.UpdatedBy,
                            updatedOn = entity.updatedOn,
                            MasterCostCenter = y.COST_CENTER_AR_NAME,
                            OpeningExpenses = entity.OpeningExpenses,
                            OpeningIncome = entity.OpeningIncome,
                            ExpectedExpenses = entity.ExpectedExpenses,
                            ExpectedIncome = entity.ExpectedIncome,
                        };
                return q.ToList();
            });
        }

        public bool Insert(CostCentersVM entity)
        {
            COST_CENTERS cur = new COST_CENTERS()
            {
                COST_CENTER_AR_NAME = entity.COST_CENTER_AR_NAME,
                COST_CENTER_CODE = entity.COST_CENTER_CODE,
                COST_CENTER_EN_NAME = entity.COST_CENTER_EN_NAME,
                COST_CENTER_ID = entity.COST_CENTER_ID,
                COST_CENTER_MASTER_ID = entity.COST_CENTER_MASTER_ID,
                COST_CENTER_REMARKS = entity.COST_CENTER_REMARKS,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                Disable = entity.Disable,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn
            };
            costCentersRepo.Add(cur);
            return true;
        }

        public Task<int> InsertAsync(CostCentersVM entity)
        {
            return Task.Run<int>(() =>
             {
                 COST_CENTERS cur = new COST_CENTERS()
                 {
                     COST_CENTER_AR_NAME = entity.COST_CENTER_AR_NAME,
                     COST_CENTER_CODE = entity.COST_CENTER_CODE,
                     COST_CENTER_EN_NAME = entity.COST_CENTER_EN_NAME,
                     COST_CENTER_ID = entity.COST_CENTER_ID,
                     COST_CENTER_MASTER_ID = entity.COST_CENTER_MASTER_ID,
                     COST_CENTER_REMARKS = entity.COST_CENTER_REMARKS,
                     AddedBy = entity.AddedBy,
                     AddedOn = entity.AddedOn,
                     Disable = entity.Disable,
                     UpdatedBy = entity.UpdatedBy,
                     updatedOn = entity.updatedOn,
                     OpeningExpenses = entity.OpeningExpenses,
                     OpeningIncome = entity.OpeningIncome,
                     ExpectedExpenses = entity.ExpectedExpenses,
                     ExpectedIncome = entity.ExpectedIncome,
                 };
                 costCentersRepo.Add(cur);
                 return cur.COST_CENTER_ID;
             });
        }

        public bool Update(CostCentersVM entity)
        {
            COST_CENTERS cur = new COST_CENTERS()
            {
                COST_CENTER_AR_NAME = entity.COST_CENTER_AR_NAME,
                COST_CENTER_CODE = entity.COST_CENTER_CODE,
                COST_CENTER_EN_NAME = entity.COST_CENTER_EN_NAME,
                COST_CENTER_ID = entity.COST_CENTER_ID,
                COST_CENTER_MASTER_ID = entity.COST_CENTER_MASTER_ID,
                COST_CENTER_REMARKS = entity.COST_CENTER_REMARKS,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                Disable = entity.Disable,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn
            };
            costCentersRepo.Update(cur, cur.COST_CENTER_ID);
            return true;
        }

        public Task<bool> UpdateAsync(CostCentersVM entity)
        {
            return Task.Run<bool>(() =>
            {
                COST_CENTERS cur = new COST_CENTERS()
                {
                    COST_CENTER_AR_NAME = entity.COST_CENTER_AR_NAME,
                    COST_CENTER_CODE = entity.COST_CENTER_CODE,
                    COST_CENTER_EN_NAME = entity.COST_CENTER_EN_NAME,
                    COST_CENTER_ID = entity.COST_CENTER_ID,
                    COST_CENTER_MASTER_ID = entity.COST_CENTER_MASTER_ID,
                    COST_CENTER_REMARKS = entity.COST_CENTER_REMARKS,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    Disable = entity.Disable,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn,
                    OpeningExpenses = entity.OpeningExpenses,
                    OpeningIncome = entity.OpeningIncome,
                    ExpectedExpenses = entity.ExpectedExpenses,
                    ExpectedIncome = entity.ExpectedIncome,
                };
                costCentersRepo.Update(cur, cur.COST_CENTER_ID);
                return true;
            });
        }


        public List<CostCentersVM> GetMainCostCenters()
        {

            var costCentersList = from c in costCentersRepo.GetAll()
                                  select new CostCentersVM
                                  {
                                      COST_CENTER_ID = c.COST_CENTER_ID,
                                      COST_CENTER_CODE = c.COST_CENTER_CODE,
                                      COST_CENTER_AR_NAME = c.COST_CENTER_AR_NAME,
                                      COST_CENTER_EN_NAME = c.COST_CENTER_EN_NAME,
                                      COST_CENTER_MASTER_ID = c.COST_CENTER_MASTER_ID,
                                      COST_CENTER_REMARKS = c.COST_CENTER_REMARKS,
                                      AddedBy = c.AddedBy,
                                      AddedOn = c.AddedOn,
                                      UpdatedBy = c.UpdatedBy,
                                      updatedOn = c.updatedOn,
                                      Disable = c.Disable,
                                      OpeningExpenses = c.OpeningExpenses,
                                      OpeningIncome = c.OpeningIncome,
                                      ExpectedExpenses = c.ExpectedExpenses,
                                      ExpectedIncome = c.ExpectedIncome,
                                  };
            return costCentersList.ToList();
        }

        public List<CostCentersGuidVM> getForTree()
        {
            var q = buildItemsTree();           
            return q;
        }
        public void GetTreeView(List<CostCentersGuidVM> list, CostCentersGuidVM current, ref List<CostCentersGuidVM> result)
        {
            // get child of current items

            var childs = list.Where(a => a.PARENT_ACC_ID == current.id);
            current.children = new List<CostCentersGuidVM>();
            current.children.AddRange(childs);
            foreach (var i in childs)
            {
                GetTreeView(list, i, ref result);
            }
        }

        public List<CostCentersGuidVM> Buildtree(List<CostCentersGuidVM> list)
        {
            List<CostCentersGuidVM> result = new List<CostCentersGuidVM>();
            // find top levels items
            var toplevels = list.Where(a => a.PARENT_ACC_ID == list.OrderBy(b => b.PARENT_ACC_ID).FirstOrDefault().PARENT_ACC_ID);
            result.AddRange(toplevels);
            foreach (var i in toplevels)
            {
                GetTreeView(list, i, ref result);
            }
            return result;
        }

        public List<CostCentersGuidVM> buildItemsTree()
        {
            List<CostCentersGuidVM> treelist = new List<CostCentersGuidVM>();
            var q = from a in costCentersRepo.GetAll()
                    select new CostCentersGuidVM
                    {
                        id = a.COST_CENTER_ID,
                        text = a.COST_CENTER_CODE + " - " + a.COST_CENTER_AR_NAME,
                        PARENT_ACC_ID = a.COST_CENTER_MASTER_ID,
                        data = new CostCentersDataGuidVM { data = 10 }
                    };


            if (q.ToList().Count > 0)
            {
                treelist = Buildtree(q.ToList());
            }
            return treelist;
        }
        public string GetLastCode()
        {
            var Code = costCentersRepo.GetAll().OrderByDescending(c => c.COST_CENTER_ID).FirstOrDefault();
            string lastCode = Code.COST_CENTER_CODE;
            return lastCode;
        }

    }
}
