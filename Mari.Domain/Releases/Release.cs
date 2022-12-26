using ErrorOr;
using Mari.Domain.Common.Models;
using Mari.Domain.Releases.Entities;
using Mari.Domain.Releases.Enums;
using Mari.Domain.Releases.ValueObjects;
using Mari.Domain.Common;

namespace Mari.Domain.Releases;

public class Release : AggregateRoot<ReleaseId>
{
    public static ErrorOr<Release> Create(
        Issue? mainIssue,
        Platform? platform,
        ReleaseCompleteDate? completeDate,
        DateTimeOffset currentDate,
        ReleaseStatus? status = null,
        ReleaseDescription? description = null,
        ReleaseVersion? version = null)
    {
        if (status is not ReleaseStatus.Draft and not ReleaseStatus.Planning)
            return Errors.Release.NewReleaseStatusMustBeDraftOrPlanning;

        var release = new Release(
            mainIssue: mainIssue,
            platform: platform,
            completeDate: completeDate,
            updateDate: ReleaseUpdateDate.Create(currentDate),
            status: status ?? ReleaseStatus.Draft,
            description: description ?? ReleaseDescription.Default,
            version: version ?? ReleaseVersion.MinValue
        );

        var result = release.CheckDraftStatus();
        if (result.IsError) return result.Errors;
        return release;
    }

    private Release()
    {
    }

    private Release(
        Issue? mainIssue,
        ReleaseVersion version,
        Platform? platform,
        ReleaseCompleteDate? completeDate,
        ReleaseUpdateDate updateDate,
        ReleaseStatus status,
        ReleaseDescription description)
    {
        Version = version;
        Platform = platform!;
        Status = status;
        CompleteDate = completeDate!;
        UpdateDate = updateDate;
        MainIssue = mainIssue!;
        Description = description;
    }

    public ReleaseVersion Version { get; private set; } = null!;
    public Platform Platform { get; private set; } = null!;
    public ReleaseStatus Status { get; private set; }
    public ReleaseCompleteDate CompleteDate { get; private set; } = null!;
    public ReleaseUpdateDate UpdateDate { get; private set; } = null!;
    public ReleaseDescription Description { get; private set; } = null!;
    public Issue MainIssue { get; private set; } = null!;

    public bool IsReadOnly => Status is ReleaseStatus.Complete;

    public ErrorOr<Updated> ChangeMainIssue(Issue mainIssue, DateTimeOffset currentDateTime)
    {
        MainIssue = mainIssue;
        return ChangeUpdateDate(currentDateTime);
    }

    public ErrorOr<Updated> ChangeDescription(ReleaseDescription description, DateTimeOffset currentDateTime)
    {
        Description = description;
        return ChangeUpdateDate(currentDateTime);
    }

    public ErrorOr<Updated> ChangeVersion(ReleaseVersion version, DateTimeOffset currentDateTime)
    {
        Version = version;
        return ChangeUpdateDate(currentDateTime);
    }

    public ErrorOr<Updated> ChangeCompleteDate(ReleaseCompleteDate completeDate, DateTimeOffset currentDateTime)
    {
        CompleteDate = completeDate;
        return ChangeUpdateDate(currentDateTime);
    }

    public ErrorOr<Updated> ChangePlatform(Platform platform, DateTimeOffset currentDateTime)
    {
        Platform = platform;
        return ChangeUpdateDate(currentDateTime);
    }

    public ErrorOr<Updated> ChangeStatus(ReleaseStatus status, DateTimeOffset currentDateTime)
    {
        Status = status;
        return ChangeUpdateDate(currentDateTime);
    }

    public ErrorOr<Success> CreateFromDraft()
    {
        Status = ReleaseStatus.Planning;
        return Result.Success;
    }

    private ErrorOr<Updated> ChangeUpdateDate(DateTimeOffset currentDateTime)
    {
        UpdateDate = ReleaseUpdateDate.Create(currentDateTime);
        return Result.Updated;
    }

    private ErrorOr<Success> CheckDraftStatus()
    {
        return Result.Success;
    }
}
