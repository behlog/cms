namespace Behlog.Cms.Handlers;

public class ContentEventHandlers :
	IBehlogEventHandler<ContentCreatedEvent>,
	IBehlogEventHandler<ContentUpdatedEvent>,
	IBehlogEventHandler<ContentRemovedEvent>
{
	private readonly IWebsiteTagWriteStore _websiteTagWriteStore;
	private readonly ITagReadStore _tagReadStore;

	public ContentEventHandlers(IWebsiteTagWriteStore websiteTagWriteStore, ITagReadStore tagReadStore) {
		_websiteTagWriteStore = websiteTagWriteStore
			?? throw new ArgumentNullException(nameof(websiteTagWriteStore));
		_tagReadStore = tagReadStore
			?? throw new ArgumentNullException(nameof(tagReadStore));
	}

	public async Task HandleAsync(
		ContentCreatedEvent @event, CancellationToken cancellationToken = default) {
		@event.ThrowExceptionIfArgumentIsNull(nameof(@event));

		if(!@event.Tags.IsNullOrEmpty()) {
			foreach(var tag in @event.Tags) {
				var websiteTag = WebsiteTag.New(
					@event.WebsiteId, tag.TagId, @event.LangId, 
					@event.ContentTypeId, tag.ContentId);

				var theTag = await _tagReadStore.FindAsync(tag.TagId).ConfigureAwait(false);
				theTag.ThrowExceptionIfReferenceIsNull($"[Error]: Tag with Id: '{tag.TagId}' not found.");
				websiteTag.WithTagTitle(theTag.Title);
				websiteTag.WithTagSlug(theTag.Slug);
				_websiteTagWriteStore.MarkForAdd(websiteTag);
			}

			await _websiteTagWriteStore.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
		}
	}

	public Task HandleAsync(ContentUpdatedEvent @event, CancellationToken cancellationToken = default) {
		throw new NotImplementedException();
	}

	public Task HandleAsync(ContentRemovedEvent @event, CancellationToken cancellationToken = default) {
		throw new NotImplementedException();
	}
}