using SMEFLOWSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEFLOWSystem.Application.Interfaces.IRepositories
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        Task<Order?> GetByIdAsync(Guid orderId);
        Task<Order?> GetByIdIgnoreTenantAsync(Guid orderId);
        Task<Order?> UpdateAsync(Order order);  
        Task<Order?> UpdateIgnoreTenantAsync(Order order);
    }
}
