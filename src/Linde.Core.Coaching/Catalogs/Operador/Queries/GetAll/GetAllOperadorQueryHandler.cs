using ErrorOr;
using Linde.Core.Coaching.Common.Models.Catalog.Operador;
using Linde.Core.Coaching.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Core.Coaching.Common.Interfaces.Services;
using Linde.Core.Coaching.Security.User.Queries.GetAll;
using MapsterMapper;
using Microsoft.Extensions.Logging;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Models.Security.User;
using Linde.Core.Coaching.Security.User.Specifications;
using Linde.Domain.Coaching.CountryAggregate.ValueObjects;
using Linde.Core.Coaching.Catalogs.Operador.Specifications;
using Linde.Core.Coaching.Common.Models.Catalogs.Questions;
using Linde.Domain.Coaching.Questionsaggregate.ValueObjects;

namespace Linde.Core.Coaching.Catalogs.Operador.Queries.GetAll
{
    internal class GetAllOperadorQueryHandler : IRequestHandler<GetAllOperadorQuery, ErrorOr<PaginatedListDto<OperadorDto>>>
    {
        private readonly IRepository<Domain.Coaching.UserAggreagate.User> _repository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<GetAllUserQuery> _logger;

        public GetAllOperadorQueryHandler(
        IRepository<Domain.Coaching.UserAggreagate.User> repository,
        IMapper mapper,
        ICurrentUserService currentUserService,
        ILogger<GetAllUserQuery> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _logger = logger;
        }

        public async Task<ErrorOr<PaginatedListDto<OperadorDto>>> Handle(GetAllOperadorQuery request, CancellationToken cancellationToken)
        {
             
            try
            {

                var total = await _repository.CountAsync(new OperatorMapSpecification(
                request.name!,
                request.lindeId!,
                request.noEmployee!,
                request.country!,
                request.plant!,
                request.Page,
                request.PageSize), cancellationToken);

                var pageSize = request.PageSize < 0 ? total : request.PageSize;

                var operators = await _repository.ListAsync(new OperatorMapSpecification(
                request.name!,
                request.lindeId!,             
                request.noEmployee!,
                request.country!,
                request.plant!,
                request.Page,
                pageSize,
                true), cancellationToken);                

                List<OperadorDto> response = _mapper.Map<List<OperadorDto>>(operators);

                return new PaginatedListDto<OperadorDto>(total, request.PageSize, request.Page, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error.");
                return Default.ServerError;
            }
        }
    }
}
