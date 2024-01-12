using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using PatikaBookStore.AuthorOperations.DeleteAuthor;
using PatikaBookStore.DbOperations;
using PatikaBookStore.Models;
using TestSetup;
using Xunit;

namespace PatikaBookStoreTest.Application.AuthorOperations
{
     public class DeleteAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public DeleteAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        [Fact]
        public void WhenGivenAuthorIdIsNotinDB_InvalidOperationException_ShouldBeReturn()
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = 0;

            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>();
        }


        [Fact]
        public void WhenGivenBookIdNotEqualAuthorId_InvalidOperationException_ShouldBeReturn()
        {
            
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = 1;

           FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>();
          }


        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
        {
            //arrange
           var author = new Author() {Name="Frank", Surname="Rebart", Birthday=new System.DateTime(1990,05,22)};
           _context.Add(author);
           _context.SaveChanges();

           DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
           command.AuthorId = author.Id;

            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            author = _context.Authors.SingleOrDefault(x=> x.Id == author.Id);
            author.Should().BeNull();

        }
    }
}