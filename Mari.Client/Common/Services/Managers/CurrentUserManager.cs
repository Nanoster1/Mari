using Mari.Client.Common.Interfaces.Managers;
using Mari.Client.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;

namespace Mari.Client.Common.Services.Managers;

public class CurrentUserManager : ICurrentUserManager
{
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public CurrentUserManager(AuthenticationStateProvider authenticationStateProvider)
    {
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<CurrentUserModel?> Get()
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        if (!(authState.User.Identity?.IsAuthenticated ?? false))
            return null;

        var userClaims = authState.User.Claims;
        return new CurrentUserModel
        {
            Id = int.Parse(userClaims.First(c => c.Type == CurrentUserModel.IdClaimType).Value),
            Name = userClaims.First(c => c.Type == CurrentUserModel.NameClaimType).Value,
            Role = Enum.Parse<UserRole>(userClaims.First(c => c.Type == CurrentUserModel.RoleClaimType).Value)
        };
    }
}
