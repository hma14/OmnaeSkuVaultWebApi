namespace OmnaeSkuVaultWebApi.IntegrationTests.FeatureTests.SkuVaultAccounts;

using OmnaeSkuVaultWebApi.SharedTestHelpers.Fakes.SkuVaultAccount;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts.Features;
using static TestFixture;
using SharedKernel.Exceptions;

public class AddSkuVaultAccountCommandTests : TestBase
{
    [Test]
    public async Task can_add_new_skuvaultaccount_to_db()
    {
        // Arrange
        var fakeSkuVaultAccountOne = new FakeSkuVaultAccountForCreationDto().Generate();

        // Act
        var command = new AddSkuVaultAccount.AddSkuVaultAccountCommand(fakeSkuVaultAccountOne);
        var skuVaultAccountReturned = await SendAsync(command);
        var skuVaultAccountCreated = await ExecuteDbContextAsync(db => db.SkuVaultAccounts
            .FirstOrDefaultAsync(s => s.Id == skuVaultAccountReturned.Id));

        // Assert
        skuVaultAccountReturned.Should().BeEquivalentTo(fakeSkuVaultAccountOne, options =>
            options.ExcludingMissingMembers());
        skuVaultAccountCreated.Should().BeEquivalentTo(fakeSkuVaultAccountOne, options =>
            options.ExcludingMissingMembers());
    }
}