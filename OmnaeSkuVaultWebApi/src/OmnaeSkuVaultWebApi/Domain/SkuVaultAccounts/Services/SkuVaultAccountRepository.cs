namespace OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts.Services;

using OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts;
using OmnaeSkuVaultWebApi.Databases;
using OmnaeSkuVaultWebApi.Services;

public interface ISkuVaultAccountRepository : IGenericRepository<SkuVaultAccount>
{
}

public class SkuVaultAccountRepository : GenericRepository<SkuVaultAccount>, ISkuVaultAccountRepository
{
    private readonly FinanceServiceDbContext _dbContext;

    public SkuVaultAccountRepository(FinanceServiceDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
