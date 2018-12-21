using System;
using System.ComponentModel.DataAnnotations;

namespace EP.CrudModalDDD.Application.ViewModels
{
    public class CidadeViewModel
    {
        public CidadeViewModel()
        {
            Id = Guid.NewGuid();

        }
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Preencha o campo Nome")]
        public string Nome { get; set; }
        public string UF { get; set; }
        public string CodigoIbge { get; set; }
    }
}