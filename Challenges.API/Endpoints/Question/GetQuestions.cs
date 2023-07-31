﻿using Challenges.Application.Commands.Question.GetQuestions;
using FastEndpoints;

namespace Challenges.API.Endpoints.Question;

public class GetQuestions : Endpoint<GetQuestionsCommand,GetQuestionsResponse>
{
    public override void Configure()
    {
        Post("question/get");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetQuestionsCommand req, CancellationToken ct)
    {
        var question = await new GetQuestionsCommand(req.QuestionId, req.CreatedBy, req.QuestionTypeId, req.SurveyId, req.Page, req.PageSize).ExecuteAsync(ct: ct);
        await SendAsync(question, cancellation: ct);
    }
}