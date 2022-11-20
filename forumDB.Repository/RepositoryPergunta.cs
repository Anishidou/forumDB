using forumDB.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace forumDB.Repository
{
    public class RepositoryPergunta
    {
        private ForumContext odb;

        public RepositoryPergunta()
        {
            odb = new ForumContext();
        }

        public List<Pergunta> ListarTodos()
        {
            return odb.Pergunta.Include(x => x.IdCursoNavigation).Include(x => x.IdUsuarioNavigation).Include(x => x.Resposta).ToList();
        }

        public Pergunta Selecionar(int id)
        {
            return odb.Pergunta.Where(x => x.Id == id).Include(x => x.IdCursoNavigation).Include(x => x.IdUsuarioNavigation).FirstOrDefault();
        }

        public void Incluir(Pergunta oPergunta)
        {
            odb.Pergunta.Attach(oPergunta);
            odb.Entry(oPergunta).State = EntityState.Added;
            odb.SaveChanges();
        }

        public void Alterar(Pergunta oPergunta)
        {
            odb.Pergunta.Attach(oPergunta);
            odb.Entry(oPergunta).State = EntityState.Modified;
            odb.SaveChanges();
        }

        public void Excluir(int id)
        {
            Pergunta oPergunta = odb.Pergunta.Where(x => x.Id == id).FirstOrDefault();
            odb.Entry(oPergunta).State = EntityState.Deleted;
            odb.SaveChanges();
        }

        public void ExcluirPerguntasPorUsuario(int id)
        {
            List<Pergunta> oLista = odb.Pergunta.Where(x => x.IdUsuario == id).ToList();
            foreach (Pergunta oPergunta in oLista)
            {
                oPergunta.IdUsuario = 1;
                odb.Entry(oPergunta).State = EntityState.Modified;
                odb.SaveChanges();
            }
        }
    }
}
