namespace Behlog.Cms.Domain
{

	public class WebsiteTag : BehlogEntity<long>
	{
		private WebsiteTag() { }

		#region props
		public Guid WebsiteId { get; protected set; }
		public Guid TagId { get; protected set; }
		public Guid LangId { get; protected set; }
		public Guid ContentTypeId { get; protected set; }
		public Guid ContentId { get; protected set; }
		public string TagTitle { get; protected set; }
		public string TagSlug { get; protected set; }

		#endregion

		#region Navigations
		public Website Website { get; protected set; }
		public Tag Tag { get; protected set; }
		public Language Language { get; protected set; }
		#endregion

		public static WebsiteTag New(Guid websiteId, Guid tagId) {
			var websiteTag = new WebsiteTag();
			websiteTag.WebsiteId = websiteId;
			websiteTag.TagId = tagId;
			return websiteTag;
		}

		public WebsiteTag WithLangId(Guid langId) {
			LangId = langId;
			return this;
		}

		public WebsiteTag WithTagTitle(string tagTitle) {
			TagTitle = tagTitle;
			return this;
		}

		public WebsiteTag WithTagSlug(string tagSlug) {
			TagSlug = tagSlug;
			return this;
		}
		
	}
}
