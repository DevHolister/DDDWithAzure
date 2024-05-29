using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Common.Models.Catalog.Plant
{
    public record PlantDTO(
        string PlantId,
        string Name,
        string Bu,
        string CountryId,
        string CountryName,
        string Division,
        string State,
        string City,
        string Municipality,
        string SuperintendentId,
        string SuperintendentName,
        string PlantManagerId,
        string PlantManagerName
        );
}
