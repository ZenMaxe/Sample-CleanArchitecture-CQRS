
using Application.UnitTests.Fixtures;

using MapsterMapper;

using Moq;

using Sample_CleanArchitecture_CQRS.Application.CQRS.v1.Products.Commands.Create;
using Sample_CleanArchitecture_CQRS.Domain.Common.Interfaces;
using Sample_CleanArchitecture_CQRS.Domain.Entities.Products;
using Sample_CleanArchitecture_CQRS.Infrastructure.Data;
using Sample_CleanArchitecture_CQRS.Infrastructure.Repositories;

namespace Application.UnitTests.CQRS;
public class CreateProductTests : IClassFixture<DbFixture>
{
    private readonly DbFixture _fixture;

    public CreateProductTests(DbFixture fixture)
    {
        _fixture = fixture;
    }


    [Fact]
    public async Task Handle_ValidCommand_ReturnsSuccessResult()
    {
        // Arrange
        using var dbContext = _fixture.DbContext;
        var unitOfWork = new UnitOfWork(dbContext);
        var productRepository = new ProductRepository(dbContext);

        var mapperMock = new Mock<IMapper>();

        var handler = new CreateProductCommandHandler(productRepository, mapperMock.Object, unitOfWork);

        var command = new CreateProductCommand("Test", 10);

        var cancellationToken = new CancellationToken();


        // Act
        var result = await handler.Handle(command, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
    }

}
