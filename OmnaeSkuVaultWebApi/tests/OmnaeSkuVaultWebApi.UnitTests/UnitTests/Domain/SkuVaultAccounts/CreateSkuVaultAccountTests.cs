namespace OmnaeSkuVaultWebApi.UnitTests.UnitTests.Domain.SkuVaultAccounts;

using OmnaeSkuVaultWebApi.SharedTestHelpers.Fakes.SkuVaultAccount;
using OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts;
using OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts.DomainEvents;
using Bogus;
using FluentAssertions;
using NUnit.Framework;

[Parallelizable]
public class CreateSkuVaultAccountTests
{
    private readonly Faker _faker;

    public CreateSkuVaultAccountTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_create_valid_skuVaultAccount()
    {
        // Arrange + Act
        var fakeSkuVaultAccount = FakeSkuVaultAccount.Generate();

        // Assert
        fakeSkuVaultAccount.Should().NotBeNull();
    }

    [Test]
    public void queue_domain_event_on_create()
    {
        // Arrange + Act
        var fakeSkuVaultAccount = FakeSkuVaultAccount.Generate();

        // Assert
        fakeSkuVaultAccount.DomainEvents.Count.Should().Be(1);
        fakeSkuVaultAccount.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(SkuVaultAccountCreated));
    }
}