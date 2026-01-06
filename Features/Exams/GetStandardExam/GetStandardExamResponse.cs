namespace AZ900Prep.Api.Features.Exams.GetStandardExam;

// Response object for standard exam endpoint
public record GetStandardExamResponse
{
    public required List<QuestionDto> Questions { get; init; } = [];
}
