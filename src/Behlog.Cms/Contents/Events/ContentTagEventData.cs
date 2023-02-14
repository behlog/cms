namespace Behlog.Cms.Events;

public class ContentTagEventData : BehlogDomainEvent
{

	public ContentTagEventData(
		Guid contentId, Guid websiteId, Guid contentTypeId,Guid tagId, Guid langId) {
		
		contentId.ThrowIfGuidIsEmpty(new BehlogInvalidEntityIdException(nameof(Content)));
		ContentId = contentId;

		websiteId.ThrowIfGuidIsEmpty(new BehlogInvalidEntityIdException(nameof(Website)));
		WebsiteId = websiteId;

		contentTypeId.ThrowIfGuidIsEmpty(new BehlogInvalidEntityIdException(nameof(ContentType)));
		ContentTypeId = contentTypeId;

		tagId.ThrowIfGuidIsEmpty(new BehlogInvalidEntityIdException(nameof(Tag)));
		TagId = tagId;

		langId.ThrowIfGuidIsEmpty(new BehlogInvalidEntityIdException(nameof(Language)));
		LangId = langId;
	}

	public Guid ContentId { get; }
	public Guid WebsiteId { get; }
	public Guid ContentTypeId { get; }
	public Guid TagId { get; }
	public Guid LangId { get; }
}