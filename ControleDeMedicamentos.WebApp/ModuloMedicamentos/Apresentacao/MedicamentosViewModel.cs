namespace ControleDeMedicamentos.WebApp.ModuloMedicamentos.Apresentacao;

public record ListarMedicamentosViewModel(
    Guid Id,
    string Nome,
    string Descricao,
    int Quantidade,
    Guid FornecedorId,
    string FornecedorNome
);