using System;
using System.Threading.Tasks;
using DevIO.Business.Intefaces;
using DevIO.Business.Intefaces.Repositorios;
using DevIO.Business.Intefaces.Servicos;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;

namespace DevIO.Business.Services
{
    public class InquilinoService : BaseService, IInquilinoService
    {
        private readonly IInquilinoRepository _inquilinoRepository;
        private readonly IUser _user;

        public InquilinoService(IInquilinoRepository inquilinoRepository,
                              INotificador notificador, 
                              IUser user) : base(notificador)
        {
            _inquilinoRepository = inquilinoRepository;
            _user = user;
        }

        public async Task Adicionar(Inquilino inquilino)
        {
            if (!ExecutarValidacao(new InquilinoValidation(), inquilino)) return;

            //var user = _user.GetUserId();

            await _inquilinoRepository.Adicionar(inquilino);
        }

        public async Task Atualizar(Inquilino inquilino)
        {
            if (!ExecutarValidacao(new InquilinoValidation(), inquilino)) return;

            await _inquilinoRepository.Atualizar(inquilino);
        }

        public async Task Remover(Guid id)
        {
            await _inquilinoRepository.Remover(id);
        }

        public void Dispose()
        {
            _inquilinoRepository?.Dispose();
        }
    }
}