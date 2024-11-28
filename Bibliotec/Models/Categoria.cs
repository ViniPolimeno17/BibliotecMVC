using System.ComponentModel.DataAnnotations;

namespace Bibliotec.Models
{
    public class Categoria
    {
        [Key]
        public int CategoriaID { get; set; }

        public string? Nome { get; set; }
    }
}