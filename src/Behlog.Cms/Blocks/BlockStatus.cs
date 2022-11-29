using Behlog.Core;

namespace Behlog.Cms.Domain;

public class BlockStatus : Enumeration
{
    public BlockStatus(int id, string name, string title = "") : base(id, name, title)
    {
    }

    public static BlockStatus Deleted => new BlockStatus(-1, nameof(Deleted));

    public static BlockStatus Disabled => new BlockStatus(0, nameof(Disabled));


    public static BlockStatus Enabled => new BlockStatus(1, nameof(Enabled));
}