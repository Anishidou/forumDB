using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using forumDB.Model;
using Microsoft.EntityFrameworkCore;

namespace forumDB.Repository
{
    public class RepositoryUsuario
    {
        private ForumContext odb;

        public RepositoryUsuario()
        {
            odb = new ForumContext();
        }

        public List<Usuario> ListarTodos()
        {
            return odb.Usuario.Include(x => x.IdCursoNavigation).ToList();
        }

        public Usuario Selecionar(int id)
        {
            return odb.Usuario.Where(x => x.Id == id).Include(x => x.IdCursoNavigation).FirstOrDefault();
        }
        public Usuario SelecionarPorEmail(string email)
        {
            return odb.Usuario.Where(x => x.Email == email).FirstOrDefault();
        }

        public void Incluir(Usuario oUsuario)
        {
            odb.Usuario.Attach(oUsuario);
            odb.Entry(oUsuario).State = EntityState.Added;
            odb.SaveChanges();
        }

        public void Alterar(Usuario oUsuario)
        {
            odb.Usuario.Attach(oUsuario);
            odb.Entry(oUsuario).State = EntityState.Modified;
            odb.SaveChanges();
        }

        public void Excluir(int id)
        {
            Usuario oUsUario = odb.Usuario.Where(x => x.Id == id).FirstOrDefault();
            odb.Entry(oUsUario).State = EntityState.Deleted;
            odb.SaveChanges();
        }
    }
}
