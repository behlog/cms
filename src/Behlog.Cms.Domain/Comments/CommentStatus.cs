using Behlog.Core;

namespace Behlog.Cms.Domain;

public class CommentStatus : Enumeration
{
    public CommentStatus(int id, string name, string title = "") 
        : base(id, name, title)
    {
    }

    public static CommentStatus Find(int id) => FromValue<CommentStatus>(id);

    public static CommentStatus Deleted
        = new CommentStatus(-1, nameof(Deleted));

    public static CommentStatus Created
        = new CommentStatus(0, nameof(Created));

    public static CommentStatus Approved
        = new CommentStatus(1, nameof(Approved));

    public static CommentStatus Rejected
        = new CommentStatus(2, nameof(Rejected));

    public static CommentStatus Spam
        = new CommentStatus(3, nameof(Spam));

    public static CommentStatus Blocked
        = new CommentStatus(4, nameof(Blocked));
}