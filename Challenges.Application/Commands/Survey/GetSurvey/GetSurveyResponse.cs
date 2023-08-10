using Challenges.Application.Commands.AnswerQuestions;
using Challenges.Application.Commands.Common;
using Challenges.Application.Commands.Common.Answer;
using Challenges.Application.Commands.Common.QuestionType;
using Challenges.Application.Commands.Common.Survey;

namespace Challenges.Application.Commands.Survey.GetSurvey;

public record GetSurveyResponse(
        Result Result
    );
public record SurveyDto(
        Guid Id,
        SurveyTypeData SurveyType,
        List<QuestionData> Questions
    );    
    
public record QuestionData(
        Guid Id,
        Guid CreatedBy,
        QuestionTypeData QuestionType,
        string Content,
        List<AnswerData> Answers
    );