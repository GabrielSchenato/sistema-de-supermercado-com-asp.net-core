using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sonmarket.Data;
using sonmarket.DTO;

namespace sonmarket.Controllers
{
    [Authorize]
    public class GestaoController : Controller
    {

        private readonly ApplicationDbContext database;
        public GestaoController(ApplicationDbContext database)
        {
            this.database = database;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Categorias()
        {
            var categorias = database.Categorias.Where(categoria => categoria.Status == true).ToList();
            return View(categorias);
        }

        public IActionResult NovaCategoria()
        {
            return View();
        }

        public IActionResult EditarCategoria(int id)
        {
            var categoria = database.Categorias.First(cat => cat.Id == id);
            CategoriaDTO categoriaView = new CategoriaDTO();
            categoriaView.Id = categoria.Id;
            categoriaView.Nome = categoria.Nome;
            return View(categoriaView);
        }

        public IActionResult Fornecedores()
        {
            var fornecedores = database.Fornecedores.Where(fornecedor => fornecedor.Status == true).ToList();
            return View(fornecedores);
        }

        public IActionResult NovoFornecedor()
        {
            return View();
        }

        public IActionResult EditarFornecedor(int id)
        {
            var fornecedor = database.Fornecedores.First(forne => forne.Id == id);
            FornecedorDTO fornecedorView = new FornecedorDTO();
            fornecedorView.Id = fornecedor.Id;
            fornecedorView.Nome = fornecedor.Nome;
            fornecedorView.Email = fornecedor.Email;
            fornecedorView.Telefone = fornecedor.Telefone;
            return View(fornecedorView);
        }


        public IActionResult Produtos()
        {
            var produtos = database.Produtos.Include(p => p.Categoria).Include(p => p.Fornecedor).Where(p => p.Status == true).ToList();
            return View(produtos);
        }

        public IActionResult NovoProduto()
        {
            ViewBag.Categorias = database.Categorias.ToList();
            ViewBag.Fornecedores = database.Fornecedores.ToList();
            return View();
        }


        public IActionResult EditarProduto(int id)
        {
            var produto = database.Produtos.Include(p => p.Categoria).Include(p => p.Fornecedor).First(p => p.Id == id);
            ProdutoDTO produtoView = new ProdutoDTO();
            produtoView.Id = produto.Id;
            produtoView.Nome = produto.Nome;
            produtoView.CategoriaId = produto.Categoria.Id;
            produtoView.FornecedorId = produto.Fornecedor.Id;
            produtoView.PrecoDeCusto = produto.PrecoDeCusto;
            produtoView.PrecoDeVenda = produto.PrecoDeVenda;
            produtoView.Medicao = produto.Medicao;

            ViewBag.Categorias = database.Categorias.ToList();
            ViewBag.Fornecedores = database.Fornecedores.ToList();

            return View(produtoView);
        }

        public IActionResult Promocoes()
        {
            var promocoes = database.Promocoes.Include(p => p.Produto).Where(p => p.Status == true).ToList();
            return View(promocoes);
        }

        public IActionResult NovaPromocao()
        {
            ViewBag.Produtos = database.Produtos.Where(p => p.Status == true).ToList();
            return View();
        }


        public IActionResult EditarPromocao(int id)
        {
            var promocao = database.Promocoes.Include(p => p.Produto).First(p => p.Id == id);
            PromocaoDTO promocaoView = new PromocaoDTO();
            promocaoView.Id = promocao.Id;
            promocaoView.Nome = promocao.Nome;
            promocaoView.ProdutoId = promocao.Produto.Id;
            promocaoView.Porcentagem = promocao.Porcentagem;

            ViewBag.Produtos = database.Produtos.ToList();

            return View(promocaoView);
        }

        public IActionResult Estoque()
        {
            var listaDeEstoque = database.Estoques.Include(p => p.Produto).ToList();
            return View(listaDeEstoque);
        }

        public IActionResult NovoEstoque()
        {
            ViewBag.Produtos = database.Produtos.Where(p => p.Status == true).ToList();
            return View();
        }


        public IActionResult EditarEstoque(int id)
        {
            var estoque = database.Estoques.Include(p => p.Produto).First(p => p.Id == id);

            ViewBag.Produtos = database.Produtos.ToList();

            return View(estoque);
        }

        public IActionResult Vendas()
        {
            var listaDeVendas = database.Vendas.ToList();
            return View(listaDeVendas);
        }

        [HttpPost]
        public IActionResult RelatorioDeVendas()
        {
            return Ok(database.Vendas.ToList());
        }

    }
}