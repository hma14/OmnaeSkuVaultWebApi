namespace OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts.Features;

using OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts.Dtos;
using OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts.Services;
using OmnaeSkuVaultWebApi.Wrappers;
using SharedKernel.Exceptions;
using MapsterMapper;
using Mapster;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetSkuVaultAccountList
{
    public class SkuVaultAccountListQuery : IRequest<PagedList<SkuVaultAccountDto>>
    {
        public readonly SkuVaultAccountParametersDto QueryParameters;

        public SkuVaultAccountListQuery(SkuVaultAccountParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public class Handler : IRequestHandler<SkuVaultAccountListQuery, PagedList<SkuVaultAccountDto>>
    {
        private readonly ISkuVaultAccountRepository _skuVaultAccountRepository;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(ISkuVaultAccountRepository skuVaultAccountRepository, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _skuVaultAccountRepository = skuVaultAccountRepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<SkuVaultAccountDto>> Handle(SkuVaultAccountListQuery request, CancellationToken cancellationToken)
        {
            var collection = _skuVaultAccountRepository.Query();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "Id",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectToType<SkuVaultAccountDto>();

            return await PagedList<SkuVaultAccountDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}