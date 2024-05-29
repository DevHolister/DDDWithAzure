using Linde.Domain.Coaching.Common;
using Linde.Domain.Coaching.Common.Enum;
using Linde.Domain.Coaching.Entities.Catalogs;
using Linde.Domain.Coaching.UserAggreagate.Entities;
using Linde.Domain.Coaching.UserAggreagate.ValueObjects;

namespace Linde.Domain.Coaching.UserAggreagate;

public sealed class User : Entity<UserId>, IAggregateRoot
{
    private readonly List<UserRole> _roleItems = new();
    private readonly List<UserCountry> _countryItems = new();
    public IReadOnlyList<UserRole> RoleItems => _roleItems.AsReadOnly();
    public IReadOnlyList<UserCountry> CountryItems => _countryItems.AsReadOnly();

    //public FullName FullName { get; private set; }
    public string FullName { get; private set; }
    public bool Lockout { get; private set; }
    public string UserName { get; private set; }
    public string Email { get; private set; }
    public int AccessFailedCount { get; private set; }
    public TypeUser TypeUser { get; private set; }
    public AccessType AccessType { get; private set; }
    public int ZoneId { get; set; }
    public string EmployeeNumber { get; set; }

    //public List<CoachingFormat> Coachs { get; set; }
    //public List<CoachingFormat> Operators { get; set; }
    //public List<CoachingFormat> SignCoachs { get; set; }
    //public List<CoachingFormat> SignOperators { get; set; }
    //public List<CoachingFormat> FinalSignCoach { get; set; }
    public List<UserPlant> UserPlants { get; set; }
    public List<Plant> PlantSuperIntendet { get; private set; }
    public List<Plant> PlantManager { get; private set; }
    private User(
        UserId userId,
        //FullName name,
        string name,
        string userName,
        string email) : base(userId)
    {
        FullName = name;
        UserName = userName;
        Email = email;
        Lockout = false;
        Visible = true;
    }

    public static User Create(
        //FullName name,
        string name,
        string userName,
        string email)
    {
        return new(
            UserId.CreateUnique(),
            name,
            userName,
            email);
    }

    private User() { }

    public void AddCountry(UserCountry country)
    {
        if (!_countryItems.Contains(country))
            _countryItems.Add(country);
    }
    public void UpdateCountries(List<UserCountry> countries)
    {
        _countryItems.RemoveAll(x => !countries.Contains(x));
        countries.ForEach(x => AddCountry(x));
    }

    public void CreateAccessType(AccessType accessType)
    {
        AccessType = accessType;
    }
    public void UpdateAccessType(AccessType accessType)
    {
        AccessType = accessType;
    }

    public void UpdateTypeUser(TypeUser type)
    {
        TypeUser = type;
    }

    public void AddPlant(UserPlant plantItem)
    {
        if (!UserPlants.Contains(plantItem))
            UserPlants.Add(plantItem);
        //return plantItem;
    }
    public void UpdatePlant(List<UserPlant> plants)
    {
        UserPlants.RemoveAll(x => !plants.Contains(x));
        plants.ForEach(x => AddPlant(x));
    }

    public void IncreaseAccessFailed(int accessFailedAllowed)
    {
        AccessFailedCount++;

        if (AccessFailedCount >= accessFailedAllowed)
            Lockout = true;
    }

    public void ResetAccessFailed()
    {
        AccessFailedCount = 0;
    }

    public void UpdateName(string name)//FullName name)
    {
        FullName = name;
    }

    public void AddRole(UserRole role)
    {
        if (!_roleItems.Contains(role))
            _roleItems.Add(role);
    }

    public void UpdateRoles(List<UserRole> roles)
    {
        _roleItems.RemoveAll(x => !roles.Contains(x));
        roles.ForEach(x => AddRole(x));
    }

    /// <summary>
    /// True para desbloquear usuario, False para bloquear.
    /// </summary>
    /// <param name="lockout"></param>
    public void SetLockout(bool lockout)
    {
        Lockout = !lockout;
    }
}
