namespace Behlog.Cms.Exceptions;

public class WebsiteTitleExistException : BehlogSeedingException
{

    public WebsiteTitleExistException(string existingTitle)
        : base($"Website with title '{existingTitle}' already existed.")
    {
    }
}