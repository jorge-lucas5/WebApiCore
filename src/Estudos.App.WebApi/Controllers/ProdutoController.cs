using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Estudos.App.Business.Interfaces;
using Estudos.App.Business.Models;
using Estudos.App.WebApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Estudos.App.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ProdutoController : MainController
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoService _produtoService;
        private readonly IMapper _mapper;
        public ProdutoController(INotificador notificador,
                                IProdutoService produtoService,
                                IProdutoRepository produtoRepository,
                                IMapper mapper) : base(notificador)
        {
            _produtoService = produtoService;
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoViewModel>>> ObterTodos()
        {
            var produtos = await _produtoRepository.ObterProdutosFornecedores();
            return CustonResponse(_mapper.Map<IEnumerable<ProdutoViewModel>>(produtos));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<FornecedorViewModel>> ObterPorId(Guid id)
        {
            var produto = await ObterProduto(id);

            if (produto == null) return NotFound();

            return CustonResponse(produto);

        }

        [HttpPost]
        public async Task<ActionResult<FornecedorViewModel>> Adicionar(ProdutoViewModel viewModel)
        {
            if (!ModelState.IsValid) return CustonResponse(ModelState);

            var imgNome = Guid.NewGuid() + "_" + viewModel.Imagem;
            if (!await UploadArquivo(viewModel.ImagemUpload, imgNome))
            {
                return CustonResponse();
            }

            viewModel.Imagem = imgNome;
            var produto = _mapper.Map<Produto>(viewModel);
            await _produtoService.Adicionar(produto);

            return CustonResponse(viewModel);

        }


        [HttpPost("Adicionar")]
        public async Task<ActionResult<FornecedorViewModel>> AdicionarAlternativo([FromForm]ProdutoImagemViewModel viewModel)
        {
            if (!ModelState.IsValid) return CustonResponse(ModelState);

            var imgPrefixo = Guid.NewGuid() + "_";
            if (!await UploadArquivoAlternativo(viewModel.ImagemUpload, imgPrefixo))
            {
                return CustonResponse();
            }

            viewModel.Imagem = imgPrefixo + viewModel.ImagemUpload.FileName;
            var produto = _mapper.Map<Produto>(viewModel);
            await _produtoService.Adicionar(produto);
            viewModel.Id = produto.Id;


            return CustonResponse(viewModel);

        }

        [HttpPost("AdicionarImagem")]
        //[DisableRequestSizeLimit]
        [RequestSizeLimit(40000000)]
        public async Task<ActionResult> AdicionarImagem(IFormFile file)
        {
            return CustonResponse(file);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<FornecedorViewModel>> Excluir(Guid id)
        {
            var produtoViewModel = await ObterProduto(id);
            if (produtoViewModel == null) return NotFound();

            await _produtoService.Remover(id);

            return CustonResponse(produtoViewModel);

        }



        #region privates

        private async Task<ProdutoViewModel> ObterProduto(Guid id)
        {
            return _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterProdutoFornecedor(id));
        }

        private async Task<bool> UploadArquivo(string arquivo, string imgNome)
        {
            if (string.IsNullOrEmpty(arquivo))
            {
                NotificarErro("Forneça uma imagem para esse produto");
                return false;
            }
            var imagemDataByteArray = Convert.FromBase64String(arquivo);

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/app/demo-webapi/src/assets", imgNome);
            if (System.IO.File.Exists(filePath))
            {
                NotificarErro("Já existem um arquivo com esse nome cadastrado!");
                return false;
            }
            await System.IO.File.WriteAllBytesAsync(filePath, imagemDataByteArray);
            return true;
        }

        private async Task<bool> UploadArquivoAlternativo(IFormFile arquivo, string imgPrefixo)
        {
            if (arquivo == null || arquivo.Length <= 0)
            {
                NotificarErro("Forneça uma imagem para esse produto");
                return false;
            }

            imgPrefixo += arquivo.FileName;
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploadImages", imgPrefixo);
            if (System.IO.File.Exists(filePath))
            {
                NotificarErro("Já existem um arquivo com esse nome cadastrado!");
                return false;
            }

            await using var stream = new FileStream(filePath, FileMode.Create);
            await arquivo.CopyToAsync(stream);
            return true;
        }

        #endregion
    }
}