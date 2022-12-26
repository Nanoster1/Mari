using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Mari.Client.Models.Users;

public class CurrentUserModel
{
    public const string IdClaimType = JwtRegisteredClaimNames.Sub;
    public const string NameClaimType = JwtRegisteredClaimNames.Name;
    public const string RoleClaimType = ClaimTypes.Role;

    public required int Id { get; init; }
    public required string Name { get; init; }
    public required UserRole Role { get; init; }
}
