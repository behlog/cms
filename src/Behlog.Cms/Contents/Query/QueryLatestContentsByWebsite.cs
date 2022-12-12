using Behlog.Core;
using Behlog.Cms.Models;


namespace Behlog.Cms.Query
{

    public class QueryLatestContentsByWebsite 
        : IBehlogQuery<IReadOnlyCollection<ContentResult>>
    {

        public QueryLatestContentsByWebsite(Guid websiteId, int recordsCount = 10) {
            WebsiteId = websiteId;
            RecordsCount = recordsCount;
        }

        public Guid WebsiteId { get; }
        public int RecordsCount { get; }
    }
}
