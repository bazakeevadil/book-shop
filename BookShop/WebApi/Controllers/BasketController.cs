using Application.Baskets.Commands;
using Application.Baskets.Requests;
using Application.Orders.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace WebApi.Controllers
{
    [ApiController, Route("api/basket")]
    public class BasketController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BasketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet("Get")]
        [SwaggerOperation("Получения заказа", "Позволяет получить для текушего пользователя книги")]
        [SwaggerResponse(200, "Успешно получено")]
        [SwaggerResponse(400, "Ошибка валидации")]
        public async Task<IActionResult> Get()
        {
            var username = HttpContext.User.FindFirstValue(ClaimTypes.Name) ?? string.Empty;
            if (username is null)
                return NotFound();

            var request = new GetBooksFromBasketQuery
            {
                Username = username
            };
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Get")]
        [SwaggerOperation("Получения заказа для админа", "Позволяет получить для текушего пользователя книги для админа")]
        [SwaggerResponse(200, "Успешно получено")]
        [SwaggerResponse(400, "Ошибка валидации")]
        public async Task<IActionResult> GetForAdmin(GetBooksFromBasketQuery request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("AddToOwn")]
        [SwaggerOperation("Добавление для пользователя", "Этом опирациее может воспользоваца пользователь")]
        [SwaggerResponse(200, "Успешно добавлен")]
        [SwaggerResponse(400, "Ошибка валидации")]
        public async Task<IActionResult> AddToOwn(string title)
        {
            if (title.IsNullOrEmpty()) return BadRequest("Title is null");

            var username = HttpContext.User.FindFirstValue(ClaimTypes.Name) ?? string.Empty;
            if (username is null)
                return NotFound();

            var command = new AddBookToBasketCommand
            {
                Title = title,
                Username = username
            };
            var response = await _mediator.Send(command);
            if (response)
                return Ok("Book add basket");
            return NotFound("Book not Found");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Add")]
        [SwaggerOperation("Добавления для админа", "Этом опирациее может воспользоваца только админ")]
        [SwaggerResponse(200, "Успешно добавлен")]
        [SwaggerResponse(400, "Ошибка валидации")]
        public async Task<IActionResult> Add(AddBookToBasketCommand command)
        {
            if (command.Title.IsNullOrEmpty()) return BadRequest("Title is null");
            if (command.Username.IsNullOrEmpty()) return BadRequest("Username is null");

            var response = await _mediator.Send(command);
            if (response)
                return Ok("Book add basket");
            return NotFound("Book not Found");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete")]
        [SwaggerOperation("Удаление для админа", "Этой опирациее может воспользоваца только админ")]
        [SwaggerResponse(200, "Успешно удален")]
        [SwaggerResponse(400, "Ошибка валидации")]
        public async Task<IActionResult> Delete(DeleteBooksByTitleFromBasketCommand command)
        {
            var response = await _mediator.Send(command);
            if (response) return Ok("Book(s) deleted");
            return NotFound();
        }

        [AllowAnonymous]
        [HttpPost("Delete")]
        [SwaggerOperation("Удаление для пользователя", "Этом опирациее может воспользоваца пользователь")]
        [SwaggerResponse(200, "Успешно удален")]
        [SwaggerResponse(400, "Ошибка валидации")]
        public async Task<IActionResult> Delete(string title)
        {
            var username = HttpContext.User.FindFirstValue(ClaimTypes.Name) ?? string.Empty;
            if (username is null)
                return NotFound();

            var command = new DeleteBooksByTitleFromBasketCommand
            {
                Title = title,
                Username = username
            };

            var response = await _mediator.Send(command);
            if (response) return Ok("Book");
            return NotFound();
        }

        [HttpPost("Check")]
        [SwaggerOperation("Просмотр корзины", "Метод для просмотра всех заказов пользователя")]
        [SwaggerResponse(200, "Успешно найдено")]
        [SwaggerResponse(400, "Ошибка валидации")]
        public async Task<IActionResult> Check([Required] string address)
        {
            var username = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            var query = new CreateOrderCommand
            {
                Username = username ?? string.Empty,
                Address = address,
            };
            var response = await _mediator.Send(query);
            return Ok(response);
        }
    }

}