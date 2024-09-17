using front.Models;
using System.Net.Http;

namespace front.Services.Interface
{
    public interface IUsuarioService
    {
        Task<List<Usuario>> GetUsuarios();
        Task<Usuario> GetUsuario(int id);

        Task CrearUsuario(Usuario usuario);
        Task EliminarUsuario(int id);
    }
}
