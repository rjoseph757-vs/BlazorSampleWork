using BlazorClassLibraryDemo.Data;

namespace BlazorClassLibraryDemo.Services
{
    public interface IProductService
    {
        List<Product> GetTopSellingProducts();
    }
}
