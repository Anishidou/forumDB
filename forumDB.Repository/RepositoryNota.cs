using forumDB.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace forumDB.Repository
{
    public class RepositoryNota
    {
        private ForumContext odb;

        public RepositoryNota()
        {
            odb = new ForumContext();
        }

        public List<Nota> ListarTodos()
        {
            return odb.Nota.Include(x => x.IdPerguntaNavigation).Include(x => x.IdUsuarioNavigation).Include(x => x.IdRespostaNavigation).ToList();
        }

        public void Incluir(Nota oNota)
        {
            if(oNota.Id == 0)
            {
                odb.Nota.Attach(oNota);
                odb.Entry(oNota).State = EntityState.Added;
                odb.SaveChanges();
            }
            else
            {
                odb.Nota.Attach(oNota);
                odb.Entry(oNota).State = EntityState.Modified;
                odb.SaveChanges();
            }
        }
    }
}
