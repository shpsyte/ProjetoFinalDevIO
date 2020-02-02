using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using App.Models;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers {
    public class HomeController : Controller {
        public IActionResult Index () {
            return View ();
        }

        public IActionResult Privacy () {
            return View ();
        }

        [Route ("erro/{id:length(3,3)}")]
        public IActionResult Error (int id) {
            var errormodel = new ErrorViewModel () {
                ErrorCode = id,
                Title = "Error",
                Message = "Erro na viewmodel"
            };

            

            return View ("Error", errormodel);
        }
    }
}