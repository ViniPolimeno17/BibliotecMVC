using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bibliotec.Models
{
    public class Curso
    {
        [Key]
        public int CursoID { get; set; }
        public string? Nome { get; set; }
        public char Periodo { get; set; }

        
    }
}