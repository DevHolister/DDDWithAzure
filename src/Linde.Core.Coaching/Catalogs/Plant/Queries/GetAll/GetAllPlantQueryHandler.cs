using ErrorOr;
using Linde.Core.Coaching.Catalogs.Operador.Specifications;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Core.Coaching.Common.Interfaces.Services;
using Linde.Core.Coaching.Common.Models.Catalog.Operador;
using Linde.Core.Coaching.Common.Models;
using Linde.Core.Coaching.Security.User.Queries.GetAll;
using MapsterMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Linde.Core.Coaching.Common.Models.Catalog.Plant;
using Linde.Core.Coaching.Catalogs.Plant.Specifications;

namespace Linde.Core.Coaching.Catalogs.Plant.Queries.GetAll
{
    internal class GetAllPlantQueryHandler : IRequestHandler<GetAllPlantQuery, ErrorOr<PaginatedListDto<PlantDTO>>>
    {
        private readonly IRepository<Domain.Coaching.Entities.Catalogs.Plant> _repository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<GetAllPlantQuery> _logger;      

        public GetAllPlantQueryHandler(
    IRepository<Domain.Coaching.Entities.Catalogs.Plant> repository,
    IMapper mapper,
    ICurrentUserService currentUserService,
    ILogger<GetAllPlantQuery> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _logger = logger;
        }
        public async Task<ErrorOr<PaginatedListDto<PlantDTO>>> Handle(GetAllPlantQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var operators = await _repository.ListAsync(new PlantMapSpecification(
                request.name!,
                request.bu!,
                request.CountryId!,
                request.Division!,
                request.State,
                request.Page,
                request.PageSize,
                true), cancellationToken);

                var total = await _repository.CountAsync(new PlantMapSpecification(
                    request.name!,
                    request.bu!,
                    request.CountryId!,
                    request.Division!,
                    request.State!),
                    cancellationToken);

                return new PaginatedListDto<PlantDTO>(total, request.PageSize, request.Page, operators);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error.");
                return Default.ServerError;
            }
        }
    }

    

    

   
}
