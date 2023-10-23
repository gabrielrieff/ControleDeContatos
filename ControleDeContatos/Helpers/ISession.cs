using ControleDeContatos.Models;

namespace ControleDeContatos.Helpers
{
    public interface ISession
    {
        void CreatedSessionUser(UsuarioModel usuario);
        void RemoveSessionUser();

        UsuarioModel SearchSessionUser();
    }
}
