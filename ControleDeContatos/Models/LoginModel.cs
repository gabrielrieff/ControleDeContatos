﻿using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Digite o Login!")]
        public string Login {  get; set; }

        [Required(ErrorMessage = "Digite a senha!")]
        public string Password { get; set; }
    }
}