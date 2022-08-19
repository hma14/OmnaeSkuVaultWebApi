namespace OmnaeSkuVaultWebApi.FunctionalTests.FunctionalTests.SkuVaultTokens;

using OmnaeSkuVaultWebApi.SharedTestHelpers.Fakes.SkuVaultToken;
using OmnaeSkuVaultWebApi.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetSkuVaultTokenTests : TestBase
{
    [Test]
    public async Task get_skuvaulttoken_returns_success_when_entity_exists()
    {
        // Arrange
        var fakeSkuVaultToken = FakeSkuVaultToken.Generate(new FakeSkuVaultTokenForCreationDto().Generate());
        await InsertAsync(fakeSkuVaultToken);

        // Act
        var route = ApiRoutes.SkuVaultTokens.GetRecord.Replace(ApiRoutes.SkuVaultTokens.Id, fakeSkuVaultToken.Id.ToString());
        var result = await _client.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}