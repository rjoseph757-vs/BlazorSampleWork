using RapidBlazor21.Application.Common.Exceptions;
using RapidBlazor21.Application.TodoLists.Commands;
using RapidBlazor21.Domain.Entities;
using RapidBlazor21.WebUI.Shared.TodoLists;

namespace RapidBlazor21.Application.SubcutaneousTests.TodoLists.Commands;

using static Testing;

public class CreateTodoListTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateTodoListCommand(
            new CreateTodoListRequest());

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldRequireUniqueTitle()
    {
        await SendAsync(new CreateTodoListCommand(
            new CreateTodoListRequest { Title = "Shopping" }));

        var command = new CreateTodoListCommand(
            new CreateTodoListRequest { Title = "Shopping" });

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateTodoList()
    {
        var userId = await RunAsDefaultUserAsync();

        var request = new CreateTodoListRequest { Title = "Tasks" };

        var command = new CreateTodoListCommand(request);

        var id = await SendAsync(command);

        var list = await FindAsync<TodoList>(id);

        list.Should().NotBeNull();
        list!.Title.Should().Be(request.Title);
        list.CreatedBy.Should().Be(userId);
        list.CreatedUtc.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMilliseconds(10000));
    }
}
