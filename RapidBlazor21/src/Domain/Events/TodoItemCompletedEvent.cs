using RapidBlazor21.Domain.Common;
using RapidBlazor21.Domain.Entities;

namespace RapidBlazor21.Domain.Events;

public class TodoItemCompletedEvent : BaseEvent
{
    public TodoItemCompletedEvent(TodoItem item)
    {
        Item = item;
    }

    public TodoItem Item { get; }
}
