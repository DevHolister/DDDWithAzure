using ErrorOr;
using Linde.Core.Coaching.Common.Models;
using Linde.Core.Coaching.Common.Models.Catalogs.Country;
using MediatR;

namespace Linde.Core.Coaching.Catalogs.Country.Queries.GetAll;

public record GetAllCountryQuery(
    int Page,
    int PageSize,
    string? Name,
    string? Code) : IRequest<ErrorOr<PaginatedListDto<CountryDto>>>;
