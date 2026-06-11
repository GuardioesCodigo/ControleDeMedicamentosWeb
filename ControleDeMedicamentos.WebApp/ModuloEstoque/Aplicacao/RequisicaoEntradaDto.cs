using ControleDeMedicamentos.WebApp.ModuloFornecedores.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloEstoque.Aplicacao;

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