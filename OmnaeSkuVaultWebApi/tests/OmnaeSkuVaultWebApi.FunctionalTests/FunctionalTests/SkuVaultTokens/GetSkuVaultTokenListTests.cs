namespace OmnaeSkuVaultWebApi.FunctionalTests.FunctionalTests.SkuVaultTokens;

using OmnaeSkuVaultWebApi.SharedTestHelpers.Fakes.SkuVaultToken;
using OmnaeSkuVaultWebApi.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetSkuVaultTokenListTests : TestBase
{
    [Test]
    public async Task get_skuvaulttoken_list_returns_success()
    {
        // Arrange
        

        // Act
        var result = await _client.GetRequestAsync(ApiRoutes.SkuVaultTokens.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}