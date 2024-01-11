using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Versioning;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PatikaBookStore.BookOperations.DeleteBook;
using PatikaBookStore.BookOperations.GetBookDetail;
using PatikaBookStore.BookOperations.UpdateBook;
using PatikaBookStore.DbOperations;

namespace PatikaBookStore.Controllers;

[ApiController]
[Route("api/[controller]s")]
public class bookController : ControllerBase
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;
    public bookController(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    [HttpGet("{id}")] // GET: api/books/id
    public IActionResult GetBookById(int id)
    {
        BookDetailViewModel result;
        try
        {
            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper)
            {
                BookId = id
            };

            QueryGetBookByIdValidator validator = new();
            validator.ValidateAndThrow(query);

            result = query.Handle();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        return Ok(result);
    }


    [HttpPut("{id}")]//PUT [FromBody] : api/books/id
    public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
    {
        try
        {
            UpdateBookCommand command = new UpdateBookCommand(_context)
            {
                BookId = id,
                Model = updatedBook
            };

            UpdateBookValidator validator = new();
            validator.ValidateAndThrow(command);

            command.Handle();
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
        return Ok();

    }

    [HttpDelete("{id}")]//DELETE [FromBody] :: api/books/id
    public IActionResult DeleteBook(int id)
    {
        try
        {
            DeleteBookCommand command = new DeleteBookCommand(_context)
            {
                BookId = id,
            };
            DeleteBookCommandValidator validator = new();
            validator.ValidateAndThrow(command);
            command.Handle();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        return Ok();
    }

}
