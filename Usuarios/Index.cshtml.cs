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
    public class IndexModel : PageModel
    {
        private readonly forumDB.Model.ForumContext _context;

        public IndexModel(forumDB.Model.ForumContext context)
        {
            _context = context;
        }

        public IList<Usuario> Usuario { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Usuario != null)
            {
                Usuario = await _context.Usuario
                .Include(u => u.IdCursoNavigation).ToListAsync();
            }
        }
    }
}
