using Microsoft.AspNetCore.Mvc;
using RapidBlazor21.Application.AccessControl.Commands;
using RapidBlazor21.Application.AccessControl.Queries;
using RapidBlazor21.WebUI.Shared.AccessControl;
using RapidBlazor21.WebUI.Shared.Authorization;

namespace RapidBlazor21.WebUI.Server.Controllers.Admin;

[Route("api/Admin/[controller]")]
public class AccessControlController : ApiControllerBase
{
    [HttpGet]
    [Authorize(Permissions.ViewAccessControl)]
    public async Task<ActionResult<AccessControlVm>> GetConfiguration()
    {
        return await Mediator.Send(new GetAccessControlQuery());
    }

    [HttpPut]
    [Authorize(Permissions.ConfigureAccessControl)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateConfiguration(RoleDto updatedRole)
    {
        await Mediator.Send(new UpdateAccessControlCommand(updatedRole.Id, updatedRole.Permissions));

        return NoContent();
    }
}
