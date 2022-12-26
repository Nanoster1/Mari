namespace Mari.Contracts.Users.Responce;

public record UserResponce(
     int Id,
     string Username,
     string Role,
     List<string> Notifications,
     bool IsActive
);
