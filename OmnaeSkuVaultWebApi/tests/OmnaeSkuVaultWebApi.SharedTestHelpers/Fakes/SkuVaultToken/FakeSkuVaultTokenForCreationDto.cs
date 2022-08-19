namespace OmnaeSkuVaultWebApi.SharedTestHelpers.Fakes.SkuVaultToken;

using AutoBogus;
using OmnaeSkuVaultWebApi.Domain.SkuVaultTokens;
using OmnaeSkuVaultWebApi.Domain.SkuVaultTokens.Dtos;

// or replace 'AutoFaker' with 'Faker' along with your own rules if you don't want all fields to be auto faked
public class FakeSkuVaultTokenForCreationDto : AutoFaker<SkuVaultTokenForCreationDto>
{
    public FakeSkuVaultTokenForCreationDto()
    {
        // if you want default values on any of your properties (e.g. an int between a certain range or a date always in the past), you can add `RuleFor` lines describing those defaults
        //RuleFor(s => s.ExampleIntProperty, s => s.Random.Number(50, 100000));
        //RuleFor(s => s.ExampleDateProperty, s => s.Date.Past());
    }
}