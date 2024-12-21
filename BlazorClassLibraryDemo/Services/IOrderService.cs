using BlazorClassLibraryDemo.Data;

namespace BlazorClassLibraryDemo.Services
{
    public interface IOrderService
    {
        List<Order> GetLatestOrders();
    }
}
