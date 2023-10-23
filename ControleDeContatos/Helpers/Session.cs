using ControleDeContatos.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ControleDeContatos.Helpers
{
    public class Session : ISession
    {

        private readonly IHttpContextAccessor _contextAccessor;

        public Session(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public void CreatedSessionUser(UsuarioModel usuario)
        {
            string valor = JsonConvert.SerializeObject(usuario);
            _contextAccessor.HttpContext.Session.SetString("SessionUserLogged", valor);
        }

        public void RemoveSessionUser()
        {
            _contextAccessor.HttpContext.Session.Remove("SessionUserLogged");
        }

        public UsuarioModel SearchSessionUser()
        {
            string sessionUser = _contextAccessor.HttpContext.Session.GetString("SessionUserLogged");

            if (string.IsNullOrEmpty(sessionUser)) return null;

            return JsonConvert.DeserializeObject<UsuarioModel>(sessionUser);
        }
    }
}
