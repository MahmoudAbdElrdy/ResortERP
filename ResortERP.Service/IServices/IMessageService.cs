using ResortERP.Core;
using ResortERP.Core.VM;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface IMessageService
    {
        List<MessageVM> GetAllMessage(int lang = 1);
        List<MessagesViewVM> GetAllUserMessage(int country = 1, int lang = 11, int MessageCategoryID = 0);
        List<MessagesViewVM> GetAllUnreadUserMessage(int country = 1, int lang = 1, int MessageCategoryID = 0);

        List<MessagesViewVM> GetAllReadedUserMessage(int country = 1, int lang = 1, int MessageCategoryID = 0);

        List<MessagesViewVM> GetAllConfirmedUserMessage(int country = 1, int lang = 1, int MessageCategoryID = 0);

        List<MessagesViewVM> GetAllUnConfirmedUserMessage(int country = 1, int lang = 1, int MessageCategoryID = 0);
        Task<bool> UpdateAsync(MessageVM entity);
        Task<bool> UpdateMessageReadStatus(int Id1, int lang = 1);
        Task<bool> UpdateMessageReceivedStatus(int Id1, int lang = 1);
        bool UpdateUserMessageReadStatus(int Id1, int lang = 1);
        Task<int> InsertAsync(MessageVM entity);
        Task<bool> DeleteAsync(int Id);
        Task<Message> getById(int Id);
        Task<MessageVM> getByIdForVM(int Id, int lang = 1);
        Task<List<MessageVM>> GetPageData(int pageNum, int pageSize);
        Task<int> GetCount();
    }
}
