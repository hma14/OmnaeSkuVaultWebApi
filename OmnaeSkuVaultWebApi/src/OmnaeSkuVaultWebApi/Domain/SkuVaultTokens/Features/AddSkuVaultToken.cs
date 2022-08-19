namespace OmnaeSkuVaultWebApi.Domain.SkuVaultTokens.Features;

using OmnaeSkuVaultWebApi.Domain.SkuVaultTokens.Services;
using OmnaeSkuVaultWebApi.Domain.SkuVaultTokens;
using OmnaeSkuVaultWebApi.Domain.SkuVaultTokens.Dtos;
using OmnaeSkuVaultWebApi.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class AddSkuVaultToken
{
    public class AddSkuVaultTokenCommand : IRequest<SkuVaultTokenDto>
    {
        public readonly SkuVaultTokenForCreationDto SkuVaultTokenToAdd;

        public AddSkuVaultTokenCommand(SkuVaultTokenForCreationDto skuVaultTokenToAdd)
        {
            SkuVaultTokenToAdd = skuVaultTokenToAdd;
        }
    }

    public class Handler : IRequestHandler<AddSkuVaultTokenCommand, SkuVaultTokenDto>
    {
        private readonly ISkuVaultTokenRepository _skuVaultTokenRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(ISkuVaultTokenRepository skuVaultTokenRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _skuVaultTokenRepository = skuVaultTokenRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<SkuVaultTokenDto> Handle(AddSkuVaultTokenCommand request, CancellationToken cancellationToken)
        {
            var skuVaultToken = SkuVaultToken.Create(request.SkuVaultTokenToAdd);
            await _skuVaultTokenRepository.Add(skuVaultToken, cancellationToken);

            await _unitOfWork.CommitChanges(cancellationToken);

            var skuVaultTokenAdded = await _skuVaultTokenRepository.GetById(skuVaultToken.Id, cancellationToken: cancellationToken);
            return _mapper.Map<SkuVaultTokenDto>(skuVaultTokenAdded);
        }
    }
}