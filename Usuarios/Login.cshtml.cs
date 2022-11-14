using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using forumDB.Model;
using forumDB.Repository;
using NuGet.Protocol.Core.Types;

namespace forumDB.View.Views.Usuarios
{
    public class LoginModel : PageModel
    {
        private readonly forumDB.Model.ForumContext _context;
        public LoginModel(forumDB.Model.ForumContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["IdCurso"] = new SelectList(_context.Curso, "Id", "Nome");
            return Page();
        }

        public IActionResult OnChange()
        {
            return Page();
        }


        [BindProperty]
        public Usuario Usuario { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Usuario.Add(Usuario);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

    }
}
