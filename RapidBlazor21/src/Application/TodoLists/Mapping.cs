using RapidBlazor21.WebUI.Shared.TodoLists;

namespace RapidBlazor21.Application.TodoLists;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<TodoList, TodoListDto>();
        CreateMap<TodoItem, TodoItemDto>();
    }
}
