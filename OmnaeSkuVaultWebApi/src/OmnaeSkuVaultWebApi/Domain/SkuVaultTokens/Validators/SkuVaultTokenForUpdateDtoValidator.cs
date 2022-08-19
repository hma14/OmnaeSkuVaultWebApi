namespace OmnaeSkuVaultWebApi.Domain.SkuVaultTokens.Validators;

using OmnaeSkuVaultWebApi.Domain.SkuVaultTokens.Dtos;
using FluentValidation;

public class SkuVaultTokenForUpdateDtoValidator: SkuVaultTokenForManipulationDtoValidator<SkuVaultTokenForUpdateDto>
{
    public SkuVaultTokenForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}