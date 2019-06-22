using ResortERP.Core;
using ResortERP.Core.VM;
using ResortERP.Repository;
using ResortERP.Service.IServices;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.Services
{
    public class GoldWorldPriceService : IGoldWorldPriceService, IDisposable
    {
        IGenericRepository<GoldWorldPrice> goldWorldPriceRepo;
        IGenericRepository<ITEMS> itemsRepo;

        public GoldWorldPriceService(IGenericRepository<GoldWorldPrice> goldWorldPriceRepo, IGenericRepository<ITEMS> itemsRepo)
        {
            this.goldWorldPriceRepo = goldWorldPriceRepo;
            this.itemsRepo = itemsRepo;

        }

        public void Dispose()
        {
            goldWorldPriceRepo.Dispose();
        }


        public bool Insert(GoldWorldPriceVM entity)
        {
            GoldWorldPrice ig = new GoldWorldPrice
            {
                ID = entity.ID,
                ARName = entity.ARName,
                Code = entity.Code,
                LatName = entity.LatName,
                PriceDate = entity.PriceDate,
                GoldPrice = entity.GoldPrice,

                Notes = entity.Notes,
                KiloPrice = entity.KiloPrice,
                OuncePrice = entity.OuncePrice
            };
            goldWorldPriceRepo.Add(ig);
            return true;
        }

        public Task<int> InsertAsync(GoldWorldPriceVM entity)
        {
            return Task.Run<int>(async () =>
            {
                GoldWorldPrice ig = new GoldWorldPrice
                {
                    ID = entity.ID,
                    ARName = entity.ARName,
                    Code = entity.Code,
                    LatName = entity.LatName,
                    PriceDate = entity.PriceDate,
                    GoldPrice = entity.GoldPrice,

                    Notes = entity.Notes,
                    KiloPrice = entity.KiloPrice,
                    OuncePrice = entity.OuncePrice
                };
                try
                {


                    goldWorldPriceRepo.Add(ig);
                    await UpdateItemPrices(ig);

                }
                catch (Exception ex)
                {


                }
                return ig.ID;
            }
              );

        }

        public bool Update(GoldWorldPriceVM entity)
        {

            GoldWorldPrice ig = new GoldWorldPrice
            {
                ID = entity.ID,
                ARName = entity.ARName,
                Code = entity.Code,
                LatName = entity.LatName,
                PriceDate = entity.PriceDate,
                GoldPrice = entity.GoldPrice,

                Notes = entity.Notes,
                KiloPrice = entity.KiloPrice,
                OuncePrice = entity.OuncePrice
            };
            try
            {
                goldWorldPriceRepo.Update(ig, ig.ID);

            }
            catch (Exception ex)
            {


            }
            return true;


        }
        public Task<bool> UpdateAsync(GoldWorldPriceVM entity)
        {
            return Task.Run<bool>(() =>
            {
                GoldWorldPrice ig = new GoldWorldPrice
                {
                    ID = entity.ID,
                    ARName = entity.ARName,
                    Code = entity.Code,
                    LatName = entity.LatName,
                    PriceDate = entity.PriceDate,
                    GoldPrice = entity.GoldPrice,

                    Notes = entity.Notes,
                    KiloPrice = entity.KiloPrice,
                    OuncePrice = entity.OuncePrice
                };
                goldWorldPriceRepo.Update(ig, ig.ID);
                return true;
            }
              );

        }

        public bool Delete(GoldWorldPriceVM entity)
        {
            GoldWorldPrice ig = new GoldWorldPrice
            {
                ID = entity.ID,
                ARName = entity.ARName,
                Code = entity.Code,
                LatName = entity.LatName,
                PriceDate = entity.PriceDate,
                GoldPrice = entity.GoldPrice,

                Notes = entity.Notes,
                KiloPrice = entity.KiloPrice,
                OuncePrice = entity.OuncePrice
            };
            goldWorldPriceRepo.Delete(ig, entity.ID);
            return true;


        }
        public Task<bool> DeleteAsync(GoldWorldPriceVM entity)
        {
            return Task.Run<bool>(() =>
            {
                GoldWorldPrice ig = new GoldWorldPrice
                {
                    ID = entity.ID,
                    ARName = entity.ARName,
                    Code = entity.Code,
                    LatName = entity.LatName,
                    PriceDate = entity.PriceDate,
                    GoldPrice = entity.GoldPrice,

                    Notes = entity.Notes,
                    KiloPrice = entity.KiloPrice,
                    OuncePrice = entity.OuncePrice
                };
                goldWorldPriceRepo.Delete(ig, entity.ID);
                return true;
            });
        }

        public Task<List<GoldWorldPriceVM>> GetAllAsync(int pageNum, int pageSize)
        {

            return Task.Run<List<GoldWorldPriceVM>>(() =>
            {

                int rowCount;
                var q = from p in goldWorldPriceRepo.GetPaged<int>(pageNum, pageSize, p => p.ID, false, out rowCount)

                        select new GoldWorldPriceVM
                        {
                            ID = p.ID,
                            ARName = p.ARName,
                            Code = p.Code,
                            LatName = p.LatName,
                            PriceDate = p.PriceDate,
                            GoldPrice = p.GoldPrice,

                            Notes = p.Notes,
                            KiloPrice = p.KiloPrice,
                            OuncePrice = p.OuncePrice
                        };



                return q.ToList();




                // return new List<GoldWorldPriceVM>();
            });


        }

        public Task<GoldWorldPriceVM> getById(int ID)
        {

            return Task.Run<GoldWorldPriceVM>(() =>
            {

                var q = from p in goldWorldPriceRepo.GetAll().Where(a=>a.ID==ID)

                        select new GoldWorldPriceVM
                        {
                            ID = p.ID,
                            ARName = p.ARName,
                            Code = p.Code,
                            LatName = p.LatName,
                            PriceDate = p.PriceDate,
                            GoldPrice = p.GoldPrice,

                            Notes = p.Notes,
                            KiloPrice = p.KiloPrice,
                            OuncePrice = p.OuncePrice
                        };



                return q.FirstOrDefault();




                // return new List<GoldWorldPriceVM>();
            });


        }
        public List<GoldWorldPriceVM> getAllGoldWorldPrices()
        {
            var q = from p in goldWorldPriceRepo.GetAll()
                    select new GoldWorldPriceVM
                    {
                        ID = p.ID,
                        ARName = p.ARName,
                        Code = p.Code,
                        LatName = p.LatName,
                        PriceDate = p.PriceDate,
                        GoldPrice = p.GoldPrice,

                        Notes = p.Notes,
                        KiloPrice = p.KiloPrice,
                        OuncePrice = p.OuncePrice
                    };
            return q.ToList();
        }


        public Task<double> GetLastGoldWorldPrice()
        {
            return Task.Run<double>(() =>
            {
                var q = goldWorldPriceRepo.GetAll().OrderByDescending(m => m.PriceDate).FirstOrDefault();
                if (q == null)
                {
                    return 0;
                }
                else
                    return Convert.ToDouble(q.GoldPrice);
            });
        }


        public Task<bool> UpdateItemPrices(GoldWorldPrice entity)
        {
            return Task.Run<bool>(() =>
            {

                SqlParameter GlobalGoldPriceParam = new SqlParameter("@GlobalGoldPrice", (object)entity.GoldPrice);
                SqlParameter KiloPriceParam = new SqlParameter("@KiloPrice", (object)entity.KiloPrice);
                SqlParameter OuncePriceParam = new SqlParameter("@OuncePrice", (object)entity.OuncePrice);

                try
                {


                    var count = itemsRepo.ExecuteSqlCommand("exec UPDATE_ITEMS_PRICES  @GlobalGoldPrice,@KiloPrice,@OuncePrice", GlobalGoldPriceParam, KiloPriceParam, OuncePriceParam);
                    return true;

                }
                catch (Exception ex)
                {

                    return false;
                }
            });
        }





        public List<GoldWorldPriceVM> GetAllItemGroupsPos()
        {
            var q = from p in goldWorldPriceRepo.GetAll().Where(x => x.Active == 1)
                    select new GoldWorldPriceVM
                    {
                        ID = p.ID,
                        ARName = p.ARName,
                        Code = p.Code,
                        LatName = p.LatName,
                        PriceDate = p.PriceDate,
                        GoldPrice = p.GoldPrice,

                        Notes = p.Notes,
                        KiloPrice = p.KiloPrice,
                        OuncePrice = p.OuncePrice
                    };
            return q.ToList();
        }

        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return goldWorldPriceRepo.CountAsync();
            });
        }


        public string GetLastCode()
        {
            var lastCode = goldWorldPriceRepo.GetAll().OrderByDescending(g => g.ID).FirstOrDefault();
            return lastCode.Code;
        }




        public double? GetLastGoldPrice()
        {
            var gold = goldWorldPriceRepo.GetAll().OrderByDescending(g => g.PriceDate).FirstOrDefault();

            // double? lastPrice = gold.GoldPrice;

            return gold.GoldPrice;
        }




        public Task<GoldWorldPriceVM> GetLastGoldWorldPriceData()
        {
            return Task.Run<GoldWorldPriceVM>(() =>
            {
                var q = from g in goldWorldPriceRepo.GetAll().OrderByDescending(m => m.PriceDate)
                        select new GoldWorldPriceVM
                        {
                            ID = g.ID,
                            ARName = g.ARName,
                            Code = g.Code,
                            LatName = g.LatName,
                            PriceDate = g.PriceDate,
                            GoldPrice = g.GoldPrice,

                            Notes = g.Notes,
                            KiloPrice = g.KiloPrice,
                            OuncePrice = g.OuncePrice
                        };
                return q.FirstOrDefault();
            });
        }
    }
}
