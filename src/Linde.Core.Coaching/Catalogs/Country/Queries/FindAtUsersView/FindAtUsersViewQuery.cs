using ErrorOr;
using Linde.Core.Coaching.Common.Models.Catalogs.Country;
using MediatR;

namespace Linde.Core.Coaching.Catalogs.Country.Queries.FindAtUsersView;

public class FindAtUsersViewQuery : IRequest<ErrorOr<CountryDto>>
{
    public string Code { get; set; }
}