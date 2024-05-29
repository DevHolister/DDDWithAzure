using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Common.Models.Menu
{
    public class MenuDTO
    {
        public Guid MenuId { get; set; }
        public string MenuName { get; set; }
        public string Icon { get; set; }
        public string Path { get; set; }
        public int Level { get; set; }
        public Guid? ParentId { get; set; }
        public bool ContainsChildren { get; set; }
        public List<MenuDTO> Attributes { get; set; }
    }
}
