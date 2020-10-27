using System.ComponentModel.DataAnnotations;

namespace sonmarket.DTO
{
    public class PromocaoDTO
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome da promoção é obrigatório.")]
        [StringLength(100, ErrorMessage = "Nome da promoção é muito grande, tente um nome menor!")]
        [MinLength(3, ErrorMessage = "Nome da promoção muito pequeno, tente um nome com mais de 2 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Produto é obrigatório.")]
        public int ProdutoId { get; set; }

        [Required(ErrorMessage = "Porcentagem é obrigatório.")]
        [Range(0, 100)]
        public float Porcentagem { get; set; }
    }
}