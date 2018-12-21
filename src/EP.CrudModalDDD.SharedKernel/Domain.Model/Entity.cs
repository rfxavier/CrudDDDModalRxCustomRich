using System;

namespace SharedKernel.Domain.Model
{
    public abstract class Entity 
    {
        public Guid Id { get; protected set; }
    }
}
