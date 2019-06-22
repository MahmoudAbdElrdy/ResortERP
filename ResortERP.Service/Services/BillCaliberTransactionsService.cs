using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResortERP.Core.VM;
using ResortERP.Core;
using ResortERP.Repository;
using ResortERP.Service.IServices;

namespace ResortERP.Service.Services
{
    public class BillCaliberTransactionsService : IBillCaliberTransactionsService, IDisposable
    {
        IGenericRepository<BillCaliberTransactions> billCailberTransRepo;

        public BillCaliberTransactionsService(IGenericRepository<BillCaliberTransactions> _billCailberTransRepo)
        {
            this.billCailberTransRepo = _billCailberTransRepo;
        }

        public void Dispose()
        {
            billCailberTransRepo.Dispose();
        }

        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return billCailberTransRepo.CountAsync();
            });
        }

        public Task<bool> DeleteAsync(BillCaliberTransactionsVM entity)
        {
            return Task.Run<bool>(() =>
            {
                BillCaliberTransactions bc = new BillCaliberTransactions
                {
                    BillMasterID = entity.BillMasterID,
                    CaliberTransID = entity.CaliberTransID,
                    ColName = entity.ColName,
                    ColQuantity = entity.ColQuantity,
                    Col24 = entity.Col24,
                    Col22 = entity.Col22,
                    Col21 = entity.Col21,
                    Col18 = entity.Col18,
                    TransQuant = entity.TransQuant
                };
                billCailberTransRepo.Delete(bc, entity.CaliberTransID);
                return true;
            });
        }

        public Task<List<BillCaliberTransactionsVM>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run<List<BillCaliberTransactionsVM>>(() =>
            {
                int rowCount;
                var q = from entity in billCailberTransRepo.GetPaged<long>(pageNum, pageSize, p => p.CaliberTransID, false, out rowCount)
                        select new BillCaliberTransactionsVM
                        {
                            BillMasterID = entity.BillMasterID,
                            CaliberTransID = entity.CaliberTransID,
                            ColName = entity.ColName,
                            ColQuantity = entity.ColQuantity,
                            Col24 = entity.Col24,
                            Col22 = entity.Col22,
                            Col21 = entity.Col21,
                            Col18 = entity.Col18,
                            TransQuant = entity.TransQuant

                        };
                return q.ToList();
            });
        }

        public Task<bool> InsertAsync(BillCaliberTransactionsVM entity)
        {
            return Task.Run<bool>(() =>
            {
                BillCaliberTransactions bc = new BillCaliberTransactions
                {
                    BillMasterID = entity.BillMasterID,
                    CaliberTransID = entity.CaliberTransID,
                    ColName = entity.ColName,
                    ColQuantity = entity.ColQuantity,
                    Col24 = entity.Col24,
                    Col22 = entity.Col22,
                    Col21 = entity.Col21,
                    Col18 = entity.Col18,
                    TransQuant = entity.TransQuant
                };
                billCailberTransRepo.Add(bc);
                return true;
            });
        }




        // public deleteAllByMasterID(int )







        public Task<bool> UpdateAsync(BillCaliberTransactionsVM entity)
        {
            return Task.Run<bool>(() =>
            {
                BillCaliberTransactions bc = new BillCaliberTransactions
                {
                    BillMasterID = entity.BillMasterID,
                    CaliberTransID = entity.CaliberTransID,
                    ColName = entity.ColName,
                    ColQuantity = entity.ColQuantity,
                    Col24 = entity.Col24,
                    Col22 = entity.Col22,
                    Col21 = entity.Col21,
                    Col18 = entity.Col18,
                    TransQuant = entity.TransQuant
                };
                billCailberTransRepo.Update(bc, bc.CaliberTransID);
                return true;
            });


        }


        public Task<List<BillCaliberTransactionsVM>> getByMasterID(int masterID)
        {


            return Task.Run<List<BillCaliberTransactionsVM>>(() =>
            {

                var q = from entity in billCailberTransRepo.Filter(b => b.BillMasterID == masterID)
                        select new BillCaliberTransactionsVM
                        {

                            BillMasterID = entity.BillMasterID,
                            CaliberTransID = entity.CaliberTransID,
                            ColName = entity.ColName,
                            ColQuantity = entity.ColQuantity,
                            Col24 = entity.Col24,
                            Col22 = entity.Col22,
                            Col21 = entity.Col21,
                            Col18 = entity.Col18,
                            TransQuant = entity.TransQuant


                        };


                return q.ToList();

            });


        }


        public Task<bool> UpdateWithMasterID(List<BillCaliberTransactionsVM> entityList, int masterID)
        {
            return Task.Run<bool>(() =>
            {

                List<BillCaliberTransactions> billCaliberList = billCailberTransRepo.Filter(c => c.BillMasterID == masterID).ToList();
                if (billCaliberList.Count > 0)
                {
                    foreach (var item in billCaliberList)
                    {
                        billCailberTransRepo.Delete(item, item.CaliberTransID);
                    }
                }


                foreach (var entity in entityList)
                {
                    BillCaliberTransactions bc = new BillCaliberTransactions();
                    {
                        bc.BillMasterID = entity.BillMasterID;
                        bc.CaliberTransID = entity.CaliberTransID;
                        bc.ColName = entity.ColName;
                        bc.ColQuantity = entity.ColQuantity;
                        bc.Col24 = entity.Col24;
                        bc.Col22 = entity.Col22;
                        bc.Col21 = entity.Col21;
                        bc.Col18 = entity.Col18;
                        bc.TransQuant = entity.TransQuant;
                    }

                    billCailberTransRepo.Add(bc);
                }
                return true;
            });


        }








        public bool DeleteByMasterID(int? MasterID)
        {
            var existCaliberTrans = billCailberTransRepo.Filter(p => p.BillMasterID == MasterID).ToList();
            if (existCaliberTrans.Count > 0)
            {
                foreach (var entity in existCaliberTrans)
                {
                    BillCaliberTransactions bp = new BillCaliberTransactions
                    {
                        BillMasterID = entity.BillMasterID,
                        CaliberTransID = entity.CaliberTransID,
                        ColName = entity.ColName,
                        ColQuantity = entity.ColQuantity,
                        Col24 = entity.Col24,
                        Col22 = entity.Col22,
                        Col21 = entity.Col21,
                        Col18 = entity.Col18,
                        TransQuant = entity.TransQuant
                    };
                    billCailberTransRepo.Delete(bp, entity.CaliberTransID);
                }
            }
            return true;
        }


        public Task<bool> InsertWithMasterID(List<BillCaliberTransactionsVM> caliberTransList)

        {
            return Task.Run<bool>(() =>
            {
                bool delete = DeleteByMasterID(caliberTransList[0].BillMasterID);
                if (delete == true)
                {

                    foreach (var entity in caliberTransList)
                    {
                        BillCaliberTransactions bp = new BillCaliberTransactions
                        {
                            BillMasterID = entity.BillMasterID,
                            CaliberTransID = entity.CaliberTransID,
                            ColName = entity.ColName,
                            ColQuantity = entity.ColQuantity,
                            Col24 = entity.Col24,
                            Col22 = entity.Col22,
                            Col21 = entity.Col21,
                            Col18 = entity.Col18,
                            TransQuant = entity.TransQuant

                        };
                        billCailberTransRepo.Add(bp);
                    }
                }
                return true;
            });


        }





    }
}
