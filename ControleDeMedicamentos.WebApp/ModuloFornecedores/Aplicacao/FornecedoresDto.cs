namespace ControleDeMedicamentos.WebApp.ModuloFornecedores.Aplicacao;

public record ListarFornecedoresDto(
    Guid Id,
    string Nome,
    string Telefone,
    string CNPJ
);

public record CadastrarFornecedoresDto(
    string Nome,
    string Telefone,
    string CNPJ    
);

public record DetalhesFornecedoresDto(
    Guid Id,
    string Nome,
    string Telefone,
    string CNPJ
);