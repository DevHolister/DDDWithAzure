using ErrorOr;
using Linde.Core.Coaching.Catalogs.Country.Specifications;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Core.Coaching.Common.Models;
using Linde.Core.Coaching.Common.Models.Catalogs.Country;
using Linde.Core.Coaching.Common.Models.Security.User;
using Linde.Domain.Coaching.CountryAggregate.ValueObjects;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Catalogs.Country.Queries.FindById;

internal class FindByIdQueryHandler : IRequestHandler<FindByIdQuery, ErrorOr<CountryDto>>
{
    private readonly IRepository<Domain.Coaching.CountryAggregate.Country> _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<FindByIdQueryHandler> _logger;

    public FindByIdQueryHandler(IRepository<Domain.Coaching.CountryAggregate.Country> repository, IMapper mapper, ILogger<FindByIdQueryHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<ErrorOr<CountryDto>> Handle(FindByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var country = await _repository.FirstOrDefaultAsync(new CountryWhereSpecification(CountryId.Create(request.CountryId!)));
            if (country is null)
                return Errors.Country.CountryNoExists;
            var item = _mapper.Map<CountryDto>(country);
            return item;
            //throw new NotImplementedException();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error.");
            return Default.ServerError;
        }
    }
}
