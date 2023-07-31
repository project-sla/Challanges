using Challenges.Application.Commands.SurveyType.GetAllSurveyType;
using FastEndpoints;

namespace Challenges.API.Endpoints.Survey;

public class GetAllSurvey : Endpoint<GetAllSurveyTypeCommand,GetAllSurveyTypeResponse>
{
    public override void Configure()
    {
        Post("survey/getAll");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetAllSurveyTypeCommand req, CancellationToken ct)
    {
        var survey = await new GetAllSurveyTypeCommand(req.PageNumber,req.PageSize).ExecuteAsync(ct: ct);
        await SendAsync(survey, cancellation: ct);
    }
}