using Behlog.Cms.Domain;
using Behlog.Core;

namespace Behlog.Cms.Store;

public interface ITagWriteStore : IBehlogWriteStore<Tag, Guid>
{
}