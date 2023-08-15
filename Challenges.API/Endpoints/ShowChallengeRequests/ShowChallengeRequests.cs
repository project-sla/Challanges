using Challenges.Application.Commands.ShowChallengeRequests;
using FastEndpoints;

namespace Challenges.API.Endpoints.ShowChallengeRequests;

public class ShowChallengeRequests : Endpoint<ShowChallengeRequestsCommand, ShowChallengeRequestsResponse>
{
    public override void Configure()
    {
        Post("challenges/requests/show");
        AllowAnonymous();
    }

    public override async Task HandleAsync(ShowChallengeRequestsCommand req, CancellationToken ct)
    {
        var challengeRequests = await new ShowChallengeRequestsCommand(req.ReceivedBy).ExecuteAsync(ct);
        await SendAsync(challengeRequests, cancellation: ct);
    }
}