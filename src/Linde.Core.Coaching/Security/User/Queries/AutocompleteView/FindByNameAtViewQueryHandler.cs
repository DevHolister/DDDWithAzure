using ErrorOr;
using Linde.Core.Coaching.Catalogs.Country.Specifications;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Core.Coaching.Common.Models;
using Linde.Core.Coaching.Common.Models.Security.User;
using Linde.Core.Coaching.Security.User.Specifications;
using Linde.Domain.Coaching.Views;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Linde.Core.Coaching.Security.User.Queries.AutocompleteView;

internal class FindByNameAtViewQueryHandler : IRequestHandler<FindByNameAtViewQuery, ErrorOr<List<UserQueryDto>>>
{
    private readonly ILogger<FindByNameAtViewQueryHandler> _logger;
    private readonly IRepository<VwEmpleado> _repository;
    private readonly IMapper _mapper;

    public FindByNameAtViewQueryHandler(ILogger<FindByNameAtViewQueryHandler> logger,
        IRepository<VwEmpleado> repository,
        IMapper mapper)
    {
        _logger = logger;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<List<UserQueryDto>>> Handle(FindByNameAtViewQuery request, CancellationToken cancellationToken)
    {
        try
        {
            //if (string.IsNullOrEmpty(request.fullName?.Trim()))
            //    return Default.ValidationError;
            //var user = await _repository.FirstOrDefaultAsync(new UserViewEspecification(request.fullName, true));
            //var item = _mapper.Map<UserQueryDto>(user!);
            //return item;

            var users = await _repository.ListAsync(new UserViewEspecification(request.fullName, true));
            var items = _mapper.Map<List<UserQueryDto>>(users!);
            return items;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error.");
            return Default.ServerError;
        }
    }
}
