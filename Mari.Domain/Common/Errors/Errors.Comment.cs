using ErrorOr;

namespace Mari.Domain.Common;

public partial class Errors
{
    public class Comment
    {
        public static readonly Error BlockedBySystem = Error.Failure(description: "Comment is blocked by system");
        public static readonly Error NotFound = Error.Failure(description: "Comment not found");
    }
}
