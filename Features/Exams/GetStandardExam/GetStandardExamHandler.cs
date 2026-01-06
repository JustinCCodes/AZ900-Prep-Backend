namespace AZ900Prep.Api.Features.Exams.GetStandardExam;

// Handler for retrieving a standard exam
public class GetStandardExamHandler(AppDbContext context)
{
    // Main method to handle the exam retrieval
    public async Task<Result<GetStandardExamResponse>> HandleAsync(CancellationToken ct = default)
    {
        // AZ-900 2026 Blueprint Weightings:
        // 1. Cloud Concepts (25-30%) -> ~9 Questions
        // 2. Architecture & Services (35-40%) -> ~11 Questions
        // 3. Management & Governance (30-35%) -> ~10 Questions

        // Fetches random questions by domain
        var concepts = await GetRandomByDomain("Cloud Concepts", 9, ct);
        var architecture = await GetRandomByDomain("Azure Architecture and Services", 11, ct);
        var governance = await GetRandomByDomain("Azure Management and Governance", 10, ct);

        // Combines all selected questions
        var allQuestions = concepts.Concat(architecture).Concat(governance).ToList();

        // Validates that enough questions were retrieved
        if (allQuestions.Count < 30)
        {
            return Result<GetStandardExamResponse>.Failure(
                new ResultError("Exam.Incomplete", "The question bank does not have enough questions for all domains."));
        }

        // Returns the assembled exam response
        return Result<GetStandardExamResponse>.Success(new GetStandardExamResponse
        {
            // Sets the questions for the exam
            Questions = allQuestions
        });
    }

    // Helper method to get random questions by category
    private async Task<List<QuestionDto>> GetRandomByDomain(string category, int count, CancellationToken ct)
    {
        return await context.Questions
            .Where(q => q.Category == category) // Filters by category
            .Include(q => q.Answers) // Includes related answers
            .OrderBy(q => EF.Functions.Random()) // Randomizes order
            .Take(count) // Takes the specified count
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
    }
}
