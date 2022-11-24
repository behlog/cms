using Behlog.Cms.Seed;

namespace Behlog.Cms.Contracts;

public interface ICmsSetup
{

    Task SetupAsync(WebsiteSeedData data, CancellationToken cancellationToken = default);
}