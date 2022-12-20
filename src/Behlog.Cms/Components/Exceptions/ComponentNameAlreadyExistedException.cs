using Behlog.Core;

namespace Behlog.Cms.Exceptions;

public class ComponentNameAlreadyExistedException : BehlogException
{
    public ComponentNameAlreadyExistedException(Guid websiteId, string componentName)
        : base(message: $"The Website with Id: [{websiteId}] already has a component with name '{componentName}'. The Component's Name must be unique across a Website.")
    {
        WebsiteId = websiteId;
        ComponentName = componentName;
    }
    
    public Guid WebsiteId { get; }
    public string ComponentName { get; }
}