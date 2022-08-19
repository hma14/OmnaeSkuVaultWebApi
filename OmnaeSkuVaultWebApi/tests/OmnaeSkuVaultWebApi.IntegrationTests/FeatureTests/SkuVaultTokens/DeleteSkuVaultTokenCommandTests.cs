namespace OmnaeSkuVaultWebApi.IntegrationTests.FeatureTests.SkuVaultTokens;

using OmnaeSkuVaultWebApi.SharedTestHelpers.Fakes.SkuVaultToken;
using OmnaeSkuVaultWebApi.Domain.SkuVaultTokens.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;

public class DeleteSkuVaultTokenCommandTests : TestBase
{
    [Test]
    public async Task can_delete_skuvaulttoken_from_db()
    {
        // Arrange
        var fakeSkuVaultTokenOne = FakeSkuVaultToken.Generate(new FakeSkuVaultTokenForCreationDto().Generate());
        await InsertAsync(fakeSkuVaultTokenOne);
        var skuVaultToken = await ExecuteDbContextAsync(db => db.SkuVaultTokens
            .FirstOrDefaultAsync(s => s.Id == fakeSkuVaultTokenOne.Id));

        // Act
        var command = new DeleteSkuVaultToken.DeleteSkuVaultTokenCommand(skuVaultToken.Id);
        await SendAsync(command);
        var skuVaultTokenResponse = await ExecuteDbContextAsync(db => db.SkuVaultTokens.CountAsync(s => s.Id == skuVaultToken.Id));

        // Assert
        skuVaultTokenResponse.Should().Be(0);
    }

    [Test]
    public async Task delete_skuvaulttoken_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteSkuVaultToken.DeleteSkuVaultTokenCommand(badId);
        Func<Task> act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task can_softdelete_skuvaulttoken_from_db()
    {
        // Arrange
        var fakeSkuVaultTokenOne = FakeSkuVaultToken.Generate(new FakeSkuVaultTokenForCreationDto().Generate());
        await InsertAsync(fakeSkuVaultTokenOne);
        var skuVaultToken = await ExecuteDbContextAsync(db => db.SkuVaultTokens
            .FirstOrDefaultAsync(s => s.Id == fakeSkuVaultTokenOne.Id));

        // Act
        var command = new DeleteSkuVaultToken.DeleteSkuVaultTokenCommand(skuVaultToken.Id);
        await SendAsync(command);
        var deletedSkuVaultToken = await ExecuteDbContextAsync(db => db.SkuVaultTokens
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == skuVaultToken.Id));

        // Assert
        deletedSkuVaultToken?.IsDeleted.Should().BeTrue();
    }
}