using Challenges.Application.Commands.Common;
using Challenges.Application.Commands.ShowChallengeRequests;
using Challenges.Persistence.Services.ChallangeRequest;
using FastEndpoints;

namespace Challenges.Application.Handlers.ShowChallengeRequests;

public class ShowChallengeRequestsHandler : ICommandHandler<ShowChallengeRequestsCommand, ShowChallengeRequestsResponse>
{
    private readonly IChallengeRequestService _challengeRequestService;

    public ShowChallengeRequestsHandler(IChallengeRequestService challengeRequestService)
    {
        _challengeRequestService = challengeRequestService;
    }

    public async Task<ShowChallengeRequestsResponse> ExecuteAsync(ShowChallengeRequestsCommand command, CancellationToken ct)
    {
        var challengeRequests = await _challengeRequestService.GetAsync(command.ReceivedBy); 
        //if(challengeRequests is null) challengeRequests = await _challengeRequestService.GetAsync(command.ReceivedBy,command.SurveyId);
        if (challengeRequests is null) return new ShowChallengeRequestsResponse(new Result(false, null, null, 404, "Challenge request not found"));
        var result = new Result(true, null, challengeRequests, 200, "Challenge request found");
        return new ShowChallengeRequestsResponse(result);
    }
}