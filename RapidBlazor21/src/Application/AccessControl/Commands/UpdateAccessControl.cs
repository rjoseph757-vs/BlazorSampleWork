using RapidBlazor21.Application.Common.Services.Identity;
using RapidBlazor21.WebUI.Shared.Authorization;

namespace RapidBlazor21.Application.AccessControl.Commands;

public record UpdateAccessControlCommand(string RoleId, Permissions Permissions) : IRequest;

public class UpdateAccessControlCommandHandler : IRequestHandler<UpdateAccessControlCommand>
{
    private readonly IIdentityService _identityService;

    public UpdateAccessControlCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task Handle(UpdateAccessControlCommand request, CancellationToken cancellationToken)
    {
        await _identityService.UpdateRolePermissionsAsync(request.RoleId, request.Permissions);
    }
}
