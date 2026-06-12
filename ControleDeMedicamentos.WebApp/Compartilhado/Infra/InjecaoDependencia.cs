using ControleDeMedicamentos.WebApp.Compartilhado.Dominio;
using ControleDeMedicamentos.WebApp.Compartilhado.Infra.Arquivos;
using ControleDeMedicamentos.WebApp.ModuloEstoque.Dominio;
using ControleDeMedicamentos.WebApp.ModuloEstoque.Infra;
using ControleDeMedicamentos.WebApp.ModuloFornecedores.Dominio;
using ControleDeMedicamentos.WebApp.ModuloFornecedores.Infra;
using ControleDeMedicamentos.WebApp.ModuloFuncionario.Dominio;
using ControleDeMedicamentos.WebApp.ModuloFuncionario.Infra;
using ControleDeMedicamentos.WebApp.ModuloMedicamentos.Dominio;
using ControleDeMedicamentos.WebApp.ModuloMedicamentos.Infra;

namespace ControleDeMedicamentos.WebApp.Compartilhado.Infra;

public static class InjecaoDependencia
{
    public static void AddInfraRepositories(this IServiceCollection services)
    {
        services.AddScoped(provider =>
        {
            ContextoJson contextoJson = new ContextoJson();

            contextoJson.Carregar();

            return contextoJson;
        });

        services.AddScoped<IRepositorioFornecedores, RepositorioFornecedoresEmArquivo>();
        services.AddScoped<IRepositorioMedicamentos, RepositorioMedicamentosEmArquivo>();
        services.AddScoped<IRepositorioFuncionario, RepositorioFuncionarioEmArquivo>();
        services.AddScoped<IRepositorioRequisicaoEntrada, RepositorioRequisicaoEntradaEmArquivo>();
    }
}