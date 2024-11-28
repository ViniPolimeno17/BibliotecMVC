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
    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(ILogger<UsuarioController> logger)
        {
            _logger = logger;
        }

        Context context = new Context();
        
        public IActionResult Index()
        {
            //pegar as informacoes que sao necessarias para aparecer nos detalhes do meu usuario
            int id = int.Parse(HttpContext.Session.GetString("UsuarioID")!);
            ViewBag.Admin = HttpContext.Session.GetString("Admin")!;
            
            
            Usuario usuarioEncontrado = context.Usuario.FirstOrDefault(usuario => usuario.UsuarioID == id)!;

            if (usuarioEncontrado == null){
                return NotFound();
            }
            // Console.WriteLine($"balinha");
            
            Curso cursoEncontrado = context.Curso.FirstOrDefault(curso => curso.CursoID == usuarioEncontrado.CursoID)!;

            if(cursoEncontrado == null){
                ViewBag.Curso = "Oh usuário não possui curso cadastrado";
            }else{
                ViewBag.Curso = cursoEncontrado.Nome;
            }
            // Console.WriteLine($"{usuarioEncontrado.CursoID}");

            ViewBag.Nome = usuarioEncontrado.Nome;
            ViewBag.Email = usuarioEncontrado.Email;
            ViewBag.Contato = usuarioEncontrado.Contato;
            ViewBag.DtNasc = usuarioEncontrado.DtNascimento.ToString("dd/MM/yyyy");

            return View();
        }

        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View("Error!");
        // }
    }
}