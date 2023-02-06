using System.Net.Http.Headers;
using Behlog.Cms.Domain;
using Behlog.Core.Validations;

namespace Behlog.Cms.Validations;

public static class WebsiteValidations
{


    public static ValidatorResult CheckWebsiteStatusOnCreate(
        this ValidatorResult result, WebsiteStatusEnum status,
        string errorMessage, string errorCode = "")
    {
        if (status == WebsiteStatusEnum.Deleted ||
            status == WebsiteStatusEnum.Closed ||
            status == WebsiteStatusEnum.Online)
        {
            return result.WithError(ValidationError
                .Create("Status", errorCode, errorMessage));
        }

        return result;
    }
}