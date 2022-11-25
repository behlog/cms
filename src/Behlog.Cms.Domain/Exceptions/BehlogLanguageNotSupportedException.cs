using Behlog.Core;

namespace Behlog.Cms.Exceptions;

public class BehlogLanguageNotSupportedException : BehlogException
{

    public BehlogLanguageNotSupportedException(string langCode)
        : base($"Behlog does not support the language with code : '{langCode}'.")
    {
    }
}