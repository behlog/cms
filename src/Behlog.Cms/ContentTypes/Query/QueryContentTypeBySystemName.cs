using Behlog.Core;

namespace Behlog.Cms.Query;

/// <summary>
/// Query <see cref="ContentType"/> by it's SystemName.
/// </summary>
public class QueryContentTypeBySystemName
{

    public QueryContentTypeBySystemName( string systemName)
    {
        this.SystemName = systemName;
    }
    
    public string SystemName { get; }
}