using Microsoft.AspNetCore.Mvc;
using ControleDeMedicamentos.WebApp.ModuloPacientes.Apresentacao;
using ControleDeMedicamentos.WebApp.ModuloPacientes.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloPacientes.Dominio;
using AutoMapper;

namespace ControleDeMedicamentos.WebApp.ModuloPacientes.Apresentacao;

// 1. Você DEVE definir uma classe e herdar de 'Controller'
[Route("paciente")]
public class PacienteController : Controller
{
    private readonly ServicoPaciente _servico;
    private readonly IMapper _mapper;

    // 2. O Controller precisa do serviço injetado no construtor
    public PacienteController(ServicoPaciente servico, IMapper mapper)  
    {
        _servico = servico;
        _mapper = mapper;
    }

    // GET: /paciente/novo
    [HttpGet("novo")]
    public IActionResult Cadastrar() => View(new PacienteViewModel());

    // POST: /paciente/novo
    [HttpPost("novo")]
    public IActionResult Cadastrar(PacienteViewModel model)
    {
        // --- ADICIONE ESTA LIMPEZA AQUI ---
        if (!string.IsNullOrEmpty(model.Telefone))
            model.Telefone = new string(model.Telefone.Where(char.IsDigit).ToArray());

        if (!string.IsNullOrEmpty(model.CPF))
            model.CPF = new string(model.CPF.Where(char.IsDigit).ToArray());
        // ----------------------------------

        if (!ModelState.IsValid) return View(model);

        try
        {
            var paciente = _mapper.Map<Paciente>(model);
            _servico.Cadastrar(paciente);
            return RedirectToAction("Listar");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(model);
        }
    }

   [Route("listar")]
    public IActionResult Listar()
    {
        var pacientes = _servico.SelecionarTodos();
    
        // O mapper converte a lista de entidades para uma lista de ViewModels
        var model = _mapper.Map<List<ListarPacienteViewModel>>(pacientes);
        
        return View(model);
    }

    [HttpGet("editar/{id:guid}")]
    public IActionResult Editar(Guid id)
    {
        var paciente = _servico.SelecionarPorId(id);
        if (paciente == null) return NotFound();

        // ALTERAÇÃO: Mapeie para EditarPacienteViewModel, não PacienteViewModel
        var model = _mapper.Map<EditarPacienteViewModel>(paciente); 
        
        return View(model); 
    }

   [HttpPost("editar/{id:guid}")]
public IActionResult Editar(Guid id, EditarPacienteViewModel model)
{
    if (!ModelState.IsValid) return View(model);

    try
    {
        model.Id = id; 
        
        // ENVIO DO MODELO DIRETO: O serviço se encarrega de mapear para Paciente
        _servico.Editar(model); 

        return RedirectToAction("Listar");
    }
    catch (Exception ex)
    {
        ModelState.AddModelError("", ex.Message);
        return View(model);
    }
}

   [HttpGet("excluir/{id:guid}")]
    public IActionResult Excluir(Guid id)
    {
        var paciente = _servico.SelecionarPorId(id);
        if (paciente == null) return NotFound();

        // Mapeia para o ViewModel de Exclusão (que você já tem)
        var model = _mapper.Map<ExcluirPacienteViewModel>(paciente);
        
        return View(model); // Aqui ele PARA e mostra a View para você!
    }

    [HttpPost("excluir/{id:guid}")]
    public IActionResult Excluir(Guid id, ExcluirPacienteViewModel model)
    {
        _servico.Excluir(id);
        return RedirectToAction("Listar");
    }
}