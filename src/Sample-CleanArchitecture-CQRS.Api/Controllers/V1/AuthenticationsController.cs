using Asp.Versioning;

using MapsterMapper;
using MediatR;

using Microsoft.AspNetCore.Mvc;
using Sample_CleanArchitecture_CQRS.Application.Common.Models.Results;
using Sample_CleanArchitecture_CQRS.Application.CQRS.v1.Authentication.Commands.Login;
using Sample_CleanArchitecture_CQRS.Application.CQRS.v1.Authentication.Dtos;

namespace Sample_CleanArchitecture_CQRS.Api.Controllers.V1;

[ApiController]
[ApiVersion(1)]
[Route("api/v{v:apiVersion}/auth")]
public class AuthenticationsController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;


    public AuthenticationsController(ISender sender,
        IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }


    /// <summary>
    /// Endpoint for user login.
    /// </summary>
    /// <remarks>
    /// Allows users to log in with their credentials and receive an authentication token if successful.
    /// </remarks>
    /// <returns>
    /// Returns an <see cref="IActionResult"/> representing the result of the login operation.
    /// If successful, returns an HTTP 202 (Accepted) status code along with the authentication token.
    /// If unsuccessful, returns the appropriate HTTP status code along with an error message.
    /// </returns>
    [HttpPost]
    [Route("login")]
    [ProducesDefaultResponseType(typeof(ApiResult<TokenDto>))]
    public async Task<IActionResult> LoginUserAsync([FromBody] LoginDto login,
        CancellationToken token)
    {
        var command = _mapper.Map<LoginCommand>(login);
        var result = await _sender.Send(command, token);

        if (result.IsSuccess)
        {
            return Accepted(result);
        }

        return StatusCode(result.StatusCode, result);
    }
}
