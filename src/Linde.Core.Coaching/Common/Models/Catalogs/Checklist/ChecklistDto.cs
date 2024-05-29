using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Common.Models.Catalogs.Checklist
{
    public class ChecklistDto
    {
        public Guid ChecklistId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ItemDto> Questions { get; set; }
    }
}
