namespace OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts.Mappings;

using OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts.Dtos;
using OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts;
using Mapster;

public class SkuVaultAccountMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SkuVaultAccountDto, SkuVaultAccount>()
            .TwoWays();
        config.NewConfig<SkuVaultAccountForCreationDto, SkuVaultAccount>()
            .TwoWays();
        config.NewConfig<SkuVaultAccountForUpdateDto, SkuVaultAccount>()
            .TwoWays();
    }
}