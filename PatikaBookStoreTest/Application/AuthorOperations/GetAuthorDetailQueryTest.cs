using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using PatikaBookStore.AuthorOperations.GetAuthorDetail;
using PatikaBookStore.DbOperations;
using TestSetup;
using Xunit;

namespace PatikaBookStoreTest.Application.AuthorOperations
{
      public class GetAuthorDetailQueryTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper  _mapper;

        public GetAuthorDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenGivenAuthorIdIsNotinDB_InvalidOperationException_ShouldBeReturn()
        {
            GetAuthorDetailQuery command = new GetAuthorDetailQuery(_context,_mapper);
            command.AuthorId=0;
                        

            FluentActions.Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void WhenGivenAuthorIdIsinDB_InvalidOperationException_ShouldBeReturn()
        {
            GetAuthorDetailQuery command = new GetAuthorDetailQuery(_context,_mapper);
            command.AuthorId=1;
            

            FluentActions.Invoking(()=> command.Handle()).Invoke();

            var author=_context.Authors.SingleOrDefault(author=>author.Id == command.AuthorId);
            author.Should().NotBeNull();
        }
    }
}