using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Bibliotec.Models
{
    public class Livro
    {
        [Key]
        public int LivroID { get; set; }
        public string? Nome { get; set; }
        public string? Escritor { get; set; }
        public string? Editora { get; set; }
        public string? Descricao { get; set; }
        public string? idioma { get; set; }
        public string? imagem { get; set; }
        public bool Reservado { get; set; }
        public bool Ativo { get; set; }
    }
}