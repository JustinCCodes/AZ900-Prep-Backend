namespace AZ900Prep.Api.Features.Exams.GetEndlessQuestions;

// Handler for retrieving endless random questions
public class GetEndlessQuestionsHandler(AppDbContext context, IValidator<int> validator)
{
    // Handles request to get endless questions
    public async Task<Result<GetEndlessQuestionsResponse>> HandleAsync(int pageSize = 10, CancellationToken ct = default)
    {
        // Validates page size parameter
        var validationResult = await validator.ValidateAsync(pageSize, ct);
        if (!validationResult.IsValid)
        {
            // Returns validation error if invalid
            return Result<GetEndlessQuestionsResponse>.Failure(
                new ResultError("Error.Validation", validationResult.Errors.First().ErrorMessage));
        }

        // Fetches random questions from database
        var questions = await context.Questions
            .Include(q => q.Answers) // Includes related answers
            .OrderBy(q => EF.Functions.Random()) // Randomizes order
            .Take(pageSize) // Takes the specified page size
            .Select(q => new QuestionDto // Maps to DTO
            {
                Id = q.Id, // Question ID
                Text = q.Text, // Question text
                Category = q.Category, // Question category
                Type = q.Type, // Question type
                Answers = q.Answers.Select(a => new AnswerDto // Maps answers to DTO
                {
                    Id = a.Id, // Answer ID
                    Text = a.Text, // Answer text
                    IsCorrect = a.IsCorrect // Indicates if the answer is correct
                }).ToList() // Converts answers to list
            })
            // Executes query and returns list
            .ToListAsync(ct);

        // Returns the questions in response
        return Result<GetEndlessQuestionsResponse>.Success(new GetEndlessQuestionsResponse(questions));
    }
}
