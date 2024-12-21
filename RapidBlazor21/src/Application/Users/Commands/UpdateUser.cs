using RapidBlazor21.Application.Common.Services.Identity;
using RapidBlazor21.WebUI.Shared.AccessControl;

namespace RapidBlazor21.Application.Users.Commands;

public record UpdateUserCommand(UserDto User) : IRequest;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly IIdentityService _identityService;

    public UpdateUserCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        await _identityService.UpdateUserAsync(request.User);
    }
}
