using Ardalis.Specification;
using Linde.Domain.Coaching.Entities.Catalogs;
using Linde.Domain.Coaching.Questionsaggregate.ValueObjects;
using Linde.Domain.Coaching.RoleAggregate.ValueObjects;
using Linde.Domain.Coaching.UserAggreagate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Catalogs.Operador.Specifications
{
    internal class OperadorWhereSpecification : Specification<Domain.Coaching.Entities.Catalogs.UserPlant>
    {       
        public OperadorWhereSpecification(UserId userId, Guid plantId)
        {
            Query
                .Where(x => x.UserId == userId && x.PlantId == plantId);
        }
        public OperadorWhereSpecification(UserId userId)
        {
            Query
                .Where(x => x.UserId == userId);
        }
    }
}
