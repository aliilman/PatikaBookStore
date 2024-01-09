using FluentValidation;
using WebApi.BookOperations.UpdateBook;

namespace PatikaBookStore.BookOperations.UpdateBook;

public class UpdateBookValidator : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookValidator()
    {
        RuleFor(cmd => cmd.BookId).GreaterThan(0);
        RuleFor(cmd => cmd.Model.GenreId).GreaterThan(0);
        RuleFor(cmd => cmd.Model.Title).NotEmpty().MinimumLength(4);

    }
}
