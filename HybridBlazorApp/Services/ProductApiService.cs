using HybridBlazorApp.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace HybridBlazorApp.Services;

public class ProductApiDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Image { get; set; } = string.Empty;
}

public class ProductApiService
{
    private readonly HttpClient _http;

    public ProductApiService()
    {
        _http = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(8)
        };
    }

    public ProductApiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<(List<Product> products, string? error)> GetProductsAsync(string url = "https://fakestoreapi.com/products")
    {
        try
        {
            var dtos = await _http.GetFromJsonAsync<List<ProductApiDto>>(url);

            var products = dtos?.Select(d => new Product
            {
                Id = d.Id.ToString(),
                Name = d.Title,
                Description = d.Description,
                Price = d.Price,
                ImageUrl = d.Image
            }).ToList() ?? [];

            return (products, null);
        }
        catch (TaskCanceledException)
        {
            return (new List<Product>(), "La petición tardó demasiado. Verifica tu conexión e intenta de nuevo.");
        }
        catch (HttpRequestException ex)
        {
            return (new List<Product>(), $"No se pudo conectar al servidor ({ex.StatusCode}).");
        }
        catch (JsonException)
        {
            return (new List<Product>(), "La respuesta del servidor no se pudo interpretar.");
        }
        catch (Exception ex)
        {
            return (new List<Product>(), $"Error inesperado: {ex.Message}");
        }
    }
}
