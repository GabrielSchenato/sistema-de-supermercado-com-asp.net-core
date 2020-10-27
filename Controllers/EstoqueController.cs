using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using sonmarket.Data;
using sonmarket.DTO;
using sonmarket.Models;

namespace sonmarket.Controllers
{
    public class EstoqueController : Controller
    {
        private readonly ApplicationDbContext database;
        public EstoqueController(ApplicationDbContext database)
        {
            this.database = database;
        }
        [HttpPost]
        public IActionResult Salvar(Estoque estoqueTemp)
        {
            database.Estoques.Add(estoqueTemp);
            database.SaveChanges();
            return RedirectToAction("Estoque", "Gestao");
        }

        [HttpPost]
        public IActionResult Atualizar(Estoque estoqueTemp)
        {
            var estoque = database.Estoques.First(estoque => estoque.Id == estoqueTemp.Id);
            estoque.ProdutoId = estoqueTemp.ProdutoId;
            estoque.Quantidade = estoqueTemp.Quantidade;
            database.Estoques.Update(estoque);
            database.SaveChanges();
            return RedirectToAction("Estoque", "Gestao");
        }


        [HttpPost]
        public IActionResult Deletar(int id)
        {
            if (id > 0)
            {
                var estoque = database.Estoques.First(p => p.Id == id);
                database.Estoques.Remove(estoque);
                database.SaveChanges();
            }
            return RedirectToAction("Estoque", "Gestao");
        }
    }
}