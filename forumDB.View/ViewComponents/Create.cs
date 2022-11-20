using forumDB.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using NuGet.Protocol.Core.Types;

namespace forumDB.View.ViewComponents
{
    public class Create : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(SelectList cursos)
        {
            Model.Pergunta mdl = new Model.Pergunta();
            ViewData["NomeCurso"] = cursos;
            return View(mdl);
        }
    }
}
