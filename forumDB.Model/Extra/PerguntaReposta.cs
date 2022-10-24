using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace forumDB.Model.Extra
{
    public partial class PerguntaReposta
    {
        public List<Resposta> Respostas { get; set; }
        public Pergunta Pergunta { get; set; }
    }
}
