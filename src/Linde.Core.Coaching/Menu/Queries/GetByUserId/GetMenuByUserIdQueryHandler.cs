using ErrorOr;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Core.Coaching.Common.Interfaces.Services;
using Linde.Core.Coaching.Common.Models.Menu;
using Linde.Core.Coaching.Menu.Specifications;
using Linde.Core.Coaching.Security.Role.Commands.Delete;
using Linde.Core.Coaching.Security.Role.Specifications;
using Linde.Core.Coaching.Security.User.Specifications;
using Linde.Domain.Coaching.MenuAggregate;
using Linde.Domain.Coaching.RoleAggregate.ValueObjects;
using Linde.Domain.Coaching.UserAggreagate.ValueObjects;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Menu.Queries.GetByUserId
{
    public class GetMenuByUserIdQueryHandler : IRequestHandler<GetMenuByUserIdQuery, ErrorOr<List<MenuDTO>>>
    {
        private readonly ILogger<GetMenuByUserIdQueryHandler> _logger;
        private readonly IRepository<Domain.Coaching.UserAggreagate.User> _repositoryUser;
        private readonly ICurrentUserService _currentUserService;
        private readonly IConfiguration _configuration;
        private readonly IRepository<Domain.Coaching.RoleAggregate.Role> _menuRepository;
        private readonly IMapper _mapper;

        public GetMenuByUserIdQueryHandler(
            ILogger<GetMenuByUserIdQueryHandler> logger,
            IRepository<Domain.Coaching.UserAggreagate.User> repositoryUser,
            ICurrentUserService currentUserService,
            IConfiguration configuration,
            IRepository<Domain.Coaching.RoleAggregate.Role> menuRepository,
            IMapper mapper)
        {
            _logger = logger;
            _repositoryUser = repositoryUser;
            _currentUserService = currentUserService;
            _configuration = configuration;
            _menuRepository = menuRepository;
            _mapper = mapper;
        }
        public async Task<ErrorOr<List<MenuDTO>>> Handle(GetMenuByUserIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = Guid.Parse(_currentUserService.UserId);
                var user = await _repositoryUser.FirstOrDefaultAsync(new UserWhereSpecification(UserId.Create(userId), true));
                if(user is null)
                {
                    return Error.NotFound();
                }
                var menuList = await GetMenuAsync(user);
                IList<Domain.Coaching.MenuAggregate.Menu> resMenu = menuList.Where(y => y.ParentId == null && y.Visible).Distinct().ToList();
                List<MenuDTO> menus = _mapper.Map<List<MenuDTO>>(resMenu);
                return menus;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error.");
                return Default.ServerError;
            }
        }
        private async Task<List<Domain.Coaching.MenuAggregate.Menu>> GetMenuAsync(Domain.Coaching.UserAggreagate.User user)
        {
            List<Domain.Coaching.MenuAggregate.Menu> result = new();
            string NomAdminGral = _configuration["NomAdminGral"]; //rol de administrador general
            // string AdminMenu = _configuration["NameMenuAdmin"]; //Nombre del menu de administracion si es que se deba excluir            
            if(user?.RoleItems.Any() ?? false)
            {
                var roleIds = user.RoleItems.Select(t => t.RoleId).ToList();
                result = await this._menuRepository.ListAsync(new MenuByRoleSpecification(roleIds));
                result.RemoveAll(t => t == null);
                var childrens = result.Where(t => t.ParentId != null);
                foreach (var item in result)
                {
                    if(item.ParentId != null)
                    {
                        continue;
                    }
                    var children = childrens.Where(t => t.ParentId.Value == item.Id.Value).ToList();
                    if(children.Count > 0)
                        item.Attributes.AddRange(children);
                }

                return result;
            }
            return result;
        }
    }
}