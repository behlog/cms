using System;
using Behlog.Core;

namespace Behlog.Cms;

public class ContentStatus : Enumeration
{

    protected ContentStatus(int id, string name, string title = "")
        : base(id, name, title)
    {
    }

    public static ContentStatus Deleted =>
        new ContentStatus(-1, nameof(Deleted));

    public static ContentStatus Draft =>
        new ContentStatus(0, nameof(Draft));

    public static ContentStatus Published =>
        new ContentStatus(1, nameof(Published));

    public static ContentStatus Planned =>
        new ContentStatus(2, nameof(Planned));
}