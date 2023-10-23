using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models
{
    public class ContatoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o nome do contato!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Digite o e-mail do contato!")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é valido!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Digite o número do contato!")]
        [Phone(ErrorMessage = "O número informado não é valido!")]
        public string Fone { get; set; }
    }
}
