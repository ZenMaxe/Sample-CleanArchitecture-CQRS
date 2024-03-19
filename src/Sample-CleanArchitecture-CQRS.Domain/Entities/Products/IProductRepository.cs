using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sample_CleanArchitecture_CQRS.Domain.Common.Interfaces;

namespace Sample_CleanArchitecture_CQRS.Domain.Entities.Products;
public interface IProductRepository : IGenericRepository<Product>
{
}
