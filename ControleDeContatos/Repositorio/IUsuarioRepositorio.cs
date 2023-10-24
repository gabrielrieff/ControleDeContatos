using ControleDeContatos.Models;
using System.Collections.Generic;
using System.Security.Principal;

namespace ControleDeContatos.Repositorio
{
    public interface IUsuarioRepositorio
    {
        UsuarioModel ListarPorId(int id);

        UsuarioModel BuscarPorEmailLogin(string email, string login);

        List<UsuarioModel> BuscarUsuarios();

        UsuarioModel Adicionar(UsuarioModel usuario);

        UsuarioModel Atualizar(UsuarioModel usuario);

        UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenhaModel);

        bool Apagar(int id);

        UsuarioModel LoginUsuario(string login);
    }
}
