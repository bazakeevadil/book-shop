﻿using Application.Contract;
using Domain.Repositories;
using MediatR;

namespace Application.Books.Requests
{
    public record GetBookByTitleQuery : IRequest<BookDto>
    {
        public required string Title { get; init; }
    }

    internal class GetBookByTitle : IRequestHandler<GetBookByTitleQuery, BookDto?>
    {
        private readonly IBookRepository _bookRepository;

        public GetBookByTitle(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<BookDto?> Handle(GetBookByTitleQuery request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByTitle(request.Title).ConfigureAwait(false);
            if (book is not null)
            {
                var response = new BookDto
                {
                    Id = book.Id,
                    Title = book.Title,
                    Description = book.Description,
                    Author = book.Author,
                    Quantity = book.Quantity,
                    Price = book.Price,
                };
                return response;
            }
            return default;
        }
    }

}