using AutoMapper;
using ControleDeMedicamentos.WebApp.ModuloFornecedores.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloMedicamentos.Aplicacao;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloMedicamentos.Apresentacao
{
    public class MedicamentosController(ServicoMedicamentos servicoMedicamentos, IMapper mapeador) : Controller
    {
        [HttpGet]
        public ActionResult Listar()
        {
            List<ListarMedicamentosDto> dtos = servicoMedicamentos.SelecionarTodos();
            List<ListarMedicamentosViewModel> listarVms = mapeador.Map<List<ListarMedicamentosViewModel>>(dtos);
            
            return View(listarVms);
        }



    }
}
