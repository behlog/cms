namespace Behlog.Cms.FileUploads.Internal;


public class FileUploaderResult
{
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public long FileSize { get; set; }
    public string FileUrl { get; set; }
    public string Extension { get; set; }
    public string ErrorMessage { get; private set; }
    public bool HasError { get; private set; }
    public DateTime StartedAt { get; private set; }
    public DateTime? FinishedAt { get; private set; }

    public FileUploaderResult WithError(string message = "")
    {
        HasError = true;
        ErrorMessage = message;
        return this;
    }

    public FileUploaderResult Finished()
    {
        FinishedAt = DateTime.UtcNow;
        return this;
    }
}