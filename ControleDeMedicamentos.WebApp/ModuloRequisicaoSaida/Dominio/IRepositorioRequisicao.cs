// using ControleDeMedicamentos.WebApp.ModuloRequisicaoSaida.Dominio; // Onde está a IRepositorioRequisicao
// using ControleDeMedicamentos.WebApp.ModuloMedicamentos.Aplicacao;  // Onde costuma ficar a IRepositorioMedicamento
// using ControleDeMedicamentos.WebApp.Compartilhado.Dominio;        // Onde costuma ficar a IContextoPersistencia
// namespace ControleDeMedicamentos.WebApp.ModuloRequisicaoSaida.Dominio;

// public class RepositorioRequisicao : IRepositorioRequisicao
// {
//     private readonly MeuDbContext _contexto; // Seu contexto do banco

//     public RepositorioRequisicao(MeuDbContext contexto) => _contexto = contexto;

//     public void Cadastrar(Requisicao novaRequisicao)
//     {
//         _contexto.Requisicoes.Add(novaRequisicao);
//     }

//     public List<Requisicao> SelecionarTodos()
//     {
//         return _contexto.Requisicoes
//             .Include(r => r.Paciente)
//             .Include(r => r.Medicamentos).ThenInclude(i => i.Medicamento)
//             .ToList();
//     }

//     public Requisicao? SelecionarPorId(Guid id)
//     {
//         return _contexto.Requisicoes
//             .Include(r => r.Paciente)
//             .Include(r => r.Medicamentos).ThenInclude(i => i.Medicamento)
//             .FirstOrDefault(r => r.Id == id);
//     }
// }