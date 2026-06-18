using System;
using ControleDeMedicamentos.WebApp.Compartilhado.Infra.Sql;
using ControleDeMedicamentos.WebApp.ModuloFornecedores.Dominio;
using Microsoft.Data.SqlClient;
using Dapper;

namespace ControleDeMedicamentos.WebApp.ModuloFornecedores.Infra;

public sealed class RepositorioFornecedorEmSql(ISqlConnectionFactory ConnectionFactory) 
 : IRepositorioFornecedor
{
    private const string InserirSql = """ 
        INSERT INTO [dbo].[TBFornecedor] ([Id],[Nome],[Telefone],[Cnpj])
        VALUES (@Id, @Nome, @Telefone, @Cnpj)
    """;

    private const string SelecionarTodosSql = """
    SELECT [id],[Nome],[Telefone],[Cnpj] 
    FROM [dbo].[TBFornecedor]
    ORDER BY [Nome]
    """;

    private const string AtualizarSql = """
        UPDATE [dbo].[TBFornecedor]
        SET
            [Nome] = @Nome,
            [Telefone] = @Telefone,
            [Cnpj] = @Cnpj
        WHERE [Id] = @Id;
     """;
    private const string ExcluirSql = """
        DELETE FROM [dbo].[TBFornecedor]
        WHERE [Id] = @Id;
     """;

     private const string SelecionarPorIdSql = """
        SELECT [id],[Nome],[Telefone],[Cnpj] 
        FROM [dbo].[TBFornecedor]
        WHERE [Id] = @Id;
     """;

    public void Cadastrar(Fornecedor entidade)
    {
        using SqlConnection conexao = ConnectionFactory.CreateConnection();

        conexao.Open();

        conexao.Execute(InserirSql,entidade);
    }

    public bool Editar(Guid idSelecionado, Fornecedor entidadeAtualizada)
    {
        entidadeAtualizada.Id = idSelecionado;

        using SqlConnection conexao = ConnectionFactory.CreateConnection();

        conexao.Open();

        return conexao.Execute(AtualizarSql,  entidadeAtualizada) == 1;
    }

    public bool Excluir(Guid idSelecionado)
    {
        using SqlConnection conexao = ConnectionFactory.CreateConnection();

        conexao.Open();

        return conexao.Execute(ExcluirSql,  new {Id = idSelecionado}) == 1;
    }

    public Fornecedor? SelecionarPorId(Guid idSelecionado)
    {
        using SqlConnection conexao = ConnectionFactory.CreateConnection();
       conexao.Open();
       return conexao.QuerySingleOrDefault<Fornecedor>(SelecionarPorIdSql, new { id = idSelecionado} );
    }

    public List<Fornecedor> SelecionarTodos()
    {
       using SqlConnection conexao = ConnectionFactory.CreateConnection();

       conexao.Open();

        return conexao.Query<Fornecedor>(SelecionarTodosSql).ToList();
    }

        public List<Fornecedor> Filtrar(Predicate<Fornecedor> filtro)
    {
        return SelecionarTodos().FindAll(filtro);
    }
}
