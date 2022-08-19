namespace OmnaeSkuVaultWebApi.Domain.SkuVaultTokens.Features;

using OmnaeSkuVaultWebApi.Domain.SkuVaultTokens;
using OmnaeSkuVaultWebApi.Domain.SkuVaultTokens.Dtos;
using OmnaeSkuVaultWebApi.Domain.SkuVaultTokens.Validators;
using OmnaeSkuVaultWebApi.Domain.SkuVaultTokens.Services;
using OmnaeSkuVaultWebApi.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class UpdateSkuVaultToken
{
    public class UpdateSkuVaultTokenCommand : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly SkuVaultTokenForUpdateDto SkuVaultTokenToUpdate;

        public UpdateSkuVaultTokenCommand(Guid skuVaultToken, SkuVaultTokenForUpdateDto newSkuVaultTokenData)
        {
            Id = skuVaultToken;
            SkuVaultTokenToUpdate = newSkuVaultTokenData;
        }
    }

    public class Handler : IRequestHandler<UpdateSkuVaultTokenCommand, bool>
    {
        private readonly ISkuVaultTokenRepository _skuVaultTokenRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(ISkuVaultTokenRepository skuVaultTokenRepository, IUnitOfWork unitOfWork)
        {
            _skuVaultTokenRepository = skuVaultTokenRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateSkuVaultTokenCommand request, CancellationToken cancellationToken)
        {
            var skuVaultTokenToUpdate = await _skuVaultTokenRepository.GetById(request.Id, cancellationToken: cancellationToken);

            skuVaultTokenToUpdate.Update(request.SkuVaultTokenToUpdate);
            await _unitOfWork.CommitChanges(cancellationToken);

            return true;
        }
    }
}