using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LojaProjeto.Models;
using LojaProjeto.Repositorio;
using LojaProjeto.Interfaces;


namespace LojaProjeto.Controllers
{
    public class ProdutoController : Controller
    {
        // necessario varieavel privada para evitar confusões

        private readonly IProdutosRepositorio _produtosRepositorio;

        //construtor
        public ProdutoController(IProdutosRepositorio produtosRepositorio)
        {
            _produtosRepositorio = produtosRepositorio;
        }

        //listagem: Utilizando a variavel correta ja instanciada

        public IActionResult Index()
        {
            var produtos = _produtosRepositorio.ListarTodos();
            return View(produtos);
        }

        [HttpGet]
        public IActionResult Criar() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Criar(Produto vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var produto = new Produto
            {
                Nome = vm.Nome,
                Preco = vm.Preco,
            };
            _produtosRepositorio.Adicionar(produto);
            return RedirectToAction(nameof(Index));
        }
        // Método editar post que pega
        [HttpGet]
        public IActionResult Editar(int id)
        {
            var produto = _produtosRepositorio.ObterPorId(id);
            if (produto == null) return NotFound();


            //mapeamento de entidade
            var viewModel = new Produto
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Preco = produto.Preco
            };
            return View(viewModel);

        }
        //Método editar post que envia tropa
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(int id, Produto model)
        {
            if (id != model.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                var produto = new Produto
                {
                    Id = model.Id,
                    Nome = model.Nome,
                    Preco = model.Preco
                };
                _produtosRepositorio.Atualizar(produto);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Excluir(int id)
        {
            _produtosRepositorio.Excluir(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
