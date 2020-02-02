using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.Models;
using Business.Models.Validations;

namespace Business.Services {
    public class ProdutoServices : BaseServices, IProdutoServices {
        private readonly IProdutoRepository _produtos;

        public ProdutoServices (IProdutoRepository produtos, INotificador notificador) : base (notificador) {
            _produtos = produtos;
        }

        public async Task adicionar (Produto produto) {
            if (!ExecutarValidacao (new ProdutosValidations (), produto)) return;

            var any = (await _produtos.Query (a => a.Name == produto.Name)).Any ();

            if (any) {
                Notificar ("Ja existe um produto com este nome");
                return;
            }

            await _produtos.Adicionar (produto);

        }

        public async Task atualizar (Produto produto) {
            if (!ExecutarValidacao (new ProdutosValidations (), produto)) return;
            await _produtos.Atualizar (produto);
        }

        public async Task remover (Produto produto) {
            await _produtos.Remover (produto);
        }

        public void Dispose () {
            _produtos?.Dispose ();
        }

    }
}