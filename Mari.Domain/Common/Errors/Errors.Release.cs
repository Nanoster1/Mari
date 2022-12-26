using ErrorOr;

namespace Mari.Domain.Common;

public partial class Errors
{
    public class Release
    {
        public static readonly Error VersionMustBeGreaterThanCurrent = Error.Validation(
            description: "Release version must be greater than current version");

        public static readonly Error CompleteDateMustBeGreaterThanCurrent = Error.Validation(
            description: "Release complete date must be greater than current date");

        public static readonly Error NonDraftReleaseFieldsCannotBeNull = Error.Validation(
            description: "Non draft release fields cannot be null");

        public static readonly Error ReleaseNotFound = Error.NotFound(
            description: "Release not found");

        public static readonly Error ReleaseMustBeDraft = Error.Conflict(
            description: "Release must be in draft status");

        public static readonly Error ReleaseIsReadOnly = Error.Conflict(
            description: "Release is read only");

        public static readonly Error NewReleaseStatusMustBeDraftOrPlanning = Error.Validation(
            description: "New release status must be draft or planning");
    }
}
