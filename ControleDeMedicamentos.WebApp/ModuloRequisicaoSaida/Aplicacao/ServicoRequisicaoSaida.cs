using FluentResults;
using ControleDeMedicamentos.WebApp.ModuloMedicamentos.Dominio;
using ControleDeMedicamentos.WebApp.ModuloFuncionario.Dominio;
using ControleDeMedicamentos.WebApp.Compartilhado.Dominio;
using ControleDeMedicamentos.WebApp.ModuloRequisicaoSaida.Dominio;
using ControleDeMedicamentos.WebApp.ModuloRequisicaoSaidae.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloRequisicaoSaida.Infra;
using ControleDeMedicamentos.WebApp.ModuloRequisicaoEntrada.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloPacientes.Dominio;
using ControleDeMedicamentos.WebApp.Compartilhado.Infra.Arquivos;

namespace ControleDeMedicamentos.WebApp.ModuloRequisicaoSaida.Aplicacao;

public class ServicoRequisicaoSaida
{
    private readonly IRepositorioRequisicaoSaida _repositorioRequisicao;
    private readonly IRepositorioMedicamentos _repositorioMedicamento;
    private readonly IRepositorio<Paciente> _repositorioPaciente;
    private readonly ContextoJson _contexto;

    public ServicoRequisicaoSaida(
        IRepositorioRequisicaoSaida repositorioRequisicao,
        IRepositorioMedicamentos repositorioMedicamento,
        IRepositorio<Paciente> repositorioPaciente,
        ContextoJson contexto
    )
    {
        this._repositorioRequisicao = repositorioRequisicao;
       this._repositorioMedicamento = repositorioMedicamento;
       this._repositorioPaciente = repositorioPaciente;
       this._contexto = contexto;
    }

    // Método público que o seu Controller chama
    public Result Cadastrar(CadastrarRequisicaoSaidaDto dto)
{
    // 1. Busque o Paciente pelo ID para carregar o Nome dele na Entidade
    var paciente = _repositorioPaciente.SelecionarPorId(Guid.Parse(dto.PacienteId));
    
    if (paciente == null)
        return Result.Fail("Paciente não encontrado!");

    // 2. Crie a entidade com a referência ao objeto paciente
    var requisicao = new RequisicaoSaida
    {
        Data = dto.Data,
        Paciente = paciente, // Isso garante que o nome exista na Entidade
        PacienteId = paciente.Id,
        Itens = dto.Itens.Select(i => new ItemRequisicaoSaida 
        {
            MedicamentoId = i.MedicamentoId,
            Quantidade = i.Quantidade
        }).ToList()
    };

    return Cadastrar(requisicao);
}

    // Método privado que contém a lógica de negócio (não acessível pelo Controller)
   private Result Cadastrar(RequisicaoSaida requisicao)
{
    // 1. Validação de Domínio
    var erros = requisicao.Validar();
    if (erros.Count > 0)
        return Result.Fail(erros);

    // 2. PRIMEIRO: Apenas verifica se TUDO é possível (Snapshot)
    foreach (var item in requisicao.Itens)
    {
        var medicamento = _repositorioMedicamento.SelecionarPorId(item.MedicamentoId);
        
        if (medicamento == null)
            return Result.Fail($"Medicamento com ID {item.MedicamentoId} não encontrado.");

        if (medicamento.Quantidade < item.Quantidade)
            return Result.Fail($"Quantidade insuficiente para o medicamento: {medicamento.Nome}.");
    }

    // 3. SE CHEGOU AQUI, é porque todos os itens estão ok.
    // AGORA SIM: Debita o estoque e salva
    DebitarEstoque(requisicao);

    _repositorioRequisicao.Cadastrar(requisicao);
    _contexto.Salvar(); // Salva tudo de uma vez

    return Result.Ok();
}

private void DebitarEstoque(RequisicaoSaida requisicao)
{
    foreach (var item in requisicao.Itens)
    {
        var medicamento = _repositorioMedicamento.SelecionarPorId(item.MedicamentoId);
        
        medicamento.Quantidade -= item.Quantidade;
        
        _repositorioMedicamento.Editar(medicamento.Id, medicamento);
    }
}
    public List<ListarRequisicaoSaidaDto> SelecionarTodos()
{
    var requisicoes = _repositorioRequisicao.SelecionarTodos();

    return requisicoes.Select(r => {
        // Buscamos o paciente
        var paciente = _repositorioPaciente.SelecionarPorId(r.PacienteId);
        
        // Montamos a lista de nomes dos medicamentos
        var listaNomes = r.Itens.Select(i => {
            var med = _repositorioMedicamento.SelecionarPorId(i.MedicamentoId);
            // Se med for nulo, retornamos uma mensagem que ajude a identificar
            return med != null ? med.Nome : $"ID: {i.MedicamentoId.ToString().Substring(0, 4)}... não encontrado";
        });

        return new ListarRequisicaoSaidaDto(
            r.Id,
            r.Data,
            paciente?.Nome ?? "Sem nome",
            string.Join(", ", listaNomes),
            r.Itens.Sum(i => i.Quantidade)
        );
    }).ToList();
}
    public List<OpcaoMedicamentosDto> SelecionarMedicamentos()
    {
        return _repositorioMedicamento
            .SelecionarTodos()
            .Select(m => new OpcaoMedicamentosDto(m.Id, m.Nome, m.Fornecedor))
            .ToList();
    }
    public List<OpcaoPacientesDto> SelecionarPaciente()
    {
        return _repositorioPaciente
            .SelecionarTodos()
            .Select(p => new OpcaoPacientesDto(p.Id, p.Nome, p.CartaoSus))
            .ToList();
    }

    // Exemplo de como construir o resumo com nomes
    public string GerarResumo(List<ItemRequisicaoSaida> itens)
    {
        // Busca os nomes dos medicamentos no repositório e concatena
        var listaNomes = itens.Select(i => 
        {
            var med = _repositorioMedicamento.SelecionarPorId(i.MedicamentoId);
            return $"{i.Quantidade}x {med?.Nome ?? "Medicamento Desconhecido"}";
        });

        return string.Join(", ", listaNomes);
    }
}
