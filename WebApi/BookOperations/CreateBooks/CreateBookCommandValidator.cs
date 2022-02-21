using System;
using FluentValidation;

namespace WebApi.BookOPerations.CreateBooks
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand> // objelerini valide eder
    {
        public CreateBookCommandValidator()
        {
            RuleFor(command => command.Model.GenreID).GreaterThan(0);
            RuleFor(command => command.Model.PageCount).GreaterThan(0);
            RuleFor(command => command.Model.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
            RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4);

        }
    }
}