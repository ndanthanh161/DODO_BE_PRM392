using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEFLOWSystem.SharedKernel.Interfaces
{
    public interface ITenantEntity
    {
        Guid TenantId { get; set; }
    }
}
