using ControleDeContatos.Enums;
using ControleDeContatos.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o nome do usuário!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Digite o login do usuário!")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Digite o e-mail do usuário")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é valido!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Digite uma senha!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Selecione um tipo de perfil para o usuário!")]
        public PerfilEnum? Perfil { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAtualizacao { get; set; }

        public virtual List<ContatoModel> Contatos { get; set; }

        public bool PasswordValid(string password)
        {
            return Password == password.GerarHash();
        }

        public void SetPasswordHash()
        {
            Password = Password.GerarHash();
        }

        public string GerarNovaSenha()
        {
            string novaSenha = Guid.NewGuid().ToString().Substring(0, 8);
            Password = novaSenha.GerarHash();

            return novaSenha;
        }

        public void setNewPassword(string novaSenha)
        {
            Password = novaSenha.GerarHash();
        }
    }
}
