using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.Models;
using Business.Models.Validations;

namespace Business.Services {
    public class FornecedorServices : BaseServices, IFornecedorServices {

        private readonly IFornecedorRepository _fornecedor;

        public FornecedorServices (IFornecedorRepository fornecedor, INotificador notificador) : base (notificador) {
            _fornecedor = fornecedor;
        }

        public async Task adicionar (Fornecedor fornecedor) {
            // validar o estado da entidade
            if (!ExecutarValidacao (new FornecedorValidations (), fornecedor)) return;

            // validar algo no banco
            if ((await _fornecedor.Query (a => a.Document == fornecedor.Document)).Any ()) {
                Notificar ("Já existe um fornecedor com este documento");
                return;
            }

            // qualquer outra coisa
            await _fornecedor.Adicionar (fornecedor);

        }

        public async Task atualizar (Fornecedor fornecedor) {
            // validar o estado da entidade
            if (!ExecutarValidacao (new FornecedorValidations (), fornecedor)) return;

            // validar algo no banco
            if ((await _fornecedor.Query (a => a.Document == fornecedor.Document && a.Id != fornecedor.Id)).Any ()) {
                Notificar ("Já existe um fornecedor com este documento");
                return;
            }

            // qualquer outra coisa
            await _fornecedor.Atualizar (fornecedor);
        }

        public void Dispose () {
            _fornecedor?.Dispose ();
        }

        public async Task remover (Fornecedor fornecedor) {
            var temProdutos = (await _fornecedor.PegarFornecedorValido (fornecedor.Id)).Produtos.Any ();

            if (temProdutos) {
                Notificar ("O fornecedor possui produtos cadastrados, retire primeiro");
                return;
            }
            await _fornecedor.Remover (fornecedor);
        }
    }
}