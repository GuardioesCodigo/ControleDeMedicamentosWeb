using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloEstoque.Apresentacao
{
    public class EstoqueController : Controller
    {
        [HttpGet]
        public ActionResult Listar()
        {
            return View();
        }

    }
}
