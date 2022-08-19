namespace OmnaeSkuVaultWebApi.UnitTests.UnitTests.Domain.SkuVaultAccounts.Features;

using OmnaeSkuVaultWebApi.SharedTestHelpers.Fakes.SkuVaultAccount;
using OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts;
using OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts.Dtos;
using OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts.Mappings;
using OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts.Features;
using OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts.Services;
using MapsterMapper;
using FluentAssertions;
using Microsoft.Extensions.Options;
using MockQueryable.Moq;
using Moq;
using Sieve.Models;
using Sieve.Services;
using NUnit.Framework;

public class GetSkuVaultAccountListTests
{
    
    private readonly SieveProcessor _sieveProcessor;
    private readonly Mapper _mapper = new Mapper();
    private readonly Mock<ISkuVaultAccountRepository> _skuVaultAccountRepository;

    public GetSkuVaultAccountListTests()
    {
        _skuVaultAccountRepository = new Mock<ISkuVaultAccountRepository>();
        var sieveOptions = Options.Create(new SieveOptions());
        _sieveProcessor = new SieveProcessor(sieveOptions);
    }
    
    [Test]
    public async Task can_get_paged_list_of_skuVaultAccount()
    {
        //Arrange
        var fakeSkuVaultAccountOne = FakeSkuVaultAccount.Generate();
        var fakeSkuVaultAccountTwo = FakeSkuVaultAccount.Generate();
        var fakeSkuVaultAccountThree = FakeSkuVaultAccount.Generate();
        var skuVaultAccount = new List<SkuVaultAccount>();
        skuVaultAccount.Add(fakeSkuVaultAccountOne);
        skuVaultAccount.Add(fakeSkuVaultAccountTwo);
        skuVaultAccount.Add(fakeSkuVaultAccountThree);
        var mockDbData = skuVaultAccount.AsQueryable().BuildMock();
        
        var queryParameters = new SkuVaultAccountParametersDto() { PageSize = 1, PageNumber = 2 };

        _skuVaultAccountRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);
        
        //Act
        var query = new GetSkuVaultAccountList.SkuVaultAccountListQuery(queryParameters);
        var handler = new GetSkuVaultAccountList.Handler(_skuVaultAccountRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
    }

