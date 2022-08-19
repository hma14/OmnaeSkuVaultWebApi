namespace OmnaeSkuVaultWebApi.IntegrationTests.FeatureTests.SkuVaultTokens;

using OmnaeSkuVaultWebApi.SharedTestHelpers.Fakes.SkuVaultToken;
using OmnaeSkuVaultWebApi.Domain.SkuVaultTokens.Dtos;
using SharedKernel.Exceptions;
using OmnaeSkuVaultWebApi.Domain.SkuVaultTokens.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using static TestFixture;

public class UpdateSkuVaultTokenCommandTests : TestBase
{
    [Test]
    public async Task can_update_existing_skuvaulttoken_in_db()
    {
        // Arrange
        var fakeSkuVaultTokenOne = FakeSkuVaultToken.Generate(new FakeSkuVaultTokenForCreationDto().Generate());
        var updatedSkuVaultTokenDto = new FakeSkuVaultTokenForUpdateDto().Generate();
        await InsertAsync(fakeSkuVaultTokenOne);

        var skuVaultToken = await ExecuteDbContextAsync(db => db.SkuVaultTokens
            .FirstOrDefaultAsync(s => s.Id == fakeSkuVaultTokenOne.Id));
        var id = skuVaultToken.Id;

        // Act
        var command = new UpdateSkuVaultToken.UpdateSkuVaultTokenCommand(id, updatedSkuVaultTokenDto);
        await SendAsync(command);
        var updatedSkuVaultToken = await ExecuteDbContextAsync(db => db.SkuVaultTokens.FirstOrDefaultAsync(s => s.Id == id));

        // Assert
        updatedSkuVaultToken.Should().BeEquivalentTo(updatedSkuVaultTokenDto, options =>
            options.ExcludingMissingMembers());
    }
}