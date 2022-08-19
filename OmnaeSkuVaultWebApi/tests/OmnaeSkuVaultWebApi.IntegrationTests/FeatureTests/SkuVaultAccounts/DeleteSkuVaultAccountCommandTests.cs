namespace OmnaeSkuVaultWebApi.IntegrationTests.FeatureTests.SkuVaultAccounts;

using OmnaeSkuVaultWebApi.SharedTestHelpers.Fakes.SkuVaultAccount;
using OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;

public class DeleteSkuVaultAccountCommandTests : TestBase
{
    [Test]
    public async Task can_delete_skuvaultaccount_from_db()
    {
        // Arrange
        var fakeSkuVaultAccountOne = FakeSkuVaultAccount.Generate(new FakeSkuVaultAccountForCreationDto().Generate());
        await InsertAsync(fakeSkuVaultAccountOne);
        var skuVaultAccount = await ExecuteDbContextAsync(db => db.SkuVaultAccounts
            .FirstOrDefaultAsync(s => s.Id == fakeSkuVaultAccountOne.Id));

        // Act
        var command = new DeleteSkuVaultAccount.DeleteSkuVaultAccountCommand(skuVaultAccount.Id);
        await SendAsync(command);
        var skuVaultAccountResponse = await ExecuteDbContextAsync(db => db.SkuVaultAccounts.CountAsync(s => s.Id == skuVaultAccount.Id));

        // Assert
        skuVaultAccountResponse.Should().Be(0);
    }

    [Test]
    public async Task delete_skuvaultaccount_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteSkuVaultAccount.DeleteSkuVaultAccountCommand(badId);
        Func<Task> act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task can_softdelete_skuvaultaccount_from_db()
    {
        // Arrange
        var fakeSkuVaultAccountOne = FakeSkuVaultAccount.Generate(new FakeSkuVaultAccountForCreationDto().Generate());
        await InsertAsync(fakeSkuVaultAccountOne);
        var skuVaultAccount = await ExecuteDbContextAsync(db => db.SkuVaultAccounts
            .FirstOrDefaultAsync(s => s.Id == fakeSkuVaultAccountOne.Id));

        // Act
        var command = new DeleteSkuVaultAccount.DeleteSkuVaultAccountCommand(skuVaultAccount.Id);
        await SendAsync(command);
        var deletedSkuVaultAccount = await ExecuteDbContextAsync(db => db.SkuVaultAccounts
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == skuVaultAccount.Id));

        // Assert
        deletedSkuVaultAccount?.IsDeleted.Should().BeTrue();
    }
}