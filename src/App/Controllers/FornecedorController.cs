using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Data;
using App.ViewModels;
using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace App.Controllers {
    public class FornecedorController : BaseController {

        protected readonly IFornecedorRepository _fornecedor;
        protected readonly IEnderecoRepository _endereco;

        public FornecedorController (IFornecedorRepository fornecedor, IEnderecoRepository endereco, IMapper mapper) : base (mapper) {
            _fornecedor = fornecedor;
            _endereco = endereco;

        }

        public async Task<IActionResult> Index () {
            var data = _mapper.Map<IEnumerable<FornecedorViewModel>> (await _fornecedor.PegarTodosFornecedores ());
            return View (data);
        }

        public async Task<IActionResult> Details (Guid id) {
            var fornecedorViewModel = _mapper.Map<FornecedorViewModel> (await _fornecedor.ObterPorId (id));
            if (fornecedorViewModel == null) return NotFound ();
            return View (fornecedorViewModel);
        }

        public IActionResult Create () {
            return View ();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create (FornecedorViewModel fornecedorViewModel) {

            if (!ModelState.IsValid) return View (fornecedorViewModel);

            var fornecedor = _mapper.Map<Fornecedor> (fornecedorViewModel);
            await _fornecedor.Adicionar (fornecedor);
            return RedirectToAction (nameof (Index));

        }

        public async Task<IActionResult> Edit (Guid id) {

            var fornecedorViewModel = _mapper.Map<FornecedorViewModel> (await _fornecedor.PegarFornecedorValido (id));
            if (fornecedorViewModel == null) {
                return NotFound ();
            }
            return View (fornecedorViewModel);
        }

        public async Task<IActionResult> AtualizarEndereco (Guid id) {
            var fornecedor = await _fornecedor.PegarFornecedorValido (id);
            var endereco = _mapper.Map<EnderecoViewModel> (fornecedor.Endereco);
            return PartialView ("_AtualizarEndereco", new FornecedorViewModel () {
                Endereco = endereco
            });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> AtualizarEndereco (FornecedorViewModel fornecedorViewmodel) {

            ModelState.Remove ("Name");
            ModelState.Remove ("LastName");
            ModelState.Remove ("Document");

            if (!ModelState.IsValid) {
                return PartialView ("_AtualizarEndereco", fornecedorViewmodel);
            }

            await _endereco.Atualizar (_mapper.Map<Endereco> (fornecedorViewmodel.Endereco));

            var url = Url.Action ("ObterEndereco", "Fornecedor", new { id = fornecedorViewmodel.Endereco.FornecedorId });

            var jsonret = new { sucess = "true", url = url };

            return Json (jsonret);
        }

        public async Task<IActionResult> ObterEndereco (Guid id) {
            var fornecedor = await _fornecedor.PegarFornecedorValido (id);
            var endereco = _mapper.Map<EnderecoViewModel> (fornecedor.Endereco);
            return PartialView ("_ListaEndereco", new FornecedorViewModel () { Endereco = endereco });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit (Guid id, FornecedorViewModel fornecedorViewModel) {
            if (id != fornecedorViewModel.Id) {
                return NotFound ();
            }

            if (!ModelState.IsValid) return View (fornecedorViewModel);

            var fornecedor = _mapper.Map<Fornecedor> (fornecedorViewModel);
            await _fornecedor.Atualizar (fornecedor);
            return RedirectToAction (nameof (Index));

        }

        public async Task<IActionResult> Delete (Guid id) {

            var fornecedorViewModel = _mapper.Map<FornecedorViewModel> (await _fornecedor.ObterPorId (id));

            if (fornecedorViewModel == null) {
                return NotFound ();
            }

            return View (fornecedorViewModel);
        }

        [HttpPost, ActionName ("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed (Guid id) {
            var fornecedor = await _fornecedor.ObterPorId (id);
            await _fornecedor.Remover (fornecedor);

            return RedirectToAction (nameof (Index));
        }

    }
}