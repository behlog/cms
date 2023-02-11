namespace Behlog.Cms.Domain
{

	public class WebsiteTag
	{
		private WebsiteTag() { }

		#region props
		public Guid WebsiteId { get; protected set; }
		public Guid TagId { get; protected set; }
		public Guid LangId { get; protected set; }
		public string TagTitle { get; protected set; }
		public string TagSlug { get; protected set; }

		#endregion

		#region Navigations
		public Website Website { get; protected set; }
		public Tag Tag { get; protected set; }
		#endregion


	}
}
