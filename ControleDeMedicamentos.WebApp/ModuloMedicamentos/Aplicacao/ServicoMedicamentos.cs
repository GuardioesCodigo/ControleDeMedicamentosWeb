using ControleDeMedicamentos.WebApp.ModuloFornecedores.Dominio;
using ControleDeMedicamentos.WebApp.ModuloMedicamentos.Dominio;
using FluentResults;

namespace ControleDeMedicamentos.WebApp.ModuloMedicamentos.Aplicacao;

public class ServicoMedicamentos
{
    private readonly IRepositorioMedicamentos repositorioMedicamentos;
    private readonly IRepositorioFornecedores repositorioFornecedores;

    public ServicoMedicamentos(IRepositorioMedicamentos repositorioMedicamentos, IRepositorioFornecedores repositorioFornecedores)
    {
        this.repositorioMedicamentos = repositorioMedicamentos;
        this.repositorioFornecedores = repositorioFornecedores;
    }

    public Result Cadastrar(CadastrarMedicamentosDto dto)
    {
        Fornecedores? fornecedoresSelecionado = repositorioFornecedores.SelecionarPorId(dto.FornecedorId);

        if (fornecedoresSelecionado == null)
            return Falha(nameof(dto.FornecedorId), "Selecione um fornecedor válido.");

        if (ExisteMedicamentoComMesmoFornecedor(dto.Nome, dto.FornecedorId))
            return Falha(nameof(dto.Nome), "Já existe um medicamento com este nome deste Fornecedor.");

        Medicamentos novoMedicamentos = new Medicamentos(
            dto.Nome,
            dto.Descricao,
            dto.Quantidade,
            fornecedoresSelecionado
        );

        Result resultadoValidacao = ValidarEntidade(novoMedicamentos);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioMedicamentos.Cadastrar(novoMedicamentos);

        return Result.Ok();
    }

    public Result Editar(EditarMedicamentosDto dto)
    {
        Medicamentos? medicamentos = repositorioMedicamentos.SelecionarPorId(dto.Id);

        if (medicamentos == null)
            return Result.Fail("Medicamento não encontrado");

        Fornecedores? fornecedoresSelecionado = repositorioFornecedores.SelecionarPorId(dto.FornecedorId);

        if (fornecedoresSelecionado == null)
            return Falha(nameof(dto.FornecedorId), "Selecione um fornecedor válido.");

        if (ExisteMedicamentoComMesmoFornecedor(dto.Nome, dto.FornecedorId))
            return Falha(nameof(dto.Nome), "Já existe um medicamento com este nome deste Fornecedor.");

        Medicamentos medicamentosAtualizado = new Medicamentos(
            dto.Nome,
            dto.Descricao,
            dto.Quantidade,
            fornecedoresSelecionado
        );

        Result resultadoValidacao = ValidarEntidade(medicamentosAtualizado);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioMedicamentos.Editar(dto.Id, medicamentosAtualizado);

        return Result.Ok();
    }

    public Result Excluir(Guid Id)
    {
        Medicamentos? medicamentos = repositorioMedicamentos.SelecionarPorId(Id);

        if (medicamentos == null)
            return Result.Fail("Medicamento não encontrado");  

        repositorioMedicamentos.Excluir(Id);

        return Result.Ok();    
    }

    public List<ListarMedicamentosDto> SelecionarTodos()
    {
        List<Medicamentos> medicamentos = repositorioMedicamentos.SelecionarTodos();

        return medicamentos
            .Select(m => new ListarMedicamentosDto(m.Id, m.Nome, m.Descricao, m.Quantidade, m.Fornecedor.Id, m.Fornecedor.Nome))
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
                medicamentos.Fornecedor.Nome
        ));
    }

    public List<OpcaoFornecedoresDto> SelecionarFornecedores()
    {
        return repositorioFornecedores
            .SelecionarTodos()
            .Select(f => new OpcaoFornecedoresDto(f.Id, f.Nome, f.Cnpj))
            .ToList();
    }

    private bool ExisteMedicamentoComMesmoFornecedor(string nome, Guid fornecedorId, Guid? idIgnorado = null)
    {
        return repositorioMedicamentos
            .SelecionarTodos()
            .Any(f =>
                f.Id != idIgnorado &&
                f.Fornecedor.Id == fornecedorId &&
                string.Equals(f.Nome, nome, StringComparison.OrdinalIgnoreCase)
            );
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
