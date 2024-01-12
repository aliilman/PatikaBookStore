using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using PatikaBookStore.AuthorOperations.GetAuthorDetail;
using TestSetup;
using Xunit;

namespace PatikaBookStoreTest.Application.AuthorOperations
{
     public class GetAuthorDetailQueryValidatorTests:IClassFixture<CommonTestFixture>
    {

        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(-10)]
        [Theory]
        public void WhenInvalidAuthoridIsGiven_Validator_ShouldBeReturnErrors(int authorid)
        {
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(null,null);
            query.AuthorId=authorid;

            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            var result = validator.Validate(query);

            result.Errors.Count.Should().BeGreaterThan(0);
        }


        [InlineData(1)]
        [InlineData(100)]
        [Theory]
        public void WhenInvalidAuthoridIsGiven_Validator_ShouldNotBeReturnErrors(int authorid)
        {
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(null,null);
            query.AuthorId=authorid;

            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            var result = validator.Validate(query);

            result.Errors.Count.Should().Be(0);
        }


    }
}