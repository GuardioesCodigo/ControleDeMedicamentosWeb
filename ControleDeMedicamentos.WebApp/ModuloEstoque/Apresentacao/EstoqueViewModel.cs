namespace ControleDeMedicamentos.WebApp.ModuloEstoque.Apresentacao;

public record ListarMovimentacoesViewModel(
    DateTime Data,
    string Medicamento,
    string Tipo,
    int Quantidade
);