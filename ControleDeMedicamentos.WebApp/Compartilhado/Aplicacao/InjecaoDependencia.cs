using ControleDeMedicamentos.WebApp.ModuloFornecedores.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloMedicamentos.Aplicacao;

namespace ControleDeMedicamentos.WebApp.Compartilhado.Aplicacao;

public static class InjecaoDependencia
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ServicoFornecedores>();
        services.AddScoped<ServicoMedicamentos>();
    }
}
