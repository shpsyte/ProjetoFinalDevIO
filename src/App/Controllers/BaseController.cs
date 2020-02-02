using AutoMapper;
using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers {
    public class BaseController : Controller {
        protected readonly IMapper _mapper;
        protected readonly INotificador _notificador;

        public BaseController (IMapper mapper, INotificador notificador) {
            _mapper = mapper;
            _notificador = notificador;
        }

        public bool OperacaoValida () {
            return !_notificador.TemNotificacao ();
        }
    }
}