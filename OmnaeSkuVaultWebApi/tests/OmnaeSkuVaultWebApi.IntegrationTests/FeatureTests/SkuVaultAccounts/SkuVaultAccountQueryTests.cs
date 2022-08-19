namespace OmnaeSkuVaultWebApi.IntegrationTests.FeatureTests.SkuVaultAccounts;

using OmnaeSkuVaultWebApi.SharedTestHelpers.Fakes.SkuVaultAccount;
using OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;

public class SkuVaultAccountQueryTests : TestBase
{
    [Test]
    public async Task can_get_existing_skuvaultaccount_with_accurate_props()
    {
        // Arrange
        var fakeSkuVaultAccountOne = FakeSkuVaultAccount.Generate(new FakeSkuVaultAccountForCreationDto().Generate());
        await InsertAsync(fakeSkuVaultAccountOne);

        // Act
        var query = new GetSkuVaultAccount.SkuVaultAccountQuery(fakeSkuVaultAccountOne.Id);
        var skuVaultAccount = await SendAsync(query);

        // Assert
        skuVaultAccount.Should().BeEquivalentTo(fakeSkuVaultAccountOne, options =>
            options.ExcludingMissingMembers());
    }

    [Test]
    public async Task get_skuvaultaccount_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var query = new GetSkuVaultAccount.SkuVaultAccountQuery(badId);
        Func<Task> act = () => SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}