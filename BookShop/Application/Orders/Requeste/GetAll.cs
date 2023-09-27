using Application.Contract;
using Domain.Repositories;
using MediatR;

namespace Application.Orders.Requests;

public record GetAllOrderQuery : IRequest<IEnumerable<OrderDto>> { }

internal class GetAllOrderHandler : IRequestHandler<GetAllOrderQuery, IEnumerable<OrderDto>>
{
    private readonly IOrderRepository _orderRepository;

    public GetAllOrderHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<IEnumerable<OrderDto>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetAll().ConfigureAwait(false);
        var response = new List<OrderDto>();
        if (orders.Any())
        {
            foreach (var order in orders)
            {
                var result = new OrderDto
                {
                    Id = order.Id,
                    UserId = order.UserId,
                    OrderStatus = order.OrderStatus,
                    TotalPrice = order.TotalPrice,
                };
                response.Add(result);
            }
            return response;
        }
        return default!;
    }
}