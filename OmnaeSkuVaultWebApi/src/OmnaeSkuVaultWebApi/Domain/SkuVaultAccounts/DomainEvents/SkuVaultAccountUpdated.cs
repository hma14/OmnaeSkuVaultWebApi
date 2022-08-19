namespace OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts.DomainEvents;

public class SkuVaultAccountUpdated : DomainEvent
{
    public Guid Id { get; set; } 
}
            