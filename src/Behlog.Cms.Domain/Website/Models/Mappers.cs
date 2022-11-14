using Behlog.Cms.Domain;
using Behlog.Cms.Events;
using Behlog.Core;
using Behlog.Extensions;

namespace Behlog.Cms.Models;

public static class WebsiteMappers
{

    public static WebsiteResult ToResult(this Website website)
    {
        website.ThrowExceptionIfArgumentIsNull(nameof(website));

        return new WebsiteResult
        {
            Id = website.Id,
            Email = website.Email,
            Title = website.Title,
            Description = website.Description,
            Keywords = website.Keywords,
            Name = website.Name,
            Password = website.Password,
            Status = website.Status,
            Url = website.Url,
            CopyrightText = website.CopyrightText,
            CreatedDate = website.CreatedDate,
            LastUpdated = website.LastUpdated,
            DefaultLangId = website.DefaultLangId,
            OwnerUserId = website.OwnerUserId,
            IsReadOnly = website.IsReadOnly,
            LastStatusChangedOn = website.LastStatusChangedOn,
            LastUpdatedByIp = website.LastUpdatedByIp,
            LastUpdatedByUserId = website.LastUpdatedByUserId
        };
    }

    public static WebsiteResult WithValidationErrors(
        this Website website,
        IReadOnlyCollection<ValidationError> errors)
    {
        website.ThrowExceptionIfArgumentIsNull(nameof(website));
        var result = website.ToResult();
        return result.WithValidationErrors(errors);
    }

    public static WebsiteCreatedEvent GetCreatedEvent(this Website website)
    {
        return new WebsiteCreatedEvent
        {
            Id = website.Id,
            Description = website.Description,
            Email = website.Email,
            Keywords = website.Keywords,
            Name = website.Name,
            Password = website.Password,
            Status = website.Status,
            Title = website.Title,
            Url = website.Url,
            CopyrightText = website.CopyrightText,
            CreatedDate = website.CreatedDate,
            DefaultLangId = website.DefaultLangId,
            IsReadOnly = website.IsReadOnly,
            OwnerUserId = website.OwnerUserId,
        };
    }


    public static WebsiteUpdatedEvent GetUpdatedEvent(this Website website)
    {
        return new WebsiteUpdatedEvent
        {
            Id = website.Id,
            Description = website.Description,
            Email = website.Email,
            Keywords = website.Keywords,
            Name = website.Name,
            Password = website.Password,
            Status = website.Status,
            Title = website.Title,
            Url = website.Url,
            CopyrightText = website.CopyrightText,
            CreatedDate = website.CreatedDate,
            LastUpdated = website.LastUpdated,
            DefaultLangId = website.DefaultLangId,
            IsReadOnly = website.IsReadOnly,
            OwnerUserId = website.OwnerUserId,
            LastStatusChangedOn = website.LastStatusChangedOn,
            LastUpdatedByIp = website.LastUpdatedByIp,
            LastUpdatedByUserId = website.LastUpdatedByUserId
        };
    }
}