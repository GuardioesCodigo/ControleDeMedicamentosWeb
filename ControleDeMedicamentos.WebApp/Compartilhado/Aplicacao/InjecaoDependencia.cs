using ControleDeMedicamentos.WebApp.ModuloFornecedores.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloMedicamentos.Aplicacao;
using ControleDeMedicamentos.WebApp.Compartilhado.Dominio;
using ControleDeMedicamentos.WebApp.ModuloPacientes.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloPacientes.Dominio;
using ControleDeMedicamentos.WebApp.ModuloPacientes.Infra;
using ControleDeMedicamentos.WebApp.ModuloRequisicaoEntrada.Aplicacao;
namespace ControleDeMedicamentos.WebApp.Compartilhado.Aplicacao;

public static class InjecaoDependencia
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ServicoFornecedores>();
        services.AddScoped<ServicoMedicamentos>();
        services.AddScoped<ServicoPaciente>();
        services.AddScoped<ServicoFuncionario>();
        services.AddScoped<IRepositorio<Paciente>, RepositorioPacienteEmArquivo>();
        services.AddScoped<ServicoRequisicaoEntrada>();

    }
}
