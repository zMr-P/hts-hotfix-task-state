using AgendamentoDeTarefas.Context;
using AgendamentoDeTarefas.Entites;
using AgendamentoDeTarefas.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace AgendamentoDeTarefas.Controllers
{
    public class TarefaController : Controller
    {
        private readonly OrganizadorContext _context;
        private readonly UserManager<MeuUsuario> _userManager;

        public TarefaController(OrganizadorContext context, UserManager<MeuUsuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> ObterTodos()
        {
            var usuario = HttpContext.User.Identity;
            if (usuario.IsAuthenticated)
            {
                var usuarioBanco = await _userManager.FindByNameAsync(usuario.Name);
                var tarefas = _context.Tarefas.ToList().FindAll(
                    x => x.IdUsuario == usuarioBanco.Id).OrderByDescending(
                    x=> x.Status == StatusTarefa.Pendente);
                
                return View(tarefas);
            }
            return NotFound();
        }
   
        public async Task<IActionResult> CriarTarefa()
        {
           if(ModelState.IsValid)
            {
                return View();
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CriarTarefa(Tarefa tarefa)
        {
            var usuario = HttpContext.User.Identity;
            if (usuario.IsAuthenticated)
            {
                var usuarioBanco = await _userManager.FindByNameAsync(usuario.Name);
                tarefa.IdUsuario = usuarioBanco.Id;
                tarefa.Status = 0;

                _context.Tarefas.Add(tarefa);
                _context.SaveChanges();
                return RedirectToAction(nameof(ObterTodos));
            }
            return BadRequest();
        }

        public IActionResult EditarTarefa(int id)
        {
            var tarefa = _context.Tarefas.Find(id);
            if (tarefa == null)
            {
                return NotFound();
            }
            return View(tarefa);
        }

        [HttpPost]
        public IActionResult EditarTarefa(Tarefa tarefa)
        {
            var tarefaBanco = _context.Tarefas.Find(tarefa.Id);

            tarefaBanco.Titulo = tarefa.Titulo;
            tarefaBanco.Descricao = tarefa.Descricao;
            tarefaBanco.Data = tarefa.Data;
            tarefaBanco.Status = tarefa.Status;

            _context.Tarefas.Update(tarefaBanco);
            _context.SaveChanges();

            return RedirectToAction(nameof(ObterTodos));
        }

        public IActionResult Detalhes(int id)
        {
            var tarefa = _context.Tarefas.Find(id);

            if (tarefa == null)
            {
                return NotFound();
            }
            return View(tarefa);
        }

        public IActionResult Deletar(int id)
        {
            var tarefa = _context.Tarefas.Find(id);

            if (tarefa == null)
            {
                return NotFound();
            }
            return View(tarefa);
        }

        [HttpPost]
        public IActionResult Deletar(Tarefa tarefa)
        {
            var tarefaBanco = _context.Tarefas.Find(tarefa.Id);

            _context.Remove(tarefaBanco);
            _context.SaveChanges();

            return RedirectToAction(nameof(ObterTodos));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
