using EP.CrudModalDDD.Application.ViewModels;
using System;
using System.Collections.Generic;

namespace EP.CrudModalDDD.Application.Interfaces
{
    public interface ICidadeAppService: IDisposable
    {
        CidadeViewModel Adicionar(CidadeViewModel cidadeViewModel);
        CidadeViewModel ObterPorId(Guid id);
        //IEnumerable<CidadeViewModel> ObterTodos();
        PagedViewModel<CidadeViewModel> ObterTodos(string nome, int pageSize, int pageNumber);

        CidadeViewModel Atualizar(CidadeViewModel cidadeViewModel);
        void Remover(Guid id);
    }
}