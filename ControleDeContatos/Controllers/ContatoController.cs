using ControleDeContatos.Helpers;
using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ControleDeContatos.Controllers
{
    public class ContatoController : Controller
    {
        private readonly IContatoRepositorio _contatoRepositorio;
        private readonly ISession _session;

        public ContatoController(IContatoRepositorio contatoRepositorio, ISession session)
        {
            _contatoRepositorio = contatoRepositorio;
            _session = session;
        }
        //Views Controller
        public IActionResult Index()
        {
            UsuarioModel usuarioLogado = _session.SearchSessionUser();
            List<ContatoModel> contatos = _contatoRepositorio.BuscarContatos(usuarioLogado.Id);

            return View(contatos);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar(int id)
        {
            ContatoModel contato= _contatoRepositorio.ListarPorId(id);
            return View(contato); 
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            ContatoModel contato = _contatoRepositorio.ListarPorId(id);
            return View(contato);
        }

        //Metodos para acessar o DB
        [HttpPost]
        public IActionResult Criar(ContatoModel contato)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuarioLogado = _session.SearchSessionUser();

                    contato.UsuarioId = usuarioLogado.Id;
                    contato = _contatoRepositorio.Adicionar(contato);

                    TempData["MensagemSucesso"] = "Contato cadastrado com sucesso!🔥";
                    return RedirectToAction("Index");
                }

                return View(contato);

            }
            catch (System.Exception error)
            {
                TempData["MensagemErro"] = $"Ops 😰, não conseguimos cadastrar o seu contato!, detalhes do erro: {error.Message}";
                return RedirectToAction("Index");

                throw;
            }
        }

        [HttpPost]
        public IActionResult Alterar(ContatoModel contato)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuarioLogado = _session.SearchSessionUser();
                    contato.UsuarioId = usuarioLogado.Id;

                    contato = _contatoRepositorio.Atualizar(contato);
                    TempData["MensagemSucesso"] = "Contato alterado com sucesso!🔥";
                    return RedirectToAction("Index");
                }

                return View("Editar", contato);

            }
            catch (System.Exception error)
            {
                TempData["MensagemErro"] = $"Ops 😰, não conseguimos atualizar o seu contato!, detalhes do erro: {error.Message}";
                return RedirectToAction("Index");

                throw;
            }

        }

        [HttpGet]
        public IActionResult Apagar(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _contatoRepositorio.Apagar(id);
                    TempData["MensagemSucesso"] = "Contato apagado com sucesso!🔥";
                    return RedirectToAction("Index");
                }

            }
            catch (System.Exception error)
            {
                TempData["MensagemErro"] = $"Ops 😰, não conseguimos excluir o seu contato!, detalhes do erro: {error.Message}";
                return RedirectToAction("Index");

                throw;
            }

            return RedirectToAction("Index");

        }
    }
}
