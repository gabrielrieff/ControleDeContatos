using ControleDeContatos.Data;
using ControleDeContatos.Models;
using System.Collections.Generic;
using System.Linq;

namespace ControleDeContatos.Repositorio
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        private readonly DataContext _dataContext;

        public ContatoRepositorio(DataContext dataContext) 
        {
            _dataContext = dataContext;
        }

        public ContatoModel Adicionar(ContatoModel contato)
        {
            _dataContext.Contatos.Add(contato);
            _dataContext.SaveChanges();
            return contato;
        }

        public bool Apagar(int id)
        {
            ContatoModel contatoDb = ListarPorId(id);

            if (contatoDb == null)
            {
                throw new System.Exception("Houve um erro na exclusão do contato!");
            }

            _dataContext.Contatos.Remove(contatoDb);
            _dataContext.SaveChanges();

            return true;
        }

        public ContatoModel Atualizar(ContatoModel contato)
        {
            ContatoModel contatoDb = ListarPorId(contato.Id);

            if(contatoDb == null)
            {
                throw new System.Exception("Houve um erro ao atualizar o contato!");
            }

            contatoDb.Name = contato.Name;
            contatoDb.Email = contato.Email;
            contatoDb.Fone = contato.Fone;

            _dataContext.Contatos.Update(contatoDb);
            _dataContext.SaveChanges();

            return contatoDb;
        }

        public List<ContatoModel> BuscarContatos(int UsuarioId)
        {
            return _dataContext.Contatos.Where(x => x.UsuarioId == UsuarioId).ToList();
        }

        public ContatoModel ListarPorId(int id)
        {
            return _dataContext.Contatos.FirstOrDefault(x => x.Id == id);
        }
    }
}
