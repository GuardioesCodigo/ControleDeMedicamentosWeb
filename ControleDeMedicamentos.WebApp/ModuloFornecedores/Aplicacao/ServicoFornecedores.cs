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
        Fornecedores novoFornecedor = new Fornecedores(
            dtos.Nome,
            dtos.Telefone,
            dtos.CNPJ
        );

        repositorioFornecedores.Cadastrar(novoFornecedor);

        return Result.Ok();
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

}
