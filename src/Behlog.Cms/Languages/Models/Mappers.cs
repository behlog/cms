namespace Behlog.Cms.Models;

public static class LanguageMappers
{

    public static LanguageResult ToResult(this Language language) {
        language.ThrowExceptionIfArgumentIsNull(nameof(language));

        return new LanguageResult {
            Id = language.Id,
            Title = language.Title,
            Code = language.Code,
            Name = language.Name,
            Status = language.Status
        };
    }
}