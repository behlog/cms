using System.Net.Http.Headers;
using Behlog.Cms.Domain;
using Behlog.Core.Validations;

namespace Behlog.Cms.Validations;

public static class WebsiteValidations
{


    public static ValidatorResult CheckWebsiteStatusOnCreate(
        this ValidatorResult result, WebsiteStatus status,
        string errorMessage, string errorCode = "")
    {
        if (status == WebsiteStatus.Deleted ||
            status == WebsiteStatus.Closed ||
            status == WebsiteStatus.Online)
        {
            return result.WithError(ValidationError
                .Create("Status", errorCode, errorMessage));
        }

        return result;
    }
}