using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sample_CleanArchitecture_CQRS.Application.Common.Models.Results;
using Sample_CleanArchitecture_CQRS.Application.CQRS.v1.Authentication.Dtos;

namespace Sample_CleanArchitecture_CQRS.Application.Common.Interfaces;
public interface IAuthenticationService
{
    Task<InfraResult<TokenDto>> LoginAsync(string email, string password);
}