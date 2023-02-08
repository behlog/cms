using System;
using Behlog.Cms.Commands;
using Behlog.Core;
using Behlog.Extensions;

namespace Behlog.Cms.Domain;

// Invariants
public partial class ContentCategory
{

    
    
    private static void checkRequiredFields(CreateContentCategoryCommand command) 
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));
        if(command.Title.IsNullOrEmpty())
            throw new BehlogRequiredFieldException(nameof(command.Title));
    }

    private static void checkRequiredFields(UpdateContentCategoryCommand command)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));
        
        if(command.Title.IsNullOrEmpty())
            throw new BehlogRequiredFieldException(nameof(command.Title));
    }
}