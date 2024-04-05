using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArchUnitNET.Fluent;
using ArchUnitNET.xUnit;
using FluentAssertions;
using FluentValidation;

using NetArchTest.Rules;

using Sample_CleanArchitecture_CQRS.Application.Common.Abstractions.Messaging.Commands;
using Sample_CleanArchitecture_CQRS.Application.Common.Abstractions.Messaging.Queries;

namespace Architecture.UnitTests.Application;
public class ArchUnitCqrsTests : ArchUnitBaseTest
{

    [Fact]
    public void CQRS_Handlers_Should_BeSealed()
    {
        ArchRuleDefinition
            .Classes()
            .That()
            .ImplementInterface(typeof(ICommandHandler<,>))
            .Or()
            .ImplementInterface(typeof(ICommandHandler<>))
            .Or()
            .ImplementInterface(typeof(IQueryHandler<,>))
            .Should()
            .BeSealed()
            .Check(Architecture);
    }

    [Fact]
    public void CQRS_Handlers_Should_BeInternal()
    {
        ArchRuleDefinition
            .Classes()
            .That()
            .ImplementInterface(typeof(AbstractValidator<>))
            .Should()
            .BeInternal()
            .Check(Architecture);
    }

    [Fact]
    public void CQRS_Queries_Commands_Should_BeSealed()
    {
        ArchRuleDefinition
            .Classes()
            .That()
            .ImplementInterface(typeof(IQuery<>))
            .Or()
            .ImplementInterface(typeof(ICommand<>))
            .Should()
            .BeSealed()
            .Check(Architecture);
    }

    [Fact]
    public void CQRS_Queries_And_Commands_Should_BeImmutable()
    {
        ArchRuleDefinition
            .Classes()
            .That()
            .ImplementInterface(typeof(IQuery<>))
            .Should()
            .BeImmutable()
            .Check(Architecture);
    }

    [Fact]
    public void CQRS_Validations_Should_BeSealed()
    {
        ArchRuleDefinition
            .Classes()
            .That()
            .ImplementInterface(typeof(AbstractValidator<>))
            .Should()
            .BeSealed()
            .Check(Architecture);
    }

    [Fact]
    public void CQRS_Validations_Should_BeInternal()
    {
        ArchRuleDefinition
            .Classes()
            .That()
            .ImplementInterface(typeof(AbstractValidator<>))
            .Should()
            .BeInternal()
            .Check(Architecture);
    }


}
