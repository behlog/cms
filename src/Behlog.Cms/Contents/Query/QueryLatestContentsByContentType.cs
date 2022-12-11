﻿namespace Behlog.Cms.Query
{
    public class QueryLatestContentsByContentType
    {

        public QueryLatestContentsByContentType(
            Guid websiteId, Guid? contentTypeId, string? contentTypeName, int recordsCount = 10) {
            WebsiteId = websiteId;
            ContentTypeId = contentTypeId;
            ContentTypeName = contentTypeName;
            RecordsCount = recordsCount;
        }

        public Guid WebsiteId { get; }
        public Guid? ContentTypeId { get; set; }
        public string? ContentTypeName { get; set; }
        public int RecordsCount { get; }
    }
}
