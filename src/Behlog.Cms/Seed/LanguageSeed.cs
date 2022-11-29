using Behlog.Cms.Store;
using Behlog.Cms.Domain;
using Behlog.Cms.Commands;

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
        var farsi = Language.Create(PersianLanguage.Id,
            new CreateLanguageCommand(
                PersianLanguage.Name, PersianLanguage.Title,
                PersianLanguage.Code, PersianLanguage.IsoCode));

        var english = Language.Create(EnglishLanguage.Id,
            new CreateLanguageCommand(
                EnglishLanguage.Name, EnglishLanguage.Title,
                EnglishLanguage.Code, EnglishLanguage.IsoCode));
        
        _writeStore.MarkForAdd(farsi);
        _writeStore.MarkForAdd(english);
        await _writeStore.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
    
}