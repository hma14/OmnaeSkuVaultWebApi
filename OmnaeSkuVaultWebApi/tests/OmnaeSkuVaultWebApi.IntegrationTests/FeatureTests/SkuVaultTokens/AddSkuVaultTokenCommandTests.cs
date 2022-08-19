namespace OmnaeSkuVaultWebApi.IntegrationTests.FeatureTests.SkuVaultTokens;

using OmnaeSkuVaultWebApi.SharedTestHelpers.Fakes.SkuVaultToken;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using OmnaeSkuVaultWebApi.Domain.SkuVaultTokens.Features;
using static TestFixture;
using SharedKernel.Exceptions;

public class AddSkuVaultTokenCommandTests : TestBase
{
    [Test]
    public async Task can_add_new_skuvaulttoken_to_db()
    {
        // Arrange
        var fakeSkuVaultTokenOne = new FakeSkuVaultTokenForCreationDto().Generate();

        // Act
        var command = new AddSkuVaultToken.AddSkuVaultTokenCommand(fakeSkuVaultTokenOne);
        var skuVaultTokenReturned = await SendAsync(command);
        var skuVaultTokenCreated = await ExecuteDbContextAsync(db => db.SkuVaultTokens
            .FirstOrDefaultAsync(s => s.Id == skuVaultTokenReturned.Id));

        // Assert
        skuVaultTokenReturned.Should().BeEquivalentTo(fakeSkuVaultTokenOne, options =>
            options.ExcludingMissingMembers());
        skuVaultTokenCreated.Should().BeEquivalentTo(fakeSkuVaultTokenOne, options =>
            options.ExcludingMissingMembers());
    }
}