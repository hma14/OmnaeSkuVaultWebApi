namespace OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts;

using SharedKernel.Exceptions;
using OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts.Dtos;
using OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts.Validators;
using OmnaeSkuVaultWebApi.Domain.SkuVaultAccounts.DomainEvents;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Sieve.Attributes;


public class SkuVaultAccount : BaseEntity
{
    [Sieve(CanFilter = true, CanSort = false)]
    public virtual int SkuVaultId { get; private set; }

    [Required]
    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string SkuVaultTokenId { get; private set; }

    [Required]
    [Sieve(CanFilter = true, CanSort = true)]
    public virtual int CompanyId { get; private set; }

    [Required]
    [Sieve(CanFilter = true, CanSort = true)]
    public virtual bool IsVendor { get; private set; }


    public static SkuVaultAccount Create(SkuVaultAccountForCreationDto skuVaultAccountForCreationDto)
    {
        new SkuVaultAccountForCreationDtoValidator().ValidateAndThrow(skuVaultAccountForCreationDto);

        var newSkuVaultAccount = new SkuVaultAccount();

        newSkuVaultAccount.SkuVaultId = skuVaultAccountForCreationDto.SkuVaultId;
        newSkuVaultAccount.SkuVaultTokenId = skuVaultAccountForCreationDto.SkuVaultTokenId;
        newSkuVaultAccount.CompanyId = skuVaultAccountForCreationDto.CompanyId;
        newSkuVaultAccount.IsVendor = skuVaultAccountForCreationDto.IsVendor;

        newSkuVaultAccount.QueueDomainEvent(new SkuVaultAccountCreated(){ SkuVaultAccount = newSkuVaultAccount });
        
        return newSkuVaultAccount;
    }

    public void Update(SkuVaultAccountForUpdateDto skuVaultAccountForUpdateDto)
    {
        new SkuVaultAccountForUpdateDtoValidator().ValidateAndThrow(skuVaultAccountForUpdateDto);

        SkuVaultId = skuVaultAccountForUpdateDto.SkuVaultId;
        SkuVaultTokenId = skuVaultAccountForUpdateDto.SkuVaultTokenId;
        CompanyId = skuVaultAccountForUpdateDto.CompanyId;
        IsVendor = skuVaultAccountForUpdateDto.IsVendor;

        QueueDomainEvent(new SkuVaultAccountUpdated(){ Id = Id });
    }
    
    protected SkuVaultAccount() { } // For EF + Mocking
}