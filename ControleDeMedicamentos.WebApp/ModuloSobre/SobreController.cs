using Microsoft.AspNetCore.Mvc;

namespace ListaCompras.WebApplication.Sobre;

public class SobreController : Controller
{
    public ActionResult Listar()
    {
        return View();
    }

}

