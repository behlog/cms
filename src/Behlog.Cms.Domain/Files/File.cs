using Behlog.Core.Domain;

namespace Behlog.Cms.Domain;

public class File : BehlogEntity<Guid>
{

    private File()
    {
    }

    #region props

    public string Title { get; }
    
    public string FilePath { get; }
    
    public string Extension { get; }
    
    public string AltTitle { get; }
    
    public string Url { get; }
    
    public FileStatus Status { get; }
    
    public string Description { get; }
    #endregion
}