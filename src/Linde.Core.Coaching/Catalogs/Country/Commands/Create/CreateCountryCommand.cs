using ErrorOr;
using MediatR;

namespace Linde.Core.Coaching.Catalogs.Country.Commands.Create;

//string OriginalCode
public record class CreateCountryCommand(string Name,
    string Code) : IRequest<ErrorOr<Guid>>;
