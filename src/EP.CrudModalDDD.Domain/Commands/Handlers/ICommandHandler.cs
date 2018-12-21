using EP.CrudModalDDD.Domain.Commands.Results;
using EP.CrudModalDDD.Domain.Interfaces;

namespace EP.CrudModalDDD.Domain.Commands.Handlers
{
    public interface ICommandHandler<T> where T : ICommand
    {
        ICommandResult Handle(T command);
    }
}
