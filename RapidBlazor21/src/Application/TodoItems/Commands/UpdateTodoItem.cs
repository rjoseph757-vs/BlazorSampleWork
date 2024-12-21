using RapidBlazor21.Domain.Enums;
using RapidBlazor21.Domain.Events;
using RapidBlazor21.WebUI.Shared.TodoItems;

namespace RapidBlazor21.Application.TodoItems.Commands;

public record UpdateTodoItemCommand(UpdateTodoItemRequest Item) : IRequest;

public class UpdateTodoItemCommandHandler
        : IRequestHandler<UpdateTodoItemCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateTodoItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateTodoItemCommand request,
            CancellationToken cancellationToken)
    {
        var entity = await _context.TodoItems.FirstOrDefaultAsync(
            i => i.Id == request.Item.Id, cancellationToken);

        Guard.Against.NotFound(request.Item.Id, entity);

        entity!.ListId = request.Item.ListId;
        entity.Title = request.Item.Title;
        entity.Done = request.Item.Done;
        entity.Priority = (PriorityLevel)request.Item.Priority;
        entity.Note = request.Item.Note;

        if (entity.Done)
        {
            entity.AddDomainEvent(new TodoItemCompletedEvent(entity));
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}
