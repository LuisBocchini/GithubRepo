using GitHubRepo.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace GitHubRepo.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> PesquisarUsuarios(string pesquisa)
        {
            var usuarios = await GithubAPI.PesquisarUsuarios(pesquisa);
            return PartialView("_ListaUsuarios", usuarios.Objeto);
        }

        public async Task<IActionResult> ObterUsuario(int id)
        {
            var response = await GithubAPI.ObterUsuario(id);
            if (!response.Sucesso)
            {
                ViewBag.erro = response.Mensagem;
            }
            return PartialView("_DetalhesUsuario", response.Objeto);
        }
    }
}
