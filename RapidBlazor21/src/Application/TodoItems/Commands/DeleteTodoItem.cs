﻿namespace RapidBlazor21.Application.TodoItems.Commands;

public record DeleteTodoItemCommand(int Id) : IRequest;

public class DeleteTodoItemCommandHandler
    : IRequestHandler<DeleteTodoItemCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteTodoItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteTodoItemCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await _context.TodoItems.FindAsync(request.Id);

        Guard.Against.NotFound(request.Id, entity);

        _context.TodoItems.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
