using Application.Users.Commands;
using Application.Users.Commands.Delete;
using Application.Users.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApi.Controllers
{
    [ApiController, Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAll")]
        [SwaggerOperation("Получает всех пользователей", "Позволяет получить всех пользователей только для админа")]
        [SwaggerResponse(200, "Успешно получены")]
        [SwaggerResponse(400, "Ошибка валидации")]
        public async Task<IActionResult> GetAll()
        {
            var request = new GetAllUsersQuery();
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("GetByName")]
        [SwaggerOperation("Получает пользователя по имени", "Позволяет получить пользователя по имени только для админа")]
        [SwaggerResponse(200, "Успешно получен")]
        [SwaggerResponse(400, "Ошибка валидации")]
        public async Task<IActionResult> GetByName(GetUserByNameQuery request)
        {
            if (request.Username.IsNullOrEmpty()) return BadRequest("Is null");
            var user = await _mediator.Send(request);
            if (user is not null)
                return Ok(user);
            return NotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("GetUsersById")]
        [SwaggerOperation("Получает пользователя по id", "Позволяет получить пользователя по id только для админа")]
        [SwaggerResponse(200, "Успешно получен")]
        [SwaggerResponse(400, "Ошибка валидации")]
        public async Task<IActionResult> GetSome(GetUserByIdQuery request)
        {
            var user = await _mediator.Send(request);
            if (user is not null)
                return Ok(user);
            return NotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("Update")]
        [SwaggerOperation("Редактирует пользователя", "Позволяет редактировать пользователя только для админа")]
        [SwaggerResponse(200, "Успешно редактировано")]
        [SwaggerResponse(400, "Ошибка валидации")]
        public async Task<IActionResult> Update(UpdateUserCommand command)
        {
            if (command.Username.IsNullOrEmpty()) return BadRequest("Is null");
            var user = await _mediator.Send(command);
            if (user is not null)
                return Ok(user);
            return NotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteByNames")]
        [SwaggerOperation("Удаляет пользователя по имени", "Позволяет удалить пользователя по имени только для админа")]
        [SwaggerResponse(200, "Успешно удалено")]
        [SwaggerResponse(400, "Ошибка валидации")]
        public async Task<IActionResult> Delete(DeleteUsersByNamesCommand command)
        {
            var response = await _mediator.Send(command);
            if (response == true)
                return Ok(response);
            return NotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteById")]
        [SwaggerOperation("Удаляет пользователя по Id", "Позволяет удалить пользователя по id только для админа")]
        [SwaggerResponse(200, "Успешно удалено")]
        [SwaggerResponse(400, "Ошибка валидации")]
        public async Task<IActionResult> Delete(DeleteUsersByIdCommand command)
        {
            var response = await _mediator.Send(command);
            if (response == true)
                return Ok(response);
            return NotFound();
        }
    }

}