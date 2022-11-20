using forumDB.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using NuGet.Protocol.Core.Types;

namespace forumDB.View.ViewComponents
{
    public class NotasPergunta : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            ViewData["idPergunta"] = id;
            Model.Nota mdl = new Model.Nota();
            return View(mdl);
        }
    }
}
