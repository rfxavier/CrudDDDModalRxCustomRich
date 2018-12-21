using EP.CrudModalDDD.Domain.Entities;
using EP.CrudModalDDD.Domain.Interfaces.Repository;
using EP.CrudModalDDD.Infra.Data.Context;
using System.Linq;
using EP.CrudModalDDD.Domain.DTO;
using System;
using Dapper;

namespace EP.CrudModalDDD.Infra.Data.Repository
{
    public class CidadeRepository : Repository<Cidade>, ICidadeRepository
    {
        public CidadeRepository(CrudModalDDDContext context) : base(context)
        {
        }

        public Cidade BuscarPorCodigoIbge(string codigoIbge)
        {
            return Buscar(c => c.CodigoIbge == codigoIbge).FirstOrDefault();
        }

        public Cidade ObterPorNome(string nome)
        {
            return Buscar(c => c.Nome == nome).FirstOrDefault();
        }

        public Paged<Cidade> ObterTodos(string nome, int pageSize, int pageNumber)
        {
            var cn = Db.Database.Connection;

            var sql = @"SELECT * FROM Cidades " +
                      "WHERE (@Nome IS NULL OR Nome LIKE @Nome + '%') " +
                      "ORDER BY [Nome] " +
                      "OFFSET " + pageSize * (pageNumber - 1) + " ROWS " +
                      "FETCH NEXT " + pageSize + " ROWS ONLY " +
                      " " +
                      "SELECT COUNT(Id) FROM Cidades " +
                      "WHERE (@Nome IS NULL OR Nome LIKE @Nome + '%') ";
                     
            var multi = cn.QueryMultiple(sql, new { Nome = nome });
            var cidades = multi.Read<Cidade>();
            var total = multi.Read<int>().FirstOrDefault();

            var pagedList = new Paged<Cidade>()
            {
                List = cidades,
                Count = total
            };

            return pagedList;
        }
    }
}