using AutoMapper;
using ControleDeMedicamentos.WebApp.Compartilhado.Apresentacao.Extensions;
using ControleDeMedicamentos.WebApp.ModuloMedicamentos.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloMedicamentos.Dominio;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloMedicamentos.Apresentacao;

public class MedicamentosController(ServicoMedicamentos servicoMedicamentos, IMapper mapeador) : Controller
{
    [HttpGet]
    public ActionResult Listar(bool apenasEmFalta = false)
    {
        ViewBag.ApenasEmFalta = apenasEmFalta;

        List<ListarMedicamentosDto> dtos = servicoMedicamentos.SelecionarTodos(apenasEmFalta);
        List<ListarMedicamentosViewModel> listarVms = mapeador.Map<List<ListarMedicamentosViewModel>>(dtos);
        
        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarMedicamentosViewModel cadastrarVm = new CadastrarMedicamentosViewModel(
            string.Empty,
            Guid.Empty,
            string.Empty,
            0,
            SelecionarFornecedores()
        );

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarMedicamentosViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm with {Fornecedores = SelecionarFornecedores() });

        CadastrarMedicamentosDto dto = mapeador.Map<CadastrarMedicamentosDto>(cadastrarVm);

        Result resultado = servicoMedicamentos.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            ModelState.Remove(nameof(CadastrarMedicamentosViewModel.Fornecedores));

            return View(cadastrarVm with {Fornecedores = SelecionarFornecedores() });
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(Guid id)
    {
        Result<DetalhesMedicamentosDto> resultado = servicoMedicamentos.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        EditarMedicamentosViewModel editarVm = 
            mapeador.Map<EditarMedicamentosViewModel>(resultado.Value) with {Fornecedores = SelecionarFornecedores()};

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarMedicamentosViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm with {Fornecedores = SelecionarFornecedores() });

        EditarMedicamentosDto dto = mapeador.Map<EditarMedicamentosDto>(editarVm);

        Result resultado = servicoMedicamentos.Editar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            ModelState.Remove(nameof(CadastrarMedicamentosViewModel.Fornecedores));

            return View(editarVm with {Fornecedores = SelecionarFornecedores() });
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir (Guid id)
    {
        Result<DetalhesMedicamentosDto> resultado = servicoMedicamentos.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }           

        ExcluirMedicamentosViewModel excluirVm = mapeador.Map<ExcluirMedicamentosViewModel>(resultado.Value); 

        return View(excluirVm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirMedicamentosViewModel excluirVm)
    {
        Result resultado = servicoMedicamentos.Excluir(excluirVm.Id);

        if (resultado.IsFailed)
            TempData.AddErrorMessage(resultado);  

        return RedirectToAction(nameof(Listar));        
    }

    private List<OpcaoFornecedoresViewModel> SelecionarFornecedores()
    {
        List<OpcaoFornecedoresDto> dtos = servicoMedicamentos.SelecionarFornecedores();

        return mapeador.Map<List<OpcaoFornecedoresViewModel>>(dtos);
    }

}

