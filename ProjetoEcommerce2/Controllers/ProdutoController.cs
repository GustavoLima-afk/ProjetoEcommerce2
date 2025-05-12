using Microsoft.AspNetCore.Mvc;
using ProjetoEcommerce2.Models;
using ProjetoEcommerce2.Repositorio;

namespace ProjetoEcommerce2.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ProdutoRepositorio _produtoRespositorio;

        public ProdutoController(ProdutoRepositorio produtoRepositorio)
        {
            _produtoRespositorio = produtoRepositorio;
        }
        public ActionResult Index()
        {
            return View(_produtoRespositorio.TodosProdutos());
        }

        public IActionResult CadastrarProduto()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CadastrarProduto(Produto produto)
        {

            _produtoRespositorio.Cadastrar(produto);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult EditarProduto(int id)
        {
            var produto = _produtoRespositorio.ObterProduto(id);

            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarProduto(int id, [Bind("IdProd, Nome, Descricao, Quantidade, Preco")] Produto produto)
        {
            if
                return BadRequest();
        }
            if (ModelState.IsValid)
            {
                try
                {
                    if (_produtoRespositorio.Atualizar(produto))
                    {
                        return RedirectToAction(nameof(Index));
    }
}
                catch (Exception)
                {
    ModelState.AddModelError("", "Ocorreu um erro ao Editar.");
    return View(produto);
}
            }
            return View(produto);
        }

        public IActionResult ExcluirProduto(int id)
{
    _produtoRespositorio.Excluir(id);
    return RedirectToAction(nameof(Index));
}

    }
}