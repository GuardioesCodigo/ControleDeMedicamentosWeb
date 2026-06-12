using Microsoft.AspNetCore.Mvc;
using ControleDeMedicamentos.WebApp.ModuloPacientes.Views;
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
        if (!ModelState.IsValid) return View(model);

        try
        {
            // Converte a ViewModel para o domínio (Paciente)
            var paciente = _mapper.Map<Paciente>(model);

            // Chama o serviço para persistir
            _servico.Cadastrar(paciente);

            //Redireciona após Salvar
            return RedirectToAction("Listar");
        }
        catch (Exception ex)
        {
            // Se der erro (ex: CPF duplicado), mostra o erro na tela
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
        if (paciente == null) return NotFound(); // Proteção caso o ID não exista

        // Converte a entidade Paciente para o PacienteViewModel
        var model = _mapper.Map<PacienteViewModel>(paciente);
        return View(model);
}

    [HttpPost("editar/{id:guid}")]
    public IActionResult Editar(Guid id, PacienteViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        try
        {
            // 1. Garante que o ID do model é o mesmo da URL
            model.Id = id; 

            // 2. Mapeia e salva
            var paciente = _mapper.Map<Paciente>(model);
            _servico.Editar(paciente);

            // 3. Importante: Limpa o ModelState para evitar que dados antigos "sujem" a próxima visualização
            ModelState.Clear();

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