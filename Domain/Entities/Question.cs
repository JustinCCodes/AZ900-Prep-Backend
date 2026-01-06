namespace AZ900Prep.Api.Domain.Entities;

// Entity representing a question in the quiz system
public class Question
{
    public Guid Id { get; init; } = Guid.NewGuid(); // Primary Key
    public required string Text { get; set; } // Question text
    public required string Category { get; set; } // e.g., "Azure Basics"
    public required QuestionType Type { get; set; } // Type of question
    public string? Explanation { get; set; } // Explanation for the answer
    public string? ImageUrl { get; set; } // URL to an associated image

    // JSON string storing SVG paths or coordinates
    public string? Metadata { get; set; }

    // Relationships
    public List<Answer> Answers { get; init; } = [];
}

// Enum representing the type of question
public enum QuestionType
{
    MultipleChoice,
    MultipleResponse,
    HotArea,
    YesNo
}
