using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Common;
using AutoMapper;
using FluentValidation;

namespace WebApi.BookOPerations.GetBooksByID
{
    public class GetBooksByIDQueryValidator : AbstractValidator<GetBooksByIDQuery>
    {
        public GetBooksByIDQueryValidator()
        {
            RuleFor(command => command.BookId).GreaterThan(0);
        }
    }
}