namespace Behlog.Cms.Models;

public static class TagMappers
{

    public static TagResult ToResult(this Tag tag)
    {
        tag.ThrowExceptionIfArgumentIsNull(nameof(tag));

        return new TagResult
        {
            Id = tag.Id,
            Slug = tag.Slug,
            Status = tag.Status,
            Title = tag.Title,
            CreatedDate = tag.CreatedDate,
            LangCode = tag.Language?.Code,
            LangId = tag.LangId,
            CreatedByUserId = tag.CreatedByUserId,
            CreatedByUserIp = tag.CreatedByIp
        };
    }
}