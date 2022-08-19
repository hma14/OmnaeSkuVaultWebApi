namespace OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts.Features;

using OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts;
using OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts.Dtos;
using OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts.Validators;
using OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts.Services;
using OmnaeSkuVaultWebApi.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class UpdateSkuVaultAccount
{
    public class UpdateSkuVaultAccountCommand : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly SkuVaultAccountForUpdateDto SkuVaultAccountToUpdate;

        public UpdateSkuVaultAccountCommand(Guid skuVaultAccount, SkuVaultAccountForUpdateDto newSkuVaultAccountData)
        {
            Id = skuVaultAccount;
            SkuVaultAccountToUpdate = newSkuVaultAccountData;
        }
    }

    public class Handler : IRequestHandler<UpdateSkuVaultAccountCommand, bool>
    {
        private readonly ISkuVaultAccountRepository _skuVaultAccountRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(ISkuVaultAccountRepository skuVaultAccountRepository, IUnitOfWork unitOfWork)
        {
            _skuVaultAccountRepository = skuVaultAccountRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateSkuVaultAccountCommand request, CancellationToken cancellationToken)
        {
            var skuVaultAccountToUpdate = await _skuVaultAccountRepository.GetById(request.Id, cancellationToken: cancellationToken);

            skuVaultAccountToUpdate.Update(request.SkuVaultAccountToUpdate);
            await _unitOfWork.CommitChanges(cancellationToken);

            return true;
        }
    }
}