namespace OmnaeSkuVaultWebApi.Services;

using OmnaeSkuVaultWebApi.Databases;

public interface IUnitOfWork : IOmnaeSkuVaultWebApiService
{
    Task CommitChanges(CancellationToken cancellationToken = default);
}

public class UnitOfWork : IUnitOfWork
{
    private readonly FinanceServiceDbContext _dbContext;

    public UnitOfWork(FinanceServiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CommitChanges(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
