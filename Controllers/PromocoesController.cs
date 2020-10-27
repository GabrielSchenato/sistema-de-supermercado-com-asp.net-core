using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using sonmarket.Data;
using sonmarket.DTO;
using sonmarket.Models;

namespace sonmarket.Controllers
{
    public class PromocoesController : Controller
    {
        private readonly ApplicationDbContext database;
        public PromocoesController(ApplicationDbContext database)
        {
            this.database = database;
        }
        [HttpPost]
        public IActionResult Salvar(PromocaoDTO promocaoTemporaria)
        {
            if (ModelState.IsValid)
            {
                Promocao promocao = new Promocao();
                promocao.Nome = promocaoTemporaria.Nome;
                promocao.Produto = database.Produtos.First(produto => produto.Id == promocaoTemporaria.ProdutoId);
                promocao.Porcentagem = promocaoTemporaria.Porcentagem;
                promocao.Status = true;
                database.Promocoes.Add(promocao);
                database.SaveChanges();

                return RedirectToAction("Promocoes", "Gestao");
            }
            else
            {
                ViewBag.Produtos = database.Produtos.ToList();
                return View("../Gestao/NovaPromocao");
            }
        }

        [HttpPost]
        public IActionResult Atualizar(PromocaoDTO promocaoTemporaria)
        {
            if (ModelState.IsValid)
            {
                var promocao = database.Promocoes.First(p => p.Id == promocaoTemporaria.Id);
                promocao.Nome = promocaoTemporaria.Nome;
                promocao.Produto = database.Produtos.First(produto => produto.Id == promocaoTemporaria.ProdutoId);
                promocao.Porcentagem = promocaoTemporaria.Porcentagem;

                database.SaveChanges();

                return RedirectToAction("Promocoes", "Gestao");
            }
            else
            {
                ViewBag.Produtos = database.Produtos.ToList();
                return View("../Gestao/EditarPromocao");
            }
        }


        [HttpPost]
        public IActionResult Deletar(int id)
        {
            if (id > 0)
            {
                var promocao = database.Promocoes.First(p => p.Id == id);
                promocao.Status = false;
                database.SaveChanges();
            }
            return RedirectToAction("Promocoes", "Gestao");
        }
    }
}