using Bibliotec.Models;
using Microsoft.EntityFrameworkCore;

namespace Bibliotec.Contexts
{
    // Classe que tera as informacoes do banco de dados
    public class Context : DbContext
    {
        public Context()
        {   }
        public Context(DbContextOptions<Context> options) : base(options)
        {     }
        // OnConfiguring -> Possui a configuracao da conexao com 
        // o banco de dados

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            //colocar as informacoes no banco de dados

            // as configuracoes existem?
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-1HURK41\VINI; Initial Catalog = Bibliotec_mvc; User Id=sa; Password=123;TrustServerCertificate = true");
            }
        }

    	public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Curso> Curso {get; set;}
    	public DbSet<Livro> Livro { get; set; }
    	public DbSet<Usuario> Usuario { get; set; }
    	public DbSet<LivroCategoria> LivroCategoria { get; set; }
    	public DbSet<LivroReserva> livroReserva { get; set; }
        
    }
}