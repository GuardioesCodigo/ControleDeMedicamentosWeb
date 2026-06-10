using System;
using ControleDeMedicamentos.WebApp.ModuloPacientes.Views;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloPacientes.Controllers;

public async Task<IActionResult> Cadastrar(PacienteViewModel model)
{
    if (!ModelState.IsValid) return View(model);

    // Verifica se já existe um paciente com esse cartão do SUS no banco
    var existeSus = await _context.Pacientes.AnyAsync(p => p.CartaoSus == model.CartaoSus);

    if (existeSus)
    {
        ModelState.AddModelError("CartaoSus", "Já existe um paciente cadastrado com este cartão do SUS.");
        return View(model);
    }

    // Prossiga com o salvamento...
}
