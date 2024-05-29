using ErrorOr;
using Linde.Core.Coaching.Common.Models.Catalogs.Checklist;
using MediatR;

namespace Linde.Core.Coaching.Catalogs.Checklists.Queries.GetById
{
    public class GetChecklistByIdQuery : IRequest<ErrorOr<ChecklistDto>>
    {
        public Guid QuestionId { get; set; }
    }
}
