namespace OmnaeSkuVaultWebApi.IntegrationTests.FeatureTests.SkuVaultAccounts;

using OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts.Dtos;
using OmnaeSkuVaultWebApi.SharedTestHelpers.Fakes.SkuVaultAccount;
using SharedKernel.Exceptions;
using OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts.Features;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;

public class SkuVaultAccountListQueryTests : TestBase
{
    
    [Test]
    public async Task can_get_skuvaultaccount_list()
    {
        // Arrange
        var fakeSkuVaultAccountOne = FakeSkuVaultAccount.Generate(new FakeSkuVaultAccountForCreationDto().Generate());
        var fakeSkuVaultAccountTwo = FakeSkuVaultAccount.Generate(new FakeSkuVaultAccountForCreationDto().Generate());
        var queryParameters = new SkuVaultAccountParametersDto();

        await InsertAsync(fakeSkuVaultAccountOne, fakeSkuVaultAccountTwo);

        // Act
        var query = new GetSkuVaultAccountList.SkuVaultAccountListQuery(queryParameters);
        var skuVaultAccounts = await SendAsync(query);

        // Assert
        skuVaultAccounts.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}