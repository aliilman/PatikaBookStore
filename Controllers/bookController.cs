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
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.UpdateBook;
using WebApi.DbOperations;

//Fluent Validation kütüphanesini kullanarak Update, Delete ve GetById
// metotları için validation sınıflarını yazınız. Controller içerisinde
// metot çağrımlarından önce validasyonları çalıştırınız.
namespace WebApi.AddControllers;

[ApiController]
[Route("api/[controller]s")]
public class bookController : ControllerBase //eğitim videso referans alınarak çözülmüştür
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;
    public bookController(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // eğitim videosunda manuel şekilde bir çzöüm gelişitirilmiş ben ödevimde automapper kullanıyoum
    [HttpGet("{id}")] // GET: api/books/id
    public IActionResult GetBookById(int id)
    {
        BookDetailViewModel result;
        try
        {
            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            query.BookId = id;
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
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = id;
            command.Model = updatedBook;
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
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = id;
            DeleteBookCommandValidator validator= new();
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
