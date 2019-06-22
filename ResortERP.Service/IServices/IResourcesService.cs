using ResortERP.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface IResourcesService
    {
        bool Insert(Resources entity);
        Task<bool> InsertAsync(Resources entity);

        bool Update(Resources entity);
        Task<bool> UpdateAsync(Resources entity);

        bool Delete(Resources entity);
        Task<bool> DeleteAsync(Resources customer);

        Task<int> CountAsync(char type);
        List<Resources> GetAll();

        Resources GetByResourceName(string ResourceName);

    }
}
