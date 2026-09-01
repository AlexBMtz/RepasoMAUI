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
                ImagenUrl = d.Image,
                Categoria = d.Category
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

    // Trae un solo producto por id. El parámetro "url" es opcional y existe
    // únicamente para poder forzar rutas rotas o con timeout durante las
    // pruebas de manejo de errores (punto 6 del ejercicio), sin tocar la
    // firma que usa el happy path.
    public async Task<(Producto producto, string error)> ObtenerProductoPorIdAsync(string id, string url = null)
    {
        url ??= $"https://fakestoreapi.com/products/{id}";

        try
        {
            var dto = await _http.GetFromJsonAsync<ProductoApiDto>(url);

            if (dto is null)
            {
                return (null, "El producto no fue encontrado.");
            }

            var producto = new Producto
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