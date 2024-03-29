using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;


namespace PatikaBookStore.BookOperations.GetBookDetail
{
    public class QueryGetBookByIdValidator : AbstractValidator<GetBookDetailQuery>
    {
        public QueryGetBookByIdValidator()
        {
            RuleFor(cmd => cmd.BookId).GreaterThan(0);
        }
    }
}