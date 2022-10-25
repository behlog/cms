using Behlog.Core;
using Behlog.Extensions;
using Behlog.Cms.Domain.Events;
using Behlog.Core.Domain;

namespace Behlog.Cms.Domain;

public partial class ContentCategory : BehlogEntity<Guid> 
{

    private ContentCategory()
    {
    }
    
    protected ContentCategory(CreateContentCategoryArg args)
    {
        if(args is null) throw new ArgumentNullException(nameof(args));
        checkRequiredFields(args);

        // Id = Guid.NewGuid();
        Title = args.Title;
        AltTitle = args.AltTitle;
        Slug = args.Slug;
        ParentId = args.ParentId;
        Description = args.Description;
        ContentTypeId = args.ContentTypeId;
        Status = EntityStatus.Enabled;
    }


    #region Methods

    public static async Task<ContentCategory> CreateAsync(CreateContentCategoryArg args)
    {
        var category = new ContentCategory(args);
        await category.publishCreatedEvent();
        return category;
    }

    public async Task UpdateAsync(UpdateContentCategoryArg args) {
        checkRequiredFields(args);
        Title = args.Title;
        Slug = args.Slug;
        ParentId = args.ParentId;
        Description = args.Description;
        ContentTypeId = args.ContentTypeId;

        await this.publishUpdatedEvent();

    }

    #endregion

    #region Props
    public string Title { get; protected set; }
    public string AltTitle { get; protected set; }
    private string _slug;
    public string Slug 
    {
        get => _slug;
        protected set 
        {
            _slug = value;
            if(_slug.IsNullOrEmpty() && Title.IsNotNullOrEmpty())
                _slug = Title.MakeSlug();
        }
    }
    public Guid? ParentId { get; protected set; }
    public string Description { get; protected set; }
    public Guid? ContentTypeId { get; protected set; }
    public EntityStatus Status { get; protected set; }

    #endregion

    #region Events 

    private async Task publishCreatedEvent() 
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
        // await _mediator.PublishAsync(e);
    }

    private async Task publishUpdatedEvent() 
    {
        var e = new ContentCategoryUpdatedEvent(
            id: Id,
            title: Title,
            altTitle: AltTitle,
            slug: Slug,
            parentId: ParentId,
            description: Description,
            contentTypeId: ContentTypeId,
            status: Status
        );
        // await _mediator.PublishAsync(e);
    }

    #endregion
}