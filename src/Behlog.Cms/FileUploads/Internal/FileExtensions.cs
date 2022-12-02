namespace Behlog.Cms.FileUploads.Internal;

internal static class FileExtensions
{
    
    public static bool IsNotNullOrEmpty(this IFormFile file)
        => file != null && file.Length > 0;

    public static bool IsNullOrEmpty(this IFormFile file)
        => file == null || file.Length == 0; 
}