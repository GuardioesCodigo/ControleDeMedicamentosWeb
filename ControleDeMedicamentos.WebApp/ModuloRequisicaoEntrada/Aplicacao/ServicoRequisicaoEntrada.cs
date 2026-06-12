using System;
using ControleDeMedicamentos.WebApp.ModuloEstoque.Dominio;
using ControleDeMedicamentos.WebApp.ModuloFuncionario.Dominio;
using ControleDeMedicamentos.WebApp.ModuloMedicamentos.Dominio;
using FluentResults;

namespace ControleDeMedicamentos.WebApp.ModuloRequisicaoEntrada.Aplicacao;

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

    public Result Cadastrar(CadastrarRequisicaoEntradaDto dto)
    {
        Funcionario? funcionarioSelecionado = repositorioFuncionario.SelecionarPorId(dto.FuncionarioId);

        if (funcionarioSelecionado == null)
            return Falha(nameof(dto.FuncionarioId), "Selecione um funcionário válido.");

        Medicamentos? medicamentoSelecionado = repositorioMedicamentos.SelecionarPorId(dto.MedicamentoId);

        if (medicamentoSelecionado == null)
            return Falha(nameof(dto.MedicamentoId), "Selecione um medicamento válido.");

        RequisicaoEntrada NewRequisicaoEntrada = new RequisicaoEntrada(
            medicamentoSelecionado,
            funcionarioSelecionado,
            dto.Quantidade,
            dto.Data
        );

        Result resultadoValidacao = ValidarEntidade(NewRequisicaoEntrada);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioRequisicaoEntrada.Cadastrar(NewRequisicaoEntrada);

        return Result.Ok();
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

    private static Result ValidarEntidade(RequisicaoEntrada requisicaoEntrada)
    {
        List<string> erros = requisicaoEntrada.Validar();

        if (erros.Count == 0)
            return Result.Ok();

        return Result.Fail(new Error(erros.First()).WithMetadata("Campo", string.Empty));
    }

    private static Result Falha(string campo, string mensagem)
    {
        IError erro = new Error(mensagem).WithMetadata("Campo", campo);

        return Result.Fail(erro);
    }
}
