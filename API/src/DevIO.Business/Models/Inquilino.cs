using System;
using System.Collections.Generic;
using System.Text;

namespace DevIO.Business.Models
{
    public class Inquilino : Entity
    {
        public Guid UsuarioId { get; set; }
        public string Nome { get; set; }
        public DateTime DataDeNascimento { get; set; }
        public string Cpf { get; set; }
        public string NomeDaEmpresaOndeTrabalha { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }

    }
}
