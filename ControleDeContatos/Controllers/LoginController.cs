using ControleDeContatos.Helpers;
using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ControleDeContatos.Controllers
{
    public class LoginController : Controller
    {

        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISession _session;
        private readonly IEmail _email;

        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISession session, IEmail email)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _session = session;
            _email = email;
        }

        public IActionResult Index()
        {

            if(_session.SearchSessionUser() != null) return RedirectToAction("Index", "Home");

            return View();
        }

        public IActionResult Logout()
        {
            _session.RemoveSessionUser();

            return RedirectToAction("Index", "Login");
        }
        
        public IActionResult RedefinirSenha()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Logar(LoginModel loginModel)
        {
            try {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuarioRepositorio.LoginUsuario(loginModel.Login);

                    if (usuario != null && usuario.PasswordValid(loginModel.Password))
                    {
                        _session.CreatedSessionUser(usuario);
                        return RedirectToAction("Index", "Home");
                    }

                    TempData["MensagemErro"] = $"Senha e/ou usuário inválido(s)!";

                }

                return View("Index");

            } catch (Exception error) {

                TempData["MensagemErro"] = $"Ops, não foi possivel fazer o login, detalhes do erro: {error.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult LinkRedefinirSenha(RedefinirSenhaModel redefinirSenhaModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorEmailLogin(redefinirSenhaModel.Email, redefinirSenhaModel.Login);

                    if (usuario != null)
                    {
                        string novaSenha = usuario.GerarNovaSenha();

                        string mensagem = $"Sua nova senha é: {novaSenha}";

                        bool emailEnviado = _email.Enviar(usuario.Email, "Sistema de contatos - Nova senha", mensagem);

                        if (emailEnviado)
                        { 
                            _usuarioRepositorio.Atualizar(usuario); 
                            TempData["MensagemSucesso"] = $"Enviamos para seu e-mail, uma nova senha!";
                        }
                        else
                        {
                            TempData["MensagemErro"] = $"Não conseguimos enviar o e-mail, tente novamente";

                        }

                        return RedirectToAction("Index", "Login");
                    }

                    TempData["MensagemErro"] = $"Não conseguimos redefinir sua senha, verifique os dados informados!";

                }

                return View("Index");

            }
            catch (Exception error)
            {

                TempData["MensagemErro"] = $"Ops, não foi possivel redefinrsua senha, detalhes do erro: {error.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
