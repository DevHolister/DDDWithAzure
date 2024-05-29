using ErrorOr;
using MediatR;

namespace Linde.Core.Coaching.Catalogs.Divisions.Commands.Delete;

public record DeleteDivisionCommand(Guid Id) : IRequest<ErrorOr<Unit>>;
