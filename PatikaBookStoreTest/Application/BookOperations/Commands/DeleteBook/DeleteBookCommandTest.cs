using System;
using System.Linq;
using FluentAssertions;
using PatikaBookStore.BookOperations.DeleteBook;
using PatikaBookStore.DbOperations;
using TestSetup;

using Xunit;

namespace Application.BookOperations.Commands.DeleteBook
{
  public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
  {
    private readonly BookStoreDbContext _context;
    public DeleteBookCommandTests(CommonTestFixture testFixture)
    {
      _context = testFixture.Context;
    }
    [Fact]
    public void WhenGivenBookIdDoesNotFound_InvalidOperationException_ShouldBeReturnError()
    {
      DeleteBookCommand command = new DeleteBookCommand(_context);
      command.BookId = 9999;

      DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
      var result = validator.Validate(command);

      FluentActions
        .Invoking(()=> command.Handle())
        .Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void WhenValideBookIdGiven_InvalidOperationException_ShouldBeReturnOk()
    {
      DeleteBookCommand command = new DeleteBookCommand(_context);
      command.BookId = 1;

      DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
      var result = validator.Validate(command);

      FluentActions.Invoking(() => command.Handle()).Invoke();

      var book = _context.Books.SingleOrDefault(book => book.Id == command.BookId);

      book.Should().BeNull();
    }
  }
}