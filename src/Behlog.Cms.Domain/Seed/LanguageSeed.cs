using Behlog.Cms.Commands;
using Behlog.Cms.Domain;
using Behlog.Cms.Store;

namespace Behlog.Cms.Seed;

internal class LanguageSeed
{
    private readonly ILanguageWriteStore _writeStore;


    public LanguageSeed(ILanguageWriteStore writeStore)
    {
        _writeStore = writeStore ?? throw new ArgumentNullException(nameof(writeStore));
    }

    /// <summary>
    /// Adds Behlog Default <see cref="Language"/>s to the Store.
    /// </summary>
    /// <param name="cancellationToken"></param>
    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        var farsi = Language.Create(FarsiLanguage.Id,
            new CreateLanguageCommand(
                FarsiLanguage.Name, FarsiLanguage.Title,
                FarsiLanguage.Code, FarsiLanguage.IsoCode));

        var english = Language.Create(EnglishLanguage.Id,
            new CreateLanguageCommand(
                EnglishLanguage.Name, EnglishLanguage.Title,
                EnglishLanguage.Code, EnglishLanguage.IsoCode));
        
        _writeStore.MarkForAdd(farsi);
        _writeStore.MarkForAdd(english);
        await _writeStore.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
    
}