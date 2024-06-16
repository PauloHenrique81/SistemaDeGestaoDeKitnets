using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevIO.Business.Intefaces.Repositorios;
using DevIO.Business.Models;
using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevIO.Data.Repository
{
    public class InquilinoRepository : Repository<Inquilino>, IInquilinoRepository
    {
        public InquilinoRepository(MeuDbContext context) : base(context) { }

        public Task<IEnumerable<Inquilino>> ObterInquilinosPorCondominio()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Inquilino>> ObterInquilinosPorUsuario(Guid usuarioId)
        {
            throw new NotImplementedException();
        }
    }
}