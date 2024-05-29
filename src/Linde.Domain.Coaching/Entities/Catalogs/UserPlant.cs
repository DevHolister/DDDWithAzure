using Linde.Domain.Coaching.Common;
using Linde.Domain.Coaching.RoleAggregate;
using Linde.Domain.Coaching.RoleAggregate.ValueObjects;
using Linde.Domain.Coaching.UserAggreagate;
using Linde.Domain.Coaching.UserAggreagate.Entities;
using Linde.Domain.Coaching.UserAggreagate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Linde.Domain.Coaching.Entities.Catalogs
{
    public class UserPlant : Entity
    {
        public UserId UserId { get; private set; }
        public User User { get; private set; }
        public Guid PlantId { get; private set; }
        public Plant Plant { get; private set; }
        public bool isOperator { get; private set; }
        private UserPlant(UserId userId, Guid plantId, bool isoperator)
        : base()
        {
            UserId = userId;
            PlantId = plantId;
            isOperator = isoperator;
        }

        public static UserPlant Create(UserId userId, Guid plantId, bool isOperator)
        {
            return new( userId, plantId, isOperator);
        }       

        private UserPlant() { }

        public void UpdatePlant(Guid newplant)
        {
            PlantId = newplant;
        }
        public void UpdateOperator(bool isoperator)
        {
            isOperator = isoperator;
        }
    }
}
