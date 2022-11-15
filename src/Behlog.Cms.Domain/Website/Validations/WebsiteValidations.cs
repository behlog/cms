using Behlog.Cms.Domain;
using Behlog.Core.Validations;

namespace Behlog.Cms.Handlers;

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
            result.AddError("Status", errorMessage, errorCode);
        }

        return result;
    }
}