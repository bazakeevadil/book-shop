using Application.Orders.Commands;
using Application.Orders.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAll")]
        [SwaggerOperation("Получает все заказы", "Получает все заказы пользователя")]
        [SwaggerResponse(200, "Успешно получено")]
        [SwaggerResponse(400, "Ошибка валидации")]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllOrderQuery();
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpPatch("Update")]
        [SwaggerOperation("Позволяет редактировать заказы", "Позволяет редактировать заказы пользователя")]
        [SwaggerResponse(200, "Успешно редактировано")]
        [SwaggerResponse(400, "Ошибка валидации")]
        public async Task<IActionResult> Update(UpdateOrderCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("DeleteByIdOrder")]
        [SwaggerOperation("Удаляет заказ по Id", "Позволяет удалить заказ по id")]
        [SwaggerResponse(200, "Успешно удалено")]
        [SwaggerResponse(400, "Ошибка валидации")]
        public async Task<IActionResult> Delete(DeleteOrderByIdCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}