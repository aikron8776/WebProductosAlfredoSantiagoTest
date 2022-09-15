using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProductos.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public decimal precio { get; set; }
        public string categoria { get; set; }
        public int cantidad { get; set; }
    }
}