namespace OmnaeSkuVaultWebApi.FunctionalTests.TestUtilities;
public class ApiRoutes
{
    public const string Base = "api";
    public const string Health = Base + "/health";

    // new api route marker - do not delete

public static class SkuVaultAccounts
    {
        public const string Id = "{id}";
        public const string GetList = $"{Base}/skuVaultAccounts";
        public const string GetRecord = $"{Base}/skuVaultAccounts/{Id}";
        public const string Create = $"{Base}/skuVaultAccounts";
        public const string Delete = $"{Base}/skuVaultAccounts/{Id}";
        public const string Put = $"{Base}/skuVaultAccounts/{Id}";
        public const string Patch = $"{Base}/skuVaultAccounts/{Id}";
        public const string CreateBatch = $"{Base}/skuVaultAccounts/batch";
    }

public static class SkuVaultTokens
    {
        public const string Id = "{id}";
        public const string GetList = $"{Base}/skuVaultTokens";
        public const string GetRecord = $"{Base}/skuVaultTokens/{Id}";
        public const string Create = $"{Base}/skuVaultTokens";
        public const string Delete = $"{Base}/skuVaultTokens/{Id}";
        public const string Put = $"{Base}/skuVaultTokens/{Id}";
        public const string Patch = $"{Base}/skuVaultTokens/{Id}";
        public const string CreateBatch = $"{Base}/skuVaultTokens/batch";
    }
}
