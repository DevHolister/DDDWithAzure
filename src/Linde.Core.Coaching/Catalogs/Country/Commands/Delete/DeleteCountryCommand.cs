using ErrorOr;
using MediatR;

namespace Linde.Core.Coaching.Catalogs.Country.Commands.Delete;

public class DeleteCountryCommand : IRequest<ErrorOr<Unit>>
{
    public Guid CountryId { get; set; }
}