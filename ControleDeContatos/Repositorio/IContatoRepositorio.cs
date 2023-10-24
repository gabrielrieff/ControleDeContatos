using ControleDeContatos.Models;
using System.Collections.Generic;
using System.Security.Principal;

namespace ControleDeContatos.Repositorio
{
    public interface IContatoRepositorio
    {
        ContatoModel ListarPorId(int id);

        List<ContatoModel> BuscarContatos(int UsuarioId);

        ContatoModel Adicionar(ContatoModel contato);

        ContatoModel Atualizar(ContatoModel contato);

        bool Apagar(int id);
    }
}
