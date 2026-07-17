using Microsoft.EntityFrameworkCore;
using SMEFLOWSystem.Application.Interfaces.IRepositories;
using SMEFLOWSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEFLOWSystem.Infrastructure.Tenancy
{
    public class Transaction : ITransaction
    {
        private readonly SMEFLOWSystemContext _context;

        public Transaction(SMEFLOWSystemContext context)
        {
            _context = context;
        }

        public async Task ExecuteAsync(Func<Task> action)
        {
            var strategy = _context.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    await action(); // Chạy các hàm của Repo ở đây
                    await transaction.CommitAsync(); // Nếu ngon lành thì Commit
                }
                catch
                {
                    await transaction.RollbackAsync(); // Lỗi thì Rollback
                    throw;
                }
            });
        }
    }
}
