using AgendamentoDeTarefas.Models;
using System.Net.Http.Json;

namespace HotfixTaskState.Tests.Integrations.Controllers
{
    public class LoginControllerTest
    {
        private readonly HttpClient _client;
        public LoginControllerTest()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5275/")
            };
        }

        [Fact]
        public async Task LogarInformandoUsuarioExistenteEDeveRetornarSucesso()
        {
            var login = new LoginModel
            {
                UserName = "paulaodamassa",
                Password = "@Pauxz-_jpa457"
            };

            var response = await _client.PostAsJsonAsync("Login/Logar", login);

            Assert.True(response.IsSuccessStatusCode);
        }
    }
}
