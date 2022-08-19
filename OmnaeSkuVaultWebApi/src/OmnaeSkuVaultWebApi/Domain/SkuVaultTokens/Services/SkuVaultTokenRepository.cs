namespace OmnaeSkuVaultWebApi.Domain.SkuVaultTokens.Services;

using OmnaeSkuVaultWebApi.Domain.SkuVaultTokens;
using OmnaeSkuVaultWebApi.Databases;
using OmnaeSkuVaultWebApi.Services;

public interface ISkuVaultTokenRepository : IGenericRepository<SkuVaultToken>
{
}

public class SkuVaultTokenRepository : GenericRepository<SkuVaultToken>, ISkuVaultTokenRepository
{
    private readonly FinanceServiceDbContext _dbContext;

    public SkuVaultTokenRepository(FinanceServiceDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
