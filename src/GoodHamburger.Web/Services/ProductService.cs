using System.Net.Http.Json;
using GoodHamburger.Web.Models;

public class ProductService
{
    private readonly HttpClient _http;

    public ProductService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _http.GetFromJsonAsync<List<Product>>("api/products") ?? [];
    }
}
