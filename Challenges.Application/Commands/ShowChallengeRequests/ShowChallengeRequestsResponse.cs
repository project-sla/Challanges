using Challenges.Application.Commands.Common;

namespace Challenges.Application.Commands.ShowChallengeRequests;

public record ShowChallengeRequestsResponse(
    Result Result
);

public record ChallengeRequestDto(
    Guid SurveyId,
    Account.GetAllAccounts.Account? SentBy,
    Account.GetAllAccounts.Account? ReceivedBy,
    DateTime SentAt
);