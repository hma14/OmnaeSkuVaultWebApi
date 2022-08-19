namespace OmnaeSkuVaultWebApi.SharedTestHelpers.Fakes.SkuVaultToken;

using AutoBogus;
using OmnaeSkuVaultWebApi.Domain.SkuVaultTokens;
using OmnaeSkuVaultWebApi.Domain.SkuVaultTokens.Dtos;

public class FakeSkuVaultToken
{
    public static SkuVaultToken Generate(SkuVaultTokenForCreationDto skuVaultTokenForCreationDto)
    {
        return SkuVaultToken.Create(skuVaultTokenForCreationDto);
    }

    public static SkuVaultToken Generate()
    {
        return SkuVaultToken.Create(new FakeSkuVaultTokenForCreationDto().Generate());
    }
}