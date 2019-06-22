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
    public interface IUserService
    {
        Task<UserVM> GetUser(string userName);
        Task<UserVM> GetUserAsync(string userName, string password);
        UserVM GetUser(string userName, string password);
        UserVM getUserObj(string userName, string password);

        Task<User> InsertAsync(UserVM entity);

        bool Insert(UserVM entity);
        bool Update(UserVM entity);
        Task<bool> UpdateAsync(UserVM entity);

        bool Delete(UserVM entity);
        Task<bool> DeleteAsync(UserVM entity);
        Task<List<UserVM>> GetAllAsync(int pageNum, int pageSize);
        Task<int> CountAsync();
        List<UserVM> GetAll();
        void Dispose();

        string CopyDataBase(string BackUpFile, string DatabaseFilePath, string _databaseName);
        string GetNextDatabaseName();

        string GetCompanyNameByUserID(int ID);
        UserVM GetAdminUserByCompany(string companyEnName);
        Task<UserVM> GetAdminUserByCompanyAsync(string companyEnName);
        //void uploadFile(object file);
        Task<string> GetUserType();
        Task<bool> SendMail(string sendFrom, string s_password, string sendTo, string emailSubject, string emailBody, FileUpload Attachment, string Host, int Port, LinkedResource inlineLogo);
        Task<bool> ReceiveMail(string sendFrom, string sendTo, string s_password, string emailR, string nameR, string emailSubject, string emailBody, string Host, int Port);

        Task<bool> CheckEmailExist(string email);
        Task<bool> CheckUserNameExist(string userName);
        Task<bool> CheckARCompanyNameExist(string companyARName);
        Task<bool> CheckENCompanyNameExist(string companyENName);
        Task<List<string>> ValidateReg(UserVM entity);
        List<UserVM> GetAllUsers();
        Task<bool> changeLanguage(string userName, int userID, int languageID);

        Task<bool> ActivateUser(int Id);
        Task<User> getById(int Id);
        Task<bool> InsertInnerUserAsync(UserVM entity);

        Task<bool> ValidateCurrentPassword(int userID, string UserName, string password);
        Task<bool> changePassword(int userID, string UserName, string password);
        Task<UserVM> GetUserData(int userID);
        Task<User> getInnerAdminById(int Id);
        UserVM getUserByUserName(string userName);
    }
}
