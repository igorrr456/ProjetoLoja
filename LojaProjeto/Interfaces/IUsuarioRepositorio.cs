using LojaProjeto.Models;

namespace LojaProjeto.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Usuario? Validar(string email, string senha);
    }
}
