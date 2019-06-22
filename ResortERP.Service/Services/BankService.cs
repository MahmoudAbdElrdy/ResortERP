using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResortERP.Service.IServices;
using ResortERP.Repository;
using ResortERP.Core;
using ResortERP.Core.VM;

namespace ResortERP.Service.Services
{
    public class BankService : IBankService, IDisposable
    {
        IGenericRepository<Bank> bankRepo;
        IGenericRepository<Currency> currencyRepo;
        public void Dispose()
        {
            bankRepo.Dispose();
        }


        public BankService(IGenericRepository<Bank> _bankRepo, IGenericRepository<Currency> _currencyRepo)
        {
            this.bankRepo = _bankRepo;
            this.currencyRepo = _currencyRepo;
        }

        public Task<List<BankVM>> getBankAsync(int pageNum, int pageSize)
        {
            return Task.Run(() =>
            {
                int rowCount;
                var bankList = from b in bankRepo.GetPaged(pageNum, pageSize, b => b.ID, false, out rowCount)
                               join c in currencyRepo.GetPaged(pageNum, pageSize, c=>c.CURRENCY_ID, false, out rowCount) on b.CurrencyID equals c.CURRENCY_ID into g 
                               from  x in g.DefaultIfEmpty()
                               select new BankVM
                               {
                                   ID = b.ID,
                                   Code = b.Code,
                                   ArName = b.ArName,
                                   LatName = b.LatName,
                                   CurrencyID = b.CurrencyID,
                                   AccountNo = b.AccountNo,
                                   Notes = b.Notes,
                                   NationID = b.NationID,
                                   GovID = b.GovID,
                                   TownID = b.TownID,
                                   VillageID = b.VillageID,
                                   AddedBy = b.AddedBy,
                                   AddedOn = b.AddedOn,
                                   UpdatedBy = b.UpdatedBy,
                                   UpdatedOn = b.UpdatedOn,
                                   Disable = b.Disable,
                                   AddressNotes = b.AddressNotes,
                                   CurrencyName = x.CURRENCY_AR_NAME,
                                   
            };
                return bankList.ToList();
            });
        }

        public Task<BankVM> getById(int ID)
        {
            return Task.Run(() =>
            {
                var bankList = from b in bankRepo.GetAll()
                               join c in currencyRepo.GetAll() on b.CurrencyID equals c.CURRENCY_ID into g
                               from x in g.DefaultIfEmpty()
                               where b.ID == ID
                               select new BankVM
                               {
                                   ID = b.ID,
                                   Code = b.Code,
                                   ArName = b.ArName,
                                   LatName = b.LatName,
                                   CurrencyID = b.CurrencyID,
                                   AccountNo = b.AccountNo,
                                   Notes = b.Notes,
                                   NationID = b.NationID,
                                   GovID = b.GovID,
                                   TownID = b.TownID,
                                   VillageID = b.VillageID,
                                   AddedBy = b.AddedBy,
                                   AddedOn = b.AddedOn,
                                   UpdatedBy = b.UpdatedBy,
                                   UpdatedOn = b.UpdatedOn,
                                   Disable = b.Disable,
                                   AddressNotes = b.AddressNotes,
                                   CurrencyName = x.CURRENCY_AR_NAME,

                               };
                return bankList.FirstOrDefault();
            });
        }
        public List<Bank> getByCurrency(int currencyId)
        {
            var q = bankRepo.GetAll().Where(a => a.CurrencyID == currencyId).ToList();
            return q;
        }
        public List<Bank> getAll()
        {
            var q = bankRepo.GetAll().ToList();
            return q;
        }
        public List<Bank> getByNationID(int nationId)
        {
            var q = bankRepo.GetAll().Where(a => a.NationID == nationId).ToList();
            return q;
        }
        public List<Bank> getByGOVID(int govId)
        {
            var q = bankRepo.GetAll().Where(a => a.GovID == govId).ToList();
            return q;
        }
        public List<Bank> getByTownID(int townId)
        {
            var q = bankRepo.GetAll().Where(a => a.TownID == townId).ToList();
            return q;
        }
        public List<Bank> getByVilID(long villageId)
        {
            var q = bankRepo.GetAll().Where(a => a.VillageID == villageId).ToList();
            return q;
        }

