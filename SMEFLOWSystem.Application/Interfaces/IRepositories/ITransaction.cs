using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEFLOWSystem.Application.Interfaces.IRepositories
{
    public interface ITransaction
    {
        Task ExecuteAsync(Func<Task> action);
    }
}
