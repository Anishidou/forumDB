using forumDB.Model;
using forumDB.Model.Extra;
using forumDB.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace forumDB.View.Controllers
{
    public class PerguntasController : Controller
    {
        RepositoryPergunta _Repository;
        RepositoryResposta _RepositoryR;
        RepositoryCurso _RepositoryC;
        RepositoryUsuario _RepositoryU;
        public PerguntasController()
        {
            _Repository = new RepositoryPergunta();
            _RepositoryR = new RepositoryResposta();
            _RepositoryC = new RepositoryCurso();
            _RepositoryU = new RepositoryUsuario();
        }
        // GET: PerguntasController
        public ActionResult Index()
        {
            List<Pergunta> Perguntas = _Repository.ListarTodos();
            return View(Perguntas);
        }

        // GET: PerguntasController/Details/5
        public ActionResult Details(int id)
        {
            Pergunta oPergunta = _Repository.Selecionar(id);
            List<Resposta> Respostas = _RepositoryR.ListarTodasRespostas(id);
            PerguntaReposta oPResposta = new PerguntaReposta();
            oPResposta.Respostas = Respostas;
            oPResposta.Pergunta = oPergunta;
            return View(oPResposta);
        }

        // GET: PerguntasController/Create
        public ActionResult Create()
        {
            ViewData["NomeCurso"] = new SelectList(_RepositoryC.ListarTodosNomes());
            return View();
        }

        // POST: PerguntasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Titulo,Texto,IdUsuario,NomeCurso")] Pergunta oPergunta)
        {
            oPergunta.Horario = DateTime.Now;
            oPergunta.Respondida = false;
            oPergunta.IdCurso = _RepositoryC.SelecionarIdPorNome(oPergunta.NomeCurso);
            _Repository.Incluir(oPergunta);
            return RedirectToAction(nameof(Index));
        }

        // GET: PerguntasController/Edit/5
        public ActionResult Edit(int id)
        {
            Pergunta oPergunta = _Repository.Selecionar(id);
            ViewData["NomeCurso"] = new SelectList(_RepositoryC.ListarTodosNomes());
            return View(oPergunta);
        }

        // POST: PerguntasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("Id, Titulo,Texto,IdUsuario,NomeCurso")] Pergunta oPergunta)
        {
            oPergunta.Horario = DateTime.Now;
            oPergunta.Respondida = false;
            oPergunta.IdCurso = _RepositoryC.SelecionarIdPorNome(oPergunta.NomeCurso);
            _Repository.Alterar(oPergunta);
            return RedirectToAction(nameof(Index));
        }

        // GET: PerguntasController/Delete/5
        public ActionResult Delete(int id)
        {
            Pergunta oPergunta = _Repository.Selecionar(id);
            return View(oPergunta);
        }

        // POST: PerguntasController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _Repository.Excluir(id);
            return RedirectToAction(nameof(Index));
        }

        // POST: PerguntasController/Create/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateResposta(int id,[Bind("Texto,IdUsuario")] Resposta oResposta)
        {
            oResposta.Horario = DateTime.Now;
            oResposta.IdPergunta = id;
            _RepositoryR.Incluir(oResposta);
            return RedirectToAction("Details", new {id = id });
        }

        public ActionResult DeleteR(int id)
        {
            Resposta oResposta = _RepositoryR.Selecionar(id);
            return View(oResposta);
        }

        // POST: PerguntasController/Delete/5
        [HttpPost, ActionName("DeleteR")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRConfirmed(int id, int idPergunta)
        {
            _RepositoryR.Excluir(id);
            return RedirectToAction("Details", new { id = idPergunta });
        }

        public ActionResult EditR(int id)
        {
            Resposta oResposta = _RepositoryR.Selecionar(id);
            ViewData["NomeCurso"] = new SelectList(_RepositoryC.ListarTodosNomes());
            return View(oResposta);
        }

        // POST: PerguntasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditR([Bind("Id, Titulo,Texto,IdUsuario,IdPergunta")] Resposta oResposta)
        {
            oResposta.Horario = DateTime.Now;
            _RepositoryR.Alterar(oResposta);
            return RedirectToAction(nameof(Details));
        }
    }
}
