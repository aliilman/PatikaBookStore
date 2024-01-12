using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using PatikaBookStore.BookOperations;
using PatikaBookStore.DbOperations;
using PatikaBookStore.Models;

using TestSetup;

using Xunit;


namespace Application.BookOperations.Commands.CreateBook
{
  public class CreateBookCommandTests : IClassFixture<CommonTestFixture>
  {
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;
    public CreateBookCommandTests(CommonTestFixture testFixture)
    {
      _context = testFixture.Context;
      _mapper = testFixture.Mapper;
    }
    [Fact]
    public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
    {      
      var book = new Book(){Title = "Test_WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn", PageCount = 100, PublishDate = new System.DateTime(1990, 01, 10), GenreID = 1, AuthorId = 1};

      _context.Books.Add(book);
      _context.SaveChanges();

      CreateBookCommand command = new CreateBookCommand(_context, _mapper);
      command.Model = new CreateBookCommand.CreateBookModel(){Title = book.Title};

      FluentActions
        .Invoking(()=> command.Handle())
        .Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
    {
      CreateBookCommand command = new CreateBookCommand(_context, _mapper);
      CreateBookCommand.CreateBookModel model = new CreateBookCommand.CreateBookModel(){Title = "Hobbit", PageCount = 1000, PublishDate = DateTime.Now.Date.AddYears(-10), GenreId = 1};
      command.Model = model;

      FluentActions.Invoking(() => command.Handle()).Invoke();

      var book = _context.Books.SingleOrDefault(book => book.Title == model.Title);
      book.Should().NotBeNull();
      book.PageCount.Should().Be(model.PageCount);
      book.PublishDate.Should().Be(model.PublishDate);
      book.GenreID.Should().Be(model.GenreId);
      // book.AuthorId.Should().Be(model.AuthorId);
    }
  }
}