using System;
using ControleDeMedicamentos.WebApp.Compartilhado.Dominio;
using ControleDeMedicamentos.WebApp.ModuloFornecedores.Dominio;
using FluentResults;

namespace ControleDeMedicamentos.WebApp.ModuloFornecedores.Aplicacao;

public class ServicoFornecedores
{
    private readonly IRepositorio<Fornecedores> repositorioFornecedores;

    public ServicoFornecedores(IRepositorio<Fornecedores> repositorioFornecedores)
    {
        this.repositorioFornecedores = repositorioFornecedores;
    }

    public Result Cadastrar(CadastrarFornecedoresDto dtos)
    {
        if (ExisteFornecedorComCnjp(dtos.CNPJ))
            return Falha("Cnpj", "Já existe um fornecedor com este CNPJ.");

        Fornecedores novoFornecedor = new Fornecedores(
            dtos.Nome,
            dtos.Telefone,
            dtos.CNPJ
        );

        repositorioFornecedores.Cadastrar(novoFornecedor);

        return Result.Ok();
    }

    private bool ExisteFornecedorComCnjp(string cnpj, Guid? idIgnorado = null)
    {
        List<Fornecedores> fornecedores = repositorioFornecedores.SelecionarTodos();

        foreach(Fornecedores f in fornecedores)
        {
            if (f.Id != idIgnorado && string.Equals(f.Cnpj, cnpj, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }

    public List<ListarFornecedoresDto> SelecionarTodos()
    {
        List<Fornecedores> fornecedores = repositorioFornecedores.SelecionarTodos();

        return fornecedores
            .Select(f => new ListarFornecedoresDto(f.Id, f.Nome, f.Telefone, f.Cnpj))
            .ToList();
    }

    public Result<DetalhesFornecedoresDto> SelecionarPorId(Guid id)
    {
        Fornecedores? fornecedores = repositorioFornecedores.SelecionarPorId(id);

        if (fornecedores == null)
            return Result.Fail("Fornecedor não encontrado");

        return Result.Ok(
            new DetalhesFornecedoresDto(fornecedores.Id, fornecedores.Nome,  fornecedores.Telefone, fornecedores.Cnpj)
        );
    }

    private static Result Falha(string campo, string mensagem)
    {
        IError erro = new Error(mensagem).WithMetadata("Campo", campo);

        return Result.Fail(erro);
    }
}
