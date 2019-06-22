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
    public class CurrencyCategoriesService : ICurrencyCategoriesService, IDisposable
    {
        IGenericRepository<CurrencyCategories> catRepo;

        public CurrencyCategoriesService(IGenericRepository<CurrencyCategories> catRepo)
        {
            this.catRepo = catRepo;
        }

        public Task<List<CurrencyCategoriesVM>> getCatAsync(int pageNum, int pageSize)
        {
            return Task.Run(() =>
            {
                int rowCount;
                var catList = from c in catRepo.GetPaged(pageNum, pageSize, c => c.CategoryID, false, out rowCount)

                              select new CurrencyCategoriesVM
                              {
                                  CategoryID = c.CategoryID,
                                  CategoryArName = c.CategoryArName,
                                  CategoryEnName = c.CategoryEnName,
                                  CurrencyID = c.CurrencyID,
                                  AddedBy = c.AddedBy,
                                  AddenOn = c.AddenOn,
                                  UpdatedBy = c.UpdatedBy,
                                  UpdatedOn = c.UpdatedOn,
                                  Disable = c.Disable,
                                  CategoryTrans = c.CategoryTrans
                              };
                return catList.ToList();
            });
        }

        public Task<bool> insertCatSync(CurrencyCategoriesVM entity)
        {
            return Task.Run(() =>
            {
                CurrencyCategories cat = new CurrencyCategories();
                {
                    //  cat.CategoryID = int.Parse(entity.CategoryCode);
                    cat.CategoryCode = entity.CategoryCode;
                    cat.CategoryArName = entity.CategoryArName;
                    cat.CategoryEnName = entity.CategoryEnName;
                    cat.CurrencyID = entity.CurrencyID;
                    cat.AddedBy = entity.AddedBy;
                    cat.AddenOn = entity.AddenOn;
                    cat.UpdatedBy = entity.UpdatedBy;
                    cat.UpdatedOn = entity.UpdatedOn;
                    cat.Disable = entity.Disable;
                    cat.CategoryTrans = entity.CategoryTrans;
                };
                catRepo.Add(cat);
                return true;
            });

        }

        public Task<bool> updateCatSync(CurrencyCategoriesVM entity)
        {
            return Task.Run(() =>
            {
                CurrencyCategories cat = new CurrencyCategories();
                {
                    cat.CategoryID = entity.CategoryID;
                    cat.CategoryArName = entity.CategoryArName;
                    cat.CategoryEnName = entity.CategoryEnName;
                    cat.CurrencyID = entity.CurrencyID;
                    cat.AddedBy = entity.AddedBy;
                    cat.AddenOn = entity.AddenOn;
                    cat.UpdatedBy = entity.UpdatedBy;
                    cat.UpdatedOn = entity.UpdatedOn;
                    cat.Disable = entity.Disable;
                    cat.CategoryTrans = entity.CategoryTrans;

                };

                catRepo.Update(cat, cat.CategoryID);
                return true;
            });

        }

        public Task<bool> deleteCatSync(CurrencyCategoriesVM entity)
        {
            return Task.Run(() =>
            {
                CurrencyCategories cat = new CurrencyCategories();
                {
                    cat.CategoryID = entity.CategoryID;
                    cat.CategoryArName = entity.CategoryArName;
                    cat.CategoryEnName = entity.CategoryEnName;
                    cat.CurrencyID = entity.CurrencyID;
                    cat.AddedBy = entity.AddedBy;
                    cat.AddenOn = entity.AddenOn;
                    cat.UpdatedBy = entity.UpdatedBy;
                    cat.UpdatedOn = entity.UpdatedOn;
                    cat.Disable = entity.Disable;
                    cat.CategoryTrans = entity.CategoryTrans;
                };

                catRepo.Delete(cat, cat.CategoryID);
                return true;
            });

        }

        public Task<int> countCatAsync()
        {
            return Task.Run(() =>
            {
                return catRepo.CountAsync();
            });
        }

        public void Dispose()
        {
            catRepo.Dispose();
        }



        public List<CurrencyCategoriesVM> getbyCurrId(int currId)
        {
            var catList = from c in catRepo.GetAll().Where(c => c.CurrencyID == currId)

                          select new CurrencyCategoriesVM
                          {
                              CategoryID = c.CategoryID,
                              CategoryArName = c.CategoryArName,
                              CategoryEnName = c.CategoryEnName,
                              CurrencyID = c.CurrencyID,
                              AddedBy = c.AddedBy,
                              AddenOn = c.AddenOn,
                              UpdatedBy = c.UpdatedBy,
                              UpdatedOn = c.UpdatedOn,
                              Disable = c.Disable,
                              CategoryTrans = c.CategoryTrans
                          };
            return catList.ToList();
        }


        public string getbyCurrIdCatId(int currId, int catId)
        {
            var catList = from c in catRepo.GetAll().Where(c => c.CurrencyID == currId && c.CategoryID == catId)

                          select new CurrencyCategoriesVM
                          {
                              CategoryID = c.CategoryID,
                              CategoryArName = c.CategoryArName,
                              CategoryEnName = c.CategoryEnName,
                              CurrencyID = c.CurrencyID,
                              AddedBy = c.AddedBy,
                              AddenOn = c.AddenOn,
                              UpdatedBy = c.UpdatedBy,
                              UpdatedOn = c.UpdatedOn,
                              Disable = c.Disable,
                              CategoryTrans = c.CategoryTrans
                          };

            if (String.IsNullOrEmpty(catList.ToString()))
            {
                return "t";
            }
            else { return "f"; }

        }



        public string GetLastCode()
        {
            var Code = catRepo.GetAll().OrderByDescending(c => c.CategoryID).FirstOrDefault();
            string lastCode = Code.CategoryCode;
            return lastCode;
        }
    }
}
