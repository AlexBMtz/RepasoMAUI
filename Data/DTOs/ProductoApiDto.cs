using System;
using System.Collections.Generic;
using System.Text;

namespace RepasoMAUI.Data.DTOs
{
    public class ProductoApiDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
    }
}
