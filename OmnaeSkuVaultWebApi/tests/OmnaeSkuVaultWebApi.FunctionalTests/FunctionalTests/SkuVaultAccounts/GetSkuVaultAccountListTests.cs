namespace OmnaeSkuVaultWebApi.FunctionalTests.FunctionalTests.SkuVaultAccounts;

using OmnaeSkuVaultWebApi.SharedTestHelpers.Fakes.SkuVaultAccount;
using OmnaeSkuVaultWebApi.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetSkuVaultAccountListTests : TestBase
{
    [Test]
    public async Task get_skuvaultaccount_list_returns_success()
    {
        // Arrange
        

        // Act
        var result = await _client.GetRequestAsync(ApiRoutes.SkuVaultAccounts.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}