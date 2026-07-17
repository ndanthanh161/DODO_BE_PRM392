using SMEFLOWSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEFLOWSystem.Application.Interfaces.IRepositories
{
    public interface IInviteRepository
    {
        Task AddInviteAsync(Invite invite);
        Task<Invite?> GetInviteByTokenAsync(string token);
        Task UpdateInviteAsync(Invite invite);
    }
}
