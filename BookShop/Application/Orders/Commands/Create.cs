using Application.Contract;
using Application.Shared;
using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using MediatR;

namespace Application.Orders.Commands;

public record CreateOrderCommand : IRequest<OrderDto>
{
    public required string Username { get; init; }
}

internal class CreateOrderHandler : IRequestHandler<CreateOrderCommand, OrderDto>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    public CreateOrderHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork, IUserRepository userRepository)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }

    public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserById(request.Username);
        if (user is null) throw new ArgumentNullException(nameof(user));

        var books = user.Basket.Books.ToList();
        if (!books.Any()) throw new ArgumentNullException(nameof(books));

        var price = books.Sum(book => book.Price);

        var order = new Order
        {
            UserId = user.Id,
            TotalPrice = price,
            OrderStatus = OrderStatus.Delivered,
            User = user,
        };

        var orderDto = new OrderDto
        {
            Id = order.Id,
            UserId = order.UserId,
            TotalPrice = order.TotalPrice,
            OrderStatus = order.OrderStatus,
        };

        await _orderRepository.Create(order);
        user.Basket.Books.Clear();
        await _unitOfWork.CommitAsync();

        return orderDto;
    }
}
