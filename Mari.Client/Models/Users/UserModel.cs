namespace Mari.Client.Models.Users;

public class UserModel
{
    public int Id { get; set; }
    public string Username { get; set; } = String.Empty;
    public UserRole Role { get; set; } 
    public List<string> Notifications { get; set; }
    public bool IsActive { get; set; }
}
