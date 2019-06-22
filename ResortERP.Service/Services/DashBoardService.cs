using ResortERP.Core;
using ResortERP.Repository;
using ResortERP.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.Services
{
   public class DashBoardService : IDashBoardService, IDisposable
    {
        IGenericRepository<DashBoard> _dashBoard;
        public DashBoardService(IGenericRepository<DashBoard> dashBoard)
        {
            this._dashBoard = dashBoard;
        }
        public void Dispose()
        {
            _dashBoard.Dispose();
        }


        // DashBoard Permission
        public DashBoard InsertDashBoardPer(DashBoard entity)
        {
            if (entity != null)
            {
                var q = _dashBoard.GetAll().Where(a => a.userId == entity.userId).FirstOrDefault();
                if (q != null)
                {
                    _dashBoard.Delete(q, q.DashBoardId);
                }
                return _dashBoard.Add(entity);
            }
            return null;
        }

        public DashBoard get(int id)
        {
            return _dashBoard.GetAll().Where(a => a.userId == id).FirstOrDefault();
        }
    }
}
