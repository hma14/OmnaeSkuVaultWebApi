using SkuVaultApiWrapper.Models.SharedModels;

namespace OmnaeSkuVaultWebApi.Dtos
{
    public class CreateBrandsDto
    {
        public int CompanyId { get; set; }
        public List<Brand> Brands { get; set; }
    }
}
