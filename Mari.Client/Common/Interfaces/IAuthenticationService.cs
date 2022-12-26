namespace Mari.Client.Common.Interfaces;

public interface IAuthenticationService
{
    void Authenticate();
    Task LoginAsync(string token);
    Task LogoutAsync();
}
