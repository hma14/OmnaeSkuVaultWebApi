namespace OmnaeSkuVaultWebApi.Domain.SkuVaultTokens.Dtos
{
    using SharedKernel.Dtos;

    public class SkuVaultTokenParametersDto : BasePaginationParameters
    {
        public string Filters { get; set; }
        public string SortOrder { get; set; }
    }
}