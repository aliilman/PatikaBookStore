using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using PatikaBookStore.DbOperations;
using PatikaBookStore.GenreOperations;
using TestSetup;
using Xunit;

namespace PatikaBookStoreTest.Application.GenreOperations
{
    public class DeleteGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenAlreadyExistGenreIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Arrange (Hazırlık)
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = 100;

            // act & asset (Çalıştırma ve Doğrulama)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>();

        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeCreated()
        {
            //arrange
           DeleteGenreCommand command = new DeleteGenreCommand(_context);
           command.GenreId = 1;

            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            var genre = _context.Genres.SingleOrDefault(x=> x.Id == command.GenreId);
            genre.Should().BeNull();

        }
    }
}