using AgendamentoDeTarefas.Entites;
using AgendamentoDeTarefas.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace AgendamentoDeTarefas.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserManager<MeuUsuario> _userManager;
        public LoginController(UserManager<MeuUsuario> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Logar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logar(LoginModel logarUsuario)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _userManager.FindByNameAsync(logarUsuario.UserName);
                if (usuario != null && await _userManager.CheckPasswordAsync(usuario, logarUsuario.Password))
                {
                    var identity = new ClaimsIdentity("cookies");
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.Id));
                    identity.AddClaim(new Claim(ClaimTypes.Name, usuario.UserName));

                    await HttpContext.SignInAsync("cookies", new ClaimsPrincipal(identity));

                    return RedirectToAction("Index", "Tarefa");
                }
                ModelState.AddModelError("", "Usuário ou senha invalida");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Registrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(RegistrarModel registrarUsuario)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _userManager.FindByNameAsync(registrarUsuario.UserName);
                if (usuario == null)
                {
                    usuario = new MeuUsuario()
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = registrarUsuario.UserName
                    };
                }
                var resultado = await _userManager.CreateAsync(
                    usuario, registrarUsuario.Password);

                return View("Sucesso");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Sucesso()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync("cookies");
            return RedirectToAction(nameof(Logar));
        }
    }

}
