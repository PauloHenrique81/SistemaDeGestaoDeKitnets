using System;
using System.Threading.Tasks;
using DevIO.Business.Models;

namespace DevIO.Business.Intefaces.Servicos
{
    public interface IInquilinoService : IDisposable
    {
        Task Adicionar(Inquilino inquilino);
        Task Atualizar(Inquilino inquilino);
        Task Remover(Guid id);
    }
}