namespace OmnaeSkuVaultWebApi.UnitTests.UnitTests.Domain.SkuVaultTokens;

using OmnaeSkuVaultWebApi.SharedTestHelpers.Fakes.SkuVaultToken;
using OmnaeSkuVaultWebApi.Domain.SkuVaultTokens;
using OmnaeSkuVaultWebApi.Domain.SkuVaultTokens.DomainEvents;
using Bogus;
using FluentAssertions;
using NUnit.Framework;

[Parallelizable]
public class UpdateSkuVaultTokenTests
{
    private readonly Faker _faker;

    public UpdateSkuVaultTokenTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_update_skuVaultToken()
    {
        // Arrange
        var fakeSkuVaultToken = FakeSkuVaultToken.Generate();
        var updatedSkuVaultToken = new FakeSkuVaultTokenForUpdateDto().Generate();
        
        // Act
        fakeSkuVaultToken.Update(updatedSkuVaultToken);

        // Assert
        fakeSkuVaultToken.Should().BeEquivalentTo(updatedSkuVaultToken, options =>
            options.ExcludingMissingMembers());
    }
    
    [Test]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakeSkuVaultToken = FakeSkuVaultToken.Generate();
        var updatedSkuVaultToken = new FakeSkuVaultTokenForUpdateDto().Generate();
        fakeSkuVaultToken.DomainEvents.Clear();
        
        // Act
        fakeSkuVaultToken.Update(updatedSkuVaultToken);

        // Assert
        fakeSkuVaultToken.DomainEvents.Count.Should().Be(1);
        fakeSkuVaultToken.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(SkuVaultTokenUpdated));
    }
}