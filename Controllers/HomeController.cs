using forumDB.Models;
using forumDB.View.Filters;
using forumDB.View.Helper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace forumDB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISessao _sessao;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ISessao sessao)
        {
            _logger = logger;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            if (_sessao.BuscarSessaoDoUsuario() == null)
            {
                return RedirectToAction("Login", "Usuarios", new { area = "" });
            }
            else
            {
                return View();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}