using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using DevIO.Api.Controllers;
using DevIO.Api.Extensions;
using DevIO.Api.ViewModels;
using DevIO.Business.Intefaces;
using DevIO.Business.Intefaces.Repositorios;
using DevIO.Business.Intefaces.Servicos;
using DevIO.Business.Models;
using DevIO.Business.Services;
using DevIO.Data.Repository;
using k8s.KubeConfigModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevIO.Api.V1.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/inquilinos")]
    public class InquilinosController : MainController
    {
        private readonly IInquilinoRepository _inquilinoRepository;
        private readonly IInquilinoService _inquilinoService;
        private readonly IMapper _mapper;

        public InquilinosController(INotificador notificador,
                                  IInquilinoRepository inquilinoRepository,
                                  IInquilinoService inquilinoService, 
                                  IMapper mapper,
                                  IUser user) : base(notificador, user)
        {
            _inquilinoRepository = inquilinoRepository;
            _inquilinoService = inquilinoService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<InquilinoViewModel>> ObterTodos()
        {
           return _mapper.Map<IEnumerable<InquilinoViewModel>>(await _inquilinoRepository.ObterTodos());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<InquilinoViewModel>> ObterPorId(Guid id)
        {
            var inquilinoViewModel = await ObterInquilino(id);

            if (inquilinoViewModel == null) return NotFound();

            return inquilinoViewModel;
        }

        //[ClaimsAuthorize("Inquilino", "Adicionar")]
        [HttpPost]
        public async Task<ActionResult<InquilinoViewModel>> Adicionar(InquilinoViewModel inquilinoViewModel)
        {
            inquilinoViewModel.UsuarioId = UsuarioId;

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _inquilinoService.Adicionar(_mapper.Map<Inquilino>(inquilinoViewModel));

            return CustomResponse(inquilinoViewModel);
        }

        //[ClaimsAuthorize("Inquilino", "Atualizar")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(Guid id, InquilinoViewModel inquilinoViewModel)
        {
            if (id != inquilinoViewModel.Id)
            {
                NotificarErro("Os ids informados não são iguais!");
                return CustomResponse();
            }

            var inquilinoAtualizacao = await ObterInquilino(id);
            if (!ModelState.IsValid) return CustomResponse(ModelState);


            inquilinoAtualizacao.UsuarioId = inquilinoViewModel.UsuarioId;
            inquilinoAtualizacao.Nome = inquilinoViewModel.Nome;
            inquilinoAtualizacao.Cpf = inquilinoViewModel.Cpf;
            inquilinoAtualizacao.DataDeNascimento = inquilinoViewModel.DataDeNascimento;
            inquilinoAtualizacao.NomeDaEmpresaOndeTrabalha = inquilinoViewModel.NomeDaEmpresaOndeTrabalha;
            inquilinoAtualizacao.Telefone = inquilinoViewModel.Telefone;
            inquilinoAtualizacao.Email = inquilinoViewModel.Email;
            inquilinoAtualizacao.Status = inquilinoViewModel.Status;

            await _inquilinoService.Atualizar(_mapper.Map<Inquilino>(inquilinoAtualizacao));

            return CustomResponse(inquilinoViewModel);
        }

        //[ClaimsAuthorize("Inquilino", "Excluir")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<InquilinoViewModel>> Excluir(Guid id)
        {
            var inquilino = await ObterInquilino(id);

            if (inquilino == null) return NotFound();

            await _inquilinoService.Remover(id);

            return CustomResponse(inquilino);
        }

       
        private async Task<InquilinoViewModel> ObterInquilino(Guid id)
        {
            return _mapper.Map<InquilinoViewModel>(await _inquilinoRepository.ObterPorId(id));
        }
    }
}