namespace OmnaeSkuVaultWebApi.FunctionalTests.FunctionalTests.SkuVaultTokens;

using OmnaeSkuVaultWebApi.SharedTestHelpers.Fakes.SkuVaultToken;
using OmnaeSkuVaultWebApi.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class CreateSkuVaultTokenTests : TestBase
{
    [Test]
    public async Task create_skuvaulttoken_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeSkuVaultToken = new FakeSkuVaultTokenForCreationDto { }.Generate();

        // Act
        var route = ApiRoutes.SkuVaultTokens.Create;
        var result = await _client.PostJsonRequestAsync(route, fakeSkuVaultToken);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}