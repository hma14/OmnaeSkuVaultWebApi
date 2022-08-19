namespace OmnaeSkuVaultWebApi.IntegrationTests.FeatureTests.SkuVaultTokens;

using OmnaeSkuVaultWebApi.SharedTestHelpers.Fakes.SkuVaultToken;
using OmnaeSkuVaultWebApi.Domain.SkuVaultTokens.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;

public class SkuVaultTokenQueryTests : TestBase
{
    [Test]
    public async Task can_get_existing_skuvaulttoken_with_accurate_props()
    {
        // Arrange
        var fakeSkuVaultTokenOne = FakeSkuVaultToken.Generate(new FakeSkuVaultTokenForCreationDto().Generate());
        await InsertAsync(fakeSkuVaultTokenOne);

        // Act
        var query = new GetSkuVaultToken.SkuVaultTokenQuery(fakeSkuVaultTokenOne.Id);
        var skuVaultToken = await SendAsync(query);

        // Assert
        skuVaultToken.Should().BeEquivalentTo(fakeSkuVaultTokenOne, options =>
            options.ExcludingMissingMembers());
    }

    [Test]
    public async Task get_skuvaulttoken_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var query = new GetSkuVaultToken.SkuVaultTokenQuery(badId);
        Func<Task> act = () => SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}