using LojaProjeto.Models;

namespace LojaProjeto.Interfaces
{
    public interface IProdutosRepositorio
    {
        IEnumerable<Produto> ListarTodos();
        Produto? ObterPorId(int id);
        void Adicionar(Produto produto);
        void Atualizar(Produto produto);
        void Excluir(int id);

    }
}
