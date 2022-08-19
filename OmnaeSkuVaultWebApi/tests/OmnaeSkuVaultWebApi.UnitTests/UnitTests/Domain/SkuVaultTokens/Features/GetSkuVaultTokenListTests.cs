namespace OmnaeSkuVaultWebApi.UnitTests.UnitTests.Domain.SkuVaultTokens.Features;

using OmnaeSkuVaultWebApi.SharedTestHelpers.Fakes.SkuVaultToken;
using OmnaeSkuVaultWebApi.Domain.SkuVaultTokens;
using OmnaeSkuVaultWebApi.Domain.SkuVaultTokens.Dtos;
using OmnaeSkuVaultWebApi.Domain.SkuVaultTokens.Mappings;
using OmnaeSkuVaultWebApi.Domain.SkuVaultTokens.Features;
using OmnaeSkuVaultWebApi.Domain.SkuVaultTokens.Services;
using MapsterMapper;
using FluentAssertions;
using Microsoft.Extensions.Options;
using MockQueryable.Moq;
using Moq;
using Sieve.Models;
using Sieve.Services;
using NUnit.Framework;

public class GetSkuVaultTokenListTests
{
    
    private readonly SieveProcessor _sieveProcessor;
    private readonly Mapper _mapper = new Mapper();
    private readonly Mock<ISkuVaultTokenRepository> _skuVaultTokenRepository;

    public GetSkuVaultTokenListTests()
    {
        _skuVaultTokenRepository = new Mock<ISkuVaultTokenRepository>();
        var sieveOptions = Options.Create(new SieveOptions());
        _sieveProcessor = new SieveProcessor(sieveOptions);
    }
    
    [Test]
    public async Task can_get_paged_list_of_skuVaultToken()
    {
        //Arrange
        var fakeSkuVaultTokenOne = FakeSkuVaultToken.Generate();
        var fakeSkuVaultTokenTwo = FakeSkuVaultToken.Generate();
        var fakeSkuVaultTokenThree = FakeSkuVaultToken.Generate();
        var skuVaultToken = new List<SkuVaultToken>();
        skuVaultToken.Add(fakeSkuVaultTokenOne);
        skuVaultToken.Add(fakeSkuVaultTokenTwo);
        skuVaultToken.Add(fakeSkuVaultTokenThree);
        var mockDbData = skuVaultToken.AsQueryable().BuildMock();
        
        var queryParameters = new SkuVaultTokenParametersDto() { PageSize = 1, PageNumber = 2 };

        _skuVaultTokenRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);
        
        //Act
        var query = new GetSkuVaultTokenList.SkuVaultTokenListQuery(queryParameters);
        var handler = new GetSkuVaultTokenList.Handler(_skuVaultTokenRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
    }

