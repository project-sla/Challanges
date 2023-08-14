using FastEndpoints;

namespace Challenges.Application.Commands.ShowChallengeRequests;

public record ShowChallengeRequestsCommand(
        Guid SurveyId,
        Guid ReceivedBy
    ) :ICommand<ShowChallengeRequestsResponse>;