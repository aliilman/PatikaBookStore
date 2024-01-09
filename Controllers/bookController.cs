using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.UpdateBook;
using WebApi.DbOperations;


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
        GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
        query.BookId = id;
        result = query.Handle();
        return Ok(result);
    }


    [HttpPut("{id}")]//PUT [FromBody] : api/books/id
    public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
    {
        UpdateBookCommand command = new UpdateBookCommand(_context);
        command.BookId = id;
        command.Model = updatedBook;
        command.Handle();
        return Ok();

    }

}
