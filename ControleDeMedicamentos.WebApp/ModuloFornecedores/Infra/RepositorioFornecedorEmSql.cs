using System;
using ControleDeMedicamentos.WebApp.ModuloFornecedores.Dominio;
using ControleDeMedicamentosWeb.WebApp.Compartilhado.Infra.Sql;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ControleDeMedicamentos.WebApp.ModuloFornecedores.Infra;

public sealed class RepositorioFornecedorSql(ISqlConnectionFactory connectionFactory)
    : IRepositorioFornecedores
{
    private const string InserirSql = """
        INSERT INTO dbo.TBFornecedor (Id, Nome, Telefone, Cnpj)
        VALUES (@Id, @Nome, @Telefone, @Cnpj);
    """;

    private const string AtualizarSql = """
        UPDATE dbo.TBFornecedor
        SET Nome = @Nome,
            Telefone = @Telefone,
            Cnpj = @Cnpj
        WHERE Id = @Id;
    """;

    private const string ExcluirSql = """
        DELETE FROM dbo.TBFornecedor
        WHERE Id = @Id;
    """;

    private const string SelecionarTodosSql = """
        SELECT Id, Nome, Telefone, Cnpj
        FROM dbo.TBFornecedor
        ORDER BY Nome;
    """;

    private const string SelecionarPorIdSql = """
        SELECT Id, Nome, Telefone, Cnpj
        FROM dbo.TBFornecedor
        WHERE Id = @Id;
    """;

    public void Cadastrar(Fornecedores entidade)
    {
        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        conexao.Execute(InserirSql, entidade);
    }

    public bool Editar(Guid idSelecionado, Fornecedores entidadeAtualizada)
    {
        entidadeAtualizada.Id = idSelecionado;

        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        return conexao.Execute(AtualizarSql, entidadeAtualizada) > 0;
    }

    public bool Excluir(Guid idSelecionado)
    {
        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        return conexao.Execute(ExcluirSql, new { Id = idSelecionado }) > 0;
    }

    public Fornecedores? SelecionarPorId(Guid idSelecionado)
    {
        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        return conexao.QuerySingleOrDefault<Fornecedores>(SelecionarPorIdSql, new { Id = idSelecionado });
    }

    public List<Fornecedores> SelecionarTodos()
    {
        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        return conexao.Query<Fornecedores>(SelecionarTodosSql).ToList();
    }

    public List<Fornecedores> Filtrar(Predicate<Fornecedores> filtro)
    {
        return SelecionarTodos().FindAll(filtro);
    }
}