using System.ComponentModel.DataAnnotations;

namespace sonmarket.DTO
{
    public class FornecedorDTO
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome do fornecedor é obrigatório.")]
        [StringLength(100, ErrorMessage = "Nome do fornecedor é muito grande, tente um nome menor!")]
        [MinLength(2, ErrorMessage = "Nome do fornecedor muito pequeno, tente um nome com mais de 2 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "E-Mail do fornecedor é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-Mail inválido!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefone do fornecedor é obrigatório.")]
        [Phone(ErrorMessage = "Número de Telefone inválido.")]
        public string Telefone { get; set; }
    }
}