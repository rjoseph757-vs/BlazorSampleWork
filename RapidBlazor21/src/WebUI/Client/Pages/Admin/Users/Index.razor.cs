using Microsoft.AspNetCore.Components;
using RapidBlazor21.WebUI.Shared.AccessControl;

namespace RapidBlazor21.WebUI.Client.Pages.Admin.Users;

public partial class Index
{
    [Inject]
    public IUsersClient UsersClient { get; set; } = null!;

    public UsersVm? Model { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Model = await UsersClient.GetUsersAsync();
    }
}
