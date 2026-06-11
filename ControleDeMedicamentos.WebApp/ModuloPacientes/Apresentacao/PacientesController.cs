using Microsoft.AspNetCore.Mvc;
using ControleDeMedicamentos.WebApp.ModuloPacientes.Views;
using ControleDeMedicamentos.WebApp.ModuloPacientes.Aplicacao;

namespace ControleDeMedicamentos.WebApp.ModuloPacientes.Apresentacao;

// 1. Você DEVE definir uma classe e herdar de 'Controller'
public class PacienteController : Controller
{
    private readonly ServicoPaciente _servico;

    // 2. O Controller precisa do serviço injetado no construtor
    public PacienteController(ServicoPaciente servico)
    {
        _servico = servico;
    }

    [HttpPost]
    public IActionResult Cadastrar(PacienteViewModel model)
    {
        // Agora o 'ModelState' e o 'View' funcionam, pois você herdou de 'Controller'
        if (!ModelState.IsValid) return View(model);

        try
        {
            // 3. A regra de negócio deve ficar no Serviço, não no Controller!
            // Note que o Controller não deve acessar o _context diretamente.
            // O ideal é que o 'Cadastrar' do serviço já faça essa validação.
            
            // var dto = ... (mapeamento com AutoMapper)
            // _servico.Cadastrar(dto);
            
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(model);
        }
    }
}