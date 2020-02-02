using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using App.Data;
using App.ViewModels;
using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace App.Controllers {
    [Route ("admin-produtos")]
    public class ProdutoController : BaseController {

        private readonly IProdutoRepository _context;
        private readonly IFornecedorRepository _fornecedor;
        private readonly IProdutoServices _produtoService;

        public ProdutoController (IProdutoRepository produto, IProdutoServices produtoService, IFornecedorRepository fornecedor, IMapper mapper, INotificador notificador) : base (mapper, notificador) {
            _context = produto;
            _fornecedor = fornecedor;
            _produtoService = produtoService;
        }

        [Route ("lista-produtos")]
        public async Task<IActionResult> Index () {
            var data = _mapper.Map<IEnumerable<ProdutoViewModel>> (await _context.PegarTodosProdutos ());
            return View (data);
        }

        [Route ("detalhes-produtos/{id:guid}")]
        public async Task<IActionResult> Details (Guid id) {

            var produtoViewModel = _mapper.Map<ProdutoViewModel> (await _context.ObterPorId (id));

            if (produtoViewModel == null) {
                return NotFound ();
            }

            return View (produtoViewModel);
        }

        [Route ("criando-produtos")]
        public async Task<IActionResult> Create () {
            ViewData["FornecedorId"] = new SelectList (await _fornecedor.ObterTodos (), "Id", "Name");
            return View ();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route ("criando-produtos")]
        public async Task<IActionResult> Create (ProdutoViewModel produtoViewModel) {
            ViewData["FornecedorId"] = new SelectList (await _fornecedor.ObterTodos (), "Id", "Name", produtoViewModel.FornecedorId);

            var prefixo = Guid.NewGuid ().ToString () + "_";

            if (produtoViewModel.ImageUpload != null) {

                produtoViewModel.Image = await UploadImage (produtoViewModel.ImageUpload, prefixo);
            }

            if (!ModelState.IsValid) return View (produtoViewModel);

            await _produtoService.adicionar (_mapper.Map<Produto> (produtoViewModel));
            if (!OperacaoValida ()) return View (produtoViewModel);

            return RedirectToAction (nameof (Index));

        }

        private async Task<string> UploadImage (IFormFile arquivo, string prefixo) {
            if (arquivo.Length <= 0) return "";

            var path = Path.Combine (Directory.GetCurrentDirectory (), "wwwroot/images", prefixo + arquivo.FileName);

            using (var stream = new FileStream (path, FileMode.Create)) {
                await arquivo.CopyToAsync (stream);
            }

            return prefixo + arquivo.FileName;

        }

        [Route ("editando-produtos/{id:guid}")]
        public async Task<IActionResult> Edit (Guid id) {

            var produtoViewModel = _mapper.Map<ProdutoViewModel> (await _context.ObterPorId (id));
            if (produtoViewModel == null) {
                return NotFound ();
            }
            ViewData["FornecedorId"] = new SelectList (await _fornecedor.ObterTodos (), "Id", "Document", produtoViewModel.FornecedorId);
            return View (produtoViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route ("editando-produtos/{id:guid}")]
        public async Task<IActionResult> Edit (Guid id, ProdutoViewModel produtoViewModel) {
            if (id != produtoViewModel.Id) {
                return NotFound ();
            }
            ViewData["FornecedorId"] = new SelectList (await _fornecedor.ObterTodos (), "Id", "Document", produtoViewModel.FornecedorId);

            var produtoAtualizacao = await _context.ObterPorId (id);
            produtoViewModel.FornecedorId = produtoAtualizacao.FornecedorId;
            produtoViewModel.Image = produtoAtualizacao.Image;

            if (!ModelState.IsValid) return View (produtoViewModel);

            if (produtoViewModel.ImageUpload != null) {
                string pre = Guid.NewGuid () + "-";

                produtoAtualizacao.Image = await UploadImage (produtoViewModel.ImageUpload, pre);
            }

            produtoAtualizacao.Name = produtoViewModel.Name;
            produtoAtualizacao.Price = produtoViewModel.Price;

            var produto = _mapper.Map<Produto> (produtoAtualizacao);

            await _produtoService.adicionar (produto);
            if (!OperacaoValida ()) return View (produtoViewModel);

            return RedirectToAction (nameof (Index));

        }

        [Route ("deletando-produtos/{id:guid}")]
        public async Task<IActionResult> Delete (Guid id) {

            var produtoViewModel = _mapper.Map<ProdutoViewModel> (await _context.ObterPorId (id));

            if (produtoViewModel == null) {
                return NotFound ();
            }

            return View (produtoViewModel);
        }

        [HttpPost, ActionName ("Delete")]
        [ValidateAntiForgeryToken]
        [Route ("deletando-produtos/{id:guid}")]
        public async Task<IActionResult> DeleteConfirmed (Guid id) {

            var produto = await _context.ObterPorId (id);
            await _produtoService.remover (produto);
            TempData["Sucesso"] = "Produto excluido com sucesso";

            return RedirectToAction (nameof (Index));
        }

    }
}