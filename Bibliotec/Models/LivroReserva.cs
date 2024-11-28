using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bibliotec.Models
{
    public class LivroReserva
    {
        [Key]
        public int LivroCategoriaID { get; set; }
        public DateOnly DtReserva { get; set; }
        public DateOnly DtDevolucao { get; set; }
        public int Status { get; set; }

        [ForeignKey ("Livro")]
        public int LivroID { get; set; }
        public Livro? Livro { get; set; }

        [ForeignKey ("Usuario")]
        public int UsuarioID { get; set; }
        public Usuario? Usuario { get; set; }

    }
}