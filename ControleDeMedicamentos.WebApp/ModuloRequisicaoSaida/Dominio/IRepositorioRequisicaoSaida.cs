using ControleDeMedicamentos.WebApp.ModuloRequisicaoSaida.Dominio; // Onde está a IRepositorioRequisicao
using ControleDeMedicamentos.WebApp.ModuloMedicamentos.Aplicacao;  // Onde costuma ficar a IRepositorioMedicamento
using ControleDeMedicamentos.WebApp.Compartilhado.Dominio;        // Onde costuma ficar a IContextoPersistencia
namespace ControleDeMedicamentos.WebApp.ModuloRequisicaoSaida.Dominio;

public interface IRepositorioRequisicaoSaida : IRepositorio<RequisicaoSaida>
{
   
}