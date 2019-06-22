using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface IEmailsService
    {
        bool Insert(EmailsVM entity);
        Task<bool> InsertAsync(EmailsVM entity);
        bool Update(EmailsVM entity);
        Task<bool> UpdateAsync(EmailsVM entity);
        bool Delete(EmailsVM entity);
        Task<bool> DeleteAsync(EmailsVM entity);
        Task<List<EmailsVM>> GetAllAsync(int pageNum, int pageSize);
        Task<List<EmailsVM>> GetByUID(int userID);
        Task<int> CountAsync();
        List<EmailsVM> GetAll();
    }
}
