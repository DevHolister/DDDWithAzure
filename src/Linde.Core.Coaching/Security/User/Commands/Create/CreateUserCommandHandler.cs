using ErrorOr;
using Linde.Core.Coaching.Catalogs.Country.Specifications;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Core.Coaching.Security.User.Specifications;
using Linde.Domain.Coaching.Common.Enum;
using Linde.Domain.Coaching.CountryAggregate;
using Linde.Domain.Coaching.CountryAggregate.ValueObjects;
using Linde.Domain.Coaching.Entities.Catalogs;
using Linde.Domain.Coaching.RoleAggregate.ValueObjects;
using Linde.Domain.Coaching.UserAggreagate.Entities;
using Linde.Domain.Coaching.UserAggreagate.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Linde.Core.Coaching.Security.User.Commands.Create;

internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ErrorOr<Guid>>
{
    private readonly IRepository<Country> _countryRepository;
    private readonly IRepository<Domain.Coaching.UserAggreagate.User> _userRepository;
    private readonly IRepository<UserPlant> _rolePlantRepository;
    private readonly ILogger<CreateUserCommandHandler> _logger;

    public CreateUserCommandHandler(
        IRepository<Country> countryRepository,
        IRepository<Domain.Coaching.UserAggreagate.User> userRepository,
        IRepository<UserPlant> rolePlantRepository,
        ILogger<CreateUserCommandHandler> logger)
    {
        _countryRepository = countryRepository;
        _userRepository = userRepository;
        _rolePlantRepository = rolePlantRepository;
        _logger = logger;
    }
    public async Task<ErrorOr<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (await _userRepository.AnyAsync(new UserWhereSpecification(request.UserName)))
                return Errors.User.DuplicateUserName;

            var countryId = CountryId.Create(request.CountryId);

            if (!await _countryRepository.AnyAsync(new CountryWhereSpecification(countryId)))
                return Default.NotFound;

            //if (FullName.Create(request.Name, request.FirstSurname, request.SecondSurname) is not FullName name)
            //    return Errors.User.InvalidName;

            var user = Domain.Coaching.UserAggreagate.User.Create(
                //name,
                request.FullName,
                request.UserName.Trim().ToUpper(),
                request.Email);

            user.AddCountry(UserCountry.Create(countryId));

            var rolesRequestId = RoleId.Create(request.Role);
            var role = UserRole.Create(rolesRequestId);
            user.AddRole(role);

            user.UpdateAccessType((AccessType)int.Parse(request.AccessType));

            user.UpdateTypeUser((TypeUser)int.Parse(request.TypeUser));

            user.ZoneId = int.Parse(request.ZoneId);
            user.EmployeeNumber = request.EmployeeNumber;

            await _userRepository.AddAsync(user);

            var rolePlant = UserPlant.Create(user.Id, request.PlantId, false);
            await _rolePlantRepository.AddAsync(rolePlant);

            return user.Id.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error.");
            return Default.ServerError;
        }
    }
}
