using forumDB.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace forumDB.Repository
{
    public class RepositoryResposta
    {
        private ForumContext odb;

        public RepositoryResposta()
        {
            odb = new ForumContext();
        }

        public List<Resposta> ListarTodos()
        {
            return odb.Resposta.Include(x => x.IdUsuarioNavigation).Include(x => x.IdPerguntaNavigation).ToList();
        }
        public List<Resposta> ListarTodasRespostas(int id)
        {
            return odb.Resposta.Where(x => x.IdPergunta == id).Include(x => x.IdUsuarioNavigation).Include(x => x.IdPerguntaNavigation).ToList();
        }

        public Resposta Selecionar(int id)
        {
            return odb.Resposta.Where(x => x.Id == id).Include(x => x.IdUsuarioNavigation).Include(x => x.IdPerguntaNavigation).FirstOrDefault();
        }

        public void Incluir(Resposta oResposta)
        {
            odb.Resposta.Attach(oResposta);
            odb.Entry(oResposta).State = EntityState.Added;
            odb.SaveChanges();
        }

        public void Alterar(Resposta oResposta)
        {
            odb.Resposta.Attach(oResposta);
            odb.Entry(oResposta).State = EntityState.Modified;
            odb.SaveChanges();
        }

        public void Excluir(int id)
        {
            Resposta oResposta = odb.Resposta.Where(x => x.Id == id).FirstOrDefault();
            odb.Entry(oResposta).State = EntityState.Deleted;
            odb.SaveChanges();
        }
    }
}
