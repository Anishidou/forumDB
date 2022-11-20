using forumDB.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace forumDB.Repository
{
    public class RepositoryDenuncia
    {
        private ForumContext odb;

        public RepositoryDenuncia()
        {
            odb = new ForumContext();
        }

        public List<Denuncia> ListarTodos()
        {
            return odb.Denuncia.Include(x => x.IdPerguntaNavigation).Include(x => x.IdUsuarioNavigation).Include(x => x.IdRespostaNavigation).ToList();
        }

        public void Incluir(Denuncia oDenuncia)
        {
            odb.Denuncia.Attach(oDenuncia);
            odb.Entry(oDenuncia).State = EntityState.Added;
            odb.SaveChanges();
        }

        public void ExcluirDenunciasPorUsuario(int id)
        {
            foreach(Denuncia oDenuncia in odb.Denuncia.Where(x => x.IdUsuario == id).ToList())
            {
                odb.Entry(oDenuncia).State = EntityState.Deleted;
                odb.SaveChanges();
            }
        }

        public void ExcluirDenunciasPorPergunta(int id)
        {
            foreach (Denuncia oDenuncia in odb.Denuncia.Where(x => x.IdUsuario == id).ToList())
            {
                odb.Entry(oDenuncia).State = EntityState.Deleted;
                odb.SaveChanges();
            }
        }

        public void ExcluirDenunciasPorResposta(int id)
        {
            foreach (Denuncia oDenuncia in odb.Denuncia.Where(x => x.IdResposta == id).ToList())
            {
                odb.Entry(oDenuncia).State = EntityState.Deleted;
                odb.SaveChanges();
            }
        }
    }
}
