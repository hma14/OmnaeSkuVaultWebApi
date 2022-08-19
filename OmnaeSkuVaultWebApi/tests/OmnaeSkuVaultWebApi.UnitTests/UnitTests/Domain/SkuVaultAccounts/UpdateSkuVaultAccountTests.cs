namespace OmnaeSkuVaultWebApi.UnitTests.UnitTests.Domain.SkuVaultAccounts;

using OmnaeSkuVaultWebApi.SharedTestHelpers.Fakes.SkuVaultAccount;
using OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts;
using OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts.DomainEvents;
using Bogus;
using FluentAssertions;
using NUnit.Framework;

[Parallelizable]
public class UpdateSkuVaultAccountTests
{
    private readonly Faker _faker;

    public UpdateSkuVaultAccountTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_update_skuVaultAccount()
    {
        // Arrange
        var fakeSkuVaultAccount = FakeSkuVaultAccount.Generate();
        var updatedSkuVaultAccount = new FakeSkuVaultAccountForUpdateDto().Generate();
        
        // Act
        fakeSkuVaultAccount.Update(updatedSkuVaultAccount);

        // Assert
        fakeSkuVaultAccount.Should().BeEquivalentTo(updatedSkuVaultAccount, options =>
            options.ExcludingMissingMembers());
    }
    
    [Test]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakeSkuVaultAccount = FakeSkuVaultAccount.Generate();
        var updatedSkuVaultAccount = new FakeSkuVaultAccountForUpdateDto().Generate();
        fakeSkuVaultAccount.DomainEvents.Clear();
        
        // Act
        fakeSkuVaultAccount.Update(updatedSkuVaultAccount);

        // Assert
        fakeSkuVaultAccount.DomainEvents.Count.Should().Be(1);
        fakeSkuVaultAccount.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(SkuVaultAccountUpdated));
    }
}