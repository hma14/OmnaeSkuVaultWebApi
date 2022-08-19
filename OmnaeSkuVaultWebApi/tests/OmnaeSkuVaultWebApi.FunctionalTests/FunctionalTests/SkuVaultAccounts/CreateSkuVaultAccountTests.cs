namespace OmnaeSkuVaultWebApi.FunctionalTests.FunctionalTests.SkuVaultAccounts;

using OmnaeSkuVaultWebApi.SharedTestHelpers.Fakes.SkuVaultAccount;
using OmnaeSkuVaultWebApi.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class CreateSkuVaultAccountTests : TestBase
{
    [Test]
    public async Task create_skuvaultaccount_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeSkuVaultAccount = new FakeSkuVaultAccountForCreationDto { }.Generate();

        // Act
        var route = ApiRoutes.SkuVaultAccounts.Create;
        var result = await _client.PostJsonRequestAsync(route, fakeSkuVaultAccount);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}