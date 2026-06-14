using ControleDeMedicamentos.WebApp.ModuloPacientes.Dominio;

namespace ControleDeMedicamentos.WebApp.Compartilhado.Apresentacao;

public static class InjecaoDependencia
{
   public static void AddPresentationConfig(this IServiceCollection services)
{
    services.AddControllersWithViews().AddRazorOptions(options =>
    {
        // Limpa os padrões para garantir que os nossos funcionem
        options.ViewLocationFormats.Clear();

        // Procura na estrutura de módulos
        options.ViewLocationFormats.Add("/Modulo{1}/Apresentacao/Views/{0}.cshtml");
        
        // Procura se a pasta não tiver o prefixo "Modulo"
        options.ViewLocationFormats.Add("/{1}/Apresentacao/Views/{0}.cshtml");

        // Procura na raiz (padrão)
        options.ViewLocationFormats.Add("/Views/{1}/{0}.cshtml");
        options.ViewLocationFormats.Add("/Views/Shared/{0}.cshtml");

        // Compartilhados
        options.ViewLocationFormats.Add("/Compartilhado/Apresentacao/Views/{0}.cshtml");
    });

    services.AddAutoMapper(config => 
    {
        config.AddMaps(typeof(Program).Assembly);
    });
}
}

