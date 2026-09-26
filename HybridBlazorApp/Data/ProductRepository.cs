using HybridBlazorApp.Models;

namespace HybridBlazorApp.Data
{
    public class ProductRepository
    {
        private readonly List<Product> _productos =
    [
        new(){ Id = "1", Name = "Teclado Mecánico", Description = "Teclado mecánico switches azules, retroiluminado.", Price = 899.00m, ImageUrl = "teclado.png" },
        new(){ Id = "2", Name = "Mouse Inalámbrico", Description = "Mouse inalámbrico ergonómico, 2.4GHz.", Price = 349.00m, ImageUrl = "mouse.png" },
        new(){ Id = "3", Name = "Monitor 27\"", Description = "Monitor 27 pulgadas, 144Hz, IPS.", Price = 4599.00m, ImageUrl = "monitor.png" },
        new(){ Id = "4", Name = "Audífonos USB", Description = "Audífonos con micrófono, cancelación de ruido.", Price = 599.00m, ImageUrl = "audifonos.png" }
    ];

        public List<Product> GetProducts() => _productos;

        public Product GetProductById(string id) => _productos.FirstOrDefault(p => p.Id == id);
    }
}
