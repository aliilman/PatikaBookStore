using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using PatikaBookStore.AuthorOperations.CreateAuthor;
using TestSetup;
using Xunit;

namespace PatikaBookStoreTest.Application.AuthorOperations
{
    public class CreateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
       
        [Theory]
        [InlineData(" ", " ")]
        [InlineData(" ", "aaa" )]
        [InlineData("aaa", " " )]
        [InlineData("as", "a" )]
        [InlineData("a", "sa" )]

        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(string firstname, string lastname)
        {
            //arrange
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = new CreateAuthorCommand.CreateAuthorViewModel(){Name = firstname, Surname = lastname, Birthday= new System.DateTime(1900,01,25)};
            
            //act
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
           
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            CreateAuthorCommand command = new CreateAuthorCommand(null!,null!);
            command.Model = new CreateAuthorCommand.CreateAuthorViewModel()
            {
                Name = "Frank",
                Surname = "Tolkien",
                Birthday = DateTime.Now.Date
                
            };

            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
            
        }

        [Theory]
        [InlineData("asdf ", " asdf")]
        [InlineData("asdf", "asdf" )]
        [InlineData("as  ", "sa  " )]
        [InlineData(" as ", " a  " )]
        [InlineData("asdadasdasd", "asdasdasdasdas" )]
        [InlineData(" aaa", "saa " )]
        public void WhenValidInputAreGiven_Validator_ShouldBeReturnErrors(string firstname, string lastname)
        {
            //arrange
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = new CreateAuthorCommand.CreateAuthorViewModel(){Name = firstname,Surname = lastname, Birthday= new System.DateTime(1900,01,25)};
            
            //act
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);
           
        } 
    }
}