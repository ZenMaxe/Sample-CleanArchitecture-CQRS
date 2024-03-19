using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Sample_CleanArchitecture_CQRS.Domain.Entities.Products;
using Sample_CleanArchitecture_CQRS.Infrastructure.Models.Identity;

namespace Sample_CleanArchitecture_CQRS.Infrastructure.Data;


public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public AppDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
        
    }



    public DbSet<Product> Products { get; set; } = default!;


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.
            ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            

        base.OnModelCreating(builder);
    }
}

