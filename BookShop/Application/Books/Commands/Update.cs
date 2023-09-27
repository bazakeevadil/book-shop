using Application.Contract;
using Application.Shared;
using Domain.Repositories;
using MediatR;

namespace Application.Books.Commands;

public record UpdateBookCommand : IRequest
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public string? Description { get; init; }
    public required string Author { get; init; }
    public required long Quantity { get; init; }
    public required decimal Price { get; init; }
}

internal class UpdateBookHandler : IRequestHandler<UpdateBookCommand>
{
    private readonly IBookRepository _bookRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateBookHandler(IBookRepository bookRepository, IUnitOfWork unitOfWork)
    {
        _bookRepository = bookRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateBookCommand command, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetById(command.Id).ConfigureAwait(false);
        if (book is null)
        {
            throw new Exception("Такой книги не существует!");
        }

        book.Title = command.Title;
        book.Description = command.Description;
        book.Author = command.Author;
        book.Quantity = command.Quantity;
        book.Price = command.Price;

        _bookRepository.Update(book);
        await _unitOfWork.CommitAsync().ConfigureAwait(false);
    }
}