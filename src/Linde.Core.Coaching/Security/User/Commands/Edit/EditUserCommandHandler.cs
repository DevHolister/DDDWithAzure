using ErrorOr;
using Linde.Core.Coaching.Catalogs.Country.Specifications;
using Linde.Core.Coaching.Catalogs.Plant.Specifications;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Core.Coaching.Security.Role.Specifications;
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
using System.Linq;

namespace Linde.Core.Coaching.Security.User.Commands.Edit;

internal class EditUserCommandHandler : IRequestHandler<EditUserCommand, ErrorOr<Unit>>
{
    private readonly IRepository<Domain.Coaching.UserAggreagate.User> _repository;
    private readonly IRepository<Domain.Coaching.RoleAggregate.Role> _roleRepository;
    private readonly IRepository<UserPlant> _rolePlantRepository;
    private readonly IRepository<Plant> _plantRepository;
    private readonly IRepository<Country> _countryRepository;
    private readonly ILogger<EditUserCommandHandler> _logger;

    public EditUserCommandHandler(
        IRepository<Domain.Coaching.UserAggreagate.User> repository,
        IRepository<Domain.Coaching.RoleAggregate.Role> roleRepository,
        IRepository<UserPlant> rolePlantRepository,
        IRepository<Plant> plantRepository,
        IRepository<Country> countryRepository,
        ILogger<EditUserCommandHandler> logger)
    {
        _repository = repository;
        _roleRepository = roleRepository;
        _rolePlantRepository = rolePlantRepository;
        _plantRepository = plantRepository;
        _countryRepository = countryRepository;
        _logger = logger;
    }
    public async Task<ErrorOr<Unit>> Handle(EditUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            bool hasChanges = false;

            var user = await _repository.FirstOrDefaultAsync(new UserWhereSpecification(UserId.Create(request.Id), true));

            if (user is null)
                return Default.NotFound;

            //if (FullName.Create(request.Name, request.FirstSurname, request.SecondSurname) is not FullName name)
            //    return Errors.User.InvalidName;

            if (user.FullName != request.FullName)
            {
                user.UpdateName(request.FullName);
                hasChanges = true;
            }

            if (user.AccessType != (AccessType)int.Parse(request.AccessType))
            {
                user.UpdateAccessType((AccessType)int.Parse(request.AccessType));
                hasChanges = true;
            }

            if (user.TypeUser != (TypeUser)int.Parse(request.TypeUser))
            {
                user.UpdateTypeUser((TypeUser)int.Parse(request.TypeUser));
                hasChanges = true;
            }

            var rolesRequestIds = request.Roles
                .Select(x => RoleId.Create(x));

            if (!rolesRequestIds.All(x => user.RoleItems.Any(y => y.RoleId == x)) || !user.RoleItems.All(x => rolesRequestIds.Contains(x.RoleId)))
            {
                var roleIds = await _roleRepository.ListAsync(new RoleIdSpecification(rolesRequestIds));

                if (!rolesRequestIds.All(x => roleIds.Contains(x)))
                    return Errors.User.InvalidRole;

                var roles = rolesRequestIds
                    .Select(x => UserRole.Create(x))
                    .ToList();

                user.UpdateRoles(roles);

                hasChanges = true;
            }

            var countryRequestIds = request.Countries
                .Select(x => CountryId.Create(x));

            if (!countryRequestIds.All(x => user.CountryItems.Any(y => y.CountryId == x)) || !user.CountryItems.All(x => countryRequestIds.Contains(x.CountryId)))
            {
                var countryIds = await _countryRepository.ListAsync(new CountryIdSpecification(countryRequestIds));

                if (!countryRequestIds.All(x => countryIds.Contains(x)))
                    return Errors.User.InvalidCountry;

                var countries = countryRequestIds
                    .Select(x => UserCountry.Create(x))
                    .ToList();

                user.UpdateCountries(countries);

                hasChanges = true;
            }

            foreach (var role in rolesRequestIds)
            {
                var plantsRequestIds = request.Plants;

                //11:20 ya jala
                //11:26 añadimos && y.RoleId == role y && x.RoleId == role en ambas partes del if
                if (!plantsRequestIds.All(x => user.UserPlants.Any(y => y.PlantId == x)) || !user.UserPlants.All(x => plantsRequestIds.Contains(x.PlantId)))
                {
                    //var plantIds = await _rolePlantRepository.ListAsync(new RolePlantIdSpecification(plantsRequestIds));
                    var plantIds = await _plantRepository.ListAsync(new PlantIdSpecification(plantsRequestIds)); 

                    if (!plantsRequestIds.All(x => plantIds.Select(p => p.PlantId).Contains(x)))
                        return Errors.User.InvalidPlant;

                    var rolePlants = plantsRequestIds
                        .Select(x => UserPlant.Create(user.Id, x, false))
                        .ToList();

                    //await _rolePlantRepository.UpdateRangeAsync(rolePlants);
                    user.UpdatePlant(rolePlants);

                    hasChanges = true;
                }
            }

            if (hasChanges)
            {
                await _repository.UpdateAsync(user);
                await _repository.SaveChangesAsync();
            }

            return Unit.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error.");
            return Default.ServerError;
        }
    }
}
