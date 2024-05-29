using Linde.Domain.Coaching.Common;
using Linde.Domain.Coaching.CountryAggregate;
using Linde.Domain.Coaching.CountryAggregate.ValueObjects;
using Linde.Domain.Coaching.UserAggreagate;
using Linde.Domain.Coaching.UserAggreagate.ValueObjects;

namespace Linde.Domain.Coaching.Entities.Catalogs
{
    public class Plant: Entity
    {
        public List<UserPlant> UserPlants { get; set; }
        //public List<CoachingFormat> CoachingFormats { get; set; }
        public Guid PlantId { get; set; }
        public string Name { get; set; }
        public string Bu { get; set; }
        public CountryId CountryId { get; set; }
        public string Division { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Municipality { get; set; }
        public UserId SuperintendentId { get; set; }
        public UserId PlantManagerId { get; set; }
        public Country Country { get; set; }
        public User UserSuperIntendent { get; set; }
        public User UserManager { get; set; }

        private Plant(
            Guid plantId,
            string name,
            string bu,
            CountryId country,
            string division,
            string state,
            string city,
            string municipality,
            UserId superintendent,
            UserId plantManager
            )
        
        {
            PlantId = plantId;
            Name = name;
            Bu = bu;
            CountryId = country;
            Division = division;
            State = state;
            City = city;
            Municipality = municipality;
            SuperintendentId = superintendent;
            PlantManagerId = plantManager;
        }

        public static Plant Create(
            Guid plantId,
            string name,
            string bu,
            CountryId country,
            string division,
            string state,
            string city,
            string municipality,
            UserId superintendent,
            UserId plantManager
            )
        {
            return new(plantId, name, bu, country, division, state, city, municipality, superintendent, plantManager);
        }

        public static Plant Update(
            Guid plantId,
            string name,
            string bu,
            CountryId country,
            string division,
            string state,
            string city,
            string municipality,
            UserId superintendent,
            UserId plantManager
            )
        {
            return new(plantId, name, bu, country, division, state, city, municipality, superintendent, plantManager);
        }

        private Plant() { }

    }
}
