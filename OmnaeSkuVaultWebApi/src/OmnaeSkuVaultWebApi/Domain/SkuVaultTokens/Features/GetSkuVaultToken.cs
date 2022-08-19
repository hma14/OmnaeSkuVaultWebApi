namespace OmnaeSkuVaultWebApi.Domain.SkuVaultTokens.Features;

using OmnaeSkuVaultWebApi.Domain.SkuVaultTokens.Dtos;
using OmnaeSkuVaultWebApi.Domain.SkuVaultTokens.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class GetSkuVaultToken
{
    public class SkuVaultTokenQuery : IRequest<SkuVaultTokenDto>
    {
        public readonly Guid Id;

        public SkuVaultTokenQuery(Guid id)
        {
            Id = id;
        }
    }

    public class Handler : IRequestHandler<SkuVaultTokenQuery, SkuVaultTokenDto>
    {
        private readonly ISkuVaultTokenRepository _skuVaultTokenRepository;
        private readonly IMapper _mapper;

        public Handler(ISkuVaultTokenRepository skuVaultTokenRepository, IMapper mapper)
        {
            _mapper = mapper;
            _skuVaultTokenRepository = skuVaultTokenRepository;
        }

        public async Task<SkuVaultTokenDto> Handle(SkuVaultTokenQuery request, CancellationToken cancellationToken)
        {
            var result = await _skuVaultTokenRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return _mapper.Map<SkuVaultTokenDto>(result);
        }
    }
}