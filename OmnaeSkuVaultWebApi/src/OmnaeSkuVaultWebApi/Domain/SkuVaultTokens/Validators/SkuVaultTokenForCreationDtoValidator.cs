namespace OmnaeSkuVaultWebApi.Domain.SkuVaultTokens.Validators;

using OmnaeSkuVaultWebApi.Domain.SkuVaultTokens.Dtos;
using FluentValidation;

public class SkuVaultTokenForCreationDtoValidator: SkuVaultTokenForManipulationDtoValidator<SkuVaultTokenForCreationDto>
{
    public SkuVaultTokenForCreationDtoValidator()
    {
        // add fluent validation rules that should only be run on creation operations here
        //https://fluentvalidation.net/
    }
}