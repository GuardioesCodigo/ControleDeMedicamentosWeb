using System;
using ControleDeMedicamentos.WebApp.ModuloEstoque.Dominio;
using ControleDeMedicamentos.WebApp.ModuloFuncionario.Dominio;
using ControleDeMedicamentos.WebApp.ModuloMedicamentos.Dominio;
using FluentResults;

namespace ControleDeMedicamentos.WebApp.ModuloEstoque.Aplicacao;

public class ServicoRequisicaoEntrada
{
    private readonly IRepositorioRequisicaoEntrada repositorioRequisicaoEntrada;
    private readonly IRepositorioMedicamentos repositorioMedicamentos;
    private readonly IRepositorioFuncionario repositorioFuncionario;

    public ServicoRequisicaoEntrada(IRepositorioMedicamentos repositorioMedicamentos, IRepositorioFuncionario repositorioFuncionario, IRepositorioRequisicaoEntrada repositorioRequisicaoEntrada)
    {
        this.repositorioMedicamentos = repositorioMedicamentos;
        this.repositorioFuncionario = repositorioFuncionario;
        this.repositorioRequisicaoEntrada = repositorioRequisicaoEntrada;
    }

    public List<ListarRequisicaoEntradaDto> SelecionarTodos()
    {
        return repositorioRequisicaoEntrada
            .SelecionarTodos()
            .Select(r => new ListarRequisicaoEntradaDto(
                r.Id,
                r.Data,
                r.Medicamento.Id,
                r.Medicamento.Nome,
                r.Funcionario.Id,
                r.Funcionario.Nome,
                r.Quantidade
            ))
            .ToList();
    }

    public List<OpcaoMedicamentosDto> SelecionarMedicamentos()
    {
        return repositorioMedicamentos
            .SelecionarTodos()
            .Select(m => new OpcaoMedicamentosDto(m.Id, m.Nome, m.Fornecedor))
            .ToList();
    }

    public List<OpcaoFuncionariosDto> SelecionarFuncionarios()
    {
        return repositorioFuncionario
            .SelecionarTodos()
            .Select(m => new OpcaoFuncionariosDto(m.Id, m.Nome, m.Cpf))
            .ToList();
    }

    private static Result Falha(string campo, string mensagem)
    {
        IError erro = new Error(mensagem).WithMetadata("Campo", campo);

        return Result.Fail(erro);
    }
}
