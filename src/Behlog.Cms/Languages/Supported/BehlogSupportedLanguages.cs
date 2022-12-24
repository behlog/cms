namespace Behlog.Cms;

/// <summary>
/// Supported languages for Behlog CMS.
/// </summary>
public static class BehlogSupportedLanguages
{
    
    public static Dictionary<string, Guid> _codeToId = new()
    {
        { PersianLanguage.Code, PersianLanguage.Id },
        { EnglishLanguage.Code, EnglishLanguage.Id }
    };

    public static Dictionary<string, Guid> _nameToId = new()
    {
        { PersianLanguage.Name, PersianLanguage.Id },
        { EnglishLanguage.Name, EnglishLanguage.Id }
    };

    public static Dictionary<string, Guid> _isoCodeToId = new()
    {
        { PersianLanguage.IsoCode, PersianLanguage.Id },
        { EnglishLanguage.IsoCode, EnglishLanguage.Id }
    };

    public static Dictionary<Guid, string> _idToCode = new()
    {
        { PersianLanguage.Id, PersianLanguage.Code },
        { EnglishLanguage.Id, EnglishLanguage.Code }
    };

    public static Dictionary<Guid, string> _idToName = new()
    {
        { PersianLanguage.Id, PersianLanguage.Name },
        { EnglishLanguage.Id, EnglishLanguage.Name }
    };


    public static Guid GetIdByCode(string code) => _codeToId[code.ToLower()];

    public static Guid GetIdByName(string name) => _nameToId[name.ToLower()];

    public static Guid GetIdByIsoCode(string isoCode) => _isoCodeToId[isoCode];

    public static string GetCodeById(Guid id) => _idToCode[id];

    public static string GetNameById(Guid id) => _idToName[id];
}