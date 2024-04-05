using ArchUnitNET.Domain;
using ArchUnitNET.Fluent;
using ArchUnitNET.Loader;

using Sample_CleanArchitecture_CQRS.Api;
using Sample_CleanArchitecture_CQRS.Domain.Common.Interfaces;
using Sample_CleanArchitecture_CQRS.Domain.Common.Resources.Products;
using Sample_CleanArchitecture_CQRS.Infrastructure.Data;

using Assembly = System.Reflection.Assembly;


namespace Architecture.UnitTests;

public abstract class BaseTest
{
    protected static readonly Assembly DomainAssembly = typeof(IUnitOfWork).Assembly;
    protected static readonly Assembly ApplicationAssembly = typeof(ProductErrors).Assembly;
    protected static readonly Assembly InfrastructureAssembly = typeof(AppDbContext).Assembly;
    protected static readonly Assembly PresentationAssembly = typeof(Program).Assembly;
}

public abstract class ArchUnitBaseTest : BaseTest
{
    protected static readonly ArchUnitNET.Domain.Architecture Architecture = new ArchLoader()
        .LoadAssemblies(DomainAssembly,
            ApplicationAssembly,
            InfrastructureAssembly,
            PresentationAssembly)
        .Build();

    protected static readonly IObjectProvider<IType> DomainLayer =
        ArchRuleDefinition.Types().That().ResideInAssembly(DomainAssembly).As("Domain Layer");

    protected static readonly IObjectProvider<IType> ApplicationLayer =
        ArchRuleDefinition.Types().That().ResideInAssembly(ApplicationAssembly).As("Application Layer");

    protected static readonly IObjectProvider<IType> InfrastructureLayer =
        ArchRuleDefinition.Types().That().ResideInAssembly(InfrastructureAssembly).As("Infrastructure Layer");

    protected static readonly IObjectProvider<IType> PresentationLayer =
        ArchRuleDefinition.Types().That().ResideInAssembly(PresentationAssembly).As("Presentation Layer");
}

