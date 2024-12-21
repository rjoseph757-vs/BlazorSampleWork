using Microsoft.AspNetCore.Identity;
using RapidBlazor21.WebUI.Shared.Authorization;

namespace RapidBlazor21.Infrastructure.Identity;

public class ApplicationRole : IdentityRole
{
    public Permissions Permissions { get; set; }
}
