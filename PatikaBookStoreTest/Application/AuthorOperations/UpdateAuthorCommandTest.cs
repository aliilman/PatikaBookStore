using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using PatikaBookStore.AuthorOperations.UpdateAuthor;
using PatikaBookStore.DbOperations;
using TestSetup;
using Xunit;

namespace PatikaBookStoreTest.Application.AuthorOperations
{
    public class UpdateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper= testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistAuthorIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Arrange (Hazırlık)
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context,_mapper);
            command.AuthorId = 0;

            // act & asset (Çalıştırma ve Doğrulama)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>();

        }

        [Fact]
        public void WhenGivenAuthorIdinDB_Author_ShouldBeUpdate()
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context,_mapper);

            UpdateAuthorCommand.UpdateAuthorViewModel model = new UpdateAuthorCommand.UpdateAuthorViewModel(){Name="WhenGivenBookIdinDB_Book_ShouldBeUpdate", Surname="Rebart"};            
            command.Model = model;
            command.AuthorId = 1;

            FluentActions.Invoking(()=> command.Handle()).Invoke();

            var author=_context.Authors.SingleOrDefault(author=>author.Id == command.AuthorId);
            author.Should().NotBeNull();
            
        }
    }
}