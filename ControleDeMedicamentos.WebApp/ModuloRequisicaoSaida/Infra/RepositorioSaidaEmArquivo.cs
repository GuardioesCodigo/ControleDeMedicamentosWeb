// using ControleDeMedicamentos.WebApp.Compartilhado.Infra.Arquivos;
// using ControleDeMedicamentos.WebApp.ModuloEstoque.RequisicoesSaida.Dominio;

// namespace ControleDeMedicamentos.WebApp.ModuloEstoque.RequisicoesSaida.Infra;

// public class RepositorioRequisicaoSaidaEmArquivo
//     : RepositorioBaseEmArquivo<RequisicaoSaida>, IRepositorioRequisicaoSaida
// {
//     public RepositorioRequisicaoSaidaEmArquivo(ContextoJson contexto) : base(contexto)
//     {
//     }

//     protected override List<RequisicaoSaida> CarregarRegistros()
//     {
//         return contexto.RequisicoesSaida;
//     }
// }