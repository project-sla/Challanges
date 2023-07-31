using FastEndpoints;
using FluentValidation;

namespace Challenges.Application.Commands.Survey.AddGenresToSurvey;

public class AddGenresToSurveyValidator : Validator<AddGenresToSurveyCommand>
{
    public AddGenresToSurveyValidator()
    {
        RuleFor(x => x.SurveyId).NotEmpty().NotEqual(Guid.Empty).WithMessage("SurveyId is required.");
        RuleFor(x => x.GenreIds).NotEmpty().WithMessage("GenreIds is required.");
    }
}