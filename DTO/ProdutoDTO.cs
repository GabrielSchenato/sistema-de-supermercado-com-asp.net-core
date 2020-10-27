using System.ComponentModel.DataAnnotations;

namespace sonmarket.DTO
{
    public class ProdutoDTO
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome do produto é obrigatório.")]
        [StringLength(100, ErrorMessage = "Nome do produto é muito grande, tente um nome menor!")]
        [MinLength(2, ErrorMessage = "Nome do produto muito pequeno, tente um nome com mais de 2 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Categoria do produto é obrigatório.")]
        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "Fornecedor do produto é obrigatório.")]
        public int FornecedorId { get; set; }

        [Required(ErrorMessage = "Preço de custo do produto é obrigatório.")]
        public float PrecoDeCusto { get; set; }

        [Required(ErrorMessage = "Preço de custo do produto é obrigatório.")]
        public string PrecoDeCustoString { get; set; }

        [Required(ErrorMessage = "Preço de venda do produto é obrigatório.")]
        public float PrecoDeVenda { get; set; }

        [Required(ErrorMessage = "Preço de venda do produto é obrigatório.")]
        public string PrecoDeVendaString { get; set; }

        [Required(ErrorMessage = "Medição do produto é obrigatório.")]
        [Range(0, 2, ErrorMessage = "Medição fora do padrão.")]
        public int Medicao { get; set; }
    }
}