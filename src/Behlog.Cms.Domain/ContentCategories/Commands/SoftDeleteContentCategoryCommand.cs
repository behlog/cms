using Behlog.Core;

namespace Behlog.Cms.Commands;

public class SoftDeleteContentCategoryCommand : IBehlogCommand
{

    public SoftDeleteContentCategoryCommand(Guid contentCategoryId)
    {
        Id = contentCategoryId;
    }
    
    public Guid Id { get; }
}