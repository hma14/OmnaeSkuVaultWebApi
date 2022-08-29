using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OmnaeSkuVaultWebApi.Domain.SkuVaultTokens.Dtos;
using SkuVaultApiWrapper;
using SkuVaultApiWrapper.Extensions;
using SkuVaultApiWrapper.Models;
using SkuVaultApiWrapper.Models.CreateBrands;
using SkuVaultApiWrapper.Models.GetBrands;
using SkuVaultApiWrapper.Models.SharedModels;

namespace OmnaeSkuVaultWebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class SkuVaultAccessController : ControllerBase
    {
        private readonly SkuVaultApiClient _skuVaultApiClient;

        public SkuVaultAccessController(SkuVaultApiClient skuVaultApiClient)
        {
            _skuVaultApiClient = skuVaultApiClient;
        }


        [ProducesResponseType(typeof(IEnumerable<GetBrandsResponse>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        [HttpGet]
        [Route("GetBrands", Name = "GetBrands")]
        public async Task<IActionResult> GetBrands()
        {
            var request = new GetBrandsRequest();

            var brands = await SkuVaultApiClientExtensions.GetBrands(_skuVaultApiClient, request);
            
            return Ok(brands);
        }

        [ProducesResponseType(typeof(CreateBrandsResponse), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpPost]
        [Route("CreateBrands", Name = "CreateBrands")]
        public async Task<IActionResult> CreateBrands()
        {
            var request = new CreateBrandsRequest { Brands = new List<Brand> { new Brand { Name = "ApiCreatedBrand1" }, new Brand { Name = "ApiCreatedBrand2" } } };
            var response = await _skuVaultApiClient.CreateBrands(request);
            if (response.Errors.Any())
            {
                throw new Exception($"Errors Exist: {response.Errors.Any()} --- Status: {response.Status}");
            }
            return Ok(response);
        }
    }
}
