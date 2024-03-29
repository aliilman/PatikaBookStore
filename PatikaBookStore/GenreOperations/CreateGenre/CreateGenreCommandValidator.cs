
using FluentValidation;


namespace PatikaBookStore.GenreOperations
{
	public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
	{
		public CreateGenreCommandValidator()
		{
			RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(4);
		}
	}
}