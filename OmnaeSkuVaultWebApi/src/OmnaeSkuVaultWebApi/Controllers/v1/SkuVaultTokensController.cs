namespace OmnaeSkuVaultWebApi.Controllers.v1;

using OmnaeSkuVaultWebApi.Domain.SkuVaultTokens.Features;
using OmnaeSkuVaultWebApi.Domain.SkuVaultTokens.Dtos;
using OmnaeSkuVaultWebApi.Wrappers;
using System.Text.Json;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;
using System.Threading;
using MediatR;

[ApiController]
[Route("api/skuvaulttokens")]
[ApiVersion("1.0")]
public class SkuVaultTokensController: ControllerBase
{
    private readonly IMediator _mediator;

    public SkuVaultTokensController(IMediator mediator)
    {
        _mediator = mediator;
    }
    

    /// <summary>
    /// Creates a new SkuVaultToken record.
    /// </summary>
    /// <response code="201">SkuVaultToken created.</response>
    /// <response code="400">SkuVaultToken has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the SkuVaultToken.</response>
    [ProducesResponseType(typeof(SkuVaultTokenDto), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost(Name = "AddSkuVaultToken")]
    public async Task<ActionResult<SkuVaultTokenDto>> AddSkuVaultToken([FromBody]SkuVaultTokenForCreationDto skuVaultTokenForCreation)
    {
        var command = new AddSkuVaultToken.AddSkuVaultTokenCommand(skuVaultTokenForCreation);
        var commandResponse = await _mediator.Send(command);

        return CreatedAtRoute("GetSkuVaultToken",
            new { commandResponse.Id },
            commandResponse);
    }


    /// <summary>
    /// Gets a single SkuVaultToken by ID.
    /// </summary>
    /// <response code="200">SkuVaultToken record returned successfully.</response>
    /// <response code="400">SkuVaultToken has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the SkuVaultToken.</response>
    [ProducesResponseType(typeof(SkuVaultTokenDto), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpGet("{id}", Name = "GetSkuVaultToken")]
    public async Task<ActionResult<SkuVaultTokenDto>> GetSkuVaultToken(Guid id)
    {
        var query = new GetSkuVaultToken.SkuVaultTokenQuery(id);
        var queryResponse = await _mediator.Send(query);

        return Ok(queryResponse);
    }


    /// <summary>
    /// Gets a list of all SkuVaultTokens.
    /// </summary>
    /// <response code="200">SkuVaultToken list returned successfully.</response>
    /// <response code="400">SkuVaultToken has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the SkuVaultToken.</response>
    /// <remarks>
    /// Requests can be narrowed down with a variety of query string values:
    /// ## Query String Parameters
    /// - **PageNumber**: An integer value that designates the page of records that should be returned.
    /// - **PageSize**: An integer value that designates the number of records returned on the given page that you would like to return. This value is capped by the internal MaxPageSize.
    /// - **SortOrder**: A comma delimited ordered list of property names to sort by. Adding a `-` before the name switches to sorting descendingly.
    /// - **Filters**: A comma delimited list of fields to filter by formatted as `{Name}{Operator}{Value}` where
    ///     - {Name} is the name of a filterable property. You can also have multiple names (for OR logic) by enclosing them in brackets and using a pipe delimiter, eg. `(LikeCount|CommentCount)>10` asks if LikeCount or CommentCount is >10
    ///     - {Operator} is one of the Operators below
    ///     - {Value} is the value to use for filtering. You can also have multiple values (for OR logic) by using a pipe delimiter, eg.`Title@= new|hot` will return posts with titles that contain the text "new" or "hot"
    ///
    ///    | Operator | Meaning                       | Operator  | Meaning                                      |
    ///    | -------- | ----------------------------- | --------- | -------------------------------------------- |
    ///    | `==`     | Equals                        |  `!@=`    | Does not Contains                            |
    ///    | `!=`     | Not equals                    |  `!_=`    | Does not Starts with                         |
    ///    | `>`      | Greater than                  |  `@=*`    | Case-insensitive string Contains             |
    ///    | `&lt;`   | Less than                     |  `_=*`    | Case-insensitive string Starts with          |
    ///    | `>=`     | Greater than or equal to      |  `==*`    | Case-insensitive string Equals               |
    ///    | `&lt;=`  | Less than or equal to         |  `!=*`    | Case-insensitive string Not equals           |
    ///    | `@=`     | Contains                      |  `!@=*`   | Case-insensitive string does not Contains    |
    ///    | `_=`     | Starts with                   |  `!_=*`   | Case-insensitive string does not Starts with |
    /// </remarks>
    [ProducesResponseType(typeof(IEnumerable<SkuVaultTokenDto>), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpGet(Name = "GetSkuVaultTokens")]
    public async Task<IActionResult> GetSkuVaultTokens([FromQuery] SkuVaultTokenParametersDto skuVaultTokenParametersDto)
    {
        var query = new GetSkuVaultTokenList.SkuVaultTokenListQuery(skuVaultTokenParametersDto);
        var queryResponse = await _mediator.Send(query);

        var paginationMetadata = new
        {
            totalCount = queryResponse.TotalCount,
            pageSize = queryResponse.PageSize,
            currentPageSize = queryResponse.CurrentPageSize,
            currentStartIndex = queryResponse.CurrentStartIndex,
            currentEndIndex = queryResponse.CurrentEndIndex,
            pageNumber = queryResponse.PageNumber,
            totalPages = queryResponse.TotalPages,
            hasPrevious = queryResponse.HasPrevious,
            hasNext = queryResponse.HasNext
        };

        Response.Headers.Add("X-Pagination",
            JsonSerializer.Serialize(paginationMetadata));

        return Ok(queryResponse);
    }


    /// <summary>
    /// Updates an entire existing SkuVaultToken.
    /// </summary>
    /// <response code="204">SkuVaultToken updated.</response>
    /// <response code="400">SkuVaultToken has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the SkuVaultToken.</response>
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpPut("{id}", Name = "UpdateSkuVaultToken")]
    public async Task<IActionResult> UpdateSkuVaultToken(Guid id, SkuVaultTokenForUpdateDto skuVaultToken)
    {
        var command = new UpdateSkuVaultToken.UpdateSkuVaultTokenCommand(id, skuVaultToken);
        await _mediator.Send(command);

        return NoContent();
    }


    /// <summary>
    /// Deletes an existing SkuVaultToken record.
    /// </summary>
    /// <response code="204">SkuVaultToken deleted.</response>
    /// <response code="400">SkuVaultToken has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the SkuVaultToken.</response>
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpDelete("{id}", Name = "DeleteSkuVaultToken")]
    public async Task<ActionResult> DeleteSkuVaultToken(Guid id)
    {
        var command = new DeleteSkuVaultToken.DeleteSkuVaultTokenCommand(id);
        await _mediator.Send(command);

        return NoContent();
    }

    // endpoint marker - do not delete this comment
}
