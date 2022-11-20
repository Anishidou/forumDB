using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using forumDB.Model;
using forumDB.Repository;
using System.Buffers.Text;
using forumDB.View.Helper;

namespace forumDB.View.Controllers
{
    
    public class ProfileController : Controller
    {
        private readonly ISessao _sessao;

        public ProfileController(ISessao sessao)
        {
            _sessao = sessao;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
