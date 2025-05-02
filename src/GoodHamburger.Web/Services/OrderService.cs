using System.Net.Http.Json;
using GoodHamburger.Web.Models;

public class OrderService
{
    private readonly HttpClient _http;
    public OrderService(HttpClient http)
    {
        _http = http;
    }

    public async Task<OrderResponse> CreateAsync(OrderRequest request)
    {
        var response = await _http.PostAsJsonAsync("api/orders", request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<OrderResponse>();
    }

    public async Task<List<OrderResponse>> GetAllAsync()
    {
        return await _http.GetFromJsonAsync<List<OrderResponse>>("api/orders");
    }

    public async Task DeleteAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/orders/{id}");
        response.EnsureSuccessStatusCode();
    }

    // auxiliar para mostrar nomes dos produtos
    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _http.GetFromJsonAsync<List<Product>>("api/products");
    }

    public async Task<OrderResponse> GetByIdAsync(int id)
    {
        var response = await _http.GetAsync($"api/orders/{id}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<OrderResponse>();
    }

    public async Task UpdateAsync(int id, OrderRequest request)
    {
        var response = await _http.PutAsJsonAsync($"api/orders/{id}", request);
        response.EnsureSuccessStatusCode();
    }
}
