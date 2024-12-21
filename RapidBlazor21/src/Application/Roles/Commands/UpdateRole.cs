using RapidBlazor21.Application.Common.Services.Identity;
using RapidBlazor21.WebUI.Shared.AccessControl;

namespace RapidBlazor21.Application.Roles.Commands;

public record UpdateRoleCommand(RoleDto Role) : IRequest;

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand>
{
    private readonly IIdentityService _identityService;

    public UpdateRoleCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        await _identityService.UpdateRoleAsync(request.Role);
    }
}
