using ErrorOr;
using MediatR;

namespace Linde.Core.Coaching.Catalogs.Country.Commands.Edit;

//string OriginalCode
public record class EditCountryCommand(Guid countryId, string Name,
string Code) : IRequest<ErrorOr<Unit>>;