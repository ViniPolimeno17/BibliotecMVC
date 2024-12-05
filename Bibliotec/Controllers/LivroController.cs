using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Bibliotec.Contexts;
using Bibliotec.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bibliotec.Controllers
{
    [Route("[controller]")]
    public class LivroController : Controller
    {
        private readonly ILogger<LivroController> _logger;

        public LivroController(ILogger<LivroController> logger)
        {
            _logger = logger;
        }

        Context context = new Context();

        public IActionResult Index()
        {
            ViewBag.Admin = HttpContext.Session.GetString("Admin")!;

            List<Livro> listaLivros = context.Livro.ToList();

            var livrosReservados = context.livroReserva.ToDictionary(livro => livro.LivroID, livror => livror.DtReserva);

            ViewBag.Livros = listaLivros;
            ViewBag.LivrosComReserva = livrosReservados;



            return View();
        }

        [Route("Cadastro")]
        //Metodo que aparece a tela de cadastro:
        public IActionResult Cadastro(){   

            ViewBag.Admin = HttpContext.Session.GetString("Admin")!;

            ViewBag.Categorias = context.Categoria.ToList();
            //Retorna a View de Cadastro
            return View();
        }

        [Route("Cadastrar")]
        public IActionResult Cadastrar(IFormCollection form){

            Livro novoLivro = new Livro();

            novoLivro.Nome = form["Nome"].ToString();
            novoLivro.Descricao = form["Descricao"].ToString();
            novoLivro.Editora = form["Editora"].ToString();
            novoLivro.Escritor = form["Escritor"].ToString();
            novoLivro.Idioma = form["Idioma"].ToString();

            if (form.Files.Count > 0){
                var arquivo = form.Files[0];

                var pasta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Livros");

                if(!Directory.Exists(pasta)){
                    Directory.CreateDirectory(pasta);

                }
                var caminho = Path.Combine(pasta, arquivo.FileName);

                using (var stream = new FileStream(caminho, FileMode.Create)) {
                    arquivo.CopyTo(stream);

                }

                novoLivro.Imagem = arquivo.FileName;

                } else{
                    novoLivro.Imagem = "padrao.png";
                }
            

            context.Livro.Add(novoLivro);
            context.SaveChanges();

            List<LivroCategoria> listaLivroCategorias = new List<LivroCategoria>();

            string[] categoriasSelecionadas = form ["Categoria"].ToString().Split(',');

            foreach(string categoria in categoriasSelecionadas){
                LivroCategoria livroCategoria = new LivroCategoria();
                livroCategoria.CategoriaID = int.Parse(categoria);
                livroCategoria.LivroID = novoLivro.LivroID;
                listaLivroCategorias.Add(livroCategoria);
            }
            context.LivroCategoria.AddRange(listaLivroCategorias);

            context.SaveChanges();

            return LocalRedirect ("/Livro/Cadastro");
            }

        [Route("Editar/{id}")]
        public IActionResult Editar(int id){
            ViewBag.Admin = HttpContext.Session.GetString("Admin")!;

            ViewBag.CategoriasDoSistema = context.Categoria.ToList(); 

            Livro livroEcontrado = context.Livro.FirstOrDefault(livro => livro.LivroID == id)!;

            var categoriasDoLivroEncontrado = context.LivroCategoria.Where(identificadorLivro => identificadorLivro.LivroID == id).Select(livro => livro.Categoria).ToList();

            ViewBag.Livro = livroEcontrado;
            ViewBag.Categoria = categoriasDoLivroEncontrado;

            return View();
        }




        }
        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View("Error!");
        // }
}