    [Test]
    public async Task can_filter_skuvaulttoken_list_using_TenantToken()
    {
        //Arrange
        var fakeSkuVaultTokenOne = FakeSkuVaultToken.Generate(new FakeSkuVaultTokenForCreationDto()
            .RuleFor(s => s.TenantToken, _ => "alpha")
            .Generate());
        var fakeSkuVaultTokenTwo = FakeSkuVaultToken.Generate(new FakeSkuVaultTokenForCreationDto()
            .RuleFor(s => s.TenantToken, _ => "bravo")
            .Generate());
        var queryParameters = new SkuVaultTokenParametersDto() { Filters = $"TenantToken == {fakeSkuVaultTokenTwo.TenantToken}" };

        var skuVaultTokenList = new List<SkuVaultToken>() { fakeSkuVaultTokenOne, fakeSkuVaultTokenTwo };
        var mockDbData = skuVaultTokenList.AsQueryable().BuildMock();

        _skuVaultTokenRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetSkuVaultTokenList.SkuVaultTokenListQuery(queryParameters);
        var handler = new GetSkuVaultTokenList.Handler(_skuVaultTokenRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeSkuVaultTokenTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_filter_skuvaulttoken_list_using_UserToken()
    {
        //Arrange
        var fakeSkuVaultTokenOne = FakeSkuVaultToken.Generate(new FakeSkuVaultTokenForCreationDto()
            .RuleFor(s => s.UserToken, _ => "alpha")
            .Generate());
        var fakeSkuVaultTokenTwo = FakeSkuVaultToken.Generate(new FakeSkuVaultTokenForCreationDto()
            .RuleFor(s => s.UserToken, _ => "bravo")
            .Generate());
        var queryParameters = new SkuVaultTokenParametersDto() { Filters = $"UserToken == {fakeSkuVaultTokenTwo.UserToken}" };

        var skuVaultTokenList = new List<SkuVaultToken>() { fakeSkuVaultTokenOne, fakeSkuVaultTokenTwo };
        var mockDbData = skuVaultTokenList.AsQueryable().BuildMock();

        _skuVaultTokenRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetSkuVaultTokenList.SkuVaultTokenListQuery(queryParameters);
        var handler = new GetSkuVaultTokenList.Handler(_skuVaultTokenRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeSkuVaultTokenTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_filter_skuvaulttoken_list_using_CompanyId()
    {
        //Arrange
        var fakeSkuVaultTokenOne = FakeSkuVaultToken.Generate(new FakeSkuVaultTokenForCreationDto()
            .RuleFor(s => s.CompanyId, _ => 1)
            .Generate());
        var fakeSkuVaultTokenTwo = FakeSkuVaultToken.Generate(new FakeSkuVaultTokenForCreationDto()
            .RuleFor(s => s.CompanyId, _ => 2)
            .Generate());
        var queryParameters = new SkuVaultTokenParametersDto() { Filters = $"CompanyId == {fakeSkuVaultTokenTwo.CompanyId}" };

        var skuVaultTokenList = new List<SkuVaultToken>() { fakeSkuVaultTokenOne, fakeSkuVaultTokenTwo };
        var mockDbData = skuVaultTokenList.AsQueryable().BuildMock();

        _skuVaultTokenRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetSkuVaultTokenList.SkuVaultTokenListQuery(queryParameters);
        var handler = new GetSkuVaultTokenList.Handler(_skuVaultTokenRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeSkuVaultTokenTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_filter_skuvaulttoken_list_using_SkuVaultCompanyId()
    {
        //Arrange
        var fakeSkuVaultTokenOne = FakeSkuVaultToken.Generate(new FakeSkuVaultTokenForCreationDto()
            .RuleFor(s => s.SkuVaultCompanyId, _ => "alpha")
            .Generate());
        var fakeSkuVaultTokenTwo = FakeSkuVaultToken.Generate(new FakeSkuVaultTokenForCreationDto()
            .RuleFor(s => s.SkuVaultCompanyId, _ => "bravo")
            .Generate());
        var queryParameters = new SkuVaultTokenParametersDto() { Filters = $"SkuVaultCompanyId == {fakeSkuVaultTokenTwo.SkuVaultCompanyId}" };

        var skuVaultTokenList = new List<SkuVaultToken>() { fakeSkuVaultTokenOne, fakeSkuVaultTokenTwo };
        var mockDbData = skuVaultTokenList.AsQueryable().BuildMock();

        _skuVaultTokenRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetSkuVaultTokenList.SkuVaultTokenListQuery(queryParameters);
        var handler = new GetSkuVaultTokenList.Handler(_skuVaultTokenRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeSkuVaultTokenTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_filter_skuvaulttoken_list_using_IsRevoked()
    {
        //Arrange
        var fakeSkuVaultTokenOne = FakeSkuVaultToken.Generate(new FakeSkuVaultTokenForCreationDto()
            .RuleFor(s => s.IsRevoked, _ => false)
            .Generate());
        var fakeSkuVaultTokenTwo = FakeSkuVaultToken.Generate(new FakeSkuVaultTokenForCreationDto()
            .RuleFor(s => s.IsRevoked, _ => true)
            .Generate());
        var queryParameters = new SkuVaultTokenParametersDto() { Filters = $"IsRevoked == {fakeSkuVaultTokenTwo.IsRevoked}" };

        var skuVaultTokenList = new List<SkuVaultToken>() { fakeSkuVaultTokenOne, fakeSkuVaultTokenTwo };
        var mockDbData = skuVaultTokenList.AsQueryable().BuildMock();

        _skuVaultTokenRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetSkuVaultTokenList.SkuVaultTokenListQuery(queryParameters);
        var handler = new GetSkuVaultTokenList.Handler(_skuVaultTokenRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeSkuVaultTokenTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_skuvaulttoken_by_UserToken()
    {
        //Arrange
        var fakeSkuVaultTokenOne = FakeSkuVaultToken.Generate(new FakeSkuVaultTokenForCreationDto()
            .RuleFor(s => s.UserToken, _ => "alpha")
            .Generate());
        var fakeSkuVaultTokenTwo = FakeSkuVaultToken.Generate(new FakeSkuVaultTokenForCreationDto()
            .RuleFor(s => s.UserToken, _ => "bravo")
            .Generate());
        var queryParameters = new SkuVaultTokenParametersDto() { SortOrder = "-UserToken" };

        var SkuVaultTokenList = new List<SkuVaultToken>() { fakeSkuVaultTokenOne, fakeSkuVaultTokenTwo };
        var mockDbData = SkuVaultTokenList.AsQueryable().BuildMock();

        _skuVaultTokenRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetSkuVaultTokenList.SkuVaultTokenListQuery(queryParameters);
        var handler = new GetSkuVaultTokenList.Handler(_skuVaultTokenRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.FirstOrDefault()
            .Should().BeEquivalentTo(fakeSkuVaultTokenTwo, options =>
                options.ExcludingMissingMembers());
        response.Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeSkuVaultTokenOne, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_skuvaulttoken_by_CompanyId()
    {
        //Arrange
        var fakeSkuVaultTokenOne = FakeSkuVaultToken.Generate(new FakeSkuVaultTokenForCreationDto()
            .RuleFor(s => s.CompanyId, _ => 1)
            .Generate());
        var fakeSkuVaultTokenTwo = FakeSkuVaultToken.Generate(new FakeSkuVaultTokenForCreationDto()
            .RuleFor(s => s.CompanyId, _ => 2)
            .Generate());
        var queryParameters = new SkuVaultTokenParametersDto() { SortOrder = "-CompanyId" };

        var SkuVaultTokenList = new List<SkuVaultToken>() { fakeSkuVaultTokenOne, fakeSkuVaultTokenTwo };
        var mockDbData = SkuVaultTokenList.AsQueryable().BuildMock();

        _skuVaultTokenRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetSkuVaultTokenList.SkuVaultTokenListQuery(queryParameters);
        var handler = new GetSkuVaultTokenList.Handler(_skuVaultTokenRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.FirstOrDefault()
            .Should().BeEquivalentTo(fakeSkuVaultTokenTwo, options =>
                options.ExcludingMissingMembers());
        response.Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeSkuVaultTokenOne, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_skuvaulttoken_by_SkuVaultCompanyId()
    {
        //Arrange
        var fakeSkuVaultTokenOne = FakeSkuVaultToken.Generate(new FakeSkuVaultTokenForCreationDto()
            .RuleFor(s => s.SkuVaultCompanyId, _ => "alpha")
            .Generate());
        var fakeSkuVaultTokenTwo = FakeSkuVaultToken.Generate(new FakeSkuVaultTokenForCreationDto()
            .RuleFor(s => s.SkuVaultCompanyId, _ => "bravo")
            .Generate());
        var queryParameters = new SkuVaultTokenParametersDto() { SortOrder = "-SkuVaultCompanyId" };

        var SkuVaultTokenList = new List<SkuVaultToken>() { fakeSkuVaultTokenOne, fakeSkuVaultTokenTwo };
        var mockDbData = SkuVaultTokenList.AsQueryable().BuildMock();

        _skuVaultTokenRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetSkuVaultTokenList.SkuVaultTokenListQuery(queryParameters);
        var handler = new GetSkuVaultTokenList.Handler(_skuVaultTokenRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.FirstOrDefault()
            .Should().BeEquivalentTo(fakeSkuVaultTokenTwo, options =>
                options.ExcludingMissingMembers());
        response.Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeSkuVaultTokenOne, options =>
                options.ExcludingMissingMembers());
    }


}