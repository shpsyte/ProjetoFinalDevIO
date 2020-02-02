using System.Collections.Generic;
using Business.Notifications;

namespace Business.Interfaces {
    public interface INotificador {
        bool TemNotificacao ();
        List<Notificacao> ObterNotificacao ();
        void Handle (Notificacao notificacao);

    }
}