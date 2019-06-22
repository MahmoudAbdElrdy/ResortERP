using ResortERP.Core;
using ResortERP.Core.VM;
using ResortERP.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace ResortERP.Service.IServices
{
    public interface IUserFormService
    {
        //Task<UserLogFile> InsertAsync(UserLogFile entity);
        List<menuFormSelVM> GetAll();
        List<User> GetAllUsers();
        List<COMPANY_BRANCHES> GetAllBranshes();
        Task<int> CountAsync(string[] Forms, string StartDate, string EndDate, string UID, string branchId);
        List<UserLogFile> GetBySelForms(string ID);
        Task<List<UserLogFile>> getByRangDate(string [] Forms, string StartDate, string EndDate, string UID, string branchId, int pageNum, int pageSize);
        Task<int> InsertUserForm(UserMenuVM entity,int id);
        void Dispose();
    }
}
