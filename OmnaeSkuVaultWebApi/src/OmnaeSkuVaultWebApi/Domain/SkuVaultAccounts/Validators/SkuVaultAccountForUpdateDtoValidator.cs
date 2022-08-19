namespace OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts.Validators;

using OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts.Dtos;
using FluentValidation;

public class SkuVaultAccountForUpdateDtoValidator: SkuVaultAccountForManipulationDtoValidator<SkuVaultAccountForUpdateDto>
{
    public SkuVaultAccountForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}