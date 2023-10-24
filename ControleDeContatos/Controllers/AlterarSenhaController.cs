using ControleDeContatos.Helpers;
using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ControleDeContatos.Controllers
{
    public class AlterarSenhaController : Controller
    {

        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISession _session;


        public AlterarSenhaController(IUsuarioRepositorio usuarioRepositorio, ISession session)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _session = session;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Alterar(AlterarSenhaModel alterarSenhaModel)
        {
            try
            {

                UsuarioModel usuarioLogado = _session.SearchSessionUser();
                alterarSenhaModel.Id = usuarioLogado.Id;

                if (ModelState.IsValid)
                {

                    _usuarioRepositorio.AlterarSenha(alterarSenhaModel);
                    TempData["MensagemSucesso"] = "Senha alterada com sucesso!🔥";
                    return View("Index", alterarSenhaModel);
                }

                return View("Index", alterarSenhaModel);

            }
            catch(Exception error)
            {
                TempData["MensagemErro"] = $"Ops 😰, não conseguimos alterar sua senha!, detalhes do erro: {error.Message}";
                return View("Index", alterarSenhaModel);

            }
        }
    }
}
