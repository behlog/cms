using Behlog.Core;
using Behlog.Cms.Domain;

namespace Behlog.Cms.Exceptions;

public class ContentCannotPublishedException : BehlogException
{

    public ContentCannotPublishedException(ContentStatusEnum currentStatus)
        : base(message: $"The content cannot published because current status is : '{(int)currentStatus}'!")
    {
        CurrentContentStatus = currentStatus;
    }
    
    public ContentStatusEnum CurrentContentStatus { get; }
}