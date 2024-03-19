using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample_CleanArchitecture_CQRS.Infrastructure.Configuration.Settings;

internal class ConnectionStrings
{
    public const string SectionName = nameof(ConnectionStrings);
    public string DefaultConnection { get; set; } = null!;
    public string? DebugConnection { get; set; }

    public string? ProductionConnection { get; set; }
}

