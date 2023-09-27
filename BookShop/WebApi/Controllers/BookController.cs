using Application.Books.Commands;
using Application.Books.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApi.Controllers;

[ApiController, Route("api/books")]
public class BookController : ControllerBase
{
    private readonly IMediator _mediator;

    public BookController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpGet("getall")]
    [SwaggerOperation("Получения всех книг", "Позволяет получить все книги из базы")]
    [SwaggerResponse(200, "Успешно получено")]
    [SwaggerResponse(400, "Ошибка валидации")]
    public async Task<IActionResult> GetAll()
    {
        var request = new GetAllBooksQuery();
        var books = await _mediator.Send(request);
        if (books is null) return Ok("The list is empty");
        return Ok(books);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("create")]
    [SwaggerOperation("Создание книг", "Позволяет создать книги только для админа")]
    [SwaggerResponse(200, "Успешно создано")]
    [SwaggerResponse(400, "Ошибка валидации")]
    public async Task<IActionResult> CreateBookAsync(AddBookCommand command)
    {
        if (command.Title.IsNullOrEmpty()) return BadRequest("Title is null");
        if (command.Price < 0)
            return BadRequest($"Price cannot be negative {command.Price}");

        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [Authorize(Roles = "Admin")]
    [HttpPatch("update")]
    [SwaggerOperation("Изменение книг", "Позволяет изменить книги только для админа")]
    [SwaggerResponse(200, "Успешно изменино")]
    [SwaggerResponse(400, "Ошибка валидации")]
    public async Task<IActionResult> Update(UpdateBookCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("deletebyid")]
    [SwaggerOperation("Удаляет по id книгу", "Позволяет удалить по id книги только для админа")]
    [SwaggerResponse(200, "Успешно удалено")]
    [SwaggerResponse(400, "Ошибка валидации")]
    public async Task<IActionResult> DeleteById(DeleteBookByIdCommand command)
    {
        var result = await _mediator.Send(command);
        if (result == true)
            return Ok("Book deleted.");
        return NotFound();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("deletebytitle")]
    [SwaggerOperation("Удаляет по названию книгу", "Позволяет удалить по названию книги только для админа")]
    [SwaggerResponse(200, "Успешно удалено")]
    [SwaggerResponse(400, "Ошибка валидации")]
    public async Task<IActionResult> DeleteByTitle(DeleteBooksByTitleCommand command)
    {
        var result = await _mediator.Send(command);
        if (result == true)
            return Ok("Book deleted.");
        return NotFound();
    }

    [AllowAnonymous]
    [HttpPost("Get")]
    [SwaggerOperation("Получает книги по названию", "Позволяет находить книги по названию книги")]
    [SwaggerResponse(200, "Успешно найдено")]
    [SwaggerResponse(400, "Ошибка валидации")]
    public async Task<IActionResult> GetSome(GetBookByTitleQuery request)
    {
        if (request.Title.IsNullOrEmpty()) return BadRequest("Title is null");

        var books = await _mediator.Send(request);
        return Ok(books);
    }


}
