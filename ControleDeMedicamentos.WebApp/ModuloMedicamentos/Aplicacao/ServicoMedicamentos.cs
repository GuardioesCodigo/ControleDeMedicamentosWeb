using System;
using System.Security.Cryptography.X509Certificates;
using ControleDeMedicamentos.WebApp.ModuloMedicamentos.Dominio;
using FluentResults;

namespace ControleDeMedicamentos.WebApp.ModuloMedicamentos.Aplicacao;

public class ServicoMedicamentos
{
    private readonly IRepositorioMedicamentos repositorioMedicamentos;

    public ServicoMedicamentos(IRepositorioMedicamentos repositorioMedicamentos)
    {
        this.repositorioMedicamentos = repositorioMedicamentos;
    }

    public List<ListarMedicamentosDto> SelecionarTodos()
    {
        List<Medicamentos> medicamentos = repositorioMedicamentos.SelecionarTodos();

        return medicamentos
            .Select(m => new ListarMedicamentosDto(m.Id, m.Nome, m.Descricao, m.Quantidade, m.Fornecedor.Id, m.Fornecedor.Cnpj))
            .ToList();
    }

    public Result<DetalhesMedicamentosDto> SelecionarPorId(Guid id)
    {
        Medicamentos? medicamentos = repositorioMedicamentos.SelecionarPorId(id);

        if (medicamentos == null)
            return Result.Fail("Fornecedor não encontrado");

        return Result.Ok(
            new DetalhesMedicamentosDto(
                medicamentos.Id,
                medicamentos.Nome,
                medicamentos.Descricao,
                medicamentos.Quantidade,
                medicamentos.Fornecedor.Id,
                medicamentos.Fornecedor.Cnpj
        ));
    }

    private static Result ValidarEntidade(Medicamentos medicamentos)
    {
        List<string> erros = medicamentos.Validar();

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
