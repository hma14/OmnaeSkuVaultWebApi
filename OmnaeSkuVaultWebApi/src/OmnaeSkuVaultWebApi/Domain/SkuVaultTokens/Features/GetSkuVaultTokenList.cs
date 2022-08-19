namespace OmnaeSkuVaultWebApi.Domain.SkuVaultTokens.Features;

using OmnaeSkuVaultWebApi.Domain.SkuVaultTokens.Dtos;
using OmnaeSkuVaultWebApi.Domain.SkuVaultTokens.Services;
using OmnaeSkuVaultWebApi.Wrappers;
using SharedKernel.Exceptions;
using MapsterMapper;
using Mapster;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetSkuVaultTokenList
{
    public class SkuVaultTokenListQuery : IRequest<PagedList<SkuVaultTokenDto>>
    {
        public readonly SkuVaultTokenParametersDto QueryParameters;

        public SkuVaultTokenListQuery(SkuVaultTokenParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public class Handler : IRequestHandler<SkuVaultTokenListQuery, PagedList<SkuVaultTokenDto>>
    {
        private readonly ISkuVaultTokenRepository _skuVaultTokenRepository;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(ISkuVaultTokenRepository skuVaultTokenRepository, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _skuVaultTokenRepository = skuVaultTokenRepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<SkuVaultTokenDto>> Handle(SkuVaultTokenListQuery request, CancellationToken cancellationToken)
        {
            var collection = _skuVaultTokenRepository.Query();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "Id",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectToType<SkuVaultTokenDto>();

            return await PagedList<SkuVaultTokenDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}