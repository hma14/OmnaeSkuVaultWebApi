namespace OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts.Dtos
{
    using System.Collections.Generic;
    using System;

    public abstract class SkuVaultAccountForManipulationDto 
    {
        public int SkuVaultId { get; set; }
        public string SkuVaultTokenId { get; set; }
        public int CompanyId { get; set; }
        public bool IsVendor { get; set; }
    }
}