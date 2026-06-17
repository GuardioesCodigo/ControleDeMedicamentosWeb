using System;
using ControleDeMedicamentos.WebApp.ModuloEstoque.Dominio;
using ControleDeMedicamentos.WebApp.ModuloFornecedores.Dominio;
using ControleDeMedicamentos.WebApp.ModuloMedicamentos.Dominio;
using ControleDeMedicamentos.WebApp.ModuloRequisicaoSaida.Dominio;
using ControleDeMedicamentosWeb.WebApp.Compartilhado.Infra.Sql;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ControleDeMedicamentos.WebApp.ModuloMedicamentos.Infra;

public sealed class RepositorioMedicamentoSql(
    ISqlConnectionFactory connectionFactory,
    IRepositorioRequisicaoEntrada repositorioRequisicaoEntrada,
    IRepositorioRequisicaoSaida repositorioRequisicaoSaida
)
    : IRepositorioMedicamentos
{

    private const string InserirMedicamentoSql = """
        INSERT INTO dbo.TBMedicamento (Id, Nome, Descricao, FornecedorId)
        VALUES (@Id, @Nome, @Descricao, @FornecedorId);
    """;

    private const string AtualizarMedicamentoSql = """
        UPDATE dbo.TBMedicamento
        SET Nome = @Nome,
            Descricao = @Descricao,
            FornecedorId = @FornecedorId
        WHERE Id = @Id;
    """;

    private const string ExcluirMedicamentoSql = """
        DELETE FROM dbo.TBMedicamento
        WHERE Id = @Id;
    """;

    private const string SelecionarTodosMedicamentosSql = """
        SELECT
            m.Id AS MedicamentoId,
            m.Nome AS MedicamentoNome,
            m.Descricao AS MedicamentoDescricao,
            f.Id AS FornecedorId,
            f.Nome AS FornecedorNome,
            f.Telefone AS FornecedorTelefone,
            f.Cnpj AS FornecedorCnpj
        FROM dbo.TBMedicamento AS m
        INNER JOIN dbo.TBFornecedor AS f
            ON f.Id = m.FornecedorId
        ORDER BY m.Nome;
    """;

    private const string SelecionarMedicamentoPorIdSql = """
        SELECT
            m.Id AS MedicamentoId,
            m.Nome AS MedicamentoNome,
            m.Descricao AS MedicamentoDescricao,
            f.Id AS FornecedorId,
            f.Nome AS FornecedorNome,
            f.Telefone AS FornecedorTelefone,
            f.Cnpj AS FornecedorCnpj
        FROM dbo.TBMedicamento AS m
        INNER JOIN dbo.TBFornecedor AS f
            ON f.Id = m.FornecedorId
        WHERE m.Id = @Id;
    """;

    public void Cadastrar(Medicamentos entidade)
    {
        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        conexao.Execute(
            InserirMedicamentoSql,
            new
            {
                entidade.Id,
                entidade.Nome,
                entidade.Descricao,
                FornecedorId = entidade.Fornecedor.Id
            }
        );
    }

    public bool Editar(Guid idSelecionado, Medicamentos entidadeAtualizada)
    {
        entidadeAtualizada.Id = idSelecionado;

        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        return conexao.Execute(
            AtualizarMedicamentoSql,
            new
            {
                entidadeAtualizada.Id,
                entidadeAtualizada.Nome,
                entidadeAtualizada.Descricao,
                FornecedorId = entidadeAtualizada.Fornecedor.Id
            }
        ) > 0;
    }

    public bool Excluir(Guid idSelecionado)
    {
        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        return conexao.Execute(ExcluirMedicamentoSql, new { Id = idSelecionado }) > 0;
    }

    public Medicamentos? SelecionarPorId(Guid idSelecionado)
    {
        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        MedicamentoRow? medicamento = conexao.QuerySingleOrDefault<MedicamentoRow>(
            SelecionarMedicamentoPorIdSql,
            new { Id = idSelecionado }
        );

        if (medicamento == null)
            return null;

        Medicamentos medicamentoMapeado = MapearMedicamento(medicamento);

        CarregarRequisicoes([medicamentoMapeado]);

        return medicamentoMapeado;
    }

    public List<Medicamentos> SelecionarTodos()
    {
        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        List<Medicamentos> medicamentos = conexao
            .Query<MedicamentoRow>(SelecionarTodosMedicamentosSql)
            .Select(MapearMedicamento)
            .ToList();

        CarregarRequisicoes(medicamentos);

        return medicamentos;
    }

    public List<Medicamentos> Filtrar(Predicate<Medicamentos> filtro)
    {
        return SelecionarTodos().FindAll(filtro);
    }

    private static Medicamentos MapearMedicamento(MedicamentoRow registro)
    {
        return new Medicamentos
        {
            Id = registro.MedicamentoId,
            Nome = registro.MedicamentoNome,
            Descricao = registro.MedicamentoDescricao,
            Fornecedor = new Fornecedores
            {
                Id = registro.FornecedorId,
                Nome = registro.FornecedorNome,
                Telefone = registro.FornecedorTelefone,
                Cnpj = registro.FornecedorCnpj
            }
        };
    }

    private void CarregarRequisicoes(List<Medicamentos> medicamentos)
    {
        if (medicamentos.Count == 0)
            return;

        Dictionary<Guid, Medicamentos> medicamentosPorId = medicamentos
            .ToDictionary(x => x.Id);

        foreach (RequisicaoEntrada entrada in repositorioRequisicaoEntrada.SelecionarTodos())
        {
            if (medicamentosPorId.TryGetValue(entrada.Medicamento.Id, out Medicamentos? medicamento))
                entrada.Medicamento = medicamento;
        }

        foreach (RequisicaoSaida saida in repositorioRequisicaoSaida.SelecionarTodos())
        {
            foreach (ItemRequisicaoSaida item in saida.Itens)
            {
                if (medicamentosPorId.TryGetValue(item.MedicamentoId, out Medicamentos? medicamento))
                    item.Medicamento = medicamento;
            }
        }
    }
}

public sealed class MedicamentoRow
{
    public Guid MedicamentoId { get; set; }
    public string MedicamentoNome { get; set; } = string.Empty;
    public string MedicamentoDescricao { get; set; } = string.Empty;
    public Guid FornecedorId { get; set; }
    public string FornecedorNome { get; set; } = string.Empty;
    public string FornecedorTelefone { get; set; } = string.Empty;
    public string FornecedorCnpj { get; set; } = string.Empty;
}
