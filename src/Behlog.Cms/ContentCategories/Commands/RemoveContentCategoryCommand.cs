namespace Behlog.Cms.Commands;

public class RemoveContentCategoryCommand : IBehlogCommand
{
    public RemoveContentCategoryCommand(Guid categoryId)
    {
        Id = categoryId;
    }
    
    public Guid Id { get; }
}