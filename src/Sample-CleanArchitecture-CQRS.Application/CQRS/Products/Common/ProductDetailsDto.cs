using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample_CleanArchitecture_CQRS.Application.CQRS.Products.Common;
public record ProductDetailsDto(
    Guid Id,
    string Name,
    decimal Price);
