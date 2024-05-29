using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Common.Models.Catalogs.Questions
{
    public class QuestionDto
    {
        public Guid QuestionId { get; set; }
        public string Name { get; set; }
        public bool IsCritical { get; set; }
        public List<ItemDto> Activities { get; set; }
    }
}
