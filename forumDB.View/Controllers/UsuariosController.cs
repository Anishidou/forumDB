using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using forumDB.Model;
using forumDB.Repository;
using System.Buffers.Text;

namespace forumDB.View.Controllers
{
    public class UsuariosController : Controller
    {
        RepositoryUsuario _Repository;
        RepositoryCurso _RepositoryC;
        private string caminhoServidor;
        public UsuariosController(IWebHostEnvironment sistema)
        {
            caminhoServidor = sistema.WebRootPath;
            _Repository = new RepositoryUsuario();
            _RepositoryC = new RepositoryCurso();
        }
        //Get: Usuarios/Login
        //Login
        public IActionResult Login()
        {
            return View();
        }
        // POST: Usuarios/Login
        //Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login([Bind("Email,Senha")] Usuario oUsuario)
        {
            string pepper = System.IO.File.ReadAllText("../pepper.txt");
            string senha = oUsuario.Senha;
            senha += pepper;
            oUsuario = _Repository.SelecionarPorEmail(oUsuario.Email);
            if(oUsuario != null)
            {
                bool alou = BCrypt.Net.BCrypt.Verify(senha, oUsuario.Senha);
                if (alou)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View("Login");
        }
        //Hash Password
        public string HashSenha(string senha)
        {
            string pepper = System.IO.File.ReadAllText("../pepper.txt");
            senha += pepper;
            string senhaHashada = BCrypt.Net.BCrypt.HashPassword(senha);
            bool alou = BCrypt.Net.BCrypt.Verify(senha, senhaHashada);
            Console.WriteLine(alou);
            return senhaHashada;
        }
        //Save Image
        public string SalvarImagem(IFormFile FotoRaw, int id)
        {
            string caminhoParaSalvarImagem = caminhoServidor + "\\Imagem\\";
            string nomeFoto = "Imagem" + id + ".jpeg";
            if (!Directory.Exists(caminhoParaSalvarImagem))
            {
                Directory.CreateDirectory(caminhoParaSalvarImagem);
            }
            if (System.IO.File.Exists(caminhoParaSalvarImagem + nomeFoto))
            {
                System.IO.File.Delete(caminhoParaSalvarImagem + nomeFoto);
            }
            using (var stream = System.IO.File.Create(caminhoParaSalvarImagem + nomeFoto))
            {
                FotoRaw.CopyToAsync(stream);
            }
            return nomeFoto;
        }
        // GET: Usuarios
        public IActionResult Index()
        {
            List<Usuario> Usuarios = _Repository.ListarTodos();
            return View(Usuarios);
        }


        // GET: Usuarios/Details/
        public IActionResult Details(int id)
        {   
            Usuario oUsuario = _Repository.Selecionar(id);
            return View(oUsuario);
        }
        
        // GET: Usuarios/Create
        public IActionResult Create()
        {
            List<String> boolist = new List<String>(); boolist.Add("True"); boolist.Add("False");
            ViewData["Administrador"] = new SelectList(boolist);
            ViewData["NomeCurso"] = new SelectList(_RepositoryC.ListarTodosNomes());
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormFile FotoRaw, [Bind("Matricula,Nome,Email,Senha,NomeCurso,Administrador")] Usuario oUsuario)
        {
            oUsuario.IdCurso = _RepositoryC.SelecionarIdPorNome(oUsuario.NomeCurso);
            oUsuario.Foto = SalvarImagem(FotoRaw, oUsuario.Id);
            oUsuario.Senha = HashSenha(oUsuario.Senha);
            _Repository.Incluir(oUsuario);
            return RedirectToAction(nameof(Index));
        }
        
        // GET: Usuarios/Edit/5
        public IActionResult Edit(int id)
        {
            List<String> boolist = new List<String>(); boolist.Add("True"); boolist.Add("False");
            ViewData["Administrador"] = new SelectList(boolist);
            ViewData["NomeCurso"] = new SelectList(_RepositoryC.ListarTodosNomes());
            _RepositoryC = new RepositoryCurso();
            Usuario oUsuario = _Repository.Selecionar(id);
            return View(oUsuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(IFormFile FotoRaw, [Bind("Id,Matricula,Nome,Email,Senha,NomeCurso,Administrador")] Usuario oUsuario)
        {
            oUsuario.IdCurso = _RepositoryC.SelecionarIdPorNome(oUsuario.NomeCurso);
            oUsuario.Foto = SalvarImagem(FotoRaw, oUsuario.Id);
            oUsuario.Senha = HashSenha(oUsuario.Senha);
            _Repository.Alterar(oUsuario);
            return RedirectToAction(nameof(Index));
        }
        
        // GET: Usuarios/Delete/5
        public IActionResult Delete(int id)
        {   
            Usuario oUsuario = _Repository.Selecionar(id);
            return View(oUsuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _Repository.Excluir(id);
            return RedirectToAction(nameof(Index));
        }
        /*
        private bool UsuarioExists(string id)
        {
          return _context.Usuario.Any(e => e.Matricula == id);
        }*/
    }
}
