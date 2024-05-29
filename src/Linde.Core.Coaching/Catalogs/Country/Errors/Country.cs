using ErrorOr;

namespace Linde.Core.Coaching.Catalogs.Country.Errors;

public static class Country
{
    public static Error DuplicateCountryName => Error.Conflict(
    code: "Country.DuplicateCountryName",
    description: "Ya existe un país con el mismo nombre.");

    public static Error DuplicateCountryCode => Error.Conflict(
    code: "Country.DuplicateCountryCode",
    description: "Ya existe un país con el mismo código.");

    public static Error CountryCodePreviousAsigned => Error.Conflict(
    code: "Country.CountryCodePreviousAsigned",
    description: "El código ya se encuentra asignado a un país existente en el catálogo.");

    public static Error InvalidName => Error.Conflict(
        code: "Country.InvalidName",
        description: "El nombre del país no es válido.");

    public static Error UsersWithCountryAsigned => Error.Conflict(
        code: "Country.UsersWithCountryAsigned",
        description: "El país no puede eliminarse porque existen usuarios que lo tienen asignado.");

    public static Error Lockout => Error.Conflict(
        code: "Country.Lockout",
        description: "Usuario bloqueado, comunicate con el administrador.");

    public static Error CountryNoExists => Error.Conflict(
        code: "Country.CountryNoExists",
        description: "El país no existe.");
}
