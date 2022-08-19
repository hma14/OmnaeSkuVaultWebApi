namespace OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts.Features;

using OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts.Dtos;
using OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class GetSkuVaultAccount
{
    public class SkuVaultAccountQuery : IRequest<SkuVaultAccountDto>
    {
        public readonly Guid Id;

        public SkuVaultAccountQuery(Guid id)
        {
            Id = id;
        }
    }

    public class Handler : IRequestHandler<SkuVaultAccountQuery, SkuVaultAccountDto>
    {
        private readonly ISkuVaultAccountRepository _skuVaultAccountRepository;
        private readonly IMapper _mapper;

        public Handler(ISkuVaultAccountRepository skuVaultAccountRepository, IMapper mapper)
        {
            _mapper = mapper;
            _skuVaultAccountRepository = skuVaultAccountRepository;
        }

        public async Task<SkuVaultAccountDto> Handle(SkuVaultAccountQuery request, CancellationToken cancellationToken)
        {
            var result = await _skuVaultAccountRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return _mapper.Map<SkuVaultAccountDto>(result);
        }
    }
}