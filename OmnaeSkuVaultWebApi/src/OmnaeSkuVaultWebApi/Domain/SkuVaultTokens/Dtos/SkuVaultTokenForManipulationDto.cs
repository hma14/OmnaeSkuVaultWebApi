namespace OmnaeSkuVaultWebApi.Domain.SkuVaultTokens.Dtos
{
    using System.Collections.Generic;
    using System;

    public abstract class SkuVaultTokenForManipulationDto 
    {
        public string TenantToken { get; set; }
        public string UserToken { get; set; }
        public int CompanyId { get; set; }
        public string SkuVaultCompanyId { get; set; }
        public bool IsRevoked { get; set; }
    }
}