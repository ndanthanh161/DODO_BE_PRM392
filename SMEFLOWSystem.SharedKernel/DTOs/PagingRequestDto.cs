using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.DTOs
{
    public class PagingRequestDto
    {
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;

        private int Skip => (PageNumber - 1) * PageSize;
        public int GetSkip() => Skip;
    }
}
