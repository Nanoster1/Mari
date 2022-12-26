namespace Mari.Client.Models.Releases;

public class ReleaseModel
{
    public Guid Id { get; set; }
    public VersionModel Version { get; set; } = new();
    public string PlatformName { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTimeOffset? CompleteDate { get; set; } = DateTime.Now.AddDays(10);
    public DateTimeOffset? UpdateDate { get; set; } = null;
    public string MainIssue { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public DateTime? CompleteDateTime { get => CompleteDate?.DateTime; set => CompleteDate = value; }
    public DateTime? UpdateDateTime { get => UpdateDate?.DateTime; set => UpdateDate = value; }

}
