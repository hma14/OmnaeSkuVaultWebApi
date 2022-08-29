using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OmnaeSkuVaultWebApi.Domain.SkuVaultTokens.Dtos;
using OmnaeSkuVaultWebApi.Domain.SkuVaultTokens.Services;

namespace OmnaeSkuVaultWebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class SkuVaultController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly SkuVaultTokensController skuVaultTokensController;
        private readonly ISkuVaultTokenRepository skuVaultTokenRepository;


        public SkuVaultController(IMediator mediator, IMapper mapper, SkuVaultTokensController skuVaultTokensController,
                                        ISkuVaultTokenRepository skuVaultTokenRepository)
        {

#if true
            _mapper = mapper;
            _mediator = mediator;
            this.skuVaultTokensController = skuVaultTokensController;
            this.skuVaultTokenRepository = skuVaultTokenRepository;
#else
            this._mapper = _mapper ??
                throw new ArgumentNullException(nameof(_mapper));

            this._mediator = _mediator ??
                throw new ArgumentNullException(nameof(_mediator));

            this.skuVaultTokensController = skuVaultTokensController ??
                throw new ArgumentNullException(nameof(skuVaultTokensController));

            this.skuVaultTokenRepository = skuVaultTokenRepository ??
                throw new ArgumentNullException(nameof(skuVaultTokenRepository));
#endif
        }

        /// <summary>
        /// Store SkuVault Token info into database
        /// </summary>
        /// <param name="skuVaultTokenForCreation">SkuVaultTokenForCreationDto</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(SkuVaultTokenDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpPost(Name = "StoreSkuVaultToken")]
        public async Task<ActionResult<SkuVaultTokenDto>> StoreSkuVaultToken([FromBody] SkuVaultTokenForCreationDto skuVaultTokenForCreation)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception(ModelState.ToString());
            }

            var all = skuVaultTokenRepository.Query();
            var target = all.Where(x => x.CompanyId == skuVaultTokenForCreation.CompanyId).FirstOrDefault();

            var result = await skuVaultTokensController.AddSkuVaultToken(skuVaultTokenForCreation);
            return Ok(result);

            //SkuVaultToken token = null;
            //if (target != null)
            //{
            //    token = _mapper.Map<SkuVaultToken>(skuVaultTokenForCreation);
            //    token.UpdateModifiedProperties(DateTime.UtcNow, User.Identity.Name);
            //}
            //else
            //{
            //    token = new SkuVaultToken
            //    {

            //    };
            //}
            //var skuVaultTokenParametersDto = new SkuVaultTokenParametersDto();
            //var query = new GetSkuVaultTokenList.SkuVaultTokenListQuery(skuVaultTokenParametersDto);
            //var queryResponse = await _mediator.Send(query);

            //await skuVaultTokensController.GetSkuVaultTokens(skuVaultTokenParametersDto);
        }

    }
}
