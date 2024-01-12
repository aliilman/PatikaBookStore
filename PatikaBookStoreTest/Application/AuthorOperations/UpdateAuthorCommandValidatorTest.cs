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
    public class UpdateAuthorCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateAuthorCommandValidatorTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper= testFixture.Mapper;
        }

        [Theory]
        [InlineData(-1, "Lord Of", " ")]
        [InlineData(1, " ", " ")]
        [InlineData(1, "", "ASDF")]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(int authorid, string firstname, string lastname)
        {
            //arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context,_mapper);
            command.Model = new UpdateAuthorCommand.UpdateAuthorViewModel() { Name = firstname, Surname = lastname };
            command.AuthorId = authorid;
            //act
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [InlineData(1, "Lord Of The Rings", "ASDF")]
        [Theory]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors(int authorid, string firstname, string lastname)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context,_mapper);
            command.Model = new UpdateAuthorCommand.UpdateAuthorViewModel()
            {
                Name = firstname,
                Surname = lastname
            };
            command.AuthorId = authorid;

            UpdateAuthorCommandValidator validations = new UpdateAuthorCommandValidator();
            var result = validations.Validate(command);

            result.Errors.Count.Should().Be(0);
        }


    }
}