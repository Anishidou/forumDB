using System.Dynamic;
using forumDB.Model;
using forumDB.Model.Extra;
using forumDB.Repository;
using forumDB.View.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace forumDB.View.Controllers
{
    [PaginaParaUsuarioLogado]
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
            string sessaoUsuario = HttpContext.Session.GetString("sessaoUsuarioLogado");

            Usuario oUsuario = JsonConvert.DeserializeObject<Usuario>(sessaoUsuario);

            ViewData["usuarioSessaoId"] = oUsuario.Id;
            ViewData["usuarioSessaoAdm"] = oUsuario.Administrador;
            ViewData["NomeCurso"] = new SelectList(_RepositoryC.ListarTodosNomes());
            List<Pergunta> Perguntas = _Repository.ListarTodos();
            Perguntas.Reverse();
            return View(Perguntas);
        }

        // GET: PerguntasController/Details/5
        public ActionResult Details(int id)
        {
            string sessaoUsuario = HttpContext.Session.GetString("sessaoUsuarioLogado");

            Usuario oUsuario = JsonConvert.DeserializeObject<Usuario>(sessaoUsuario);

            ViewData["usuarioSessaoId"] = oUsuario.Id;
            ViewData["usuarioSessaoAdm"] = oUsuario.Administrador;
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
            string sessaoUsuario = HttpContext.Session.GetString("sessaoUsuarioLogado");

            Usuario oUsuario = JsonConvert.DeserializeObject<Usuario>(sessaoUsuario);

            oPergunta.IdUsuario = oUsuario.Id;
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
            RepositoryDenuncia oRepositoryD = new RepositoryDenuncia();            
            RepositoryResposta oRepositoryR = new RepositoryResposta();
            oRepositoryD.ExcluirDenunciasPorPergunta(id);
            oRepositoryR.ExcluirRespostasPorPergunta(id);

            _Repository.Excluir(id);
            return RedirectToAction(nameof(Index));
        }

        // POST: PerguntasController/Create/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateResposta(int id,[Bind("Texto,IdUsuario")] Resposta oResposta)
        {
            string sessaoUsuario = HttpContext.Session.GetString("sessaoUsuarioLogado");

            Usuario oUsuario = JsonConvert.DeserializeObject<Usuario>(sessaoUsuario);
            oResposta.IdUsuario = oUsuario.Id;
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
            RepositoryDenuncia oRepositoryD = new RepositoryDenuncia();
            oRepositoryD.ExcluirDenunciasPorResposta(id);

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

        // POST: DenunciaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDenuncia(Denuncia oDenuncia)
        {
            RepositoryDenuncia oRepository = new RepositoryDenuncia();
            oRepository.Incluir(oDenuncia);
            return RedirectToAction("Index");
        }
    }
}
