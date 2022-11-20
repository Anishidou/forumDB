using forumDB.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using NuGet.Protocol.Core.Types;

namespace forumDB.View.ViewComponents
{
    public class NotasResposta : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int id, bool pergunta)
        {
            ViewData["idResposta"] = id;
            Model.Nota mdl = new Model.Nota();
            return View(mdl);
        }
    }
}
