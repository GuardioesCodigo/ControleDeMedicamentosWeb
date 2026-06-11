using ControleDeMedicamentos.WebApp.Compartilhado.Aplicacao;
using ControleDeMedicamentos.WebApp.Compartilhado.Apresentacao;
using ControleDeMedicamentos.WebApp.Compartilhado.Infra;
using ControleDeMedicamentos.WebApp.ModuloPacientes.Dominio;

var builder = WebApplication.CreateBuilder(args);

// 1. Configuração de Dependências (Injeção de Dependência)
builder.Services.AddInfraRepositories();
builder.Services.AddApplicationServices();
builder.Services.AddPresentationConfig();
builder.Services.AddAutoMapper(cfg => {
    cfg.AddProfile<PacienteProfile>();
});

var app = builder.Build();

// 2. Configuração de Middlewares (A ordem importa!)
app.UseStaticFiles();

// O UseRouting deve vir antes de qualquer mapeamento de rotas
app.UseRouting();

// 3. Mapeamento de Rotas
// Removemos o MapDefaultControllerRoute e mantemos apenas um padrão único
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Paciente}/{action=Index}/{id?}");

// 4. Execução do Servidor
app.Run();