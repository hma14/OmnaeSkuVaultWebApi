namespace OmnaeSkuVaultWebApi.IntegrationTests.FeatureTests.SkuVaultAccounts;

using OmnaeSkuVaultWebApi.SharedTestHelpers.Fakes.SkuVaultAccount;
using OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts.Dtos;
using SharedKernel.Exceptions;
using OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using static TestFixture;

public class UpdateSkuVaultAccountCommandTests : TestBase
{
    [Test]
    public async Task can_update_existing_skuvaultaccount_in_db()
    {
        // Arrange
        var fakeSkuVaultAccountOne = FakeSkuVaultAccount.Generate(new FakeSkuVaultAccountForCreationDto().Generate());
        var updatedSkuVaultAccountDto = new FakeSkuVaultAccountForUpdateDto().Generate();
        await InsertAsync(fakeSkuVaultAccountOne);

        var skuVaultAccount = await ExecuteDbContextAsync(db => db.SkuVaultAccounts
            .FirstOrDefaultAsync(s => s.Id == fakeSkuVaultAccountOne.Id));
        var id = skuVaultAccount.Id;

        // Act
        var command = new UpdateSkuVaultAccount.UpdateSkuVaultAccountCommand(id, updatedSkuVaultAccountDto);
        await SendAsync(command);
        var updatedSkuVaultAccount = await ExecuteDbContextAsync(db => db.SkuVaultAccounts.FirstOrDefaultAsync(s => s.Id == id));

        // Assert
        updatedSkuVaultAccount.Should().BeEquivalentTo(updatedSkuVaultAccountDto, options =>
            options.ExcludingMissingMembers());
    }
}