using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Sample_CleanArchitecture_CQRS.Application.Common.Models.Results;
using Sample_CleanArchitecture_CQRS.Application.CQRS.Products.Common;
using Sample_CleanArchitecture_CQRS.Application.CQRS.Users.Commands.Create;
using Sample_CleanArchitecture_CQRS.Application.CQRS.Users.Dtos;

namespace Sample_CleanArchitecture_CQRS.Api.Controllers;


[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ISender _sender;


    public UsersController(IMapper mapper,
        ISender sender)
    {
        _mapper = mapper;
        _sender = sender;
    }

    [HttpPost]
    [Route("register")]
    [AllowAnonymous]
    [ProducesDefaultResponseType(typeof(ApiResult<UserDto>))]
    public async Task<IActionResult> RegisterAccountAsync([FromBody] CreateUserDto create,
        CancellationToken token)
    {
        var command = _mapper.Map<CreateUserCommand>(create);
        var result = await _sender.Send(command, token);

        if (result.IsSuccess)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
}
