using Behlog.Core;

namespace Behlog.Cms.Exceptions;


public class ContentTypeSystemNameExistedException : BehlogException
{

    public ContentTypeSystemNameExistedException(string contentTypeSystemName)
        : base($"The ContentType with SystemName : '{contentTypeSystemName}' already existed.")
    {
        ContentTypeSystemName = contentTypeSystemName;
    }
    
    
    public string ContentTypeSystemName { get; }
}