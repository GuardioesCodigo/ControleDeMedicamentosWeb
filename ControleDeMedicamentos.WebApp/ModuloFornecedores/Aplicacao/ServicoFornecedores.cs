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
            return Falha("Cnpj", "Já existe um Fornecedor com este CNPJ.");

        Fornecedores novoFornecedor = new Fornecedores(
            dtos.Nome,
            dtos.Telefone,
            dtos.CNPJ
        );

        repositorioFornecedores.Cadastrar(novoFornecedor);

        return Result.Ok();
    }

    public Result Editar(EditarFornecedoresDto dto)
    {
        if (ExisteFornecedorComCnjp(dto.CNPJ, dto.Id))
            return Falha(nameof(dto.CNPJ), "Já existe um Fornecedor com este CNPJ");

        Fornecedores fornecedorAtualizado = new Fornecedores(dto.Nome, dto.Telefone, dto.CNPJ);

        Result resultadoValidacao = ValidarEntidade(fornecedorAtualizado);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        bool conseguiuEditar = repositorioFornecedores.Editar(dto.Id, fornecedorAtualizado);

        if (!conseguiuEditar)
            return Result.Fail("Fornecedor não encontrado.");

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        Fornecedores? fornecedores = repositorioFornecedores.SelecionarPorId(id);

        if (fornecedores == null)
            return Result.Fail("Fornecedor não encontrado.");

        repositorioFornecedores.Excluir(id);

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

    private static Result ValidarEntidade(Fornecedores fornecedores)
    {
        List<string> erros = fornecedores.Validar();

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
