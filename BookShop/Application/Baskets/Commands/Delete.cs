using Application.Shared;
using Domain.Repositories;
using MediatR;

namespace Application.Baskets.Commands;

public record DeleteBooksByTitleFromBasketCommand : IRequest<bool>
{
    public required string Title { get; init; }

    public required string Username { get; init; }
}

internal class DeleteBooksFromBasket : IRequestHandler<DeleteBooksByTitleFromBasketCommand, bool>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBookRepository _bookRepository;

    public DeleteBooksFromBasket(IUserRepository userRepository, IUnitOfWork unitOfWork, IBookRepository bookRepository)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _bookRepository = bookRepository;
    }

    public async Task<bool> Handle(DeleteBooksByTitleFromBasketCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByUsername(command.Username).ConfigureAwait(false);
        var book = await _bookRepository.GetByTitle(command.Title).ConfigureAwait(false);

        if (user != null && book != null)
        {
            user.Basket.Books.RemoveAll(b => b.Title == book.Title);
            await _unitOfWork.CommitAsync().ConfigureAwait(false);
            return true;
        }
        return false;
    }
}