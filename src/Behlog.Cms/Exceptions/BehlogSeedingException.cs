using Behlog.Core;

namespace Behlog.Cms.Exceptions;

public class BehlogSeedingException : BehlogException
{

    public BehlogSeedingException() : base()
    {
    }

    public BehlogSeedingException(string seedName) : base($"Seeding Error for {seedName}!")
    {
    } 
}