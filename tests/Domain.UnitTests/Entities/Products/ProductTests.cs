using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

using Sample_CleanArchitecture_CQRS.Domain.Entities.Products;

namespace Domain.UnitTests.Entities.Products;

public class ProductTests
{
    [Fact]
    public void Product_CreateNew_Expected_Data()
    {
        const decimal value = 10.0m;
        Product product = Product.CreateNew("Test", value);

        Assert.NotNull(product);
        Assert.Equal(value, product.Price);
        
    }

    [Theory]
    [InlineData(-10)]
    [InlineData(-10.0)]
    [InlineData(-10.3)]
    [InlineData(-5)]
    [InlineData(-999999999)]
    public void Product_Create_Throws_ArgumentException_For_Negative_Numbers(decimal data)
    {
        Assert.Throws<ArgumentException>(() => Product.CreateNew("Test", data));
    }

    [Fact]
    public void Product_Edit_Name_Expect_Data()
    {
        Product product = Product.CreateNew("Test", 1);

        product.UpdateProduct("Test2", null);
        Assert.Equal("Test2", product.Name);
    }

    [Fact]
    public void Product_Edit_Price_Expect_Data()
    {
        Product product = Product.CreateNew("Test", 1);

        product.UpdateProduct(null, 10);
        Assert.Equal(10, product.Price);
    }
    [Fact]
    public void Product_Edit_Price_And_Name_Expect_Data()
    {
        Product product = Product.CreateNew("Test", 1);

        product.UpdateProduct("Test2", 10);
        Assert.Equal(10, product.Price);
        Assert.Equal("Test2", product.Name);
    }
}

