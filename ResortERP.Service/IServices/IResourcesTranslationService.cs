using ResortERP.Core;
using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface IResourcesTranslationService
    {
        bool Insert(ResourcesTranslation entity);
        Task<bool> InsertAsync(ResourcesTranslation entity);

        bool Update(ResourcesTranslation entity);
        Task<bool> UpdateAsync(ResourcesTranslation entity);

        bool Delete(ResourcesTranslation entity);
        Task<bool> DeleteAsync(ResourcesTranslation customer);

        Task<int> CountAsync(char type);
        List<ResourcesTranslationVM> GetAll();

        ResourcesTranslation GetByID(int id);

        Task<bool> UpdateAsyncByID(ResourcesTranslation entity);

    }
}
