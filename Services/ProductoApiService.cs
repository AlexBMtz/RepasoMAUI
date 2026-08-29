using RepasoMAUI.Data.DTOs;
using RepasoMAUI.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace RepasoMAUI.Services;

public class ProductoApiService
{
    private readonly HttpClient _http;

    public ProductoApiService()
    {
        _http = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(8)
        };
    }

    public async Task<(List<Producto> productos, string error)> ObtenerProductosAsync(string url = "https://fakestoreapi.com/products")
    {
        try
        {
            var dtos = await _http.GetFromJsonAsync<List<ProductoApiDto>>(url);

            var productos = dtos?.Select(d => new Producto
            {
                Id = d.Id.ToString(),
                Nombre = d.Title,
                Descripcion = d.Description,
                Precio = d.Price,
                ImagenUrl = d.Image
            }).ToList() ?? [];

            return (productos, null);
        }
        catch (TaskCanceledException)
        {
            return (new List<Producto>(), "La petición tardó demasiado. Verifica tu conexión e intenta de nuevo.");
        }
        catch (HttpRequestException ex)
        {
            return (new List<Producto>(), $"No se pudo conectar al servidor ({ex.StatusCode}).");
        }
        catch (JsonException)
        {
            return (new List<Producto>(), "La respuesta del servidor no se pudo interpretar.");
        }
    }

    public async Task<(Producto producto, string error)> ObtenerProductoPorIdAsync(string id, string urlBase = "https://fakestoreapi.com/products")
    {
        try
        {
            var dto = await _http.GetFromJsonAsync<ProductoApiDto>($"{urlBase}/{id}");

            var producto = dto is null ? null : new Producto
            {
                Id = dto.Id.ToString(),
                Nombre = dto.Title,
                Descripcion = dto.Description,
                Precio = dto.Price,
                ImagenUrl = dto.Image,
                Categoria = dto.Category
            };

            return (producto, null);
        }
        catch (TaskCanceledException)
        {
            return (null, "La petición tardó demasiado. Verifica tu conexión e intenta de nuevo.");
        }
        catch (HttpRequestException ex)
        {
            return (null, $"No se pudo conectar al servidor ({ex.StatusCode}).");
        }
        catch (JsonException)
        {
            return (null, "La respuesta del servidor no se pudo interpretar.");
        }
    }
}