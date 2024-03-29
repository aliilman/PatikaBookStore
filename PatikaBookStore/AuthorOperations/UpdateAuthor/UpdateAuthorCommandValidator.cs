using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;


namespace PatikaBookStore.AuthorOperations.UpdateAuthor
{
public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
{
	public UpdateAuthorCommandValidator()
	{
		RuleFor(a => a.Model.Name).MinimumLength(2).NotEmpty();
		RuleFor(a => a.Model.Surname).MinimumLength(2).NotEmpty();
		RuleFor(a => a.Model.Birthday.Date).LessThan(DateTime.Now.Date);
	}
}
}