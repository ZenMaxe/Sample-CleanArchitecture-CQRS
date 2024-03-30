using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample_CleanArchitecture_CQRS.Infrastructure.Models.Identity;

namespace Sample_CleanArchitecture_CQRS.Infrastructure.Configuration.Persistence.Identity;
internal class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder
            .Property(x => x.FirstName)
            .IsRequired(false)
            .HasMaxLength(200);

        builder
            .Property(x => x.LastName)
            .IsRequired(false)
            .HasMaxLength(200);
    }
}
