using System;
using AutoMapper;
using FluentAssertions;
using PatikaBookStore.BookOperations.UpdateBook;
using PatikaBookStore.DbOperations;
using PatikaBookStore.Models;
using TestSetup;

namespace Application.BookOperations.Commands.UpdateBook
{
  public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
  {
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;
    public UpdateBookCommandTests(CommonTestFixture testFixture)
    {
      _context = testFixture.Context;
      _mapper = testFixture.Mapper;
    }
    [Fact]
    public void WhenInvalidBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
    {      
      var book = new Book(){Title = "WhenInvalidBookTitleIsGiven_InvalidOperationException_ShouldBeReturn", PageCount = 100, PublishDate = new System.DateTime(1990, 01, 10), GenreID = 1, AuthorId = 1};

      _context.Books.Add(book);
      _context.SaveChanges();

      UpdateBookCommand command = new UpdateBookCommand(_context);
      command.Model = new UpdateBookModel(){Title = book.Title};

      FluentActions
        .Invoking(()=> command.Handle())
        .Should().Throw<InvalidOperationException>();
    }
  }
}