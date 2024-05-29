using ErrorOr;
using Linde.Core.Coaching.Catalogs.Country.Queries.GetEnumerable;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Core.Coaching.Common.Models;
using Linde.Core.Coaching.Common.Models.Catalog.Plant;
using Linde.Core.Coaching.Common.Models.Catalogs.Country;
using Linde.Domain.Coaching.CountryAggregate.ValueObjects;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Linde.Core.Coaching.Catalogs.Plant.Queries.GetEnumerable;

internal class GetEnumerablePlantQueryHandler : IRequestHandler<GetEnumerablePlantQuery, ErrorOr<IEnumerable<ItemDto>>>
{
    private readonly IRepository<Domain.Coaching.Entities.Catalogs.Plant> _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetEnumerablePlantQueryHandler> _logger;

    public GetEnumerablePlantQueryHandler(IRepository<Domain.Coaching.Entities.Catalogs.Plant> repository,
        IMapper mapper,
        ILogger<GetEnumerablePlantQueryHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<ErrorOr<IEnumerable<ItemDto>>> Handle(GetEnumerablePlantQuery request, CancellationToken cancellationToken)
    {
        try
        {
            //De un solo Pais
            //var plants = await _repository.ListAsync();
            //var items = _mapper.Map<List<ItemDto>>(plants.Where(x => x.CountryId.Equals(CountryId.Create(request.CountryId))));
            //return items;

            //Con Lista de Paises
            var plants = await _repository.ListAsync();
            var items = new List<ItemDto>();

            //Para pruebas
            //var countryLst = new List<Guid>
            //{
            //    Guid.Parse("096D2C19-FC8D-4AD2-A585-99F42F531DED"),
            //    Guid.Parse("7AA9DE94-F24D-46F3-9E2A-6292F90BC36A")
            //};

            if (request.CountryId != null)
            {
                foreach (var country in request.CountryId) //countryLst)
                {
                    var itemLst = _mapper.Map<List<ItemDto>>(plants.Where(x => x.CountryId.Equals(CountryId.Create(country))));

                    foreach (var ctry in itemLst)
                    {
                        items.Add(ctry);
                    }
                }
            }
            return items;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error.");
            return Default.ServerError;
        }
    }
}