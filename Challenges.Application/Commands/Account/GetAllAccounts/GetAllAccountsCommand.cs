using FastEndpoints;

namespace Challenges.Application.Commands.Account.GetAllAccounts;

public record GetAllAccountsCommand(
    string? Id = null,
    bool Status = true,
    int? PageIndex = null,
    int? PageSize = null) : ICommand<GetAllAccountsCommandResponse>;