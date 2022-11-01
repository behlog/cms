using Behlog.Core;
using Behlog.Core.Domain;

namespace Behlog.Cms.Domain;

public class File : BehlogEntity<Guid>, IHasMetadata
{

    private File() { }

    #region props
    
    public string Title { get; protected set; }
    public string FilePath { get; protected set; }
    public string AlternateFilePath { get; protected set; }
    public string Extension { get; protected set; }
    public string AltTitle { get; protected set; }
    public string Url { get; protected set; }
    public FileStatus Status { get; protected set; }
    public string Description { get; protected set; }
    public DateTime CreatedDate { get; protected set; }
    public DateTime? LastUpdated { get; protected set; }
    public string CreatedByUserId { get; protected set; }
    public string LastUpdatedByUserId { get; protected set; }
    public string CreatedByIp { get; protected set; }
    public string LastUpdatedByIp { get; protected set; }
    #endregion

    #region Builders

    

    #endregion
}