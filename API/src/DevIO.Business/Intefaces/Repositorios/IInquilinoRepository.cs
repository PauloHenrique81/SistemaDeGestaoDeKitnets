using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevIO.Business.Models;

namespace DevIO.Business.Intefaces.Repositorios
{
    public interface IInquilinoRepository : IRepository<Inquilino>
    {
        Task<IEnumerable<Inquilino>> ObterInquilinosPorUsuario(Guid usuarioId);
        Task<IEnumerable<Inquilino>> ObterInquilinosPorCondominio();
    }
}