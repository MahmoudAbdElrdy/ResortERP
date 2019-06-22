using ResortERP.Core;
using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace ResortERP.Service.IServices
{
    public interface IUserLogFileService
    {
        //Task<UserLogFile> InsertAsync(UserLogFile entity);
        Task<UserLogFile> Insert(UserLogFile entity);
        List<UserLogFile> GetAll(int pageNum, int pageSize);
        Task<int> CountAsync();
        void Dispose();
    }
}
