using forumDB.Model;
using forumDB.Repository;
using forumDB.View.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace forumDB.View.Controllers
{
    [PaginaParaUsuarioLogado]
    public class RespostasController : Controller
    {
        RepositoryResposta _Repository;
        RepositoryPergunta _RepositoryP;
        RepositoryUsuario _RepositoryU;
        public RespostasController()
        {
            _Repository = new RepositoryResposta();
            _RepositoryP = new RepositoryPergunta();
            _RepositoryU = new RepositoryUsuario();
        }
        // GET: RespostasController
        public ActionResult Index()
        {
            List<Resposta> Respostas = _Repository.ListarTodos();
            return View(Respostas);
        }

        // GET: RespostasController/Details/5
        public ActionResult Details(int id)
        {
            Resposta oResposta = _Repository.Selecionar(id);
            return View(oResposta);
        }

        // GET: RespostasController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RespostasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Titulo,Texto,IdUsuario,IdPergunta")] Resposta oResposta)
        {
            oResposta.Horario = DateTime.Now;
            _Repository.Incluir(oResposta);
            return RedirectToAction(nameof(Index));
        }

        // GET: RespostasController/Edit/5
        public ActionResult Edit(int id)
        {
            Resposta oResposta = _Repository.Selecionar(id);
            return View(oResposta);
        }

        // POST: RespostasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("Id, Titulo,Texto,IdUsuario,IdPergunta")] Resposta oResposta)
        {
            oResposta.Horario = DateTime.Now;
            _Repository.Alterar(oResposta);
            return RedirectToAction(nameof(Index));
        }

        // GET: RespostasController/Delete/5
        public ActionResult Delete(int id)
        {
            Resposta oResposta = _Repository.Selecionar(id);
            return View(oResposta);
        }

        // POST: RespostasController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _Repository.Excluir(id);
            List<Resposta> Respostas = _Repository.ListarTodos();
            return RedirectToAction(nameof(Index));
        }
    }
}
