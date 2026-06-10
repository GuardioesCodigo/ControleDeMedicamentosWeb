using AutoMapper;
using ControleDeMedicamentos.WebApp.Compartilhado.Apresentacao.Extensions;
using ControleDeMedicamentos.WebApp.ModuloFornecedores.Aplicacao;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloFornecedores.Apresentacao;

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

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarFornecedoresViewModel cadastrarVm = new CadastrarFornecedoresViewModel(
            string.Empty, 
            string.Empty,
            string.Empty);

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarFornecedoresViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm);

        CadastrarFornecedoresDto dtos = mapeador.Map<CadastrarFornecedoresDto> (cadastrarVm);
        Result resultado = servicoFornecedores.Cadastrar(dtos);

        if (resultado.IsFailed)
        {
            foreach (IError erro in resultado.Errors)
            {
                string campo =
                    erro.Metadata["Campo"] is string
                        ? erro.Metadata["Campo"].ToString()!
                        : string.Empty;

                ModelState.AddModelError(campo, erro.Message);
            }

            return View(cadastrarVm);
        }

        return RedirectToAction(nameof(Listar)); 
    }  

    [HttpGet]
    public ActionResult Editar(Guid id)
    {
        Result<DetalhesFornecedoresDto> resultado = servicoFornecedores.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        DetalhesFornecedoresDto fornecedores = resultado.Value;

        EditarFornecedoresViewModel editarVm = mapeador.Map<EditarFornecedoresViewModel>(resultado.Value);

        return View(editarVm);
    } 

    [HttpPost]
    public ActionResult Editar(EditarFornecedoresViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm);

        EditarFornecedoresDto dto = mapeador.Map<EditarFornecedoresDto>(editarVm);
        Result resultado =  servicoFornecedores.Editar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(editarVm);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(Guid id)
    {
        Result<DetalhesFornecedoresDto> resultado = servicoFornecedores.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        ExcluirFornecedoresViewModel excluirVm = mapeador.Map<ExcluirFornecedoresViewModel>(resultado.Value);

        return View(excluirVm);
    }

        [HttpPost]
    public ActionResult Excluir(ExcluirFornecedoresViewModel excluirVm)
    {
        Result resultado = servicoFornecedores.Excluir(excluirVm.Id);

        if (resultado.IsFailed)
            TempData.AddErrorMessage(resultado);

        return RedirectToAction(nameof(Listar));
    }
}

