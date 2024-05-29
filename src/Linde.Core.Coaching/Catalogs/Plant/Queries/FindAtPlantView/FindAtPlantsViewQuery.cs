using ErrorOr;
using Linde.Core.Coaching.Common.Models.Catalog.Plant;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Catalogs.Plant.Queries.FindAtPlantView;

public class FindAtPlantsViewQuery : IRequest<ErrorOr<List<PlantDTO>>>
{
    public string Code { get; set; }
}
