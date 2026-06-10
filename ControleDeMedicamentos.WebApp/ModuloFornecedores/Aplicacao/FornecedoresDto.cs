namespace ControleDeMedicamentos.WebApp.ModuloFornecedores.Aplicacao;

public record ListarFornecedoresDto(
    Guid Id,
    string Nome,
    string Telefone,
    string CNPJ
);