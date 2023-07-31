using FastEndpoints;
using FluentValidation;

namespace Challenges.Application.Commands.Question.GetQuestions;

public class GetQuestionsValidator : Validator<GetQuestionsCommand>
{
    public GetQuestionsValidator()
    {
        RuleFor(x => x.QuestionId)
            .NotEmpty()
            .When(x => x.CreatedBy == null && x.QuestionTypeId == null && x.SurveyId == null)
            .WithMessage("QuestionId must be provided if no other filter is provided");
        
        RuleFor(x => x.CreatedBy)
            .NotEmpty()
            .When(x => x.QuestionId == null && x.QuestionTypeId == null && x.SurveyId == null)
            .WithMessage("CreatedBy must be provided if no other filter is provided");

        RuleFor(x => x.QuestionTypeId)
            .NotEmpty()
            .When(x => x.QuestionId == null && x.CreatedBy == null && x.SurveyId == null)
            .WithMessage("QuestionTypeId must be provided if no other filter is provided");

        RuleFor(x => x.SurveyId)
            .NotEmpty()
            .When(x => x.QuestionId == null && x.CreatedBy == null && x.QuestionTypeId == null)
            .WithMessage("SurveyId must be provided if no other filter is provided");
    
    }
}