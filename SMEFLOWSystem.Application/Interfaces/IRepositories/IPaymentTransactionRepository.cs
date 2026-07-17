using SMEFLOWSystem.Core.Entities;

namespace SMEFLOWSystem.Application.Interfaces.IRepositories
{
    public interface IPaymentTransactionRepository
    {
        Task<PaymentTransaction?> GetByGatewayTransactionIdAsync(string gateway, string gatewayTransactionId, bool ignoreTenantFilter = false);
        /// <summary>
        /// Adds a payment transaction to the context (saved when the enclosing transaction commits).
        /// </summary>
        Task AddAsync(PaymentTransaction transaction);
        /// <summary>
        /// Attempts to persist a payment transaction.
        /// Returns false when a duplicate (same Gateway + GatewayTransactionId) already exists.
        /// </summary>
        Task<bool> TryAddAsync(PaymentTransaction transaction);
    }
}
