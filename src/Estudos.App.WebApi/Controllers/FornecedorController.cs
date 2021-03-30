using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Estudos.App.Business.Interfaces;
using Estudos.App.Business.Models;
using Estudos.App.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Estudos.App.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class FornecedorController : MainController
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IFornecedorService _fornecedorService;
        private readonly IMapper _mapper;

        public FornecedorController(IFornecedorRepository fornecedorRepository, IMapper mapper, IFornecedorService fornecedorService)
        {
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
            _fornecedorService = fornecedorService;
        }

        [HttpGet]
        public async Task<IEnumerable<FornecedorViewModel>> ObterTodos()
        {
            var fornecedores = await _fornecedorRepository.ObterTodos();
            return _mapper.Map<IEnumerable<FornecedorViewModel>>(fornecedores);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<FornecedorViewModel>> ObterPorId(Guid id)
        {
            var fornecedor = await ObeterFornecedorProdutosEndereco(id);

            if (fornecedor == null) return NotFound();

            return Ok(fornecedor);

        }

        [HttpPost]
        public async Task<ActionResult<FornecedorViewModel>> Adicionar(FornecedorViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest();

            var fornecedor = _mapper.Map<Fornecedor>(viewModel);
            await _fornecedorService.Adicionar(fornecedor);
            viewModel.Id = fornecedor.Id;


            return Ok(viewModel);

        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<FornecedorViewModel>> Atualizar(Guid id, FornecedorViewModel viewModel)
        {
            if (id != viewModel.Id) return BadRequest();

            if (!ModelState.IsValid) return BadRequest();

            var fornecedor = _mapper.Map<Fornecedor>(viewModel);
            await _fornecedorService.Adicionar(fornecedor);

            return Ok(viewModel);

        }



        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<FornecedorViewModel>> Excluir(Guid id)
        {
            var fornecedor = await _fornecedorRepository.ObterPorId(id);
            if (fornecedor == null) return BadRequest();

            await _fornecedorService.Remover(id);

            var viewModel = _mapper.Map<FornecedorViewModel>(fornecedor);
            return Ok(viewModel);

        }
        #region private

        private async Task<FornecedorViewModel> ObeterFornecedorProdutosEndereco(Guid id)
        {
            var fornecedores = await _fornecedorRepository.ObeterFornecedorProdutosEndereco(id);
            return _mapper.Map<FornecedorViewModel>(fornecedores);
        }

        #endregion
    }
}