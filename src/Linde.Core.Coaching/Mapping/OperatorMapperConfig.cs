using Linde.Core.Coaching.Common.Models.Catalogs.Questions;
using Linde.Core.Coaching.Common.Models;
using Linde.Domain.Coaching.Questions;
using Linde.Domain.Coaching.Questionsaggregate.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linde.Domain.Coaching.Entities.Catalogs;
using Linde.Core.Coaching.Common.Models.Catalog.Operador;

namespace Linde.Core.Coaching.Mapping
{
    public class OperatorMapperConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<UserPlant, OperadorDto>()
                .Map(x => x.Name, src => src.UserId.Value)
                .Map(x => x.LindeId, src => src.User.FullName)
                .Map(x => x.NoEmployee, src => src.User.EmployeeNumber);
        }
    }
}
