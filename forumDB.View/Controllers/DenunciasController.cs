using forumDB.Model;
using forumDB.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace forumDB.View.Controllers
{
    public class DenunciasController : Controller
    {
        // GET: DenunciaController
        public ActionResult Index()
        {
            RepositoryDenuncia oRepository = new RepositoryDenuncia();
            List<Denuncia> oLista = oRepository.ListarTodos();
            return View(oLista);
        }

        // GET: DenunciaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DenunciaController/Create
        public ActionResult CreatePergunta(int id)
        {
            string sessaoUsuario = HttpContext.Session.GetString("sessaoUsuarioLogado");

            Usuario oUsuario = JsonConvert.DeserializeObject<Usuario>(sessaoUsuario);

            ViewData["usuarioSessaoId"] = oUsuario.Id;
            ViewData["idPergunta"] = id;
            ViewData["idResposta"] = null;
            return View("Create");
        }

        // GET: DenunciaController/Create
        public ActionResult CreateResposta(int id)
        {
            string sessaoUsuario = HttpContext.Session.GetString("sessaoUsuarioLogado");

            Usuario oUsuario = JsonConvert.DeserializeObject<Usuario>(sessaoUsuario);

            ViewData["usuarioSessaoId"] = oUsuario.Id;
            ViewData["idResposta"] = id;
            ViewData["idPergunta"] = null;
            return View("Create");
        }

        // POST: DenunciaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDenuncia(Denuncia oDenuncia)
        {
            RepositoryDenuncia oRepository = new RepositoryDenuncia();
            oRepository.Incluir(oDenuncia);
            return RedirectToAction("Index", "../Controllers/PerguntasController", null);
        }

        // GET: DenunciaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DenunciaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DenunciaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DenunciaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
