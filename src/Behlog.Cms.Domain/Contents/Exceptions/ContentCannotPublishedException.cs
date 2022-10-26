using Behlog.Core;
using Behlog.Cms.Domain;

namespace Behlog.Cms.Exceptions;

public class ContentCannotPublishedException : BehlogException
{

    public ContentCannotPublishedException(ContentStatus currentStatus)
        : base(message: $"The content cannot published because current status is : '{currentStatus.Title}'!")
    {
        CurrentContentStatus = currentStatus;
    }
    
    public ContentStatus CurrentContentStatus { get; }
}