using System;
using Behlog.Core;

namespace Behlog.Cms;

public class ContentCategory : AggregateRoot<Guid> 
{

    protected ContentCategory(CreateContentCategoryArg args) : base()
    {
        if(args is null) throw new ArgumentNullException(nameof(args));

        Id = Guid.NewGuid();
        Title = args.Title;
        AltTitle = args.AltTitle;
        Slug = args.Slug;
        ParentId = args.ParentId;
        Description = args.Description;
        ContentTypeId = args.ContentTypeId;
        Status = EntityStatus.Enabled;
    }


    #region Methods

    public static ContentCategory Create(CreateContentCategoryArg args)
    {
        var category = new ContentCategory(args);
        return category;
    }

    #endregion

    #region Props
    public string Title { get; }
    public string AltTitle { get; }
    public string Slug { get; }
    public Guid? ParentId { get; }
    public string Description { get; }
    public Guid? ContentTypeId { get; }
    public EntityStatus Status { get; }
    #endregion
}