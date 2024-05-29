using ErrorOr;

namespace Linde.Core.Coaching.Common.Errors;

public static class Default
{
    public static Error ServerError => Error.Failure(
        code: "Common.ServerError",
        description: "Surgió un error al procesar la solicitud.");

    public static Error NotFound => Error.NotFound(
        code: "Common.NotFound",
        description: "No se encontraron resultados.");

    public static Error ValidationError => Error.Validation(
        code: "Common.ValidationError",
        description: "Errores de validación.");

    public static Error UnauthorizedError => Error.Unauthorized(
        code: "Common.UnauthorizedError",
        description: "Usuario no autorizado.");
}
