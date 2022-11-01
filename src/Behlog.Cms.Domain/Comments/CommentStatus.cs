using Behlog.Core;

namespace Behlog.Cms.Domain;

public class CommentStatus : Enumeration
{
    private CommentStatus(int id, string name, string title = "") 
        : base(id, name, title)
    {
        if (id == Approved.Id && !CanApproved())
            throw new Exception("Comment cannot be approved!");

        if (id == Rejected.Id && !CanRejected())
            throw new Exception("Comment cannot be rejected!");
    }

    public static CommentStatus Find(int id) => FromValue<CommentStatus>(id);

    public static CommentStatus Deleted = new(-1, nameof(Deleted));

    public static CommentStatus Created = new(0, nameof(Created));

    public static CommentStatus Approved = new(1, nameof(Approved));

    public static CommentStatus Rejected = new(2, nameof(Rejected));

    public static CommentStatus Spam = new(3, nameof(Spam));

    public static CommentStatus Blocked = new(4, nameof(Blocked));


    private bool CanApproved()
    {
        return this != Deleted &&
               this != Spam &&
               this != Blocked;
    }

    private bool CanRejected()
    {
        return this != Deleted &&
               this != Spam &&
               this != Blocked;
    }
    
}