using GitHubRepo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using GitHubRepo.Servicos;

namespace GitHubRepo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> PesquisarRepositorios(string pesquisa)
        {
            var repositorios = await GithubAPI.PesquisarRepositorios(pesquisa);           
            return PartialView("_ListaRepositorios", repositorios.Objeto);
        }

        public async Task<IActionResult> ObterRepositorio(int id)
        {
            var response = await GithubAPI.ObterRepositorio(id);
            if (!response.Sucesso)
            {
                ViewBag.erro = response.Mensagem;
            }
            return PartialView("_DetalhesRepositorio", response.Objeto);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}