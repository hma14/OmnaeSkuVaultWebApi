namespace OmnaeSkuVaultWebApi.UnitTests.UnitTests.Domain.SkuVaultTokens;

using OmnaeSkuVaultWebApi.SharedTestHelpers.Fakes.SkuVaultToken;
using OmnaeSkuVaultWebApi.Domain.SkuVaultTokens;
using OmnaeSkuVaultWebApi.Domain.SkuVaultTokens.DomainEvents;
using Bogus;
using FluentAssertions;
using NUnit.Framework;

[Parallelizable]
public class CreateSkuVaultTokenTests
{
    private readonly Faker _faker;

    public CreateSkuVaultTokenTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_create_valid_skuVaultToken()
    {
        // Arrange + Act
        var fakeSkuVaultToken = FakeSkuVaultToken.Generate();

        // Assert
        fakeSkuVaultToken.Should().NotBeNull();
    }

    [Test]
    public void queue_domain_event_on_create()
    {
        // Arrange + Act
        var fakeSkuVaultToken = FakeSkuVaultToken.Generate();

        // Assert
        fakeSkuVaultToken.DomainEvents.Count.Should().Be(1);
        fakeSkuVaultToken.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(SkuVaultTokenCreated));
    }
}