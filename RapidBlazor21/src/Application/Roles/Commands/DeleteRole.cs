﻿using RapidBlazor21.Application.Common.Services.Identity;

namespace RapidBlazor21.Application.Roles.Commands;

public record DeleteRoleCommand(string RoleId) : IRequest;

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand>
{
    private readonly IIdentityService _identityService;

    public DeleteRoleCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        await _identityService.DeleteRoleAsync(request.RoleId);
    }
}
