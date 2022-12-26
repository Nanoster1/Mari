using Mari.Domain.Common.Models;
using Throw;

namespace Mari.Domain.Releases.ValueObjects;

public record ReleaseVersion : ValueObject, IComparable<ReleaseVersion>
{
    #region Constants
    public const int MaxMajor = int.MaxValue - 1;
    public const int MaxMinor = int.MaxValue - 1;
    public const int MaxPatch = int.MaxValue - 1;
    public const int MinMajor = 0;
    public const int MinMinor = 0;
    public const int MinPatch = 0;
    #endregion

    public static readonly ReleaseVersion MinValue = ReleaseVersion.Create(MinMajor, MinMinor, MinPatch + 1);
    public static readonly ReleaseVersion MaxValue = ReleaseVersion.Create(MaxMajor, MaxMinor, MaxPatch);

    public static ReleaseVersion Create(int major, int minor, int patch)
    {
        return new ReleaseVersion(major, minor, patch);
    }

    private ReleaseVersion(int major, int minor, int patch)
    {
        Major = major;
        Minor = minor;
        Patch = patch;
    }

    public int Major { get; private set; }
    public int Minor { get; private set; }
    public int Patch { get; private set; }

    public int CompareTo(ReleaseVersion? other)
    {
        other.ThrowIfNull();

        var firstPartCompareResult = Major.CompareTo(other.Minor);
        if (firstPartCompareResult != 0) return firstPartCompareResult;

        var secondPartCompareResult = Minor.CompareTo(other.Minor);
        if (secondPartCompareResult != 0) return secondPartCompareResult;

        return Patch.CompareTo(other.Patch);
    }

    public string ToVersionString() => $"{Major}.{Minor}.{Patch}";

    #region Operators
    public static bool operator >(ReleaseVersion left, ReleaseVersion right) => left.CompareTo(right) > 0;
    public static bool operator <(ReleaseVersion left, ReleaseVersion right) => left.CompareTo(right) < 0;
    public static bool operator <=(ReleaseVersion left, ReleaseVersion right) => left.CompareTo(right) <= 0;
    public static bool operator >=(ReleaseVersion left, ReleaseVersion right) => left.CompareTo(right) >= 0;
    #endregion
}
