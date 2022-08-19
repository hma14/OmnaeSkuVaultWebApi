namespace OmnaeSkuVaultWebApi.Domain.SkuVaultTokens.Mappings;

using OmnaeSkuVaultWebApi.Domain.SkuVaultTokens.Dtos;
using OmnaeSkuVaultWebApi.Domain.SkuVaultTokens;
using Mapster;

public class SkuVaultTokenMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SkuVaultTokenDto, SkuVaultToken>()
            .TwoWays();
        config.NewConfig<SkuVaultTokenForCreationDto, SkuVaultToken>()
            .TwoWays();
        config.NewConfig<SkuVaultTokenForUpdateDto, SkuVaultToken>()
            .TwoWays();
    }
}