using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ControleDeContatos.Controllers
{
    public class UsuarioController : Controller
    {

        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }
        public IActionResult Index()
        {
            List<UsuarioModel> usuario = _usuarioRepositorio.BuscarUsuarios();

            return View(usuario);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id);
            return View(usuario);
        }

        public IActionResult Editar(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id);
            return View(usuario);
        }

        //Metodos para acessar o DB
        [HttpPost]
        public IActionResult Criar(UsuarioModel usuario)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    _usuarioRepositorio.Adicionar(usuario);
                    TempData["MensagemSucesso"] = "Usuário cadastrado com sucesso!🔥";
                    return RedirectToAction("Index");
                }

            }
            catch (System.Exception error)
            {
                TempData["MensagemErro"] = $"Ops 😰, não conseguimos cadastrar o seu usuário!, detalhes do erro: {error.Message}";
                return RedirectToAction("Index");

                throw;
            }

            return View(usuario);

        }

        [HttpPost]
        public IActionResult Alterar(UsuarioSemSenhaModel usuarioSemSenha)
        {

            try
            {

                UsuarioModel usuario = null;
                if (ModelState.IsValid)
                {
                    usuario = new UsuarioModel()
                    {
                        Id = usuarioSemSenha.Id,
                        Name = usuarioSemSenha.Name,
                        Login = usuarioSemSenha.Login,
                        Email = usuarioSemSenha.Email,
                        Perfil = usuarioSemSenha.Perfil
                    };

                    usuario = _usuarioRepositorio.Atualizar(usuario);
                    TempData["MensagemSucesso"] = "Usuario alterado com sucesso!🔥";
                    return RedirectToAction("Index");
                }

                return View("Editar", usuario);

            }
            catch (System.Exception error)
            {
                TempData["MensagemErro"] = $"Ops 😰, não conseguimos atualizar o seu usuario!, detalhes do erro: {error.Message}";
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
                    _usuarioRepositorio.Apagar(id);
                    TempData["MensagemSucesso"] = "Usuario apagado com sucesso!🔥";
                    return RedirectToAction("Index");
                }

            }
            catch (System.Exception error)
            {
                TempData["MensagemErro"] = $"Ops 😰, não conseguimos excluir o seu usuario!, detalhes do erro: {error.Message}";
                return RedirectToAction("Index");

                throw;
            }

            return RedirectToAction("Index");

        }
    }
}
