using System;
using Behlog.Core;
using Behlog.Extensions;

namespace Behlog.Cms.Domain;

// Invariants
public partial class ContentCategory
{

    private static void checkRequiredFields(CreateContentCategoryArg args) 
    {
        if(args is null) throw new ArgumentNullException(nameof(args));
        
        if(args.Title.IsNullOrEmpty())
            throw new BehlogRequiredFieldException(nameof(args.Title));
    }

    private static void checkRequiredFields(UpdateContentCategoryArg args)
    {
        if(args is null) throw new ArgumentNullException(nameof(args));
        
        if(args.Title.IsNullOrEmpty())
            throw new BehlogRequiredFieldException(nameof(args.Title));
    }
}