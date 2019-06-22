using ResortERP.Core;
using ResortERP.Core.VM;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface INotificationService
    {
        List<NotificationVM> GetAllNotification( int lang = 1);
        List<NotificationsViewVM> GetAllUserNotification(int country = 1, int lang = 1, int NotifcationCategoryID = 0);
        List<NotificationsViewVM> GetAllUnreadUserNotification(int country = 1, int lang = 1, int NotifcationCategoryID = 0);

        List<NotificationsViewVM> GetAllReadedUserNotification(int country = 1, int lang = 1, int NotifcationCategoryID = 0);

        List<NotificationsViewVM> GetAllConfirmedUserNotification(int country = 1, int lang = 1, int NotifcationCategoryID = 0);

        List<NotificationsViewVM> GetAllUnConfirmedUserNotification(int country = 1, int lang = 1, int NotifcationCategoryID = 0);


        Task<bool> UpdateAsync(NotificationVM entity);
        Task<bool> UpdateNotificationConfirmedStatus(int Id1, int lang = 1);
        bool UpdateUserNotificationReadStatus(int Id1, int lang = 1);


        Task<int> InsertAsync(NotificationVM entity);

        Task<bool> DeleteAsync(int Id);

        Task<Notification> getById(int Id);
        Task<NotificationVM> getByIdForVM(int Id, int lang = 1);

        Task<List<NotificationVM>> GetPageData(int pageNum, int pageSize);

        Task<int> GetCount();
    }
}
