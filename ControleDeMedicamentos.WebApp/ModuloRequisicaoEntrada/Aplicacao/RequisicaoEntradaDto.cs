using ControleDeMedicamentos.WebApp.ModuloFornecedores.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloRequisicaoEntrada.Aplicacao;

public record OpcaoMedicamentosDto(
    Guid Id,
    string Nome,
    Fornecedores Fornecedor
);

public record OpcaoFuncionariosDto(
    Guid Id,
    string Nome,
    string Cpf
);

public record ListarRequisicaoEntradaDto(
    Guid Id,
    DateTime Data,
    Guid MedicamentoId,
    string MedicamentoNome,
    Guid FuncionarioId,
    string FuncionarioNome,
    int Quantidade
);

public record CadastrarRequisicaoEntradaDto(
    DateTime Data, 
    Guid MedicamentoId, 
    Guid FuncionarioId, 
    int Quantidade
);