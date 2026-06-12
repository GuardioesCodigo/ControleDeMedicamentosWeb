using AutoMapper;
using ControleDeMedicamentos.WebApp.ModuloEstoque.Aplicacao;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloEstoque.Apresentacao;

public class RequisicaoEntradaController(ServicoRequisicaoEntrada servicoRequisicaoEntrada, IMapper mapeador) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarRequisicaoEntradaDto> dtos = servicoRequisicaoEntrada.SelecionarTodos();
        List<ListarRequisicaoEntradaViewModel> listarVm = mapeador.Map<List<ListarRequisicaoEntradaViewModel>>(dtos);

        return View(listarVm);
    }
}

