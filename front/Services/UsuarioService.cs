using front.Models;
using front.Services.Interface;

namespace front.Services
{
    public class UsuarioService : IUsuarioService
    {
        private string urlBase = "https://localhost:7014/api/";
        private readonly HttpClient _httpClient;
        public UsuarioService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CrearUsuario(Usuario usuario)
        {
            await _httpClient.PostAsJsonAsync($"{urlBase}Usuarios", usuario);
        }

        public async Task EliminarUsuario(int id)
        {
            await _httpClient.DeleteAsync($"{urlBase}Usuarios/{id}");
        }

        public async Task<Usuario> GetUsuario(int id)
        {
            return await _httpClient.GetFromJsonAsync<Usuario>($"{urlBase}Usuarios/{id}") ?? new ();
        }

        public async Task<List<Usuario>> GetUsuarios()
        {
            return await _httpClient.GetFromJsonAsync<List<Usuario>>($"{urlBase}Usuarios") ?? new();
        }
    }
}
