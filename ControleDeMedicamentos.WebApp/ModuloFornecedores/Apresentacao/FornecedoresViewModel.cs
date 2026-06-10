namespace ControleDeMedicamentos.WebApp.ModuloFornecedores.Apresentacao;

public record ListarFornecedoresViewModel(
    Guid Id,
    string Nome,
    string Telefone,
    string CNPJ
);