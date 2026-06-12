using ControleDeMedicamentos.WebApp.ModuloPacientes.Dominio;

namespace ControleDeMedicamentos.WebApp.Compartilhado.Apresentacao;

public static class InjecaoDependencia
{
  public static void AddPresentationConfig(this IServiceCollection services)
{
    // 1. Configuração do Razor (Isolada)
    services.AddControllersWithViews().AddRazorOptions(options =>
    {
        options.ViewLocationFormats.Clear();
        options.ViewLocationFormats.Add("/Modulo{1}/Apresentacao/Views/{0}.cshtml");
        options.ViewLocationFormats.Add("/Compartilhado/Apresentacao/Views/{0}.cshtml");
        options.ViewLocationFormats.Add("/ModuloPacientes/Apresentacao/Views/{0}.cshtml");
    }); // O bloco do AddRazorOptions termina aqui!

    // 2. Registro do AutoMapper (Fora e após o bloco anterior)
    services.AddAutoMapper(config => 
    {
        config.AddMaps(typeof(Program).Assembly);
    });
}
}