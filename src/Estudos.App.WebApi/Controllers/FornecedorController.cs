using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Estudos.App.Business.Interfaces;
using Estudos.App.Business.Models;
using Estudos.App.WebApi.Extensions;
using Estudos.App.WebApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estudos.App.WebApi.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class FornecedorController : MainController
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IFornecedorService _fornecedorService;
        private readonly IMapper _mapper;

        public FornecedorController(IFornecedorRepository fornecedorRepository,
                                    IMapper mapper, IFornecedorService fornecedorService,
                                    INotificador notificador,
                                    IEnderecoRepository enderecoRepository,
                                    IUser user) : base(notificador, user)
        {
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
            _fornecedorService = fornecedorService;
            _enderecoRepository = enderecoRepository;
        }

        #region verbos

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FornecedorViewModel>>> ObterTodos()
        {
            var fornecedores = await _fornecedorRepository.ObterTodos();
            return CustomResponse(_mapper.Map<IEnumerable<FornecedorViewModel>>(fornecedores));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<FornecedorViewModel>> ObterPorId(Guid id)
        {
            var fornecedor = await ObeterFornecedorProdutosEndereco(id);

            if (fornecedor == null) return NotFound();

            return CustomResponse(fornecedor);

        }

        [ClaimsAuthorize("Fornecedor", "Adicionar")]
        [HttpPost]
        public async Task<ActionResult<FornecedorViewModel>> Adicionar(FornecedorViewModel viewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var fornecedor = _mapper.Map<Fornecedor>(viewModel);
            await _fornecedorService.Adicionar(fornecedor);

            return CustomResponse(_mapper.Map<FornecedorViewModel>(fornecedor));

        }

        [ClaimsAuthorize("Fornecedor", "Atualizar")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<FornecedorViewModel>> Atualizar(Guid id, FornecedorViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo passado na query");
                return CustomResponse();
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var fornecedor = _mapper.Map<Fornecedor>(viewModel);
            await _fornecedorService.Atualizar(fornecedor);

            return CustomResponse(viewModel);

        }

        [ClaimsAuthorize("Fornecedor", "Excluir")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<FornecedorViewModel>> Excluir(Guid id)
        {
            var fornecedor = await _fornecedorRepository.ObterPorId(id);
            if (fornecedor == null) return NotFound();

            await _fornecedorService.Remover(id);

            var viewModel = _mapper.Map<FornecedorViewModel>(fornecedor);

            return CustomResponse(viewModel);

        }

        [HttpGet("obter-endereco/{id:guid}")]
        public async Task<ActionResult<FornecedorViewModel>> ObterEnderecoPorId(Guid id)
        {
            var fornecedor = await ObeterFornecedorProdutosEndereco(id);
            if (fornecedor == null) return NotFound();

            return CustomResponse(fornecedor);

        }

        [ClaimsAuthorize("Fornecedor", "Atualizar")]
        [HttpPut("atualizar-endereco/{id:guid}")]
        public async Task<ActionResult<FornecedorViewModel>> AtualizarEndereco(Guid id, EnderecoViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo passado na query");
                return CustomResponse();
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var endereco = _mapper.Map<Endereco>(viewModel);
            await _enderecoRepository.Atualizar(endereco);

            return CustomResponse(viewModel);

        }

        #endregion

        #region private

        private async Task<FornecedorViewModel> ObeterFornecedorProdutosEndereco(Guid id)
        {
            var fornecedores = await _fornecedorRepository.ObeterFornecedorProdutosEndereco(id);
            return _mapper.Map<FornecedorViewModel>(fornecedores);
        }

        #endregion
    }
}