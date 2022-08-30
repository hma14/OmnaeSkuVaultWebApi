using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OmnaeSkuVaultWebApi.Domain.SkuVaultTokens.Dtos;
using OmnaeSkuVaultWebApi.Domain.SkuVaultTokens.Services;
using OmnaeSkuVaultWebApi.Dtos;
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
        private readonly HttpClient httpClient;
        private readonly SkuVaultTokensController skuVaultTokensController;

        public SkuVaultAccessController(HttpClient httpClient, SkuVaultTokensController skuVaultTokensController)
        {
            this.httpClient = httpClient;
            this.skuVaultTokensController = skuVaultTokensController;
        }


        [ProducesResponseType(typeof(IEnumerable<GetBrandsResponse>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        [HttpGet]
        [Route("GetBrands/{companyId:int}", Name = "GetBrands")]
        public async Task<IActionResult> GetBrands(int companyId)
        {
            var skuVaultApiClient = GetSkuVaultApiClient(companyId);
            var request = new GetBrandsRequest();
            var brands = await skuVaultApiClient.GetBrands(request);

            return Ok(brands);
        }

        [ProducesResponseType(typeof(CreateBrandsResponse), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpPost]
        [Route("CreateBrands", Name = "CreateBrands")]
        public async Task<IActionResult> CreateBrands([FromBody] CreateBrandsDto dto)
        {
            var request = new CreateBrandsRequest { Brands = dto.Brands };
            var skuVaultApiClient = GetSkuVaultApiClient(dto.CompanyId);
            var response = await skuVaultApiClient.CreateBrands(request);
            if (response.Errors.Any())
            {
                throw new Exception($"Errors Exist: {response.Errors.Any()} --- Status: {response.Status}");
            }
            return Ok(response);
        }

        private SkuVaultApiClient GetSkuVaultApiClient(int companyId)
        {
            var token = skuVaultTokensController.GetSkuVaultTokenByCompanyId(companyId).Result.Value;
            if (token == null)
                throw new Exception("No entry was found in SkuVaultToken table");

            var config = new SkuVaultApiClientConfig(token.TenantToken, token.UserToken);
            return new SkuVaultApiClient(httpClient, config);
        }
    }
}
