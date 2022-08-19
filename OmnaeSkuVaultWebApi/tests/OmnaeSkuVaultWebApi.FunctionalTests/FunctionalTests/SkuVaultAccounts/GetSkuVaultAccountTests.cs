namespace OmnaeSkuVaultWebApi.FunctionalTests.FunctionalTests.SkuVaultAccounts;

using OmnaeSkuVaultWebApi.SharedTestHelpers.Fakes.SkuVaultAccount;
using OmnaeSkuVaultWebApi.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetSkuVaultAccountTests : TestBase
{
    [Test]
    public async Task get_skuvaultaccount_returns_success_when_entity_exists()
    {
        // Arrange
        var fakeSkuVaultAccount = FakeSkuVaultAccount.Generate(new FakeSkuVaultAccountForCreationDto().Generate());
        await InsertAsync(fakeSkuVaultAccount);

        // Act
        var route = ApiRoutes.SkuVaultAccounts.GetRecord.Replace(ApiRoutes.SkuVaultAccounts.Id, fakeSkuVaultAccount.Id.ToString());
        var result = await _client.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}