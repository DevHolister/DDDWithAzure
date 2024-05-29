using ErrorOr;
using Linde.Core.Coaching.Common.Models.Catalogs.Country;
using MediatR;

namespace Linde.Core.Coaching.Catalogs.Country.Queries.FindById;

public class FindByIdQuery : IRequest<ErrorOr<CountryDto>>
{
    public Guid CountryId { get; set; }
}