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
                Categoria = d.Category,
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

    public async Task<ProductoApiDto?> ObtenerProductoPorIdAsync(int id)
    {
        try
        {
            return await _http.GetFromJsonAsync<ProductoApiDto>($"https://fakestoreapi.com/products/{id}");
        }
        catch (TaskCanceledException)
        {
            throw new Exception("La petición tardó demasiado tiempo en responder (Timeout).");
        }
        catch (HttpRequestException)
        {
            throw new Exception("No se pudo conectar con el servidor o la ruta no existe.");
        }
        catch (JsonException)
        {
            throw new Exception("La respuesta recibida no se pudo interpretar como JSON.");
        }
    }



}