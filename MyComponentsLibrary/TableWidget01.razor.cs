using Microsoft.AspNetCore.Components;

namespace MyComponentsLibrary
{
    public partial class TableWidget01<TItem>
    {
        [Parameter]
        public required RenderFragment HeaderTemplate { get; set; }

        [Parameter]
        public required RenderFragment<TItem> RowTemplate { get; set; }

        [Parameter]
        public required RenderFragment FooterTemplate { get; set; }

        [Parameter]
        public required IReadOnlyList<TItem> Items { get; set; }
    }
}
