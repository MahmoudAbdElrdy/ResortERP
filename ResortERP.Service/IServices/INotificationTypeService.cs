using ResortERP.Core;
using ResortERP.Core.VM;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface INotificationTypeService
    {
        List<NotificationTypeVM> GetAllNotificationType(int country = 1, int lang = 1);

        Task<bool> UpdateAsync(NotificationTypeVM entity);

        Task<int> InsertAsync(NotificationTypeVM entity);

        Task<bool> DeleteAsync(int Id);

        Task<NotificationType> getById(int Id);
        Task<NotificationTypeVM> getByIdForVM(int Id, int lang = 1);

        Task<List<NotificationTypeVM>> GetPageData(int pageNum, int pageSize, int countryId);

        Task<int> GetCount(int countryId);
    }
}
