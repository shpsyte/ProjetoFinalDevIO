using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Data;
using App.ViewModels;
using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace App.Controllers {
    [Route ("admin-fornecedores")]
    public class FornecedorController : BaseController {

        protected readonly IFornecedorRepository _fornecedor;
        protected readonly IEnderecoRepository _endereco;
        protected readonly IFornecedorServices _fornecedorServices;

        public FornecedorController (IFornecedorServices fornecedorServices, IFornecedorRepository fornecedor, IEnderecoRepository endereco, IMapper mapper, INotificador notificador) : base (mapper, notificador) {
            _fornecedor = fornecedor;
            _endereco = endereco;
            _fornecedorServices = fornecedorServices;

        }

        [Route ("lista-de-fornecedores")]
        public async Task<IActionResult> Index () {
            var data = _mapper.Map<IEnumerable<FornecedorViewModel>> (await _fornecedor.PegarTodosFornecedores ());
            return View (data);
        }

        [Route ("detalhes-do-fornecedore/{id:guid}")]
        public async Task<IActionResult> Details (Guid id) {
            var fornecedorViewModel = _mapper.Map<FornecedorViewModel> (await _fornecedor.ObterPorId (id));
            if (fornecedorViewModel == null) return NotFound ();
            return View (fornecedorViewModel);
        }

        [Route ("criando-um-fornecedor")]
        public IActionResult Create () {
            return View ();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route ("criando-um-fornecedor")]
        public async Task<IActionResult> Create (FornecedorViewModel fornecedorViewModel) {

            if (!ModelState.IsValid) return View (fornecedorViewModel);

            var fornecedor = _mapper.Map<Fornecedor> (fornecedorViewModel);
            // await _fornecedor.Adicionar (fornecedor);
            await _fornecedorServices.adicionar (fornecedor);
            if (!OperacaoValida ()) return View (fornecedorViewModel);

            return RedirectToAction (nameof (Index));

        }

        [Route ("editando-um-fornecedor/{id:guid}")]
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
        [Route ("editando-um-fornecedor/{id:guid}")]
        public async Task<IActionResult> Edit (Guid id, FornecedorViewModel fornecedorViewModel) {
            if (id != fornecedorViewModel.Id) {
                return NotFound ();
            }

            if (!ModelState.IsValid) return View (fornecedorViewModel);

            var fornecedor = _mapper.Map<Fornecedor> (fornecedorViewModel);
            await _fornecedorServices.atualizar (fornecedor);
            if (!OperacaoValida ()) return View (fornecedorViewModel);

            return RedirectToAction (nameof (Index));

        }

        [Route ("excuir-fornecedore/{id:guid}")]
        public async Task<IActionResult> Delete (Guid id) {

            var fornecedorViewModel = _mapper.Map<FornecedorViewModel> (await _fornecedor.PegarFornecedorValido (id));

            if (fornecedorViewModel == null) {
                return NotFound ();
            }

            return View (fornecedorViewModel);
        }

        [HttpPost, ActionName ("Delete")]
        [ValidateAntiForgeryToken]
        [Route ("excuir-fornecedore/{id:guid}")]
        public async Task<IActionResult> DeleteConfirmed (Guid id) {
            var fornecedor = await _fornecedor.ObterPorId (id);
            await _fornecedorServices.remover (fornecedor);
            if (!OperacaoValida ()) return View (_mapper.Map<FornecedorViewModel> (fornecedor));

            TempData["Sucesso"] = "Fornecedor excluido com sucesso";
            return RedirectToAction (nameof (Index));
        }

    }
}