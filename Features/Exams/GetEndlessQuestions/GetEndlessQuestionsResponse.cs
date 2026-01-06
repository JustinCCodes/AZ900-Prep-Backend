namespace AZ900Prep.Api.Features.Exams.GetEndlessQuestions;

// Response object for endless questions endpoint
public record GetEndlessQuestionsResponse(List<QuestionDto> Questions);
