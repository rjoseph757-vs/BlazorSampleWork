using RapidBlazor21.Application.Common.Services.Identity;
using RapidBlazor21.WebUI.Shared.AccessControl;

namespace RapidBlazor21.Application.Roles.Commands;

public record CreateRoleCommand(RoleDto Role) : IRequest;

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand>
{
    private readonly IIdentityService _identityService;

    public CreateRoleCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        await _identityService.CreateRoleAsync(request.Role);
    }
}
