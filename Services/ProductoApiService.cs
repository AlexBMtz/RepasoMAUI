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

    public async Task<(List<Producto> productos, string? error)> ObtenerProductosAsync(string url = "https://fakestoreapi.com/products")
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

    /// <summary>
    /// Consulta el endpoint de Fakestore API para obtener el detalle de un producto específico mediante su ID.
    /// </summary>
    /// <param name="id">Identificador único del producto.</param>
    /// <returns>Una tupla con el objeto Producto mapeado o un mensaje de error si ocurre una falla.</returns>
    public async Task<(Producto? producto, string? error)> ObtenerProductoPorIdAsync(string id)
    {
        try
        {
            // Realiza la petición HTTP GET al endpoint del producto en FakeStore API
            var dto = await _http.GetFromJsonAsync<ProductoApiDto>($"https://fakestoreapi.com/products/{id}");

            if (dto is null)
            {
                return (null, "No se encontró el producto solicitado.");
            }

            // Mapea los datos recibidos del DTO al modelo de la aplicación
            var producto = new Producto
            {
                Id = dto.Id.ToString(),
                Nombre = dto.Title,
                Descripcion = dto.Description,
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
            return (null, "La respuesta del servidor no se pudo interpretar.");
        }
    }
}