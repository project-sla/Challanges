using FastEndpoints;

namespace Challenges.Application.Commands.QuestionType.CreateQuestionType;

public record CreateQuestionTypeCommand(
    string? Value
) : ICommand<CreateQuestionTypeResponse>;