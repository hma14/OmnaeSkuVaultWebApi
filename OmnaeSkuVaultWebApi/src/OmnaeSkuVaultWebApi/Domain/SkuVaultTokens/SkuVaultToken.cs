namespace OmnaeSkuVaultWebApi.Domain.SkuVaultTokens;

using SharedKernel.Exceptions;
using OmnaeSkuVaultWebApi.Domain.SkuVaultTokens.Dtos;
using OmnaeSkuVaultWebApi.Domain.SkuVaultTokens.Validators;
using OmnaeSkuVaultWebApi.Domain.SkuVaultTokens.DomainEvents;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Sieve.Attributes;


public class SkuVaultToken : BaseEntity
{
    [Sieve(CanFilter = true, CanSort = false)]
    public virtual string TenantToken { get; private set; }

    [Required]
    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string UserToken { get; private set; }

    [Required]
    [Sieve(CanFilter = true, CanSort = true)]
    public virtual int CompanyId { get; private set; }

    [Required]
    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string SkuVaultCompanyId { get; private set; }

    [Required]
    [Sieve(CanFilter = true, CanSort = true)]
    public virtual bool IsRevoked { get; private set; }


    public static SkuVaultToken Create(SkuVaultTokenForCreationDto skuVaultTokenForCreationDto)
    {
        new SkuVaultTokenForCreationDtoValidator().ValidateAndThrow(skuVaultTokenForCreationDto);

        var newSkuVaultToken = new SkuVaultToken();

        newSkuVaultToken.TenantToken = skuVaultTokenForCreationDto.TenantToken;
        newSkuVaultToken.UserToken = skuVaultTokenForCreationDto.UserToken;
        newSkuVaultToken.CompanyId = skuVaultTokenForCreationDto.CompanyId;
        newSkuVaultToken.SkuVaultCompanyId = skuVaultTokenForCreationDto.SkuVaultCompanyId;
        newSkuVaultToken.IsRevoked = skuVaultTokenForCreationDto.IsRevoked;

        newSkuVaultToken.QueueDomainEvent(new SkuVaultTokenCreated(){ SkuVaultToken = newSkuVaultToken });
        
        return newSkuVaultToken;
    }

    public void Update(SkuVaultTokenForUpdateDto skuVaultTokenForUpdateDto)
    {
        new SkuVaultTokenForUpdateDtoValidator().ValidateAndThrow(skuVaultTokenForUpdateDto);

        TenantToken = skuVaultTokenForUpdateDto.TenantToken;
        UserToken = skuVaultTokenForUpdateDto.UserToken;
        CompanyId = skuVaultTokenForUpdateDto.CompanyId;
        SkuVaultCompanyId = skuVaultTokenForUpdateDto.SkuVaultCompanyId;
        IsRevoked = skuVaultTokenForUpdateDto.IsRevoked;

        QueueDomainEvent(new SkuVaultTokenUpdated(){ Id = Id });
    }
    
    protected SkuVaultToken() { } // For EF + Mocking
}