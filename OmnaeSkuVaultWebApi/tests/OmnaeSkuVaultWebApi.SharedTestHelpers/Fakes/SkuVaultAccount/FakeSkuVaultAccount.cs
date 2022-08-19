namespace OmnaeSkuVaultWebApi.SharedTestHelpers.Fakes.SkuVaultAccount;

using AutoBogus;
using OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts;
using OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts.Dtos;

public class FakeSkuVaultAccount
{
    public static SkuVaultAccount Generate(SkuVaultAccountForCreationDto skuVaultAccountForCreationDto)
    {
        return SkuVaultAccount.Create(skuVaultAccountForCreationDto);
    }

    public static SkuVaultAccount Generate()
    {
        return SkuVaultAccount.Create(new FakeSkuVaultAccountForCreationDto().Generate());
    }
}