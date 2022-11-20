using forumDB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace forumDB.Repository
{
    public class RepositoryCurso
    {
        private ForumContext odb;

        public RepositoryCurso()
        {
            odb = new ForumContext();
        }

        public List<Curso> ListarTodos()
        {
            return odb.Curso.ToList();
        }

        public List<String> ListarTodosNomes()
        {
            return odb.Curso.Select(x => x.Nome).ToList();
        }

        public int SelecionarIdPorNome(string nome)
        {
            return odb.Curso.Where(x => x.Nome == nome).Select(x => x.Id).FirstOrDefault();
        }
    }
}
