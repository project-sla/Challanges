namespace Challenges.Application.Commands.Account.GetAllAccounts;

public record GetAllAccountsCommandResponse(List<Account> Accounts);

public record Account(
    Guid guid,
    string fcmToken,
    bool isAndroidDevice,
    Guid companyGroupGuid,
    Guid accountTypeGuid,
    string profilePic,
    string firstname,
    string lastname,
    string username,
    string email,
    string phoneNumber,
    bool status,
    object companyGroup,
    object accountType,
    object membershipAccounts,
    object accountOperationClaims,
    object posts,
    object friends,
    object followers,
    DateTime createdAt,
    DateTime updatedAt,
    bool isDeleted,
    string tenantId
);
