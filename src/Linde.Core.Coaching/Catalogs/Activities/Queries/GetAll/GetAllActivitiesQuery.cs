using ErrorOr;
using Linde.Core.Coaching.Common.Models;
using MediatR;
using Linde.Core.Coaching.Common.Models.Catalog.Activity;

namespace Linde.Core.Coaching.Catalogs.Activities.Queries.GetAll
{
    public record GetAllActivitiesQuery
    (
        int Page,
        int PageSize,
        string? name,
        string? description) : IRequest<ErrorOr<PaginatedListDto<ActivityDTO>>>;
}
