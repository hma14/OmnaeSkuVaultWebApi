namespace OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts.Validators;

using OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts.Dtos;
using FluentValidation;

public class SkuVaultAccountForCreationDtoValidator: SkuVaultAccountForManipulationDtoValidator<SkuVaultAccountForCreationDto>
{
    public SkuVaultAccountForCreationDtoValidator()
    {
        // add fluent validation rules that should only be run on creation operations here
        //https://fluentvalidation.net/
    }
}