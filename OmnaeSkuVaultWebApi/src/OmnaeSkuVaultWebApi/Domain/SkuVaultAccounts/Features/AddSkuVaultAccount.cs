namespace OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts.Features;

using OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts.Services;
using OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts;
using OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts.Dtos;
using OmnaeSkuVaultWebApi.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class AddSkuVaultAccount
{
    public class AddSkuVaultAccountCommand : IRequest<SkuVaultAccountDto>
    {
        public readonly SkuVaultAccountForCreationDto SkuVaultAccountToAdd;

        public AddSkuVaultAccountCommand(SkuVaultAccountForCreationDto skuVaultAccountToAdd)
        {
            SkuVaultAccountToAdd = skuVaultAccountToAdd;
        }
    }

    public class Handler : IRequestHandler<AddSkuVaultAccountCommand, SkuVaultAccountDto>
    {
        private readonly ISkuVaultAccountRepository _skuVaultAccountRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(ISkuVaultAccountRepository skuVaultAccountRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _skuVaultAccountRepository = skuVaultAccountRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<SkuVaultAccountDto> Handle(AddSkuVaultAccountCommand request, CancellationToken cancellationToken)
        {
            var skuVaultAccount = SkuVaultAccount.Create(request.SkuVaultAccountToAdd);
            await _skuVaultAccountRepository.Add(skuVaultAccount, cancellationToken);

            await _unitOfWork.CommitChanges(cancellationToken);

            var skuVaultAccountAdded = await _skuVaultAccountRepository.GetById(skuVaultAccount.Id, cancellationToken: cancellationToken);
            return _mapper.Map<SkuVaultAccountDto>(skuVaultAccountAdded);
        }
    }
}