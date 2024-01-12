using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using AutoMapper;
using PatikaBookStore.DbOperations;


namespace PatikaBookStore.AuthorOperations.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        public int AuthorId { get; set; }
        public UpdateAuthorViewModel Model { get; set; }

        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateAuthorCommand(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(a => a.Id == AuthorId);

            if (author is null)
                throw new InvalidOperationException("ID is not correct.");

            _mapper.Map(Model, author);

            _dbContext.SaveChanges();
        }

        public class UpdateAuthorViewModel
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public DateTime Birthday { get; set; }
        }
    }
}