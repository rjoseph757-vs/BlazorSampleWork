using RapidBlazor21.Domain.Enums;
using RapidBlazor21.WebUI.Shared.TodoLists;

namespace RapidBlazor21.Application.TodoLists.Queries;

public record GetTodoListsQuery : IRequest<TodosVm>;

public class GetTodoListsQueryHandler
    : IRequestHandler<GetTodoListsQuery, TodosVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTodoListsQueryHandler(
        IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TodosVm> Handle(
        GetTodoListsQuery request,
        CancellationToken cancellationToken)
    {
        return new TodosVm
        {
            // TODO: Create an enum helper to build these...
            PriorityLevels = Enum.GetValues(typeof(PriorityLevel))
                .Cast<PriorityLevel>()
                .Select(p => new LookupDto
                {
                    Id = (int)p,
                    Title = p.ToString()
                })
                .ToList(),

            Lists = await _context.TodoLists
                .ProjectTo<TodoListDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
        };
    }
}
