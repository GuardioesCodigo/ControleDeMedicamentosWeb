using ControleDeMedicamentos.WebApp.ModuloFornecedores.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloEstoque.Apresentacao;

public record OpcaoMedicamentosViewModel(
    Guid Id,
    string Nome,
    Fornecedores Fornecedor
);

public record OpcaoFuncionariosViewModel(
    Guid Id,
    string Nome,
    string Cpf
);
public record ListarRequisicaoEntradaViewModel(
    Guid Id,
    DateTime Data,
    Guid MedicamentoId,
    string MedicamentoNome,
    Guid FuncionarioId,
    string FuncionarioNome,
    int Quantidade
);
