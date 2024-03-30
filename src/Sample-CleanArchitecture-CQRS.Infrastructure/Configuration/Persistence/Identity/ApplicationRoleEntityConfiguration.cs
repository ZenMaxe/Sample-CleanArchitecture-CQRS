using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample_CleanArchitecture_CQRS.Infrastructure.Models.Identity;

namespace Sample_CleanArchitecture_CQRS.Infrastructure.Configuration.Persistence.Identity;
internal class ApplicationRoleEntityConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder.Property(x => x.DisplayName)
            .HasMaxLength(200);
    }
}
