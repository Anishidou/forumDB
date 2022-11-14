using forumDB.Model;
using forumDB.Model.Extra;
using forumDB.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace forumDB.View.Helper
{
    public interface ISessao
    {
        void CriarSessaoDoUsuario(Usuario oUsuario);
        void RemoverSessãoDoUsuario();
        Usuario BuscarSessaoDoUsuario();

    }
}
