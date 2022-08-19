namespace OmnaeSkuVaultWebApi.Domain.SkuVaultTokens.Features;

using OmnaeSkuVaultWebApi.Domain.SkuVaultTokens.Services;
using OmnaeSkuVaultWebApi.Services;
using SharedKernel.Exceptions;
using MediatR;

public static class DeleteSkuVaultToken
{
    public class DeleteSkuVaultTokenCommand : IRequest<bool>
    {
        public readonly Guid Id;

        public DeleteSkuVaultTokenCommand(Guid skuVaultToken)
        {
            Id = skuVaultToken;
        }
    }

    public class Handler : IRequestHandler<DeleteSkuVaultTokenCommand, bool>
    {
        private readonly ISkuVaultTokenRepository _skuVaultTokenRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(ISkuVaultTokenRepository skuVaultTokenRepository, IUnitOfWork unitOfWork)
        {
            _skuVaultTokenRepository = skuVaultTokenRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteSkuVaultTokenCommand request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _skuVaultTokenRepository.GetById(request.Id, cancellationToken: cancellationToken);

            _skuVaultTokenRepository.Remove(recordToDelete);
            await _unitOfWork.CommitChanges(cancellationToken);
            return true;
        }
    }
}