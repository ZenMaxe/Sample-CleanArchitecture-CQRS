using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sample_CleanArchitecture_CQRS.Domain.Common.Interfaces;
using Sample_CleanArchitecture_CQRS.Domain.Entities.Products;
using Sample_CleanArchitecture_CQRS.Infrastructure.Data;

namespace Sample_CleanArchitecture_CQRS.Infrastructure.Repositories;



public sealed class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    // Any Implementation

}

