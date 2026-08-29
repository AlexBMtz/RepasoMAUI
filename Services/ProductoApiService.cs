using RepasoMAUI.Data.DTOs;
using RepasoMAUI.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace RepasoMAUI.Services;

public class ProductoApiService
{
    private const string BaseUrl = "https://fakestoreapi.com/products";
    private readonly HttpClient _http;

    public ProductoApiService()
    {
        _http = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(8)
        };
    }

    public async Task<(List<Producto> productos, string? error)> ObtenerProductosAsync(string url = BaseUrl)
    {
        try
        {
            var dtos = await _http.GetFromJsonAsync<List<ProductoApiDto>>(url);

            var productos = dtos?.Select(d => new Producto
            {
                Id = d.Id.ToString(),
                Nombre = d.Title,
                Descripcion = d.Description,
                Categoria = d.Category,
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
            return (new List<Producto>(), "La respuesta del servidor no se pudo interpretar como JSON.");
        }
    }

    public async Task<(Producto? producto, string? error)> ObtenerProductoAsync(string id, string? url = null, int? timeoutMilliseconds = null)
    {
        var originalTimeout = _http.Timeout;

        if (timeoutMilliseconds.HasValue)
        {
            _http.Timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds.Value);
        }

        var requestUrl = url ?? $"{BaseUrl}/{id}";

        try
        {
            var dto = await _http.GetFromJsonAsync<ProductoApiDto>(requestUrl);

            if (dto is null)
            {
                return (null, "No se recibió información del producto.");
            }

            var producto = new Producto
            {
                Id = dto.Id.ToString(),
                Nombre = dto.Title,
                Descripcion = dto.Description,
                Categoria = dto.Category,
                Precio = dto.Price,
                ImagenUrl = dto.Image
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
            return (null, "La respuesta del servidor no se pudo interpretar como JSON.");
        }
        finally
        {
            if (timeoutMilliseconds.HasValue)
            {
                _http.Timeout = originalTimeout;
            }
        }
    }
}