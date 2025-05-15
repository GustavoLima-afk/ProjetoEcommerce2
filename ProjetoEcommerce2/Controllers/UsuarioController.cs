using Microsoft.AspNetCore.Mvc;
using ProjetoEcommerce.Repositorio;
using ProjetoEcommerce2.Repositorio;

namespace ProjetoEcommerce2.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(UsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }


        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(string email, string senha)
        {

            var usuario = _usuarioRepositorio.ObterUsuario(email);
            if (usuario != null && usuario.Senha == senha)
            {
                return RedirectToAction("Index", "Cliente");
            }

            ModelState.AddModelError("", "Email ou senha inválidos.");
            return View();
        }
    }
}