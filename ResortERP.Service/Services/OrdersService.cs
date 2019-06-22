using AutoMapper;
using ResortERP.Core;
using ResortERP.Core.VM;
using ResortERP.Repository;
using ResortERP.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResortERP.Service.Services
{
    public class OrdersService : IOrdersService, IDisposable
    {
        IGenericRepository<ORDERS> ordersRepo;
        public OrdersService(IGenericRepository<ORDERS> ORDERSRepo)
        {
            this.ordersRepo = ORDERSRepo;
        }

        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return ordersRepo.CountAsync();
            });
        }

        public bool Delete(OrdersVM entity)
        {
            ORDERS ord = new ORDERS
            {
                ORDER_ID = entity.ORDER_ID
            };
            ordersRepo.Delete(ord, entity.ORDER_ID);
            return true;
        }

        public Task<bool> DeleteAsync(OrdersVM entity)
        {
            return Task.Run<bool>(() =>
            {
                ORDERS ord = new ORDERS
                {
                    ORDER_ID = entity.ORDER_ID
                };
                ordersRepo.Delete(ord, entity.ORDER_ID);
                return true;
            });
        }

        public void Dispose()
        {
            ordersRepo.Dispose();
        }

        public List<OrdersVM> GetAll()
        {
            var q = (from entity in ordersRepo.GetAll()
                     select entity).ToList().Select(x => Mapper.Map<ORDERS, OrdersVM>(x));
            return q.ToList();
        }



        public Task<List<OrdersVM>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run<List<OrdersVM>>(() =>
            {
                int rowCount;
                var q = (from entity in ordersRepo.GetPaged<long>(pageNum, pageSize, p => p.ORDER_ID, false, out rowCount)
                         select entity).ToList().Select(x => Mapper.Map<ORDERS, OrdersVM>(x));
                return q.ToList();
            });
        }

        public bool Insert(OrdersVM entity)
        {
            ORDERS ord = Mapper.Map<OrdersVM, ORDERS>(entity);
            ordersRepo.Add(ord);
            return true;
        }

        public Task<bool> InsertAsync(OrdersVM entity)
        {
            return Task.Run<bool>(() =>
            {
                ORDERS ord = Mapper.Map<OrdersVM, ORDERS>(entity);
                ordersRepo.Add(ord);
                return true;
            });
        }

        public bool Update(OrdersVM entity)
        {
            ORDERS ord = Mapper.Map<OrdersVM, ORDERS>(entity);
            ordersRepo.Update(ord, ord.ORDER_ID);
            return true;
        }

        public Task<bool> UpdateAsync(OrdersVM entity)
        {
            return Task.Run<bool>(() =>
            {
                ORDERS ord = Mapper.Map<OrdersVM, ORDERS>(entity);
                ordersRepo.Update(ord, ord.ORDER_ID);
                return true;
            });
        }

    }
}
