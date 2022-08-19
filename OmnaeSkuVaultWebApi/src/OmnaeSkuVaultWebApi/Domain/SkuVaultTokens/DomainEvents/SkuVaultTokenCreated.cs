namespace OmnaeSkuVaultWebApi.Domain.SkuVaultTokens.DomainEvents;

public class SkuVaultTokenCreated : DomainEvent
{
    public SkuVaultToken SkuVaultToken { get; set; } 
}
            