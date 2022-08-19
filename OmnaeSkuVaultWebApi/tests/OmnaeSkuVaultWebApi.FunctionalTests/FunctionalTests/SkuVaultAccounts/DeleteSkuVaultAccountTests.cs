namespace OmnaeSkuVaultWebApi.FunctionalTests.FunctionalTests.SkuVaultAccounts;

using OmnaeSkuVaultWebApi.SharedTestHelpers.Fakes.SkuVaultAccount;
using OmnaeSkuVaultWebApi.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class DeleteSkuVaultAccountTests : TestBase
{
    [Test]
    public async Task delete_skuvaultaccount_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeSkuVaultAccount = FakeSkuVaultAccount.Generate(new FakeSkuVaultAccountForCreationDto().Generate());
        await InsertAsync(fakeSkuVaultAccount);

        // Act
        var route = ApiRoutes.SkuVaultAccounts.Delete.Replace(ApiRoutes.SkuVaultAccounts.Id, fakeSkuVaultAccount.Id.ToString());
        var result = await _client.DeleteRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}