    [Test]
    public async Task can_filter_skuvaultaccount_list_using_SkuVaultId()
    {
        //Arrange
        var fakeSkuVaultAccountOne = FakeSkuVaultAccount.Generate(new FakeSkuVaultAccountForCreationDto()
            .RuleFor(s => s.SkuVaultId, _ => 1)
            .Generate());
        var fakeSkuVaultAccountTwo = FakeSkuVaultAccount.Generate(new FakeSkuVaultAccountForCreationDto()
            .RuleFor(s => s.SkuVaultId, _ => 2)
            .Generate());
        var queryParameters = new SkuVaultAccountParametersDto() { Filters = $"SkuVaultId == {fakeSkuVaultAccountTwo.SkuVaultId}" };

        var skuVaultAccountList = new List<SkuVaultAccount>() { fakeSkuVaultAccountOne, fakeSkuVaultAccountTwo };
        var mockDbData = skuVaultAccountList.AsQueryable().BuildMock();

        _skuVaultAccountRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetSkuVaultAccountList.SkuVaultAccountListQuery(queryParameters);
        var handler = new GetSkuVaultAccountList.Handler(_skuVaultAccountRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeSkuVaultAccountTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_filter_skuvaultaccount_list_using_SkuVaultTokenId()
    {
        //Arrange
        var fakeSkuVaultAccountOne = FakeSkuVaultAccount.Generate(new FakeSkuVaultAccountForCreationDto()
            .RuleFor(s => s.SkuVaultTokenId, _ => "alpha")
            .Generate());
        var fakeSkuVaultAccountTwo = FakeSkuVaultAccount.Generate(new FakeSkuVaultAccountForCreationDto()
            .RuleFor(s => s.SkuVaultTokenId, _ => "bravo")
            .Generate());
        var queryParameters = new SkuVaultAccountParametersDto() { Filters = $"SkuVaultTokenId == {fakeSkuVaultAccountTwo.SkuVaultTokenId}" };

        var skuVaultAccountList = new List<SkuVaultAccount>() { fakeSkuVaultAccountOne, fakeSkuVaultAccountTwo };
        var mockDbData = skuVaultAccountList.AsQueryable().BuildMock();

        _skuVaultAccountRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetSkuVaultAccountList.SkuVaultAccountListQuery(queryParameters);
        var handler = new GetSkuVaultAccountList.Handler(_skuVaultAccountRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeSkuVaultAccountTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_filter_skuvaultaccount_list_using_CompanyId()
    {
        //Arrange
        var fakeSkuVaultAccountOne = FakeSkuVaultAccount.Generate(new FakeSkuVaultAccountForCreationDto()
            .RuleFor(s => s.CompanyId, _ => 1)
            .Generate());
        var fakeSkuVaultAccountTwo = FakeSkuVaultAccount.Generate(new FakeSkuVaultAccountForCreationDto()
            .RuleFor(s => s.CompanyId, _ => 2)
            .Generate());
        var queryParameters = new SkuVaultAccountParametersDto() { Filters = $"CompanyId == {fakeSkuVaultAccountTwo.CompanyId}" };

        var skuVaultAccountList = new List<SkuVaultAccount>() { fakeSkuVaultAccountOne, fakeSkuVaultAccountTwo };
        var mockDbData = skuVaultAccountList.AsQueryable().BuildMock();

        _skuVaultAccountRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetSkuVaultAccountList.SkuVaultAccountListQuery(queryParameters);
        var handler = new GetSkuVaultAccountList.Handler(_skuVaultAccountRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeSkuVaultAccountTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_filter_skuvaultaccount_list_using_IsVendor()
    {
        //Arrange
        var fakeSkuVaultAccountOne = FakeSkuVaultAccount.Generate(new FakeSkuVaultAccountForCreationDto()
            .RuleFor(s => s.IsVendor, _ => false)
            .Generate());
        var fakeSkuVaultAccountTwo = FakeSkuVaultAccount.Generate(new FakeSkuVaultAccountForCreationDto()
            .RuleFor(s => s.IsVendor, _ => true)
            .Generate());
        var queryParameters = new SkuVaultAccountParametersDto() { Filters = $"IsVendor == {fakeSkuVaultAccountTwo.IsVendor}" };

        var skuVaultAccountList = new List<SkuVaultAccount>() { fakeSkuVaultAccountOne, fakeSkuVaultAccountTwo };
        var mockDbData = skuVaultAccountList.AsQueryable().BuildMock();

        _skuVaultAccountRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetSkuVaultAccountList.SkuVaultAccountListQuery(queryParameters);
        var handler = new GetSkuVaultAccountList.Handler(_skuVaultAccountRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeSkuVaultAccountTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_skuvaultaccount_by_SkuVaultTokenId()
    {
        //Arrange
        var fakeSkuVaultAccountOne = FakeSkuVaultAccount.Generate(new FakeSkuVaultAccountForCreationDto()
            .RuleFor(s => s.SkuVaultTokenId, _ => "alpha")
            .Generate());
        var fakeSkuVaultAccountTwo = FakeSkuVaultAccount.Generate(new FakeSkuVaultAccountForCreationDto()
            .RuleFor(s => s.SkuVaultTokenId, _ => "bravo")
            .Generate());
        var queryParameters = new SkuVaultAccountParametersDto() { SortOrder = "-SkuVaultTokenId" };

        var SkuVaultAccountList = new List<SkuVaultAccount>() { fakeSkuVaultAccountOne, fakeSkuVaultAccountTwo };
        var mockDbData = SkuVaultAccountList.AsQueryable().BuildMock();

        _skuVaultAccountRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetSkuVaultAccountList.SkuVaultAccountListQuery(queryParameters);
        var handler = new GetSkuVaultAccountList.Handler(_skuVaultAccountRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.FirstOrDefault()
            .Should().BeEquivalentTo(fakeSkuVaultAccountTwo, options =>
                options.ExcludingMissingMembers());
        response.Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeSkuVaultAccountOne, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_skuvaultaccount_by_CompanyId()
    {
        //Arrange
        var fakeSkuVaultAccountOne = FakeSkuVaultAccount.Generate(new FakeSkuVaultAccountForCreationDto()
            .RuleFor(s => s.CompanyId, _ => 1)
            .Generate());
        var fakeSkuVaultAccountTwo = FakeSkuVaultAccount.Generate(new FakeSkuVaultAccountForCreationDto()
            .RuleFor(s => s.CompanyId, _ => 2)
            .Generate());
        var queryParameters = new SkuVaultAccountParametersDto() { SortOrder = "-CompanyId" };

        var SkuVaultAccountList = new List<SkuVaultAccount>() { fakeSkuVaultAccountOne, fakeSkuVaultAccountTwo };
        var mockDbData = SkuVaultAccountList.AsQueryable().BuildMock();

        _skuVaultAccountRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetSkuVaultAccountList.SkuVaultAccountListQuery(queryParameters);
        var handler = new GetSkuVaultAccountList.Handler(_skuVaultAccountRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.FirstOrDefault()
            .Should().BeEquivalentTo(fakeSkuVaultAccountTwo, options =>
                options.ExcludingMissingMembers());
        response.Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeSkuVaultAccountOne, options =>
                options.ExcludingMissingMembers());
    }


}