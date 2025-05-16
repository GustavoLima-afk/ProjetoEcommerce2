using Microsoft.AspNetCore.Mvc;
using ProjetoEcommerce2.Models;
using ProjetoEcommerce2.Repositorio;

namespace ProjetoEcommerce.Controllers
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

        // Fazendo o cadastro do produto
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

        // Fazendo o editar do produto
        public IActionResult EditarProduto(int id)
        {
            var produto = _produtoRespositorio.ObterProduto(id);

            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // Carrega a lista de Cliente que envia a alteração (post)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarProduto(int id, [Bind("IdProd, Nome, Descricao, Quantidade, Preco")] Produto produto)
        {
            if (id != produto.IdProd)
            {
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

        // Adicionando a função excluir
        public IActionResult ExcluirProduto(int id)
        {
            _produtoRespositorio.Excluir(id);
            return RedirectToAction(nameof(Index));
        }
    }
}