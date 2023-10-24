﻿using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models
{
    public class AlterarSenhaModel
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Digite a senha atual!")]
        public string SenhaAtual { get; set; }

        [Required(ErrorMessage = "Digite a nova senha!")]
        public string NovaSenha { get; set; }

        [Required(ErrorMessage = "Confirme a nova senha!")]
        [Compare("NovaSenha", ErrorMessage = "A confirmação de nova senha não é igual a nova senha!")]
        public string ConfirmarNovaSenha { get; set; }
    }
}
