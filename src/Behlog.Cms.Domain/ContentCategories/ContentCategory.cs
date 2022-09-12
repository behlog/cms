using System;
using System.Collections.Generic;
using iman.Domain;
using Behlog.Core;
using Behlog.Extensions;
using Behlog.Cms.Domain.Events;

namespace Behlog.Cms.Domain;

public partial class ContentCategory : AggregateRoot<Guid> 
{

    protected ContentCategory(CreateContentCategoryArg args) : base()
    {
        if(args is null) throw new ArgumentNullException(nameof(args));
        checkRequiredFields(args);

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
        category.publishCreatedEvent();
        return category;
    }


    #endregion

    #region Props
    public string Title { get; }
    public string AltTitle { get; }
    private string _slug;
    public string Slug 
    {
        get => _slug;
        set 
        {
            _slug = value;
            if(_slug.IsNullOrEmpty() && Title.IsNotNullOrEmpty())
                _slug = Title.MakeSlug();
        }
    }
    public Guid? ParentId { get; }
    public string Description { get; }
    public Guid? ContentTypeId { get; }
    public EntityStatus Status { get; }

    #endregion

    #region Events 

    private void publishCreatedEvent() 
    {
        var e = new ContentCategoryCreatedEvent(
            id: Id,
            title: Title,
            altTitle: AltTitle,
            slug: Slug,
            parentId: ParentId,
            description: Description,
            contentTypeId: ContentTypeId,
            status: Status
        );
        //TODO : publish event
    }

    private void publishUpdatedEvent(UpdateContentCategoryArg args) 
    {

    }

    #endregion
}