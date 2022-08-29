using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkuVaultApiWrapper.Models.GetBrands;

namespace OmnaeSkuVaultWebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyController : ControllerBase
    {
        private readonly IMapper _mapper;
        public MyController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        [HttpGet]
        [Route("Hello", Name = "Hello")]
        public async Task<IActionResult> Hello()
        {
            var result = await Task.FromResult("Hello");

            return Ok(result);
        }
    }
}
