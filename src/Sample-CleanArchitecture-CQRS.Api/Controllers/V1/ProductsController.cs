using Asp.Versioning;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sample_CleanArchitecture_CQRS.Application.Common.Models.Results;
using Sample_CleanArchitecture_CQRS.Application.CQRS.v1.Products.Commands.Delete;
using Sample_CleanArchitecture_CQRS.Application.CQRS.v1.Products.Queries.Details;
using Sample_CleanArchitecture_CQRS.Application.CQRS.v1.Products.Queries.GetAll;
using Sample_CleanArchitecture_CQRS.Application.CQRS.v1.Products.Commands.Create;
using Sample_CleanArchitecture_CQRS.Application.CQRS.v1.Products.Commands.Edit;
using Sample_CleanArchitecture_CQRS.Application.CQRS.v1.Products.Common;


namespace Sample_CleanArchitecture_CQRS.Api.Controllers.V1;

[ApiController]
[ApiVersion(1)]
[Route("api/v{v:apiVersion}/products")]
[Authorize]
public class ProductsController : ControllerBase
{

    private readonly ISender _sender;
    private readonly IMapper _mapper;


    public ProductsController(ISender sender,
        IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("{productId}")]
    [ProducesDefaultResponseType(typeof(ApiResult<ProductDetailsDto>))]
    public async Task<IActionResult> GetProductDetailsAsync(string productId, CancellationToken token)
    {
        if (!Guid.TryParse(productId, out var id))
        {
            return BadRequest();
        }

        var query = new ProductDetailsQuery(id);
        ApiResult<ProductDetailsDto> result = await _sender.Send(query, token);

        if (result.IsSuccess)
        {
            return Ok(result);
        }
        return StatusCode(result.StatusCode, result);
    }


    [HttpPost]
    [Route("")]
    [ProducesDefaultResponseType(typeof(ApiResult<ProductDetailsDto>))]
    public async Task<IActionResult> CreateProductAsync([FromBody] CreateProductDto create,
        CancellationToken token)
    {


        var command = _mapper.Map<CreateProductCommand>(create);
        var result = await _sender.Send(command, token);

        if (result.IsSuccess)
        {
            return Ok(result);
        }
        return StatusCode(result.StatusCode, result);
    }

    [HttpPut]
    [Route("{productId}")]
    [ProducesDefaultResponseType(typeof(ApiResult<ProductDetailsDto>))]
    public async Task<IActionResult> EditProductDetailsAsync(string productId, [FromBody] EditProductDto edit,
        CancellationToken token)
    {
        if (!Guid.TryParse(productId, out var id))
        {
            return BadRequest();
        }

        var command = _mapper.Map<EditProductCommand>((id, edit));
        var result = await _sender.Send(command, token);

        if (result.IsSuccess)
        {
            return Ok(result);
        }
        return StatusCode(result.StatusCode, result);
    }

    [HttpDelete]
    [Route("{productId}")]
    [ProducesDefaultResponseType(typeof(ApiResult<string>))]
    public async Task<IActionResult> DeleteProductAsync(string productId, CancellationToken token)
    {
        if (!Guid.TryParse(productId, out var id))
        {
            return BadRequest();
        }

        var command = new DeleteProductCommand(id);
        var result = await _sender.Send(command, token);

        if (result.IsSuccess)
        {
            return Ok(result);
        }
        return StatusCode(result.StatusCode, result);
    }


    [HttpGet]
    [Route("")]
    [ProducesDefaultResponseType(typeof(ApiResult<ProductListDto>))]
    public async Task<IActionResult> GetProductsListAsync(CancellationToken token)
    {
        var query = new GetAllProductsQuery();

        var result = await _sender.Send(query, token);

        if (result.IsSuccess)
        {
            return Ok(result);
        }
        return StatusCode(result.StatusCode, result);
    }
}
