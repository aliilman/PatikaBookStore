using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PatikaBookStore.AuthorOperations.CreateAuthor;
using PatikaBookStore.AuthorOperations.DeleteAuthor;
using PatikaBookStore.AuthorOperations.GetAuthorDetail;
using PatikaBookStore.AuthorOperations.GetAuthors;
using PatikaBookStore.AuthorOperations.UpdateAuthor;

using PatikaBookStore.DbOperations;

namespace PatikaBookStore.Controllers
{
    [Route("[controller]")]
    public class AuthorController : Controller
    {
        // Yazar Ekleme
        // Yazar Bilgileri Güncelleme
        // Yazar Silme
        // Tüm Yazarları Listeleme
        // Spesifik Bir Yazarın Bilgilerini Getirme
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public AuthorController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAuthors()
        {
            var query = new GetAuthorsQuery(_context, _mapper);
            return Ok(query.Handle());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetAuthorById(int id)
        {
            var query = new GetAuthorDetailQuery(_context, _mapper)
            {
            AuthorId = id
            };
            
            var validator = new GetAuthorDetailQueryValidator();
            validator.ValidateAndThrow(query);
            
            return Ok(query.Handle());
        }

        [HttpPost]
        public IActionResult AddAuthor([FromBody] CreateAuthorCommand.CreateAuthorViewModel newAuthor)
        {
            var command = new CreateAuthorCommand(_context, _mapper)
            {
                Model = newAuthor
            };

            var validator = new CreateAuthorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorCommand.UpdateAuthorViewModel updatedAuthor)
        {
            var command = new UpdateAuthorCommand(_context, _mapper)
            {
                AuthorId = id,
                Model = updatedAuthor
            };

            var validator = new UpdateAuthorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public IActionResult RemoveAuthor(int id)
        {
            var command = new DeleteAuthorCommand(_context)
            {
                AuthorId = id
            };

            command.Handle();
            return Ok();
        }
    }
}