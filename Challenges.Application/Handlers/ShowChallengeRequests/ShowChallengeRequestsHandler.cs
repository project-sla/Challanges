using System.Diagnostics;
using Challenges.Application.Commands.Common;
using Challenges.Application.Commands.ShowChallengeRequests;
using Challenges.Application.Helpers.GetAllAccounts;
using Challenges.Persistence.Services.ChallangeRequest;
using FastEndpoints;

namespace Challenges.Application.Handlers.ShowChallengeRequests;

public class ShowChallengeRequestsHandler : ICommandHandler<ShowChallengeRequestsCommand, ShowChallengeRequestsResponse>
{
    private readonly IChallengeRequestService _challengeRequestService;
    private readonly GetAllAccountHandler _getAllAccountHandler;
    public ShowChallengeRequestsHandler(IChallengeRequestService challengeRequestService, GetAllAccountHandler getAllAccountHandler)
    {
        _challengeRequestService = challengeRequestService;
        _getAllAccountHandler = getAllAccountHandler;
    }

    public async Task<ShowChallengeRequestsResponse> ExecuteAsync(ShowChallengeRequestsCommand command,
        CancellationToken ct)
    {
        var challengeRequests = await _challengeRequestService.GetAsync(command.ReceivedBy);
        if (challengeRequests is null)
            return new ShowChallengeRequestsResponse(new Result(false, null, null, 404, "Challenge request not found"));
        var resultObj = new List<ChallengeRequestDto>();
        var challengeRequestedAccount = await _getAllAccountHandler.GetAllAccounts(command.ReceivedBy.ToString());
        Debug.WriteLine(challengeRequestedAccount);
        if (challengeRequestedAccount is null)
            return new ShowChallengeRequestsResponse(new Result(false, null, null, 404, "Account not found"));
        foreach (var challenge in challengeRequests)
        {
            var sender =  await _getAllAccountHandler.GetAllAccounts(challenge.CreatedBy.ToString());
            Debug.WriteLine(sender);
            if (sender is null)
                return new ShowChallengeRequestsResponse(new Result(false, null, null, 404, "Account not found"));
            var challengeObj = new ChallengeRequestDto(
                challenge.SurveyId,
                sender,
                challengeRequestedAccount,
                challenge.CreatedAt
            );
            resultObj.Add(challengeObj);
        }

        if (resultObj.Count == 0)
            return new ShowChallengeRequestsResponse(new Result(false, null, null, 404, "Challenge request not found"));
        var result = new Result(true, null, resultObj, 200, "Challenge request successfully fetched");
        return new ShowChallengeRequestsResponse(result);
    }
}