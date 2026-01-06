namespace AZ900Prep.Api.Features.Exams;

// Data Transfer Object representing a question with its answers
public record QuestionDto
{
    public Guid Id { get; init; } // Question ID
    public required string Text { get; init; } // Question text
    public required string Category { get; init; } // Question category
    public required QuestionType Type { get; init; } // Question type
    public string? ImageUrl { get; init; } // URL to associated image
    public string? Explanation { get; init; } // Explanation for the answer
    public string? Metadata { get; init; } // Additional metadata (e.g., SVG paths)
    public List<AnswerDto> Answers { get; init; } = []; // List of answer options
}

// Data Transfer Object representing an answer option
public record AnswerDto
{
    public Guid Id { get; init; } // Answer ID
    public required string Text { get; init; } // Answer text
    public bool IsCorrect { get; init; } // Included for exam review purposes
}
