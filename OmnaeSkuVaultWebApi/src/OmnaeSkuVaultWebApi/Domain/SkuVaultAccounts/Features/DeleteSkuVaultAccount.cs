namespace OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts.Features;

using OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts.Services;
using OmnaeSkuVaultWebApi.Services;
using SharedKernel.Exceptions;
using MediatR;

public static class DeleteSkuVaultAccount
{
    public class DeleteSkuVaultAccountCommand : IRequest<bool>
    {
        public readonly Guid Id;

        public DeleteSkuVaultAccountCommand(Guid skuVaultAccount)
        {
            Id = skuVaultAccount;
        }
    }

    public class Handler : IRequestHandler<DeleteSkuVaultAccountCommand, bool>
    {
        private readonly ISkuVaultAccountRepository _skuVaultAccountRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(ISkuVaultAccountRepository skuVaultAccountRepository, IUnitOfWork unitOfWork)
        {
            _skuVaultAccountRepository = skuVaultAccountRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteSkuVaultAccountCommand request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _skuVaultAccountRepository.GetById(request.Id, cancellationToken: cancellationToken);

            _skuVaultAccountRepository.Remove(recordToDelete);
            await _unitOfWork.CommitChanges(cancellationToken);
            return true;
        }
    }
}