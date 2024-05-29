using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Common.Models.Catalog.Operador
{
    public class OperadorDto
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string LindeId { get; set; }
        public string NoEmployee { get; set; }
        public List<ItemDto> Countries { get; set; }
        public List<ItemDto> Plants { get; set; }
    }
 
}
