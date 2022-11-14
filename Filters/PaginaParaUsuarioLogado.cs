using forumDB.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace forumDB.View.Filters
{
    public class PaginaParaUsuarioLogado : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string sessaoUsuario = context.HttpContext.Session.GetString("sessaoUsuarioLogado");

            if (string.IsNullOrEmpty(sessaoUsuario))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Usuarios" }, { "Usuarios", "Login" } });
            }
            else
            {
                Usuario oUsuario = JsonConvert.DeserializeObject<Usuario>(sessaoUsuario);
                if (oUsuario == null)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Usuarios" }, { "Usuarios", "Login" } });
                }

                base.OnActionExecuting(context);
            }
        }
    }
}
