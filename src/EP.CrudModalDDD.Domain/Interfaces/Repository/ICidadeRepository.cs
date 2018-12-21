using EP.CrudModalDDD.Domain.DTO;
using EP.CrudModalDDD.Domain.Entities;

namespace EP.CrudModalDDD.Domain.Interfaces.Repository
{
    public interface ICidadeRepository : IRepository<Cidade>
    {
        Cidade BuscarPorCodigoIbge(string codigoIbge);

        Cidade ObterPorNome(string nome);
        Paged<Cidade> ObterTodos(string nome, int pageSize, int pageNumber);

    }
}