using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using forumDB.Model;

namespace forumDB.View.Views.Usuarios
{
    public class EditModel : PageModel
    {
        private readonly forumDB.Model.ForumContext _context;

        public EditModel(forumDB.Model.ForumContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Usuario Usuario { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.Usuario == null)
            {
                return NotFound();
            }

            var usuario =  await _context.Usuario.FirstOrDefaultAsync(m => m.Matricula == id);
            if (usuario == null)
            {
                return NotFound();
            }
            Usuario = usuario;
           ViewData["IdCurso"] = new SelectList(_context.Curso, "Id", "Nome");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(Usuario.Matricula))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool UsuarioExists(string id)
        {
          return _context.Usuario.Any(e => e.Matricula == id);
        }
    }
}
