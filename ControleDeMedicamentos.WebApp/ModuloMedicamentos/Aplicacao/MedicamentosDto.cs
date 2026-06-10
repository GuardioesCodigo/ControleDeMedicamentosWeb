namespace ControleDeMedicamentos.WebApp.ModuloMedicamentos.Aplicacao;

public record ListarMedicamentosDto(
    Guid Id,
    string Nome,
    string Descricao,
    int Quantidade,
    Guid FornecedorId,
    string FornecedorCnpj
);

public record DetalhesMedicamentosDto(
    Guid Id,
    string Nome,
    string Descricao,
    int Quantidade,
    Guid FornecedorId,
    string FornecedorCnpj
);
