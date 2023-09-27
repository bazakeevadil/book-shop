using Application.Books.Commands;
using Application.Books.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Controllers;
[AllowAnonymous]
[ApiController, Route("api/books")]
public class BookController : ControllerBase
{
    private readonly IMediator _mediator;

    public BookController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetAll()
    {
        var request = new GetAllBooksQuery();
        var books = await _mediator.Send(request);
        if (books is null) return Ok("The list is empty");
        return Ok(books);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateBookAsync(AddBookCommand command)
    {
        if (command.Title.IsNullOrEmpty()) return BadRequest("Title is null");
        if (command.Price < 0)
            return BadRequest($"Price cannot be negative {command.Price}");

        var response = await _mediator.Send(command);
        return Ok(response);
    }
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(DeleteBookByIdCommand command)
    {
        var result = await _mediator.Send(command);
        if (result == true)
            return Ok("Book deleted.");
        return NotFound();
    }


}
