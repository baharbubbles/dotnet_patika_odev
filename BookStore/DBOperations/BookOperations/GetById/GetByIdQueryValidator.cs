using FluentValidation;
using BookStore.BookOperations.GetByIdQuery;

namespace BookStore.DBOperations.BookOperations.GetById
{
    public class GetByIdQueryValidator : AbstractValidator<GetByIdQuery>
    {
        public GetByIdQueryValidator()
        {
            RuleFor(query => query.BookId).GreaterThan(0);
        }
    }
}