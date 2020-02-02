using System.Collections.Generic;
using System.Linq;
using Business.Interfaces;

namespace Business.Notifications {
    public class Notificador : INotificador {
        private List<Notificacao> _notificacoes;

        public Notificador () {
            _notificacoes = new List<Notificacao> ();
        }

        public void Handle (Notificacao notificacao) {
            _notificacoes.Add (notificacao);
        }

        public List<Notificacao> ObterNotificacao () {
            return _notificacoes;
        }

        public bool TemNotificacao () {
            return _notificacoes.Any ();
        }
    }

    public class Notificacao {
        public string Message { get; }
        public Notificacao (string message) {
            Message = message;
        }

    }
}