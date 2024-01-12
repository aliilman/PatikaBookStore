using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using PatikaBookStore.AuthorOperations.CreateAuthor;
using PatikaBookStore.DbOperations;
using PatikaBookStore.Models;
using TestSetup;
using Xunit;

namespace PatikaBookStoreTest.Application.AuthorOperations
{
    public class CreateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistAuthorNameIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            //arrange (hazırlık)
            var author = new Author { Name = "Eric" ,Surname=" "};
            _context.Authors.Add(author);
            _context.SaveChanges();

            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            command.Model = new CreateAuthorCommand.CreateAuthorViewModel
            {
                Name = author.Name,
            };

            //act && assert (çalıştırma && doğrulama)
            FluentActions
                .Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>();
        }


        [Fact]
        public void WhenValidInputIsGiven_Author_ShouldBeCreated()
        {
            //arrange (hazırlama)
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            command.Model = new CreateAuthorCommand.CreateAuthorViewModel { Name = "ali", Surname = "ilman" , Birthday = DateTime.ParseExact("1978-09-22", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture) };

            //act (çalıştırma)
            FluentActions
                .Invoking(() => command.Handle()).Invoke();

            //assert (doğrulama)
            var author = _context.Authors.SingleOrDefault(x => x.Name == command.Model.Name);
            author.Should().NotBeNull();
            author.Surname.Should().Be(command.Model.Surname);
            // author.BookID.Should().Be(command.Model.BookID);
            author.Birthday.Should().Be(command.Model.Birthday);
        }
    }
}