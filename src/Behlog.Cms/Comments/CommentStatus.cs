namespace Behlog.Cms.Domain;

public enum CommentStatusEnum
{
    Deleted = -1,
    Created = 0,
    Approved = 1,
    Rejected = 2,
    Spam = 3,
    Blocked = 4
}