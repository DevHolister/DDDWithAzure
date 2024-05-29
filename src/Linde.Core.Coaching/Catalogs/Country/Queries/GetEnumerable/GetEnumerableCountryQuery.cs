using ErrorOr;
using Linde.Core.Coaching.Common.Models.Catalogs.Country;
using MediatR;

namespace Linde.Core.Coaching.Catalogs.Country.Queries.GetEnumerable;

public record GetEnumerableCountryQuery() : IRequest<ErrorOr<IEnumerable<CountryDto>>>;