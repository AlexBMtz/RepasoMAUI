namespace RepasoMAUI.Models
{
    public class Producto
    {
        public string Id { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public string ImagenUrl { get; set; } = string.Empty;
    }
}
