namespace OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts.Dtos
{
    using System.Collections.Generic;
    using System;

    public class SkuVaultAccountDto 
    {
        public Guid Id { get; set; }
        public int SkuVaultId { get; set; }
        public string SkuVaultTokenId { get; set; }
        public int CompanyId { get; set; }
        public bool IsVendor { get; set; }
    }
}