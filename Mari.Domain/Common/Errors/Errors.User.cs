using ErrorOr;

namespace Mari.Domain.Common;

public static partial class Errors
{
    public static class User
    {
        public static readonly Error UserNotFound = Error.NotFound(description: "User not found");
        public static readonly Error UserAlreadyExists = Error.Conflict(description: "User already exists");
        public static readonly Error UserIsBlocked = Error.Failure(description: "User is blocked");
    }
}
