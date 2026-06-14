using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ControleDeMedicamentos.WebApp.ModuloRequisicaoSaida.Aplicacao;
using ControleDeMedicamentosWeb.WebApp.ModuloRequisicaoSaida.Apresentacao;
using AutoMapper;
using FluentResults;
using ControleDeMedicamentos.WebApp.ModuloRequisicaoSaidae.Aplicacao;
using ControleDeMedicamentos.WebApp.Compartilhado.Dominio;

namespace ControleDeMedicamentosWeb.WebApp.ModuloRequisicaoSaida.Apresentacao;

public class RequisicaoSaidaController : Controller
{
    private readonly ServicoRequisicaoSaida _servico;
    private readonly IMapper _mapeador;

    public RequisicaoSaidaController(ServicoRequisicaoSaida servico, IMapper mapeador)
    {
        _servico = servico;
        _mapeador = mapeador;
    }

    public IActionResult Listar()
    {
        var requisicoes = _servico.SelecionarTodos();
        var listarVm = _mapeador.Map<List<ListarRequisicaoSaidaViewModel>>(requisicoes);
        return View(listarVm);
    }

    [HttpGet]
    public IActionResult Cadastrar()
    {
        var viewModel = new CadastrarRequisicaoSaidaViewModel();
        viewModel.Pacientes = _servico.SelecionarPaciente().Select(p => new SelectListItem(p.Nome, p.Id.ToString()));
        viewModel.Medicamentos = CarregarMedicamentos();
        return View(viewModel);
    }

    [HttpPost]
public IActionResult Cadastrar(CadastrarRequisicaoSaidaViewModel vm)
{
    // Log para verificar se os dados chegam do form
    System.Diagnostics.Debug.WriteLine($"Paciente selecionado: {vm.PacienteId}, Qtd Itens: {vm.Itens?.Count}");

    if (!ModelState.IsValid)
    {
        vm.Pacientes = CarregarPacientes();
        vm.Medicamentos = CarregarMedicamentos();
        return View(vm);
    }

    var dto = _mapeador.Map<CadastrarRequisicaoSaidaDto>(vm);
    var resultado = _servico.Cadastrar(dto);

    if (resultado.IsFailed)
    {
        foreach (var erro in resultado.Errors)
            ModelState.AddModelError(string.Empty, erro.Message);

        vm.Pacientes = CarregarPacientes();
        vm.Medicamentos = CarregarMedicamentos();
        return View(vm);
    }

    // Redirecionamento explícito
    return RedirectToAction(nameof(Listar));
}

    // Métodos Auxiliares para popular os Dropdowns
   private List<SelectListItem> CarregarPacientes()
    {
        var pacientes = _servico.SelecionarPaciente(); 
        return pacientes.Select(p => new SelectListItem(p.Nome, p.Id.ToString())).ToList();
    }

    private List<SelectListItem> CarregarMedicamentos()
    {
        var medicamentos = _servico.SelecionarMedicamentos(); // Garanta que este método exista no seu Serviço
        return medicamentos.Select(m => new SelectListItem(m.Nome, m.Id.ToString())).ToList();
    }

}