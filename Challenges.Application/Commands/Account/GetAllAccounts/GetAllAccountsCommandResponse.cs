namespace Challenges.Application.Commands.Account.GetAllAccounts;

public record GetAllAccountsCommandResponse(List<Account> Accounts);

public record Account(
    Guid Id,
    string FcmToken,
    bool IsAndroidDevice,
    string CompanyGroupName,
    Guid CompanyGroupGuid,
    string Email,
    string UserName,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Tenant,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    bool IsDeleted,
    bool Status
);