namespace Behlog.Cms.Domain;

public class ContentCategoryItem
{
   private ContentCategoryItem() { }

   public ContentCategoryItem(
      Guid contentId, Guid categoryId)
   {
      ContentId = contentId;
      CategoryId = categoryId;
   }
   
   public Guid ContentId { get; set; }
   public Content Content { get; set; }
   
   public Guid CategoryId { get; set; }
   public ContentCategory Category { get; set; }
}