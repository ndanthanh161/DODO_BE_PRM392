using Microsoft.EntityFrameworkCore;
using SMEFLOWSystem.Application.Interfaces.IRepositories;
using SMEFLOWSystem.Core.Entities;
using SMEFLOWSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEFLOWSystem.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly SMEFLOWSystemContext _context;

        public OrderRepository(SMEFLOWSystemContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task<Order?> GetByIdAsync(Guid orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            return order;
        }

        public async Task<Order?> GetByIdIgnoreTenantAsync(Guid orderId)
        {
            return await _context.Orders
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<Order?> UpdateAsync(Order order)
        {
            var existingOrder = await _context.Orders.FirstOrDefaultAsync(o => o.Id == order.Id);
            if(existingOrder == null)
                return null;
            
            existingOrder.TenantId = order.TenantId;
            existingOrder.OrderNumber = order.OrderNumber;
            existingOrder.CustomerId = order.CustomerId;
            existingOrder.CustomerId = order.CustomerId;
            existingOrder.TotalAmount = order.TotalAmount;
            existingOrder.DiscountAmount = order.DiscountAmount;
            existingOrder.FinalAmount = order.FinalAmount;
            existingOrder.PaymentStatus = order.PaymentStatus;
            existingOrder.Status = order.Status;
            existingOrder.Notes = order.Notes;
            existingOrder.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return existingOrder;
        }

        public async Task<Order?> UpdateIgnoreTenantAsync(Order order)
        {
            var existingOrder = await _context.Orders
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(o => o.Id == order.Id);
            if (existingOrder == null)
                return null;

            existingOrder.TenantId = order.TenantId;
            existingOrder.OrderNumber = order.OrderNumber;
            existingOrder.CustomerId = order.CustomerId;
            existingOrder.CustomerId = order.CustomerId;
            existingOrder.TotalAmount = order.TotalAmount;
            existingOrder.DiscountAmount = order.DiscountAmount;
            existingOrder.FinalAmount = order.FinalAmount;
            existingOrder.PaymentStatus = order.PaymentStatus;
            existingOrder.Status = order.Status;
            existingOrder.Notes = order.Notes;
            existingOrder.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return existingOrder;
        }
    }
}
