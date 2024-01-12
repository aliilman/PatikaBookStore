using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using PatikaBookStore.DbOperations;
using PatikaBookStore.GenreOperations;
using TestSetup;
using Xunit;

namespace PatikaBookStoreTest.Application.GenreOperations
{
    public class CreateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {

        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateGenreCommandValidatorTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
       
        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData("as")]
        [InlineData("asd")]
        [InlineData("a")]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(string name)
        {
            //arrange
            CreateGenreCommand command = new CreateGenreCommand(_context,_mapper);
            command.Model = new CreateGenreModel(){Name = name};
            
            //act
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
           
        }

        [Theory]
        [InlineData("asdf ")]
        [InlineData("asdf")]
        [InlineData("as123")]
        [InlineData("12asd")]
        [InlineData("    a")]
        public void WhenValidInputAreGiven_Validator_ShouldBeReturnErrors(string name)
        {
            //arrange
            CreateGenreCommand command = new CreateGenreCommand(_context,_mapper);
            command.Model = new CreateGenreModel(){Name = name};
            
            //act
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);
           
        }
       
    }
}