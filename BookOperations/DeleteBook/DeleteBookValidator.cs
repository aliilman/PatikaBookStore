
using FluentValidation;
using WebApi.BookOperations.DeleteBook;

namespace PatikaBookStore.BookOperations.DeleteBook
{
    public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookCommandValidator()
        {
            RuleFor(cmd => cmd.BookId).GreaterThan(0);
        }
    }
}