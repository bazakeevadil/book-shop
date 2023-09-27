using Application.Contract;
using Application.Shared;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;

namespace Application.Books.Commands;

public record AddBookCommand : IRequest<BookDto>
{
    public required string Title { get; init; }
    public string? Description { get; init; }
    public required string Author { get; init; }
    public required long Quantity { get; init; }
    public required decimal Price { get; init; }
}

internal class AddBookCommandHandler
    : IRequestHandler<AddBookCommand, BookDto>
{
    private readonly IBookRepository _bookRepository;
    private readonly IUnitOfWork _uow;

    public AddBookCommandHandler(IBookRepository bookRepository, IUnitOfWork uow)
    {
        _bookRepository = bookRepository;
        _uow = uow;
    }

    public async Task<BookDto> Handle(
        AddBookCommand request, CancellationToken cancellationToken)
    {
        var book = request.Adapt<Book>();

        await _bookRepository.CreateAsync(book);
        await _uow.CommitAsync();

        var response = book.Adapt<BookDto>();

        return response;
    }
}