        public Task<int> insertBankSync(BankVM entity) {
            return Task.Run(()=> {
                Bank bank = new Bank();
                {
                    bank.ID = entity.ID;
                    bank.Code = entity.Code;
                    bank.ArName = entity.ArName;
                    bank.LatName = entity.LatName;
                    bank.CurrencyID = entity.CurrencyID;
                    bank.AccountNo = entity.AccountNo;
                    bank.Notes = entity.Notes;
                    bank.NationID = entity.NationID;
                    bank.GovID = entity.GovID;
                    bank.TownID = entity.TownID;
                    bank.VillageID = entity.VillageID;
                    bank.AddedBy = entity.AddedBy;
                    bank.AddedOn = entity.AddedOn;
                    bank.UpdatedBy = entity.UpdatedBy;
                    bank.UpdatedOn = entity.UpdatedOn;
                    bank.Disable = entity.Disable;
                    bank.AddressNotes = entity.AddressNotes;
                };

                bankRepo.Add(bank);
                return bank.ID;
            });
           
        }

        public Task<bool> updateBankSync(BankVM entity)
        {
            return Task.Run(() => {
                Bank bank = new Bank();
                {
                    bank.ID = entity.ID;
                    bank.Code = entity.Code;
                    bank.ArName = entity.ArName;
                    bank.LatName = entity.LatName;
                    bank.CurrencyID = entity.CurrencyID;
                    bank.AccountNo = entity.AccountNo;
                    bank.Notes = entity.Notes;
                    bank.NationID = entity.NationID;
                    bank.GovID = entity.GovID;
                    bank.TownID = entity.TownID;
                    bank.VillageID = entity.VillageID;
                    bank.AddedBy = entity.AddedBy;
                    bank.AddedOn = entity.AddedOn;
                    bank.UpdatedBy = entity.UpdatedBy;
                    bank.UpdatedOn = entity.UpdatedOn;
                    bank.Disable = entity.Disable;
                    bank.AddressNotes = entity.AddressNotes;

                };

                bankRepo.Update(bank,bank.ID);
                return true;
            });

        }

        public Task<bool> deleteBankSync(BankVM entity)
        {
            return Task.Run(() => {
                Bank bank = new Bank();
                {
                    bank.ID = entity.ID;
                    bank.Code = entity.Code;
                    bank.ArName = entity.ArName;
                    bank.LatName = entity.LatName;
                    bank.CurrencyID = entity.CurrencyID;
                    bank.AccountNo = entity.AccountNo;
                    bank.Notes = entity.Notes;
                    bank.NationID = entity.NationID;
                    bank.GovID = entity.GovID;
                    bank.TownID = entity.TownID;
                    bank.VillageID = entity.VillageID;
                    bank.AddedBy = entity.AddedBy;
                    bank.AddedOn = entity.AddedOn;
                    bank.UpdatedBy = entity.UpdatedBy;
                    bank.UpdatedOn = entity.UpdatedOn;
                    bank.Disable = entity.Disable;
                    bank.AddressNotes = entity.AddressNotes;

                };

                bankRepo.Delete(bank, bank.ID);
                return true;
            });

        }

        public Task<int> countBankAsync()
        {
            return Task.Run(() => {
                return bankRepo.CountAsync();
            });
        }

        public string GetLastCode()
        {
            var lastCode = bankRepo.GetAll().OrderByDescending(b => b.ID).FirstOrDefault();
            return lastCode.Code.ToString();
        }
             public Task<List<BankVM>> GetSearchResultAsync(BankVM entity, int pageNum, int pageSize)
        {
            return Task.Run(() =>
            {
                int rowCount;
                var q = from b in bankRepo.GetPaged<int>(pageNum, pageSize, p => p.ID, false, out rowCount).Where(x => x.ArName.Contains(entity.ArName))
                        select new BankVM
                        {
                            ID = b.ID,
                            Code = b.Code,
                            ArName = b.ArName,
                            LatName = b.LatName,
                            CurrencyID = b.CurrencyID,
                            AccountNo = b.AccountNo,
                            Notes = b.Notes,
                            NationID = b.NationID,
                            GovID = b.GovID,
                            TownID = b.TownID,
                            VillageID = b.VillageID,
                            AddedBy = b.AddedBy,
                            AddedOn = b.AddedOn,
                            UpdatedBy = b.UpdatedBy,
                            UpdatedOn = b.UpdatedOn,
                            Disable = b.Disable,
                            AddressNotes = b.AddressNotes,
                           
                        };
                return q.ToList();
            });
        }

        List<BankVM> IBankService.getAll()
        {
            throw new NotImplementedException();
        }
    }
}
