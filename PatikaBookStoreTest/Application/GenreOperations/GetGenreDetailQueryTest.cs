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
   public class GetGenreDetailQueryTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenreDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenGivenGenreIdIsNotinDB_InvalidOperationException_ShouldBeReturn()
        {
            GetGenreDetailQuery command = new GetGenreDetailQuery(_context,_mapper);
            command.GenreId=0;
                        

            FluentActions.Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>();
        }

        //[Fact]
        //public void WhenGivenGenreIdIsinDB_InvalidOperationException_ShouldBeReturn()
        //{
        //    GetGenreDetailQuery command = new GetGenreDetailQuery(_context,_mapper);
        //    command.GenreId=1;
            
        //    FluentActions.Invoking(()=> command.Handle()).Invoke();

        //    var genre =_context.Books.SingleOrDefault(genre=>genre.Id == command.GenreId);
        //    genre.Should().NotBeNull();
        //}
    }
}