using AutoMapper;

using FluentResults;
using Microsoft.AspNetCore.Mvc;
using ControleDeMedicamentos.WebApp.Compartilhado.Apresentacao.Extensions;
using ControleDeMedicamentos.WebApp.ModuloRequisicaoEntrada.Aplicacao;

namespace ControleDeMedicamentos.WebApp.ModuloRequisicaoEntrada.Apresentacao;

public class RequisicaoEntradaController(ServicoRequisicaoEntrada servicoRequisicaoEntrada, IMapper mapeador) : Controller
{
    [HttpGet]
    public ActionResult ListarEntrada()
    {
        List<ListarRequisicaoEntradaDto> dtos = servicoRequisicaoEntrada.SelecionarTodos();
        List<ListarRequisicaoEntradaViewModel> listarVm = mapeador.Map<List<ListarRequisicaoEntradaViewModel>>(dtos);

        return View(listarVm);
    }

    [HttpGet]
    public ActionResult CadastrarEntrada()
    {
       return View(new CadastrarRequisicaoEntradaViewModel(
            DateTime.Now,
            Guid.Empty,
            Guid.Empty,
            0,
            SelecionarFuncionarios(),
            SelecionarMedicamentos()
        ));
    }

    [HttpPost]
    public ActionResult CadastrarEntrada(CadastrarRequisicaoEntradaViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
        {
            return View(cadastrarVm with
            {
                Medicamentos = SelecionarMedicamentos(),
                Funcionarios = SelecionarFuncionarios()
            });
        }

        CadastrarRequisicaoEntradaDto dto = mapeador.Map<CadastrarRequisicaoEntradaDto>(cadastrarVm);

        Result resultado = servicoRequisicaoEntrada.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View("CadastrarEntrada", cadastrarVm with
            {
                Medicamentos = SelecionarMedicamentos(),
                Funcionarios = SelecionarFuncionarios()
            });
        }

        return RedirectToAction(nameof(ListarEntrada));
    }

    private List<OpcaoFuncionariosViewModel> SelecionarFuncionarios()
    {
        List<OpcaoFuncionariosDto> dtos = servicoRequisicaoEntrada.SelecionarFuncionarios();

        return mapeador.Map<List<OpcaoFuncionariosViewModel>>(dtos);
    }

    private List<OpcaoMedicamentosViewModel> SelecionarMedicamentos()
    {
        List<OpcaoMedicamentosDto> dtos = servicoRequisicaoEntrada.SelecionarMedicamentos();

        return mapeador.Map<List<OpcaoMedicamentosViewModel>>(dtos);
    }
}

