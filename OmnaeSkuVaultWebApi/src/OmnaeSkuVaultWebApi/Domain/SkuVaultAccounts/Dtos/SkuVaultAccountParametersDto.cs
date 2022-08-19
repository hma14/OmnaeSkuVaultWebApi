namespace OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts.Dtos
{
    using SharedKernel.Dtos;

    public class SkuVaultAccountParametersDto : BasePaginationParameters
    {
        public string Filters { get; set; }
        public string SortOrder { get; set; }
    }
}