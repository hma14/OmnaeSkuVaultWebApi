namespace OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts.DomainEvents;

public class SkuVaultAccountCreated : DomainEvent
{
    public SkuVaultAccount SkuVaultAccount { get; set; } 
}
            