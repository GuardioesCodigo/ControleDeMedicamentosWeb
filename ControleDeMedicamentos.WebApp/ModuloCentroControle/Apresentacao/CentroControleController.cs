using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloCentroControle.Apresentacao
{
    public class CentroControleController : Controller
    {
        // GET: CentroControleController
        public ActionResult Listar()
        {
            return View();
        }

    }
}
