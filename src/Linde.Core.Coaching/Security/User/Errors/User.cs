using ErrorOr;

namespace Linde.Core.Coaching.Security.User.Errors;

public static class User
{
    public static Error DuplicateUserName => Error.Conflict(
        code: "User.DuplicateUserName",
        description: "Ya existe un usuario con el mismo usuario de red.");

    public static Error UnableRemoveAdmin => Error.Conflict(
        code: "User.UnableRemoveAdmin",
        description: "El usuario Administrador no puede ser eliminado.");

    public static Error InvalidName => Error.Conflict(
        code: "User.InvalidName",
        description: "El nombre completo no es válido.");

    public static Error Lockout => Error.Conflict(
        code: "User.Lockout",
        description: "Usuario bloqueado, comunicate con el administrador.");

    public static Error InvalidCredentials(
        int accessFailesAllowed,
        int accessFailedCount) => Error.Unauthorized(
            code: "User.InvalidCredentials",
            description: $"Usuario y/o contraseña incorrecta por favor recuerde que tiene {accessFailesAllowed} intentos para ingresar, luego de esto su usuario será bloqueado. LLeva {accessFailedCount} intentos fallidos de @paramAccessFailed.");

    public static Error LockoutUser => Error.Unauthorized(
        code: "User.LockoutUser",
        description: "Su usuario ha sido bloqueado, por favor comuníquese con el Administrador de la Aplicación.");

    public static Error InvalidRole => Error.Conflict(
        code: "User.InvalidRole",
        description: "Se encontró un rol no válido.");

    public static Error InvalidCountry => Error.Conflict(
        code: "User.InvalidCountry",
        description: "Se encontró un país no válido.");
    public static Error InvalidPlant => Error.Conflict(
        code: "User.InvalidPlant",
        description: "Se encontró una planta no válida.");
    public static Error UserNotFound => Error.Conflict(
        code: "User.UserNotFound",
        description: "El Usuario no se encuentra dado de Alta.");
}
