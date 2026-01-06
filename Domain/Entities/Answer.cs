namespace AZ900Prep.Api.Domain.Entities;

// Entity representing an answer option for a question
public class Answer
{
    public Guid Id { get; init; } = Guid.NewGuid(); // Primary Key
    public Guid QuestionId { get; init; } // Foreign Key to Question
    public required string Text { get; set; } // Answer text
    public required bool IsCorrect { get; set; } // Indicates if the answer is correct
}
