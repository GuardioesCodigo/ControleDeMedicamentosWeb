namespace ControleDeMedicamentos.WebApp.ModuloMedicamentos.Aplicacao;

public record ListarMedicamentosDto(
    Guid Id,
    string Nome,
    string Descricao,
    int Quantidade,
    Guid FornecedorId,
    string FornecedorNome
);

public record CadastrarMedicamentosDto(
    string Nome,
    string Descricao,
    int Quantidade,
    Guid FornecedorId
);

public record OpcaoFornecedoresDto(
    Guid Id,
    string Nome,
    string Cnpj
);

public record DetalhesMedicamentosDto(
    Guid Id,
    string Nome,
    string Descricao,
    int Quantidade,
    Guid FornecedorId
);
