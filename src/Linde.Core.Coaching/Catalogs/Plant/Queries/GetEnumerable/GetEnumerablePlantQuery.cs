using ErrorOr;
using Linde.Core.Coaching.Common.Models;
using Linde.Core.Coaching.Common.Models.Catalog.Plant;
using MediatR;

namespace Linde.Core.Coaching.Catalogs.Plant.Queries.GetEnumerable;

//public record GetEnumerablePlantQuery(Guid CountryId) : IRequest<ErrorOr<IEnumerable<ItemDto>>>;
//public record GetEnumerablePlantQuery(List<Guid>? CountryId) : IRequest<ErrorOr<IEnumerable<ItemDto>>>;
public class GetEnumerablePlantQuery : IRequest<ErrorOr<IEnumerable<ItemDto>>>
{
    public List<Guid>? CountryId { get; set; }
}