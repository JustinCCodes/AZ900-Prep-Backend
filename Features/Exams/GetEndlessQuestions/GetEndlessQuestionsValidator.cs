public class GetEndlessQuestionsValidator : AbstractValidator<int>
{
    public GetEndlessQuestionsValidator()
    {
        RuleFor(x => x).InclusiveBetween(1, 50)
            .WithMessage("You can only request between 1 and 50 questions at a time.");
    }
}
