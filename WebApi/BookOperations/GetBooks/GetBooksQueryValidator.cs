using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Common;
using AutoMapper;
using FluentValidation;

namespace WebApi.BookOPerations.GetBooks
{
    public class GetBooksQueryValidator : AbstractValidator<GetBooksQuery>
    {
        public GetBooksQueryValidator()
        {
            
        }
    }
}
