using ErrorOr;
using Linde.Core.Coaching.Common.Models.Catalogs.Questions;
using Linde.Core.Coaching.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Catalogs.Questions.Commands.Save
{
    public class SaveQuestionCommand : IRequest<ErrorOr<Guid>>
    {
        public string Question { get; set; }
        public bool IsCritical { get; set; }
        public List<Guid>? ActivitiesIds { get; set; }
    }
}
