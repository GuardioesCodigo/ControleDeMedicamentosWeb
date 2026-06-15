namespace ControleDeMedicamentos.WebApp.ModuloEstoque.Apresentacao;

public record ListarMovimentacoesViewModel(
    DateTime Data,
    string Tipo,
    string Descricao,
    int Quantidade
);