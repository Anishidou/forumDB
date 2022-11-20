using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using forumDB.Model;

namespace forumDB.View.Views.Usuarios
{
    public class DetailsModel : PageModel
    {
        private readonly forumDB.Model.ForumContext _context;

        public DetailsModel(forumDB.Model.ForumContext context)
        {
            _context = context;
        }

      public Usuario Usuario { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.Usuario == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario.FirstOrDefaultAsync(m => m.Matricula == id);
            if (usuario == null)
            {
                return NotFound();
            }
            else 
            {
                Usuario = usuario;
            }
            return Page();
        }
    }
}
