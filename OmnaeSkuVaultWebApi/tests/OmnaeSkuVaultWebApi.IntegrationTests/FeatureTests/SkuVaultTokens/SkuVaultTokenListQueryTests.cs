namespace OmnaeSkuVaultWebApi.IntegrationTests.FeatureTests.SkuVaultTokens;

using OmnaeSkuVaultWebApi.Domain.SkuVaultTokens.Dtos;
using OmnaeSkuVaultWebApi.SharedTestHelpers.Fakes.SkuVaultToken;
using SharedKernel.Exceptions;
using OmnaeSkuVaultWebApi.Domain.SkuVaultTokens.Features;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;

public class SkuVaultTokenListQueryTests : TestBase
{
    
    [Test]
    public async Task can_get_skuvaulttoken_list()
    {
        // Arrange
        var fakeSkuVaultTokenOne = FakeSkuVaultToken.Generate(new FakeSkuVaultTokenForCreationDto().Generate());
        var fakeSkuVaultTokenTwo = FakeSkuVaultToken.Generate(new FakeSkuVaultTokenForCreationDto().Generate());
        var queryParameters = new SkuVaultTokenParametersDto();

        await InsertAsync(fakeSkuVaultTokenOne, fakeSkuVaultTokenTwo);

        // Act
        var query = new GetSkuVaultTokenList.SkuVaultTokenListQuery(queryParameters);
        var skuVaultTokens = await SendAsync(query);

        // Assert
        skuVaultTokens.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}