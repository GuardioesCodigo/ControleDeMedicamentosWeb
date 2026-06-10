using AutoMapper;
using ControleDeMedicamentos.WebApp.ModuloFornecedores.Aplicacao;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloFornecedores.Apresentacao
{
    public class FornecedoresController(IMapper mapeador, ServicoFornecedores servicoFornecedores) : Controller
    {
        [HttpGet]
        public ActionResult Listar()
        {
            List<ListarFornecedoresDto> dtos = servicoFornecedores.SelecionarTodos();

            List<ListarFornecedoresViewModel> listarVms = mapeador.Map<List<ListarFornecedoresDto>>(dtos)
                .Select(f => new ListarFornecedoresViewModel(f.Id, f.Nome, f.Telefone, f.CNPJ))
                .ToList();

            return View(listarVms);
        }
    }
}
